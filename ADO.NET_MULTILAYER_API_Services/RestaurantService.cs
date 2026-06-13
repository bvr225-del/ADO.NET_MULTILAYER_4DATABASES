using ADO.NET_MULTILAYER_API_BusinessEntities.Dtos;
using ADO.NET_MULTILAYER_API_BusinessEntities.Interfaces;
using ADO.NET_MULTILAYER_API_BusinessEntities.Models;
using ADO.NET_MULTILAYER_API_DbConnectivity.data;
using AutoMapper;
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
        public readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly ILoggingFactory _loggingFactory;
        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper,ILoggingFactory loggingFactory)
        {
            _restaurantRepository = restaurantRepository;
            this._mapper = mapper;
            this._loggingFactory = loggingFactory;
        }

        public async Task<int> AddRestaurant(RestaurantDto Objres)
        {
            Log.Information("Restaurant Service:AddRestaurant API method execution started");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service:AddRestaurant API method execution started");

            Restaurant res = new Restaurant();
            _mapper.Map(Objres, res);
            var result = await _restaurantRepository.AddRestaurant(res);
            Log.Information("Restaurant Service:AddRestaurant API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service:AddRestaurant API method execution ended successfully");
            return result;

        }

        public async Task<bool> DeleteRestaurant(int Id)
        {
            Log.Information("Restaurant Service:DeleteRestaurant API method execution started");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service:DeleteRestaurant API method execution started");

            var result = await _restaurantRepository.DeleteRestaurant(Id);
            Log.Information("Restaurant Service:DeleteRestaurant API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service:DeleteRestaurant API method execution ended successfully");

            return result;
        }

        public async Task<List<RestaurantDto>> GetallRestaurants()
        {
            Log.Information("Restaurant Service:GetallRestaurants API method execution started");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service:GetallRestaurants API method execution started");

            var getrestaurants = await _restaurantRepository.GetallRestaurants();
            Log.Information("Restaurant Service:GetallRestaurants API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service:GetallRestaurants API method execution ended successfully");

            return _mapper.Map<List<RestaurantDto>>(getrestaurants);

        }

        public async Task<RestaurantDto> GetRestaurantById(int Id)
        {
            Log.Information("Restaurant Service: GetRestaurantById API method execution started");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service: GetRestaurantById API method execution started");

            var res = await _restaurantRepository.GetRestaurantById(Id);
            Log.Information("Restaurant Service: GetRestaurantById API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service: GetRestaurantById API method execution ended successfully");

            return _mapper.Map<RestaurantDto>(res);

        }

        public async Task<bool> UpdateRestaurant(RestaurantDto Objres)
        {
            Log.Information("Restaurant Service: UpdateRestaurant API method execution started");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service: UpdateRestaurant API method execution started");

            Restaurant res = new Restaurant();
            _mapper.Map(Objres, res);
            var result = await _restaurantRepository.UpdateRestaurant(res);
            Log.Information("Restaurant Service: UpdateRestaurant API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages("venkat", "information", "Restaurant Service: UpdateRestaurant API method execution ended successfully");

            return result;

        }

    }
}