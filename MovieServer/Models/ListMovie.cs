using System.Runtime.InteropServices;

namespace MovieServer.Models
{
    public class ListMovie
    {
        private int Id;
        private int Lm_list_id;
        private int Lm_movie_id;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        public  int lm_list_id
        {
            get { return Lm_list_id;}
            set {  Lm_list_id = value; }    
        }
        public int lm_movie_id
        {
            get { return Lm_movie_id;}
            set
            {
                Lm_movie_id = value;
            }
        }
        public ListMovie() { }
        public ListMovie(int id , int  lm_list_id , int list_movie_id)
        {
            this.Id = id;
            this.Lm_list_id = lm_list_id;
            this.Lm_movie_id = lm_movie_id;
        }
    }
}
