namespace KPAPI.Model
{
    public class Item
    {
        public Guid Id { get; set; }
        public int Sku { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime ValidUntil { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorNickName { get; set; }
        public double Price { get; set; }
        public string PriceType { get; set; }
        public string Condition { get; set; }
        public string SellerName { get; set; }
        public string SellerPhone { get; set; }

        public List<string> Images { get; set; } = new List<string>();
    }
}