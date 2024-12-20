using Domein.Dtos;
using Domein.Models;
using Domein.Responses;

namespace Domein.Interface;

public interface IProduct
{
    public Task<Response<List<Product>>> GetProducts();
    public Task<Response<Product>> GetProductById(int id);
    public Task<Response<bool>> AddProduct(ProductDto product);
    public Task<Response<bool>> UpdateProduct(Product product);
    public Task<Response<bool>> DeleteProduct(int id);
    public Task<Response<List<Product>>> GetProductsWithStock(int stock);
    public Task<Response<Product>> GetExpenisivProduct();
}