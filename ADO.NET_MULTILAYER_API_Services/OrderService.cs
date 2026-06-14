using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Serilog;
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
        private readonly ILoggingFactory _loggerFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IOrdersRepository orderRepository, IMapper mapper,ILoggingFactory loggerFactory,IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            this._mapper = mapper;
            _loggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddOrder(OrderDto order)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrdersService:AddOrder api method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName,"information", "OrdersService:AddOrder api method execution started");
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

            Log.Information("OrdersService:AddOrder api method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService:AddOrder api method execution completed");

            return result;
            #endregion
        }

        public async Task<bool> DeleteOrder(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrderService: DeleteOrder  method execution starts and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService:DeleteOrder api method execution started");


            var result = await _orderRepository.DeleteOrder(orderid);
            Log.Information("OrderService: DeleteOrder  method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService:DeleteOrder api method execution completed");

            return result;

        }

        public async Task<OrderDto> GetOrderById(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrderService: GetOrderById Order  method execution starts and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: GetOrderById api method execution started");

            //var res= await _orderRepository.GetOrderById(orderid);
            //OrderDto orderDto = new OrderDto();
            //orderDto.orderid = res.orderid;
            //orderDto.ordername = res.ordername;
            //orderDto.orderlocation = res.orderlocation;
            //return orderDto;
            #region AutoMapper code
            var res = await _orderRepository.GetOrderById(orderid);
            Log.Information("OrderService: GetOrderById Order  method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: GetOrderById api method execution completed");
            return _mapper.Map<OrderDto>(res);
            #endregion
        }

        public async Task<List<OrderDto>> GetOrders()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrderService: GetOrders Order  method execution starts and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: GetOrders api method execution started");
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
            Log.Information("OrderService: GetOrders Order  method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: GetOrders api method execution completed");

            return _mapper.Map<List<OrderDto>>(res);
            #endregion

        }

        public async Task<bool> UpdateOrder(OrderDto order)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrderService: UpdateOrder Order  method execution starts and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: UpdateOrder api method execution started");

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
            Log.Information("OrderService: UpdateOrder Order  method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: UpdateOrder api method execution completed");

            return result;
            #endregion

        }
    }
}
