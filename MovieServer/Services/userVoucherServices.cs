using MovieServer.Models;
using MySql.Data.MySqlClient;

namespace MovieServer.Services
{
    public class userVoucherServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public userVoucherServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        // check wallet balance 
        private double checkPointBalance (int user_id, int voucher_id)
        {
            // get wallet balance of user 
            userServices userServices = new userServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var user = userServices.getUser(user_id);
            double point = user.point;
            //get price of movie 
            voucherServices voucherservices = new voucherServices("server=127.0.0.1;user id=root;password=;port=3306;database=moviestore;");
            var voucher = voucherservices.getVoucher(voucher_id);
            double point_cost = voucher.point_cost;
            return point - point_cost;
        }

        // check if the user already owns the movie
        public object checkUserHaveVoucher(int user_id, int voucher_id)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "SELECT COUNT(user.id) as 'quantity' from vouchers INNER JOIN user_vouchers uv on uv.uv_voucher_id = vouchers.id INNER JOIN user on user.id = uv.uv_user_id where vouchers.id =@voucher_id and user.id = @user_id;";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.Parameters.AddWithValue("voucher_id", voucher_id);
                var reader = cmd.ExecuteScalar();

                var ob = new { quantity = Convert.ToInt32(reader) };

                return ob;

            }
        }

        public object byVoucher(UserVoucher userVoucher)
        {
            double checkPoint = checkPointBalance(userVoucher.uv_user_id, userVoucher.uv_voucher_id);
            object checkMovie = checkUserHaveVoucher(userVoucher.uv_user_id, userVoucher.uv_voucher_id);

            int quantity = Convert.ToInt32(checkMovie.GetType().GetProperty("quantity").GetValue(checkMovie, null));


            if (quantity > 0)
            {
                return new
                {
                    Success = false,
                    Message = "The Voucher already exit on your vouchers list!"
                };
            }
            else if (checkPoint < 0)
            {
                return new
                {
                    Success = false,
                    Message = "Insufficient point balance"
                };
            }

            else
            {

                using (MySqlConnection conn = GetConnection())
                {

                    conn.Open();
                    var str = "insert into user_vouchers (uv_user_id , uv_voucher_id) values(@uv_user_id,@uv_voucher_id)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("uv_user_id", userVoucher.uv_user_id);
                    cmd.Parameters.AddWithValue("uv_voucher_id", userVoucher.uv_voucher_id);
                    int flag = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (flag > 0)
                    {
                        conn.Open();
                        var str2 = "update user set point = @point where id = @id";
                        MySqlCommand cmd2 = new MySqlCommand(str2, conn);
                        cmd2.Parameters.AddWithValue("point", checkPoint);
                        cmd2.Parameters.AddWithValue("id", userVoucher.uv_user_id);
                        cmd2.ExecuteNonQuery();
                        return new
                        {
                            Success = true,
                            Message = "Buy Voucher Successfully!"
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
