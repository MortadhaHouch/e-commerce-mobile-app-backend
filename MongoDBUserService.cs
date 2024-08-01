using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace e_commerce_web_app_server
{
    public class MongoDBUserService
    {
        private readonly IMongoCollection<User> _users;

        public MongoDBUserService(IMongoClient client)
        {
            var database = client.GetDatabase("your_database_name");
            _users = database.GetCollection<User>("Users");
        }

        // CRUD operations for User
        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _users.Find<User>(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _users.Find<User>(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task UpdateUserAsync(string id, User userIn)
        {
            await _users.ReplaceOneAsync(user => user.Id == id, userIn);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }

        // Add Product to User
        public async Task AddProductToUserAsync(string userId, string productId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.ProductIds.Add(productId);
                await UpdateUserAsync(userId, user);
            }
        }

        // Remove Product from User
        public async Task RemoveProductFromUserAsync(string userId, string productId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.ProductIds.Remove(productId);
                await UpdateUserAsync(userId, user);
            }
        }
    }
}