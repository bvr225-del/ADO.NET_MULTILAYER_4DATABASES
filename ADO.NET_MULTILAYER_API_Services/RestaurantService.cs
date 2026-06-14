using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using ADO.NET_MULTILAYER_API_DbConnectivity.data;
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
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper,ILoggingFactory loggingFactory,IHttpContextAccessor httpContextAccessor)
        {
            _restaurantRepository = restaurantRepository;
            this._mapper = mapper;
            this._loggingFactory = loggingFactory;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddRestaurant(RestaurantDto Objres)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service:AddRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:AddRestaurant API method execution started");

            Restaurant res = new Restaurant();
            _mapper.Map(Objres, res);
            var result = await _restaurantRepository.AddRestaurant(res);
            Log.Information("Restaurant Service:AddRestaurant API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:AddRestaurant API method execution ended successfully");
            return result;

        }

        public async Task<bool> DeleteRestaurant(int Id)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service:DeleteRestaurant API method execution startedand Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:DeleteRestaurant API method execution started");

            var result = await _restaurantRepository.DeleteRestaurant(Id);
            Log.Information("Restaurant Service:DeleteRestaurant API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:DeleteRestaurant API method execution ended successfully");

            return result;
        }

        public async Task<List<RestaurantDto>> GetallRestaurants()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service:GetallRestaurants API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:GetallRestaurants API method execution started");

            var getrestaurants = await _restaurantRepository.GetallRestaurants();
            Log.Information("Restaurant Service:GetallRestaurants API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:GetallRestaurants API method execution ended successfully");

            return _mapper.Map<List<RestaurantDto>>(getrestaurants);

        }

        public async Task<RestaurantDto> GetRestaurantById(int Id)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service: GetRestaurantById API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service: GetRestaurantById API method execution started");

            var res = await _restaurantRepository.GetRestaurantById(Id);
            Log.Information("Restaurant Service: GetRestaurantById API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service: GetRestaurantById API method execution ended successfully");

            return _mapper.Map<RestaurantDto>(res);

        }

        public async Task<bool> UpdateRestaurant(RestaurantDto Objres)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service: UpdateRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service: UpdateRestaurant API method execution started");

            Restaurant res = new Restaurant();
            _mapper.Map(Objres, res);
            var result = await _restaurantRepository.UpdateRestaurant(res);
            Log.Information("Restaurant Service: UpdateRestaurant API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service: UpdateRestaurant API method execution ended successfully");

            return result;

        }

    }
}