namespace MovieServer.Models
{
    public class UserVoucher
    {
        private int Id;
        private int Uv_user_id;
        private int Uv_voucher_id;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }
        public int uv_user_id
        {
            get { return Uv_user_id; }
            set { Uv_user_id = value; }
        }
        public int uv_voucher_id
        {
            get { return Uv_user_id; }
            set
            {
                Uv_user_id = value;
            }   
        }
        public UserVoucher() { }
        public UserVoucher(int id , int uv_user_id, int movie_id)
        {
            this.Id = id;
            this.Uv_user_id = uv_user_id;
            this.Uv_voucher_id = uv_voucher_id;
        }

    }
}
