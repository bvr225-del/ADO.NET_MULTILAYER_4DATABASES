using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
namespace ADO.NET_MULTILAYER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ILoggingFactory _loggerFactory;
        public RestaurantController(IRestaurantService restaurantService,ILoggingFactory loggerFactory)
        {
            _restaurantService = restaurantService;
            _loggerFactory = loggerFactory;
        }
        [HttpPost]
        [Route("AddRestaurant")]
        public async Task<IActionResult> Post([FromBody] RestaurantDto Objres)
        {//dtos are used to transafer the data purpose used.
            #region serilog
            Log.Information("RestaurantController:AddRestaurant API method execution started");
            Log.Information($"RestaurantController:called input parameter Restaurant Name:{Objres.RestaurantName}");
            Log.Information($"RestaurantController:called input parameter Restaurant Location:{Objres.RestaurantLocation}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages("venkat","information", "RestaurantController:AddRestaurant API method execution started");
            await _loggerFactory.AddLoggingMessages("venkat","information", $"RestaurantController:called input parameter Restaurant Name:{Objres.RestaurantName}");
            await _loggerFactory.AddLoggingMessages("venkat","information", $"RestaurantController:called input parameter Restaurant Location:{Objres.RestaurantLocation}");
            #endregion

            if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var resdata = await _restaurantService.AddRestaurant(Objres);
                Log.Information("RestaurantController:AddRestaurant API method execution ended successfully");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:AddRestaurant API method execution ended successfully");
                return StatusCode(StatusCodes.Status201Created, resdata);
                }
        }
        [HttpDelete]
        [Route("DeleteRestaurantById/{Id}")]
        public async Task<IActionResult> delete(int Id)
        {
            #region serilog
            Log.Information("RestaurantController:DeleteRestaurantById API method execution started");
            Log.Information($"RestaurantController:called input parameter Restaurant ID:{Id}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:DeleteRestaurantById API method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"RestaurantController:called input parameter Restaurant ID:{Id}");
            #endregion

            if (Id < 0)
            {//If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
                var resdata = await _restaurantService.DeleteRestaurant(Id);
                if (resdata == false)
                {//in db if you get empty data we need to retrun this statuscode:Status404NotFound
                    return StatusCode(StatusCodes.Status404NotFound, "data not  found");
                }
                else
                {
                Log.Information("RestaurantController:DeleteRestaurantById API method execution ended successfully");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:DeleteRestaurantById API method execution ended successfully");
                return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
        }
        [HttpGet]
        [Route("GetallRestaurants")]
        public async Task<IActionResult> GetallRestaurants()
        {
            #region serilog
            Log.Information("RestaurantController:GetRestaurants API method execution started");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:GetRestaurants API method execution started");
            #endregion

            var resdata = await _restaurantService.GetallRestaurants();
                if (resdata == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                Log.Information("RestaurantController:GetRestaurants API method execution ended successfully");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:GetRestaurants API method execution ended successfully");
                return StatusCode(StatusCodes.Status200OK, resdata);
                }

        }
        [HttpGet]
        [Route("GetRestaurantById/{Id}")]
        public async Task<IActionResult> GetRestaurantById(int Id)
        {
            #region serilog
            Log.Information("RestaurantController:GetRestaurantById API method execution started");
            Log.Information($"RestaurantController:called input parameter Restaurant ID:{Id}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:GetRestaurantById API method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"RestaurantController:called input parameter Restaurant ID:{Id}");
            #endregion

            if (Id < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
                var resdata = await _restaurantService.GetRestaurantById(Id);
            Log.Information("RestaurantController:GetRestaurantById API method execution ended successfully");
            await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:GetRestaurantById API method execution ended successfully");
            return StatusCode(StatusCodes.Status200OK, resdata);
        }
        [HttpPut]
        [Route("UpdateRestaurant")]
        public async Task<IActionResult> put([FromBody] RestaurantDto Objres)
        {
            #region serilog
            Log.Information("RestaurantController:UpdateRestaurant API method execution started");
            Log.Information($"RestaurantController:called input parameter Restaurant ID:{Objres.Id}");
            Log.Information($"RestaurantController:called input parameter Restaurant Name:{Objres.RestaurantName}");
            Log.Information($"RestaurantController:called input parameter Restaurant Location:{Objres.RestaurantLocation}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:UpdateRestaurant API method execution started");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"RestaurantController:called input parameter Restaurant ID:{Objres.Id}");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"RestaurantController:called input parameter Restaurant Name:{Objres.RestaurantName}");
            await _loggerFactory.AddLoggingMessages("venkat", "information", $"RestaurantController:called input parameter Restaurant Location:{Objres.RestaurantLocation}");
            #endregion
            var resdata = await _restaurantService.UpdateRestaurant(Objres);

                if (resdata==false)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "DATA NOT FOUND");
                }
                else
                {
                Log.Information("RestaurantController:UpdateRestaurant API method execution ended successfully");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:UpdateRestaurant API method execution ended successfully");
                return StatusCode(StatusCodes.Status200OK, resdata);
                }
        }
    }
}
