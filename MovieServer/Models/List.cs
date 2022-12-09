namespace MovieServer.Models
{
    public class List
    {
        private int Id;
        private string Title;
        private string Type;
        private string Genre;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        public string title
        {
            get { return Title; }
            set { Title = value; }
        }
        public string type
        {
            get {  return Type; }
            set { Type = value; }   
        }
        public string genre
        {
            get { return Genre; }
            set
            {
                Genre = value;
            }   
        }
        public List() { }
        public List(int id , string title , string  type , string genre)
        {
            this.Id = id;
            this.Title = title;
            this.Type = type;
            this.Genre = genre;
        }
    }
}
