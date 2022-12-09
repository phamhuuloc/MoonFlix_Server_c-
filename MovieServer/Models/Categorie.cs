namespace MovieServer.Models
{
    public class Categorie
    {
        private int Id;
        private string  Cate_name;
        private int Cate_type;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        public string cate_name
        {
            get { return Cate_name; }
            set { Cate_name = value; }
        }
        public int cate_type
        {
            get { return Cate_type; }
            set { Cate_type = value; }
        }
        public Categorie() { }
        public Categorie (int id , string cate_name, int cate_type)
        {
            this.Id = id;
            this.Cate_name = cate_name;
            this.Cate_type = cate_type;
        }
    }
}
