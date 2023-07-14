namespace BlazorApp_1
{
    public class Product
    {
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; } 
        public string[] Images { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }
        public DateTimeOffset CreationAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
