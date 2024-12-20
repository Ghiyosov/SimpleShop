using Domein.Dtos;
using Domein.Interface;
using Domein.Models;
using Domein.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController(IOrder _order) : ControllerBase
{
    [HttpGet("GetOrders")]
    public async Task<Response<List<Order>>> GetOrders()=> await _order.GetOrders();

    [HttpGet("GetOrders/{id}")]
    public async Task<Response<Order>> GetOrder(int id) => await _order.GetOrderById(id);
    
    [HttpPost("AddOrder")]
    public async Task<Response<bool>> AddOrder(OrderDto order) => await _order.AddOrder(order);
    
    [HttpPut("UpdateOrder")]
    public async Task<Response<bool>> UpdateOrder(Order order) => await _order.UpdateOrder(order);
    
    [HttpDelete("DeleteOrder")]
    public async Task<Response<bool>> DeleteOrder(int id) => await _order.DeleteOrder(id);
    
    [HttpGet("GetOrdersByPeriod")]
    public async Task<Response<List<Order>>> GetOrdersByPeriod(DateTime startDate, DateTime endDate)=> await _order.GetOrderByPeriob(startDate, endDate);
    
    [HttpGet("GetProductWithTotalPriceOrders")]
    public async Task<Response<ProductWithTotalPriceOrdersDto>> GetProductWithTotalPriceOrders(int productId)=> await _order.GetProductWithTotalPriceOrders(productId);
    
    [HttpGet("GetTop3Products")]
    public async Task<Response<List<ProductOrderCountsDto>>> GetTop3Products()=> await _order.GetTop3Products();
    
    [HttpGet("GetOrederCountInDate")]
    public async Task<Response<List<OrederCountInDateDto>>> GetOrederCountInDate()=> await _order.GetOrederCountInDate();
}