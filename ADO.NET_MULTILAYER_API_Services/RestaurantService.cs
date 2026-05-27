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
    public class RestaurantService : IRestaurantService
    {
        public readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            this._mapper = mapper;
        }

        public async Task<int> AddRestaurant(RestaurantDto Objres)
        {
            Restaurant res = new Restaurant();
            _mapper.Map(Objres, res);
            var result = await _restaurantRepository.AddRestaurant(res);
            return result;

        }

        public async Task<bool> DeleteRestaurant(int Id)
        {
            var result = await _restaurantRepository.DeleteRestaurant(Id);
            return result;
        }

        public async Task<List<RestaurantDto>> GetallRestaurants()
        {
            var getrestaurants = await _restaurantRepository.GetallRestaurants();
            return _mapper.Map<List<RestaurantDto>>(getrestaurants);

        }

        public async Task<RestaurantDto> GetRestaurantById(int Id)
        {
            var res = await _restaurantRepository.GetRestaurantById(Id);
            return _mapper.Map<RestaurantDto>(res);

        }

        public async Task<bool> UpdateRestaurant(RestaurantDto Objres)
        {
            Restaurant res = new Restaurant();
            _mapper.Map(Objres, res);
            var result = await _restaurantRepository.UpdateRestaurant(res);
            return result;

        }

    }
}