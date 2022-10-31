using System.Text.Json.Serialization;

namespace ElasticWebApi.Model.Product
{
    public class Product
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Measurement { get; set; }
        public string? Title { get; set; }
        public string? Unit { get; set; }
        
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
    }
}
