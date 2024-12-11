namespace XYZ_Pharmaceuticals.Entities
{
    public class Quote
    {
        public int ID { get; set; }
        public string QuoteText { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string Status { get; set; }
    }
}
