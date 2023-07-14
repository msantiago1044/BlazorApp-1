namespace BlazorApp_1.Models
{
    public class Product
    {
        public string Title { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
        public Uri[] Images { get; set; }
        public Category Category { get; set; }
        public long Id { get; set; }
        public DateTimeOffset CreationAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
