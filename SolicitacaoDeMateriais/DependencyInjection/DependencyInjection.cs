using SolicitacaoDeMateriais.Infra.Data;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Infra.Repository;

namespace SolicitacaoDeMateriais.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection DependenciesInjection(this IServiceCollection services)
        {
            services.AddTransient<DataContext>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IRequestProductServiceRepository, RequestProductServiceRepository>();
            services.AddTransient<IStockEntryRepository, StockEntryRepository>();
            services.AddTransient<IStockExitRepository, StockExitRepository>();
            services.AddTransient<IStockRepository, StockRepository>();
            return services;
        }
    }
}
