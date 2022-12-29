using MovieServer.Models;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO.Pipelines;

namespace MovieServer.Services
{
    public class userServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public userServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        // CREATE A NEW USER (SIGN UP)
        public int createNewUser(User user)
        {

            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var str = "insert into user(username,email,password,profilePic,wallet_balance,point,money_spended,phone,isAdmin) values(@username,@email,@password,@profilePic,@wallet_balance,@point,@money_spended,@phone,@isAdmin)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("username", user.username);
                cmd.Parameters.AddWithValue("email", user.email);
                cmd.Parameters.AddWithValue("password", user.password);
                cmd.Parameters.AddWithValue("profilePic", user.profilePic);
                cmd.Parameters.AddWithValue("wallet_balance", user.wallet_balance);
                cmd.Parameters.AddWithValue("point", user.point);
                cmd.Parameters.AddWithValue("money_spended", user.money_spended);
                cmd.Parameters.AddWithValue("phone", user.phone);
                cmd.Parameters.AddWithValue("isAdmin", user.isAdmin);
                return (cmd.ExecuteNonQuery());
            }
        }
        public int updateUser(User user, string id)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update user set username = @username, email = @email, password = @password , profilePic = @profilePic, wallet_balance = @wallet_balance , point = @point , money_spended = @money_spended , phone = @phone , isAdmin = @isAdmin where id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("username", user.username);
                cmd.Parameters.AddWithValue("email", user.email);
                cmd.Parameters.AddWithValue("password", user.password);
                cmd.Parameters.AddWithValue("profilePic", user.profilePic);
                cmd.Parameters.AddWithValue("wallet_balance", user.wallet_balance);
                cmd.Parameters.AddWithValue("point", user.point);
                cmd.Parameters.AddWithValue("money_spended", user.money_spended);
                cmd.Parameters.AddWithValue("phone", user.phone);
                cmd.Parameters.AddWithValue("isAdmin", user.isAdmin);
                return (cmd.ExecuteNonQuery());
            }
        }
        // DELETE USER 
        public int deleteUser(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from user where id=@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                return (cmd.ExecuteNonQuery());
            }

        }
        // GET LIST ALL USERS
        public List<User> getAllUser()
        {
            List<User> list = new List<User>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from user";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            username = reader["username"].ToString(),
                            email = reader["email"].ToString(),
                            password = reader["password"].ToString(),
                            profilePic = reader["profilePic"].ToString(),
                            wallet_balance = Convert.ToDouble(reader["wallet_balance"]),
                            point = Convert.ToDouble(reader["point"]),
                            money_spended = Convert.ToDouble(reader["money_spended"]),
                            phone = reader["phone"].ToString(),
                            isAdmin = Convert.ToBoolean(reader["isAdmin"]),

                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // GET USER BY ID 
        public User getUser(int id)
        {
            User user = new User();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from user where id =@id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    user.id = Convert.ToInt32(reader["id"]);
                    user.username = reader["username"].ToString();
                    user.email = reader["email"].ToString();
                    user.password = reader["password"].ToString();
                    user.profilePic = reader["profilePic"].ToString();
                    user.wallet_balance = Convert.ToDouble(reader["wallet_balance"]);
                    user.point = Convert.ToDouble(reader["point"]);
                    user.money_spended = Convert.ToDouble(reader["money_spended"]);
                    user.phone = reader["phone"].ToString();
                    user.isAdmin = Convert.ToBoolean(reader["isAdmin"]);


                }

            }
            return (user);
        }

        //GET TOP 10 USER ORDER BY DESC 
        public object getTopUser()
        {
            List<User> topUserMonth = new List<User>();
            List<User> topUserYear = new List<User>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * from user    WHERE MONTH(create_at) = MONTH(CURRENT_DATE()) order BY money_spended DESC LIMIT 10";
                MySqlCommand cmd = new MySqlCommand(str, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        topUserMonth.Add(new User()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            username = reader["username"].ToString(),
                            email = reader["email"].ToString(),
                            password = reader["password"].ToString(),
                            profilePic = reader["profilePic"].ToString(),
                            wallet_balance = Convert.ToDouble(reader["wallet_balance"]),
                            point = Convert.ToDouble(reader["point"]),
                            money_spended = Convert.ToDouble(reader["money_spended"]),
                            phone = reader["phone"].ToString(),
                            isAdmin = Convert.ToBoolean(reader["isAdmin"]),

                        });
                    }
                    reader.Close();


                }
                conn.Close();

            }
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * from user  WHERE YEAR(create_at) = YEAR(CURRENT_DATE())   order BY money_spended DESC LIMIT 10";
                MySqlCommand cmd = new MySqlCommand(str, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        topUserYear.Add(new User()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            username = reader["username"].ToString(),
                            email = reader["email"].ToString(),
                            password = reader["password"].ToString(),
                            profilePic = reader["profilePic"].ToString(),
                            wallet_balance = Convert.ToDouble(reader["wallet_balance"]),
                            point = Convert.ToDouble(reader["point"]),
                            money_spended = Convert.ToDouble(reader["money_spended"]),
                            phone = reader["phone"].ToString(),
                            isAdmin = Convert.ToBoolean(reader["isAdmin"]),

                        });
                    }
                    reader.Close();


                }
                conn.Close();

            }
            return new
            {
                Success = true,
                Message = "Login Successfully!",
                TopUserMonth = topUserMonth,
                TopUserYear = topUserYear,

            };
        }
        public User findUserByEmail(string email)
        {
            User user = new User();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from user where email =@email";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("email", email);
                using (var reader = cmd.ExecuteReader())
                {
                    //reader.Read();
                    if (reader.Read())
                    {
                        reader.Read();
                        user.id = Convert.ToInt32(reader["id"]);
                        user.username = reader["username"].ToString();
                        user.email = reader["email"].ToString();
                        user.password = reader["password"].ToString();
                        user.profilePic = reader["profilePic"].ToString();
                        user.wallet_balance = Convert.ToDouble(reader["wallet_balance"]);
                        user.point = Convert.ToDouble(reader["point"]);
                        user.money_spended = Convert.ToDouble(reader["money_spended"]);
                        user.phone = reader["phone"].ToString();
                        user.face_id = reader["face_id"].ToString();
                        user.isAdmin = Convert.ToBoolean(reader["isAdmin"]);
                    }


                }

            }
            return (user);
        }

        // GET MOVIE OF USER
        public List<Movie> getMovieOfUser(int id)
        {
            List<Movie> list = new List<Movie>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT *  FROM  movie   INNER JOIN user_movies um ON um.um_movie_id = movie.id where um.um_user_id = @id";
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

        // GET VOUCHER OF USER 
        public List<Voucher> getVoucherOfUser(int id)
        {
            List<Voucher> list = new List<Voucher>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT * from vouchers  INNER JOIN user_vouchers uv ON uv.uv_voucher_id = vouchers.id WHERE uv.uv_user_id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Voucher()
                        {

                            id = Convert.ToInt32(reader["id"]),
                            image = reader["image"].ToString(),
                            percent_discount = Convert.ToDouble(reader["percent_discount"]),
                            description = reader["description"].ToString(),
                            supplier_name = reader["supplier_name"].ToString(),
                            point_cost = Convert.ToDouble(reader["point_cost"])
                        });
                    }
                    reader.Close();

                }
            }
            return (list);

        }

        // GET Revenue
        public object getRevenue()
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT SUM(user.money_spended) as 'revenue' from user   WHERE MONTH(create_at) = MONTH(CURRENT_DATE());";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    var ob = new { revenue = Convert.ToDouble(reader["revenue"]) };
                    reader.Close();
                    return new
                    {
                        Success = true,
                        Message = "Get revenue Successfully!",
                        Data = ob

                    };
                }
                conn.Close();


            }
            return new
            {
                Success = false,
                Message = "Get revenue Failer!"

            };
        }
        public List<object> getStasUser()
        {
            List<object> userStatus = new List<object>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "SELECT Month(create_at)  as month , COUNT(user.id ) as total  from user group by Month(create_at);";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reader.Read();
                        var ob = new { month = reader["month"].ToString(), total = Convert.ToInt32(reader["total"]) };
                        userStatus.Add(ob);
                    }
                    reader.Close();

                }
                conn.Close();


            }
            return userStatus;
        }

        // vn payment 

    }
}
