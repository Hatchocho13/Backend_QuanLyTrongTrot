namespace backend_qltt2.Models
{
    public class GiongCayTrong
    {
        public int ID { get; set; }
        public string TenGiong { get; set; }
        public string ThongTin { get; set; }

        // Quan hệ nhiều-nhiều với Xa (qua GiongCay_LuuHanh)
        public ICollection<GiongCay_LuuHanh> Xas { get; set; }
    }
}
