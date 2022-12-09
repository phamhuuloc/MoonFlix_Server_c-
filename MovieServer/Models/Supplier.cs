namespace MovieServer.Models
{
    public class Supplier
    {
        private int Id;
        private string Sl_name;
        private string Sl_email;
        private string Sl_phone;
        private string Sl_address;
        
       public int  id
        {
            get { return Id; }
            set { Id = value; }
        }
       public string sl_name
        {
            get { return Sl_name; }
            set { Sl_name = value; }
        }
        public string sl_email
        {
            get { return Sl_email; }
            set { Sl_email = value; }
        }
        public string sl_phone
        {
            get { return Sl_phone; }
            set { Sl_phone = value; }
        }
        public string sl_address
        {
            get { return Sl_address; }
            set { Sl_address = value; }
        }
        public Supplier() { }
        public Supplier( int id , string sl_name , string sl_email ,string sl_phone , string sl_address)
        {
            this.Id = id;
            this.Sl_name = sl_name;
            this.Sl_email = sl_email;
            this.Sl_phone = sl_phone;
            this.Sl_address = sl_address;
        }
    }
}
