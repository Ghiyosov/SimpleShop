using Domein.Dtos;
using Domein.Interface;
using Domein.Models;
using Domein.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProduct _product) : ControllerBase
{
    [HttpGet("GetProducts")]
    public async Task<Response<List<Product>>> GetProducts() => await _product.GetProducts();

    [HttpGet("GetProducts/{id}")]
    public async Task<Response<Product>> GetProduct(int id) => await _product.GetProductById(id);
    
    [HttpPost("AddProduct")]
    public async Task<Response<bool>> AddProduct(ProductDto product)=> await _product.AddProduct(product);
    
    [HttpPut("UpdateProduct")]
    public async Task<Response<bool>> UpdateProduct(Product product)=> await _product.UpdateProduct(product);
    
    [HttpDelete("DeleteProduct")]
    public async Task<Response<bool>> DeleteProduct(int id)=> await _product.DeleteProduct(id);
    
    [HttpGet("GetProductsWhith stock")]
    public async Task<Response<List<Product>>> GetProductsWhithStock(int stock)=>await _product.GetProductsWithStock(stock);

    [HttpGet("GetExpenesivProduct")]
    public async Task<Response<Product>> GetExpenesivProduct() => await _product.GetExpenisivProduct();
}