using AutoMapper;
using JobsApi.JobsCore.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JobsApi.JobsCore.Utils
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
            // Jobs
            CreateMap<Job, JobCreateDto>().ReverseMap();
        }
    }
}