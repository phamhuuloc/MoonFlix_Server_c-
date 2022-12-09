using System;
namespace MovieServer.Models
{
    public class User
    {
        private int Id;
        private string Username;
        private string Email = "";
        private string Password = "";
        private string ProfilePic;
        private double Wallet_balance = 0;
        private double Point = 0;
        private double  Money_spended = 0;
        private string Phone = "";
        private Boolean IsAdmin = false;
        private string Face_id = "";
        //private string Create_at;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        public string username
        {
            get { return Username; }
            set { Username = value; }
        }
        public string email
        {
            get { return Email; }
            set { Email = value; }
        }
        public string password
        {
            get { return Password; }
            set { Password = value; }
        }
        public string profilePic
        {
            get { return ProfilePic; }
            set { ProfilePic = value; }
        }
        public double wallet_balance
        {
            get { return Wallet_balance; }
            set { Wallet_balance = value; }
        }
        public double point
        {
            get { return Point; }
            set { Point = value; }
        }
        public double money_spended
        {
            get { return Money_spended; }
            set { Money_spended = value; }
        }
        public string phone
        {
            get { return Phone; }
            set { Phone = value; }
        }
        public Boolean isAdmin
        {
            get { return IsAdmin; }
            set { IsAdmin = value; }
        }
        public string face_id
        {
            get { return Face_id; }
            set { Face_id = value; }
        }

        public User() { }
        public User (string email, string password)
        {
            this.email = email;
            this.password = password;
        }
        public User(int id , string username , string email , string password , string profilePic, double wallet_balance, double point,double money_spended,string phone, Boolean isAdmin, string face_id)
        {
            this.id = id;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.ProfilePic = profilePic;
            this.Wallet_balance = wallet_balance;
            this.Point = point;
            this.Money_spended = money_spended;
            this.Phone = phone;
            this.IsAdmin = isAdmin;
            this.Face_id = face_id;

        }
    }
}
