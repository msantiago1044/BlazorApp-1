using BlazorApp_1.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp_1.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client;

        private readonly JsonSerializerOptions options;

        public ProductService(HttpClient httpClient, JsonSerializerOptions optionsJson) {

            this.client = httpClient;

            this.options = optionsJson;
        
        }

        public async Task <List<Product>?> Get()
        {
            var response = await client.GetAsync("/products");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            return JsonSerializer.Deserialize<List<Product>>(content, options);
        }
        public async Task Add(Product product)
        {
            var response = await client.PostAsync("/products", JsonContent.Create(product));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }
        public async Task Update(int productId, Product product)
        {
            var response = await client.PutAsync("/products/{productId}", JsonContent.Create(product));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }
        public async Task Delete(int productId)
        {
            var response = await client.DeleteAsync("/products/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
        }
        public async Task<List<Product>?> Pag(int offset, int limit)
        {
            var response = await client.GetAsync("/products?offset={offset}&limit={limit}");
            return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
        }
    }
}
public interface IProductService
{
    Task<List<Product>?> Get();
    Task Add(Product product);
    Task Update(int productId, Product product);
    Task Delete(int productId);
    Task<List<Product>?> Pag(int offset, int limit);

}
