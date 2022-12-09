namespace MovieServer.Models
{
    public class UserMovie
    {
        private int Id;
        private int Um_user_id;
        private int Um_movie_id;
        public  int id
        {
            get { return Id; }
            set { Id = value; }

        }
        public int um_user_id
        {
            get { return Um_user_id; }
            set { Um_user_id = value; }
        }
        public int um_movie_id
        {
            get { return Um_movie_id; }
            set
            {
                Um_movie_id = value;
            }   

        }
        public UserMovie() { }
        public UserMovie(int id , int um_user_id , int um_movie_id)
        {
            this.Id = id;
            this.Um_user_id = um_user_id;
            this.Um_movie_id = um_movie_id; 
        }

    }
}
