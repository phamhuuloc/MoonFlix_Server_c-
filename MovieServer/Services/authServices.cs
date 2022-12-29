using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MovieServer.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieServer.Services
{
    public class authServices
    {
        public string ConnectionString { get; set; }//biết thành viên 

        public authServices(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        // GENERATE TOKEN USING FOR LOGIN
        private string GenerateToken(User  user )
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes("FreeCourseDemoASPNETCoreWebAPI");

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {

                    new Claim("Id", user.id.ToString()),
                    new Claim("UserName", user.username),
                    new Claim ("email", user.email),
                    new Claim("face_id", user.face_id),

                }),
                Expires = DateTime.UtcNow.AddMinutes(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
        // LOGIN FOR USER
        public object login(Auth userInfo)
        {
            userServices userservices = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");
            var user = userservices.findUserByEmail(userInfo.email);
            if (user == null)
            {
                return new
                {
                    Success = false,
                    message = "Wrong password or email!",
                };
            }
            if( user.face_id == userInfo.password)
            {
                return new
                {
                    Success = true,
                    Message = "Login Successfully!",
                    Data = user,
                    token = GenerateToken(user)
                };
            }
            else if (user.password != userInfo.password)
            {
                return new
                {
                    Success = false,
                    Message = "Password is not valid!"
                };
            }
            return new
            {
                Success = true,
                Message = "Login Successfully!",
                Data = user,
                token = GenerateToken(user)
            };

        }

        // REGISTER FOR USER
        public int insertUser(string email, string password)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into user(email,password) values(@email,@password)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("password", password);
                return (cmd.ExecuteNonQuery());

            }
        }
        public object register(FaceInfo userInfo)
        {
            userServices userservices = new userServices("server=movieserver.mysql.database.azure.com;uid=loc281202;pwd=@#PHAMHUUNAM281202;database=movieserver;");

            var user = userservices.findUserByEmail(userInfo.email);
            if (user.email  == "")
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var str = "insert into user(email,password,username, face_id) values(@email,@password,@username,@face_id)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("email", userInfo.email);
                    cmd.Parameters.AddWithValue("password", userInfo.password);
                    cmd.Parameters.AddWithValue("username", userInfo.username);
                    cmd.Parameters.AddWithValue("face_id", userInfo.face_id);
                    cmd.ExecuteNonQuery();

                    return new
                    {
                        Success = true,
                        Message = "Register Successfully!",
                    };
                }
                
            }


            return new
            {
                Success = false,
                message = "Email already exists",
                user = user
            };

        }


    }


 
}
