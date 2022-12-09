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
                var str = "insert into vouchers (image ,voucher_code , description,supplier_name ,point_cost) values(@image,@voucher_code,@description,@supplier_name,@point_cost)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("image", voucher.image);
                cmd.Parameters.AddWithValue("voucher_code", voucher.voucher_code);
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
                var str = "update vouchers set image = @image , voucher_code = @voucher_code , description = @description ,supplier_name =@supplier_name, point_cost = @point_cost where id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", voucher.id);
                cmd.Parameters.AddWithValue("image",voucher.image);
                cmd.Parameters.AddWithValue("voucher_code", voucher.voucher_code);
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
        public List<Voucher> getAllVoucher()
        {
            List<Voucher> list = new List<Voucher>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from vouchers";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Voucher()
                        {
                          id = Convert.ToInt32(reader["id"]),
                          image = reader["image"].ToString(),
                          voucher_code = reader["voucher_code"].ToString(),
                          description = reader["description"].ToString(),
                          supplier_name = reader["supplier_name"].ToString(),
                          point_cost = Convert.ToDouble(reader["point_cost"])

                        });
                    }
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
                        voucher.voucher_code = reader["voucher_code"].ToString();
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
