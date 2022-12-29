using MovieServer.Models;
using MySql.Data.MySqlClient;
using System.Net.WebSockets;

namespace MovieServer.Services
{
    public class userMovieServieces
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public userMovieServieces(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        // check wallet balance 
        private  double checkWalletBalance(int user_id , int movie_id)
        {
            // get wallet balance of user 
            userServices userServices = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var user = userServices.getUser(user_id);
            double wallet_balance = user.wallet_balance;
            //get price of movie 
            movieServices movieservices = new movieServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var movie = movieservices.getMovie(movie_id);
            double price = movie.price;
            return wallet_balance - price;
        }

        // check if the user already owns the movie
        public object checkUserHaveMovie(int user_id, int movie_id)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "SELECT COUNT(user.id) as 'quantity' from movie INNER JOIN user_movies um on um.um_movie_id = movie.id INNER JOIN user on user.id = um.um_user_id where movie.id =@movie_id and user.id = @user_id;";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.Parameters.AddWithValue("movie_id", movie_id);
                var reader  = cmd.ExecuteScalar();
                
                var  ob = new { quantity = Convert.ToInt32(reader)};

                return ob;
       
            }
        }

        public object  byMovie(UserMovie user_movie)
        {
            movieServices movieservices = new movieServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var movie = movieservices.getMovie(user_movie.um_movie_id);
            double price = movie.price;

            double checkWallet = checkWalletBalance(user_movie.um_user_id, user_movie.um_movie_id);
            object checkMovie = checkUserHaveMovie(user_movie.um_user_id , user_movie.um_movie_id);
         
            int quantity = Convert.ToInt32( checkMovie.GetType().GetProperty("quantity").GetValue(checkMovie, null));


            if (quantity > 0)
            {
                return new
                {
                    Success = false,
                    Message = "The Movie already exit on your movie list!"
                };
            }
           else if (checkWallet < 0 )
            {
                return new
                {
                    Success = false,
                    Message = "Insufficient account balance"
                };
            }

            else {
              
                using (MySqlConnection conn = GetConnection())
                {

                    conn.Open();
                    var str = "insert into user_movies (um_user_id, um_movie_id) values(@um_user_id,@um_movie_id)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("um_user_id", user_movie.um_user_id);
                    cmd.Parameters.AddWithValue("um_movie_id", user_movie.um_movie_id);
                    int flag = cmd.ExecuteNonQuery();
                    conn.Close();

                    if(flag > 0)
                    {
                        conn.Open();
                        var str2 = "update user set wallet_balance = @wallet_balance, point= point + 4 , money_spended = money_spended +@price where id = @id";
                        MySqlCommand cmd2 = new MySqlCommand(str2, conn);
                        cmd2.Parameters.AddWithValue("wallet_balance",checkWallet);
                        cmd2.Parameters.AddWithValue("id", user_movie.um_user_id);
                        cmd2.Parameters.AddWithValue("price", price);
                        cmd2.ExecuteNonQuery();
                        return new
                        {
                            Success = true,
                            Message = "Buy Movie Successfully!"
                        };
                    }
                }
            }
            return new
            {
                Success = true,
                Message = "Buy Movie failer!"
            };


        }
    }
}
