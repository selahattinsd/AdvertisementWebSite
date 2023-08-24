namespace IlanSitesiEF.Models
{
    public class IlanModel
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public DateTime Created_on { get; set; }
        public DateTime Updated_on { get; set; }
        public string Images { get; set; }
    }
}
        