namespace backend_qltt2.Models
{
    public class Xa
    {
        public int ID { get; set; }
        public string TenXa { get; set; }

        // FK đến Huyện
        public int ID_Huyen { get; set; }
        public Huyen Huyen { get; set; }

        // Quan hệ 1-nhiều với SXTT
        public ICollection<SXTT> SXTTs { get; set; }

        // Quan hệ nhiều-nhiều với Giống cây trồng
        public ICollection<GiongCay_LuuHanh> GiongCayTrongs { get; set; }
    }
}
