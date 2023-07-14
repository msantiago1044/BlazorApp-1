using BlazorApp_1;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp_1;
public class CategoryService : ICategoryService
{
    private readonly HttpClient client;

    private readonly JsonSerializerOptions options;

    public CategoryService(HttpClient httpClient)
    {
        this.client = httpClient;
        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<List<Category>?> Get()
    {
        var response = await client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Category>>(content, options);

    }
    public async Task Add(Category category)
    {
        var response = await client.PostAsync("v1/categories", JsonContent.Create(category));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
    public async Task Update(int categoryId, Category category)
    {
        var response = await client.PutAsync($"v1/categories/{categoryId}", JsonContent.Create(category));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
    public async Task Delete(int categoryId)
    {
        var response = await client.DeleteAsync($"v1/categories/{categoryId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
    public async Task<List<Product>?> Pag(int categoryID)
    {
        var response = await client.GetAsync($"v1/categories/{categoryID}/products");
        return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
    }
}

public interface ICategoryService
{
    Task<List<Category>?> Get();
    Task Add(Category category);
    Task Delete(int categoryID);
    Task Update(int categoryID, Category category);
    Task<List<Product>?> Pag(int categoryID);
}