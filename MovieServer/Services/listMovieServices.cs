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
        // DELETE USER 
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
    }
}
