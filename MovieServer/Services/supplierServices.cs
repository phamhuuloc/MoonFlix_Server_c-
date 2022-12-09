using MovieServer.Models;
using MySql.Data.MySqlClient;

namespace MovieServer.Services
{
    public class supplierServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public supplierServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        // CREATE A NEW supplier (SIGN UP)
        public int createNewSupplier(Supplier supplier)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "insert into suppliers (sl_name,sl_email,sl_phone,sl_address) values(@sl_name,@sl_email,@sl_phone,@sl_address)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("sl_name", supplier.sl_name);
                cmd.Parameters.AddWithValue("sl_email", supplier.sl_email);
                cmd.Parameters.AddWithValue("sl_phone", supplier.sl_phone);
                cmd.Parameters.AddWithValue("sl_address", supplier.sl_address);
  
                return (cmd.ExecuteNonQuery());
            }
        }
        public int updateSupplier(Supplier supplier, string id)
        {
        

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update suppliers set sl_name = @sl_name, sl_email = @sl_email, sl_phone = @sl_phone ,sl_address = @sl_address where id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("sl_name", supplier.sl_name);
                cmd.Parameters.AddWithValue("sl_email", supplier.sl_email);
                cmd.Parameters.AddWithValue("sl_phone", supplier.sl_phone);
                cmd.Parameters.AddWithValue("sl_address", supplier.sl_address);
                cmd.Parameters.AddWithValue("id", id);
                return (cmd.ExecuteNonQuery());
            }
        }
        // DELETE USER 
        public int deleteSupplier(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from suppliers where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                return (cmd.ExecuteNonQuery());
            }

        }
        // GET LIST ALL USERS
        public List<Supplier> getAllSuppliers()
        {
            List<Supplier> list = new List<Supplier>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from suppliers";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Supplier()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            sl_name = reader["sl_name"].ToString(),
                            sl_email = reader["sl_email"].ToString(),
                            sl_phone = reader["sl_phone"].ToString(),
                            sl_address = reader["sl_address"].ToString(),

                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return (list);
        }
        // GET USER BY ID 
        public Supplier GetSupplierById(int id)
        {
            Supplier supplier = new Supplier();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from suppliers where id =@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    supplier.id = Convert.ToInt32(reader["id"]);
                    supplier.sl_name = reader["sl_name"].ToString();
                    supplier.sl_email = reader["sl_email"].ToString();
                    supplier.sl_phone = reader["sl_phone"].ToString();
                    supplier.sl_address = reader["sl_address"].ToString();
                   
                }

            }
            return (supplier);
        }
        // GET MOVIE OF USER
        public List<Movie> getMovieOfUser(int id)
        {
            List<Movie> list = new List<Movie>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT *  FROM  movie   INNER JOIN user_movies um ON um.id = movie.id where um.um_user_id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
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
