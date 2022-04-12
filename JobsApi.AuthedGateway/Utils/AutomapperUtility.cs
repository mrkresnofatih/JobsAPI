using AutoMapper;
using JobsApi.AuthedGateway.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JobsApi.AuthedGateway.Utils
{
    public static class AutomapperUtility
    {
        private static IMapper CreateIMapper()
        {
            var mapperProfile = new MapperConfiguration(mp => mp.AddProfile(new AutomapperProfile()));
            return mapperProfile.CreateMapper();
        }

        public static void AddAutomapperConfig(this IServiceCollection services)
        {
            services.AddSingleton(CreateIMapper());
        }
    }

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            // Player
            CreateMap<Player, PlayerGetDto>().ReverseMap();
        }
    }
}