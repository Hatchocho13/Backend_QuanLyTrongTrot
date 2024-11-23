namespace backend_qltt2.Models
{
    public class SXTT
    {
        public int ID { get; set; }
        public bool CSAnToanVietGap { get; set; }
        public string VungTrong { get; set; } // GEOGRAPHY có thể lưu dưới dạng string WKT
        public string SinhVatGayHai { get; set; }

        // FK đến Xã
        public int ID_Xa { get; set; }
        public Xa Xa { get; set; }
    }
}
