using System.Runtime.InteropServices;

namespace MovieServer.Models
{
    public class Voucher
    {
        private int Id;
        private string Image;
        private string Voucher_code;
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
        public string voucher_code
        {
            get { return Voucher_code; }
            set { Voucher_code = value; }
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
        public Voucher (int id  , string image , string voucher_code, string description, string supplier_name, double point_cost)
        {
            this.Id = id;
            this.Image = image;
            this.Voucher_code = voucher_code;
            this.Description = description;
            this.Supplier_name = supplier_name;
            this.Point_cost = point_cost;

        }
    }
}
