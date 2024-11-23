namespace backend_qltt2.Models
{
    public class CoSoBuonBan
    {
        public int ID { get; set; }
        public string TenCS { get; set; }
        public string ViTri { get; set; } // GEOGRAPHY lưu dưới dạng string WKT

        // FK đến Xã
        public int ID_Xa { get; set; }
        public Xa Xa { get; set; }
    }
}
