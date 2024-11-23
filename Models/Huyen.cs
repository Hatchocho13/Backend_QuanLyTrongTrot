namespace backend_qltt2.Models
{
    public class Huyen
    {
        public int ID { get; set; }
    public string TenHuyen { get; set; }

    // Quan hệ 1-nhiều với Xa
    public ICollection<Xa> Xas { get; set; }
    }
}
