namespace backend_qltt2.Models
{
    public class ThuocBVTV
    {
        public int ID { get; set; }
        public string TenThuoc { get; set; }
        public string ThongTin { get; set; }

        // FK đến Cơ sở sản xuất
        public int ID_CSSX { get; set; }
        public CoSoSanXuat CoSoSanXuat { get; set; }

        // FK đến Cơ sở buôn bán
        public int ID_CSBB { get; set; }
        public CoSoBuonBan CoSoBuonBan { get; set; }
    }
}
