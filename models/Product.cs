using System.Text.Json.Serialization;

namespace e_commerce_web_app_server;
// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Rating{
        [JsonPropertyName("rate")]
        public double Rate { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
    public class Product{
    private decimal price;

    public Product(int id, string Title, decimal Price, string Description, string Category, Rating Rating){
        Id = id;
        this.Title = Title;
        price = Price;
        this.Description = Description;
        this.Category = Category;
        this.Rating = Rating;
    }
    [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]        
        public string Title { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("rating")]
        public Rating Rating { get; set; }
    }
