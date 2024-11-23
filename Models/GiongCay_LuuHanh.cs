namespace backend_qltt2.Models
{
    public class GiongCay_LuuHanh
    {
        public int ID_GiongCay { get; set; }
        public int ID_Xa { get; set; }

        // Navigation properties
        public GiongCayTrong GiongCayTrong { get; set; }
        public Xa Xa { get; set; }
    }
}
