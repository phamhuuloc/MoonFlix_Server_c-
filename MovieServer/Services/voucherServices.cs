using MovieServer.Models;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace MovieServer.Services
{
    public class voucherServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public voucherServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        public int createNewVoucher(Voucher voucher)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "insert into vouchers (image ,percent_discount , description,supplier_name ,point_cost) values(@image,@percent_discount,@description,@supplier_name,@point_cost)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("image", voucher.image);
                cmd.Parameters.AddWithValue("percent_discount", voucher.percent_discount);
                cmd.Parameters.AddWithValue("description", voucher.description);
                cmd.Parameters.AddWithValue("supplier_name", voucher.supplier_name);
                cmd.Parameters.AddWithValue("point_cost", voucher.point_cost);
                return (cmd.ExecuteNonQuery());
            }
        }

        public int updateVoucher(Voucher voucher, string id)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update vouchers set image = @image , percent_discount = @percent_discount , description = @description ,supplier_name =@supplier_name, point_cost = @point_cost where id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", voucher.id);
                cmd.Parameters.AddWithValue("image",voucher.image);
                cmd.Parameters.AddWithValue("percent_discount", voucher.percent_discount);
                cmd.Parameters.AddWithValue("description", voucher.description);
                cmd.Parameters.AddWithValue("supplier_name", voucher.supplier_name);
                cmd.Parameters.AddWithValue("point_cost", voucher.point_cost);

                return (cmd.ExecuteNonQuery());
            }
        }
        // DELETE USER 
        public int deleteVoucher(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from vouchers where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                return (cmd.ExecuteNonQuery());
            }

        }
        // GET LIST ALL Movie
        public List<object> getAllVoucher()
        {
            List<object> list = new List<object>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from vouchers";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = new
                        {
                            id = Convert.ToInt32(reader["id"]),
                            image = reader["image"].ToString(),
                            percent_discount = Convert.ToDouble(reader["percent_discount"]),
                            description = reader["description"].ToString(),
                            supplier_name = reader["supplier_name"].ToString(),
                            point_cost = Convert.ToDouble(reader["point_cost"]),
                            create_at = reader["create_at"].ToString()
                        };

                        list.Add(obj);
                    };

                
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // GET Movie BY ID 
        public Voucher getVoucher(int id)
        {
            Voucher  voucher = new Voucher();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from vouchers where id =@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reader.Read();
                        voucher.id = Convert.ToInt32(reader["id"]);
                        voucher.image = reader["image"].ToString();
                        voucher.percent_discount = Convert.ToDouble(reader["percent_discount"]);
                        voucher.description = reader["description"].ToString();
                        voucher.supplier_name = reader["supplier_name"].ToString();
                        voucher.point_cost = Convert.ToDouble(reader["point_cost"]);

                    }
                    else
                        return (null);
                    
                }

            }
            return (voucher);
        }
    }
}
