using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_MULTILAYER_API_Services.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeMap(IServiceCollection services)
        {
            //Initialize all AutoMapper profiles
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<AutoMapperProfile>();


            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

    }
}
