using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ADO.NET_MULTILAYER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILoggingFactory _loggerFactory;
        public OrdersController(IOrderService orderService,ILoggingFactory loggerFactory)
        {
            _orderService = orderService;
            _loggerFactory = loggerFactory;
        }
        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderDto order)
        {
            #region serilog
            Log.Information("OrdersController:AddOrder API method execution started");
            Log.Information($"OrdersController:input parameter OrderName:{order.ordername}");
            Log.Information($"OrdersController:input parameter OrderLocation:{order.orderlocation}");
            #endregion

            #region Database log
            await _loggerFactory.AddLoggingMessages("venkat","information", "OrdersController:AddOrder API method execution started");
            await _loggerFactory.AddLoggingMessages("venkat","information", $"OrdersController:input parameter OrderName:{order.ordername}");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"OrdersController:input parameter OrderLocation:{order.orderlocation}");
            #endregion 


            var res = await _orderService.AddOrder(order);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                Log.Information("OrdersController:AddOrder API method execution ended");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "OrdersController:AddOrder API method execution ended");

                return StatusCode(StatusCodes.Status201Created, "created successfully");
                }

        }
        [HttpDelete]
        [Route("DeleteOrder/{orderid}")]
        public async Task<IActionResult> DeleteOrder(int orderid)
        {
            #region serilog
            Log.Information("OrdersController:DeleteOrder API method execution started");
            Log.Information($"OrdersController:input parameter OrderId:{orderid}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages("venkat", "information", "OrdersController:DeleteOrder API method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"OrdersController:input parameter OrderId:{orderid}");
            #endregion





            var res = await _orderService.DeleteOrder(orderid);
                if (res == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                Log.Information("OrdersController:DeleteOrder API method execution ended");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "OrdersController:DeleteOrder API method execution ended");
                return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
        }
        [HttpGet]
        [Route("GetOrderById/{orderid}")]
        public async Task<IActionResult> GetOrderById(int orderid)
        {
            #region serilog
            Log.Information("OrdersController:GetOrderById API method execution started");
            Log.Information($"OrdersController:input parameter OrderId:{orderid}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages("venkat", "information", "OrdersController:GetOrderById API method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"OrdersController:input parameter OrderId:{orderid}");
            #endregion



            var res = await _orderService.GetOrderById(orderid);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                Log.Information("OrdersController:GetOrderById API method execution ended");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "OrdersController:GetOrderById API method execution ended");

                return StatusCode(StatusCodes.Status200OK, res);
                }
        }
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            #region serilog
            Log.Information("OrdersController:GetOrders API method execution started");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages("venkat", "information", "OrdersController:GetOrders API method execution started");
            #endregion 


            var res = await _orderService.GetOrders();
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                Log.Information("OrdersController:GetOrders API method execution started");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "OrdersController:GetOrders API method execution started");
                return StatusCode(StatusCodes.Status200OK, res);
                }
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrderDto order)
        {
            #region serilog
            Log.Information("OrdersController:UpdateOrder API method execution started");
            Log.Information($"OrdersController:input parameter OrderId:{order.orderid}");
            Log.Information($"OrdersController:input parameter OrderName:{order.ordername}");
            Log.Information($"OrdersController:input parameter OrderLocation:{order.orderlocation}");
            #endregion

            #region Database log
            await _loggerFactory.AddLoggingMessages("venkat", "information", "OrdersController:UpdateOrder API method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"OrdersController:input parameter OrderId:{order.orderid}");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"OrdersController:input parameter OrderName:{order.ordername}");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"OrdersController:input parameter OrderLocation:{order.orderlocation}");
            #endregion 
            var res = await _orderService.UpdateOrder(order);
                if (res == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                Log.Information("OrdersController:UpdateOrder API method execution ended");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "OrdersController:UpdateOrder API method execution ended");
                return StatusCode(StatusCodes.Status200OK, "updated successfully");
                }
        }
    }
}