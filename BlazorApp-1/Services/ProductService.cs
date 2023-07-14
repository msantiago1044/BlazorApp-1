﻿using BlazorApp_1;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp_1;
public class ProductService : IProductService
{
    private readonly HttpClient client;

    private readonly JsonSerializerOptions options;

    public ProductService(HttpClient httpClient) {

        this.client = httpClient;
		options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

	}

    public async Task <List<Product>?> Get()
    {
        var response = await client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Product>>(content, options);
    }
    public async Task Add(Product product)
    {
        var response = await client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
    public async Task Update(int productId, Product product)
    {
        var response = await client.PutAsync($"v1/products/{productId}", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
    public async Task Delete(int productId)
    {
        var response = await client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
    public async Task<List<Product>?> Pag(int offset, int limit)
    {
        var response = await client.GetAsync("v1/products?offset={offset}&limit={limit}");
        return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
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
