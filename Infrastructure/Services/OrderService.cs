using System.Net;
using Dapper;
using Domein.Dtos;
using Domein.Interface;
using Domein.Models;
using Domein.Responses;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class OrderService(IContext _context) : IOrder
{
    public async Task<Response<List<Order>>> GetOrders()
    {
        try
        {
            var sql = @"select * from Orders";
            var res = await _context.Connection().QueryAsync<Order>(sql);
            return new Response<List<Order>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Order>> GetOrderById(int id)
    {
        try
        {
            var sql = @"select * from Orders where OrderId = @id";
            var res = await _context.Connection().QuerySingleOrDefaultAsync<Order>(sql, new { id });
            return new Response<Order>(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> AddOrder(OrderDto order)
    {
        try
        {
            var sql = @"insert into Orders (ProductId, Quantity, TotalPrice, OrderDate) ";
            var res = await _context.Connection().ExecuteAsync(sql, order);
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.Created, "Order added");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> UpdateOrder(Order order)
    {
        try
        {
            var sql = @"update Orders set ProductId=@ProductId, Quantity=@Quantity, TotalPrice=@TotalPrice, OrderDate=@OrderDate where OrderId = @OrderId";
            var res = await _context.Connection().ExecuteAsync(sql, order);
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.Created, "Order updated");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<bool>> DeleteOrder(int id)
    {
        try
        {
            var sql = @"delete from Orders where OrderId = @id";
            var res = await _context.Connection().ExecuteAsync(sql, new { id });
            return res == 0 
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.Created, "Order deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<List<Order>>> GetOrderByPeriob(DateTime startDate, DateTime endDate)
    {
        try
        {
            var sql = @"select * from Orders where OrderDate >= @startDate and OrderDate <= @endDate";
            var res = await _context.Connection().QueryAsync<Order>(sql, new { startDate, endDate });
            return new Response<List<Order>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<ProductWithTotalPriceOrdersDto>> GetProductWithTotalPriceOrders(int productId)
    {
        try
        {
            var sql = @"select * from Products where ProductId = @productId";
            var res = await _context.Connection().QuerySingleOrDefaultAsync<ProductWithTotalPriceOrdersDto>(sql, new { productId });
            
            var sqlOrders = @"select * from Orders where ProductId = @productId";
            var orders = await _context.Connection().QueryAsync<Order>(sqlOrders, new { productId });
            
            var  sqltotalPriceOrders = @"select sum(TotalPrice) from Orders where ProductId = @productId";
            var totalPriceOrders = await _context.Connection().QuerySingleOrDefaultAsync<decimal>(sqltotalPriceOrders, new { productId });
            
            res.TotalPriceOrders = totalPriceOrders;
            
            res.Orders = orders.ToList();

            return new Response<ProductWithTotalPriceOrdersDto>(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<List<ProductOrderCountsDto>>> GetTop3Products()
    {
        try
        {
            var sql = @"select p.ProductId as ProductId , p.ProductName as ProductName, p.Price as Price, p.Stock as Stock, count(o.orderId) as OrderCount 
                        from Products as p 
                        join Orders as o on p.ProductId = o.OrderId
                        group by p.ProductId , p.ProductName, p.Price, p.Stock
                        order by count(o.orderId) desc
                        limit 3";
            var res = await _context.Connection().QueryAsync<ProductOrderCountsDto>(sql);
            return new Response<List<ProductOrderCountsDto>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<List<OrederCountInDateDto>>> GetOrederCountInDate()
    {
        try
        {
            var sql = @"SELECT DATE(OrderDate) AS DateOrder, COUNT(*) AS CountOrder
                        FROM Orders
                        GROUP BY DATE(OrderDate)
                        ORDER BY  DateOrder";
            var res = await _context.Connection().QueryAsync<OrederCountInDateDto>(sql);
            return new Response<List<OrederCountInDateDto>>(res.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}