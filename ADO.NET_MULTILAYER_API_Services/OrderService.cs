using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrdersRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            this._mapper = mapper;
        }
        public async Task<int> AddOrder(OrderDto order)
        {
            //Orders orders=new Orders();
            //orders.orderid = order.orderid;
            //orders.ordername = order.ordername;
            //orders.orderlocation = order.orderlocation;
            //var result= await _orderRepository.AddOrder(orders);
            //return result;
            #region AutoMapper code
            Orders ord=new Orders();
            _mapper.Map(order, ord);
            var result= await _orderRepository.AddOrder(ord);
            return result;
            #endregion
        }

        public async Task<bool> DeleteOrder(int orderid)
        {
            var result = await _orderRepository.DeleteOrder(orderid);
            return result;

        }

        public async Task<OrderDto> GetOrderById(int orderid)
        {
            //var res= await _orderRepository.GetOrderById(orderid);
            //OrderDto orderDto = new OrderDto();
            //orderDto.orderid = res.orderid;
            //orderDto.ordername = res.ordername;
            //orderDto.orderlocation = res.orderlocation;
            //return orderDto;
            #region AutoMapper code
            var res= await _orderRepository.GetOrderById(orderid);
            return _mapper.Map<OrderDto>(res);
            #endregion
        }

        public async Task<List<OrderDto>> GetOrders()
        {
            //var res = await _orderRepository.GetOrders();
            //List<OrderDto> orderDtos = new List<OrderDto>();
            //foreach (var item in res)
            //{
            //    OrderDto orderDto = new OrderDto();
            //    orderDto.orderid = item.orderid;
            //    orderDto.ordername = item.ordername;
            //    orderDto.orderlocation = item.orderlocation;
            //    orderDtos.Add(orderDto);
            //}
            //return orderDtos;
            #region AutoMapper code
            var res = await _orderRepository.GetOrders();
            return _mapper.Map<List<OrderDto>>(res);
            #endregion

        }

        public async Task<bool> UpdateOrder(OrderDto order)
        {
            //Orders ord = new Orders();
            //ord.orderid = order.orderid;
            //ord.ordername = order.ordername;
            //ord.orderlocation = order.orderlocation;
            //var result = await _orderRepository.UpdateOrder(ord);
            //return result;
            #region AutoMapper code
            Orders ord = new Orders();
            _mapper.Map(order, ord);
            var result = await _orderRepository.UpdateOrder(ord);
            return result;
            #endregion

        }
    }
}
