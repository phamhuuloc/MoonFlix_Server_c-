namespace MovieServer.Models
{
    public class FaceInfo
    {
        public  string email { get; set; }
        public string password { get; set; }   
        public string username { get; set; }
        public string face_id { get; set; } 
        public FaceInfo() { }
        public FaceInfo(string email, string password, string face_id, string username)
        {
            this.email = email;
            this.password = password;
            this.face_id = face_id;
            this.username = username;
        }
    }
}
