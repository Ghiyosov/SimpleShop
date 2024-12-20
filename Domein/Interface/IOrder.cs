using Domein.Dtos;
using Domein.Models;
using Domein.Responses;

namespace Domein.Interface;

public interface IOrder
{
    public Task<Response<List<Order>>> GetOrders();
    public Task<Response<Order>> GetOrderById(int id);
    public Task<Response<bool>> AddOrder(OrderDto order);
    public Task<Response<bool>> UpdateOrder(Order order);
    public Task<Response<bool>> DeleteOrder(int id);
    public Task<Response<List<Order>>> GetOrderByPeriob(DateTime startDate, DateTime endDate);
    public Task<Response<ProductWithTotalPriceOrdersDto>> GetProductWithTotalPriceOrders(int productId);
    public Task<Response<List<ProductOrderCountsDto>>> GetTop3Products();
    public Task<Response<List<OrederCountInDateDto>>> GetOrederCountInDate();

}