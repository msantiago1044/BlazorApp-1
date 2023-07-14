namespace BlazorApp_1.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Uri Image { get; set; }
        public DateTimeOffset CreationAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

    }
}
