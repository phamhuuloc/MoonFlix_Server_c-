namespace MovieServer.Models
{
    public class Movie
    {
        private int Id;
        private int Supplier_id;
        private string Title;
        private string Desc;
        private string Img;
        private string ImgSm;
        private string Trailer;
        private string Video;
        private int Year;
        private double Limit;
        private double Price;
        private int Clicked;
        private Boolean IsSeries;
      
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        public int supplier_id
        {
            get { return Supplier_id; }
            set { Supplier_id = value; }
        }
        public string title
        {
            get { return Title; }
            set { Title = value; }
        }
        public string _desc
        {
            get { return Desc; }
            set { Desc = value; }
        }
        public string img
        {
            get { return Img; }
            set { Img = value; }
        }
        public string imgSm
        {
            get { return ImgSm; }
            set { ImgSm = value; }
        }
        public string trailer
        {
            get { return Trailer; }
            set { Trailer = value; }
        }
        public string video 
        {
            get { return Video; }
            set { Video = value; }
        }
        public int year
        {
            get { return Year; }
            set { Year = value; }
        }
        public double _limit
        {
            get { return Limit; }
            set { Limit = value; }
        }
        public double price
        {
            get { return Price  ; }
            set { Price = value; }
        }
        public int clicked
        {
            get { return Clicked; }
            set { Clicked = value; }
        }
        public Boolean isSeries                                                                                                                                                                                                                                                                                                                                                                                                                  
        {
            get { return IsSeries; }
            set { IsSeries = value; }
        }


        public Movie() { }


        public Movie(int id ,  string title, string desc, string img, string imgSm, string trailer, string video, int year, double limit, double price, int clicked, bool isSeries)
        { 
            this.Id = id;
            this.Supplier_id = supplier_id;
            this.Title = title;
            this.Desc = desc;
            this.Img = img;
            this.ImgSm = imgSm;
            this.Trailer = trailer;
            this.Video = video;
            this.Year = year;
            this.Limit = limit;
            this.Price = price;
            this.Clicked = clicked;
            this.IsSeries = isSeries;            
        }
    }
}

