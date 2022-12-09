using MovieServer.Models;
using MySql.Data.MySqlClient;

namespace MovieServer.Services
{
    public class listServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public  listServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        // CREATE A NEW USER (SIGN UP)
        public int createNewList(List listMovie)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into lists (title , type , genre) values (@title , @type , @genre)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", listMovie.id);
                cmd.Parameters.AddWithValue("title", listMovie.title);
                cmd.Parameters.AddWithValue("type", listMovie.type);
                cmd.Parameters.AddWithValue("genre", listMovie.genre);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int updateMovie(List listMovie, int  id)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update lists set title = @title , type = @type , genre = @genre  where id = @id ";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", listMovie.id);
                cmd.Parameters.AddWithValue("title", listMovie.title);
                cmd.Parameters.AddWithValue("type", listMovie.type);
                cmd.Parameters.AddWithValue("genre", listMovie.genre);
                return (cmd.ExecuteNonQuery());
            }
        }
        // DELETE USER 
        public int deleteListMovie(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from lists where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                return (cmd.ExecuteNonQuery());
            }

        }
        // GET LIST ALL Movie
        public List<List> getAllList()
        {
            List<List> list = new List<List>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from lists";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new List()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            title = reader["title"].ToString(),
                            type = reader["type"].ToString(),
                            genre = reader["genre"].ToString(),

                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // GET Movie BY ID 
        public List getList(int id)
        {
            List listMovie = new List();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from lists where id =@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    listMovie.id = Convert.ToInt32(reader["id"]);
                    listMovie.title = reader["title"].ToString();
                    listMovie.type = reader["type"].ToString();
                    listMovie.genre = reader["genre"].ToString();
                }

            }
            return (listMovie);
        }
    }
}
