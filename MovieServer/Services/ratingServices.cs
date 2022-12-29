using MovieServer.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace MovieServer.Services
{
    public class ratingServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public ratingServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        public int createNewRating(Rating rating)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "insert into ratings (r_number_star,r_user_id ,r_movie_id ,r_content) values (@r_number_star,@r_user_id,@r_movie_id,@r_content) ";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("r_number_star", rating.r_number_star);
                cmd.Parameters.AddWithValue("r_user_id", rating.r_user_id);
                cmd.Parameters.AddWithValue("r_movie_id", rating.r_movie_id);
                cmd.Parameters.AddWithValue("r_content", rating.r_content);
                return (cmd.ExecuteNonQuery());
            }
        }
        public List<object> getAllRatingOfMovie(int id)
        {
            List<object> list = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select user.profilePic , user.username , ratings.r_content , ratings.create_at from  user  inner join ratings on user.id = ratings.r_user_id where ratings.r_movie_id = @id";
               
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new { username = reader["username"].ToString(), profilePic = reader["profilePic"].ToString(), r_content = reader["r_content"].ToString(), create_at = reader["create_at"].ToString()};
                        list.Add(ob);
                    }

                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        public int deleteRating (int rating_id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from ratings where id=@id  ";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", rating_id);
                return (cmd.ExecuteNonQuery());
            }

        }
    }
}
