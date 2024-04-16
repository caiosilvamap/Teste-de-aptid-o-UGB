using AutoMapper;

namespace SolicitacaoDeMateriais.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });



            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);



            return services;
        }
    }
}

