using MovieServer.Models;
using MySql.Data.MySqlClient;

namespace MovieServer.Services
{
    public class categoriesServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public categoriesServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        // CREATE A NEW supplier (SIGN UP)
        public int createNewCategorie(Categorie categorie)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "insert into categories (cate_name,cate_type) values(@cate_name,@cate_type)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("cate_name", categorie.cate_name);
                cmd.Parameters.AddWithValue("cate_type", categorie.cate_type);
        

                return (cmd.ExecuteNonQuery());
            }
        }
        public int updateCategories(Categorie  categorie, int id)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update categories set cate_name = @cate_name, cate_type = @cate_type  where id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("cate_name", categorie.cate_name);
                cmd.Parameters.AddWithValue("cate_type", categorie.cate_type);
                cmd.Parameters.AddWithValue("id", id);
                return (cmd.ExecuteNonQuery());
            }
        }
        // DELETE USER 
        public int deleteCategories(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from categories where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                return (cmd.ExecuteNonQuery());
            }

        }
        // GET LIST ALL USERS
        public List<Categorie> getAllCategories()
        {
            List<Categorie> list = new List<Categorie>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from categories";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Categorie()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            cate_name = reader["cate_name"].ToString(),
                            cate_type = Convert.ToInt32(reader["cate_type"])

                        });          
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return (list);
        }
        public List<Movie> getListMovieOfCategorie(string cate_name)
        {
            List<Movie> list = new List<Movie>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * FROM movie INNER JOIN movie_categoties mc  on mc.mv_movie_id = movie.id INNER JOIN categories cate on cate.id = mc.mv_cate_id WHERE cate.cate_name =@cate_name";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("cate_name", cate_name);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Movie()
                        {
                            id = Convert.ToInt32(reader["id"].ToString()),
                            supplier_id = Convert.ToInt32(reader["supplier_id"]),
                            title = reader["title"].ToString(),
                            _desc = reader["_desc"].ToString(),
                            img = reader["img"].ToString(),
                            imgSm = reader["imgSm"].ToString(),
                            trailer = reader["trailer"].ToString(),
                            video = reader["video"].ToString(),
                            year = Convert.ToInt32(reader["year"]),
                            _limit = Convert.ToInt32(reader["_limit"]),
                            price = Convert.ToDouble(reader["price"]),
                            clicked = Convert.ToInt32(reader["clicked"]),
                            isSeries = Convert.ToBoolean(reader["isSeries"])
                        });
                    }
                    reader.Close();

                }
            }
            return (list);

        }


    }
}
