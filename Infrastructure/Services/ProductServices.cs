using System.Net;
using Dapper;
using Domein.Dtos;
using Domein.Interface;
using Domein.Models;
using Domein.Responses;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class ProductServices(IContext _context) : IProduct
{
    public async Task<Response<List<Product>>> GetProducts()
    {
        try
        {
            var sql = @"select * from Products";
            var res = await _context.Connection().QueryAsync<Product>(sql);
            return new Response<List<Product>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Product>> GetProductById(int id)
    {
        try
        {
            var sql = @"select * from Products where ProductId = @id";
            var res = await _context.Connection().QuerySingleOrDefaultAsync<Product>(sql, new { id });
            return new Response<Product>(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> AddProduct(ProductDto product)
    {
        try
        {
            var sql = @"insert into Products (ProductName,Price,Stock ) values (@ProductName, @Price, @Stock)";
            var res = await _context.Connection().ExecuteAsync(sql, product);
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.Created, "Product added successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> UpdateProduct(Product product)
    {
        try
        {
            var sql = @"update Products set ProductName = @ProductName, Price=@Price, Stock=@Stock where ProductId = @ProductId";
            var res = await _context.Connection().ExecuteAsync(sql, product);
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.OK, "Product updated successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> DeleteProduct(int id)
    {
        try
        {
            var sql = @"delete from Products where ProductId = @id";
            var res = await _context.Connection().ExecuteAsync(sql, new { id });
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.OK, "Product deleted successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<List<Product>>> GetProductsWithStock(int stock)
    {
        try
        {
            var sql = @"select * from Products where Stock > @stock";
            var res = await _context.Connection().QueryAsync<Product>(sql, new { stock });
            return new Response<List<Product>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Product>> GetExpenisivProduct()
    {
        try
        {
            var sql = @"select * from Products
                        order by Price desc
                        limit 1";
            var res = await _context.Connection().QuerySingleOrDefaultAsync<Product>(sql);
            return new Response<Product>(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}