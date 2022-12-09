namespace MovieServer.Models
{
    public class Auth
    {
        public  string email { get; set; }
        public string password { get; set; }   
        public Auth() { }
        public Auth(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
