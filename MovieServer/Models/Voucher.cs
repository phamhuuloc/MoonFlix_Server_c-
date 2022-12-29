using System.Runtime.InteropServices;

namespace MovieServer.Models
{
    public class Voucher
    {
        private int Id;
        private string Image;
        private double Percent_discount;
        private string Description;
        private string Supplier_name;
        private double Point_cost;

        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        public string image
        {
            get { return Image; }
            set { Image = value; }
        }
        public double percent_discount
        {
            get { return Percent_discount; }
            set { Percent_discount = value; }
        }
        public string description
        {
            get { return Description; }
            set
            {
                Description = value;
            }
        }
        public string supplier_name
        {
            get { return Supplier_name; }
            set
            {
                Supplier_name = value;
            }
        }
        public  double point_cost
        {
            get { return Point_cost; }
            set
            {
                Point_cost = value;
            }
        }
        public Voucher()
        {

        }
        public Voucher (int id  , string image , string percent_disount, string description, string supplier_name, double point_cost)
        {
            this.Id = id;
            this.Image = image;
            this.Percent_discount = percent_discount ;
            this.Description = description;
            this.Supplier_name = supplier_name;
            this.Point_cost = point_cost;

        }
    }
}
