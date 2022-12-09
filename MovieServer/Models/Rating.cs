namespace MovieServer.Models
{
    public class Rating
    {
        private int Id;
        private int R_number_star;
        private int R_user_id;
        private int R_movie_id;
        private string R_content;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        public int r_number_star
        {
            get { return R_number_star; }
            set { R_number_star = value; }
        }
        public int r_user_id
        {
            get { return R_user_id; }
            set
            {
                R_user_id = value;
            }
        }
        public int r_movie_id
        {
            get { return R_movie_id; }
            set
            {
                R_movie_id = value;
            }
        }
        public string r_content
        {
            get { return R_content; }
            set
            {
                R_content = value;
            }
        }
        public Rating() { }
        public Rating(int id , int r_number_star , int r_user_id, int r_movie_id, string r_content)
        {
            this.Id = id;
            this.R_number_star = r_number_star;
            this.R_user_id = r_user_id;
            this.R_movie_id = r_movie_id;
            this.R_content = r_content;
        }
    }
}
