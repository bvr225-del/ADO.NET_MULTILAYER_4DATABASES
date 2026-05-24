using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetOrders();
        Task<OrderDto> GetOrderById(int orderid);
        Task<int> AddOrder(OrderDto order);
        Task<bool> UpdateOrder(OrderDto order);
        Task<bool> DeleteOrder(int orderid);
    }
}
