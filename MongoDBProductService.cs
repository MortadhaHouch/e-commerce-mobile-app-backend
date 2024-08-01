using e_commerce_web_app_server;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_commerce_web_app_server
{
    public class MongoDBProductService
    {
        private readonly IMongoCollection<Product> _products;

        public MongoDBProductService(IMongoClient client)
        {
            var database = client.GetDatabase("your_database_name");
            _products = database.GetCollection<Product>("Products");
        }

        // CRUD operations for Product
        public async Task CreateProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _products.Find(product => true).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _products.Find<Product>(product => Convert.ToString(product.Id) == id).FirstOrDefaultAsync();
        }

        public async Task UpdateProductAsync(string id, Product productIn)
        {
            await _products.ReplaceOneAsync(product => Convert.ToString(product.Id) == id, productIn);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _products.DeleteOneAsync(product => Convert.ToString(product.Id) == id);
        }
    }
}