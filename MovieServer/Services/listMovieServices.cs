using MovieServer.Models;
using MySql.Data.MySqlClient;

namespace MovieServer.Services
{
    public class listMovieServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public listMovieServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        // CREATE A NEW USER (SIGN UP)
        public int createNewListMovie(ListMovie  listMovie)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "insert into list_movies (lm_list_id, lm_movie_id) values (@lm_list_id, @lm_movie_id)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("lm_list_id", listMovie.lm_list_id);
                cmd.Parameters.AddWithValue("lm_movie_id", listMovie.lm_movie_id);
                return (cmd.ExecuteNonQuery());
            }
        }
        // DELETE lists 
        public int deleteListMovie(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from list_movies where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                return (cmd.ExecuteNonQuery());
            }

        }
        // DELETE movie of list 
        public int deleteMovieOfList(ListMovie  listMovie)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from list_movies where lm_movie_id=@movie_id and lm_list_id =@list_id ";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("movie_id", listMovie.lm_movie_id); ;

                cmd.Parameters.AddWithValue("list_id", listMovie.lm_list_id);

                return (cmd.ExecuteNonQuery());     
            }

        }
        // GET ALL MOVIE OF LIST
        public List<Movie> allMovieOfList(int id )
        {
            List<Movie> list = new List<Movie>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from movie inner join list_movies  lm on lm.lm_movie_id = movie.id where lm.lm_list_id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Movie()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            supplier_id = Convert.ToInt32(reader["supplier_id"]),
                            title = reader["title"].ToString(),
                            _desc = reader["_desc"].ToString(),
                            img = reader["img"].ToString(),
                            imgSm = reader["imgSm"].ToString(),
                            trailer = reader["trailer"].ToString(),
                            video = reader["video"].ToString(),
                            year = Convert.ToInt32(reader["year"]),
                            _limit = Convert.ToDouble(reader["_limit"]),
                            price = Convert.ToDouble(reader["price"]),
                            clicked = Convert.ToInt32(reader["clicked"]),
                            isSeries = Convert.ToBoolean(reader["isSeries"]),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
    }
}
