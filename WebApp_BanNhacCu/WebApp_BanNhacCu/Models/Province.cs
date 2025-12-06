namespace WebApp_BanNhacCu.Models
{
    public class Province
    {
        public int code { get; set; }
        public string name { get; set; }
        public string division_type { get; set; }
        public string codename { get; set; }
        public int phone_code { get; set; }
        public List<Ward> wards { get; set; }
    }
}
