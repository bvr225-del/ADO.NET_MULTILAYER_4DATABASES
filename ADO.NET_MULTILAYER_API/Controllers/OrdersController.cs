using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET_MULTILAYER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderDto order)
        {
            try
            {
                var res = await _orderService.AddOrder(order);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    return StatusCode(StatusCodes.Status201Created, "created successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }

        }
        [HttpDelete]
        [Route("DeleteOrder/{orderid}")]
        public async Task<IActionResult> DeleteOrder(int orderid)
        {
            try
            {
                var res = await _orderService.DeleteOrder(orderid);
                if (res == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetOrderById/{orderid}")]
        public async Task<IActionResult> GetOrderById(int orderid)
        {
            try
            {
                var res = await _orderService.GetOrderById(orderid);
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var res = await _orderService.GetOrders();
                if (res == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, res);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrderDto order)
        {
            try
            {
                var res = await _orderService.UpdateOrder(order);
                if (res == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "updated successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
    }
}