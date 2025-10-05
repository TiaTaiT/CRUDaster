using CRUDaster.Core.Application.Interfaces.DtoServices;
using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Application.Services;
using CRUDaster.Infrastructure.Data;
using CRUDaster.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CRUDaster.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            // Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Register repositories
            //services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IComponentRepository, ComponentRepository>();
            services.AddScoped<IDraftRepository, DraftRepository>();
            services.AddScoped<IHardwareRepository, HardwareRepository>();
            services.AddScoped<IFunctionalityRepository, FunctionalityRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IPimRepository, PimRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IProtocolRepository, ProtocolRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();

            services.AddScoped<IComponentService, ComponentService>();
            services.AddScoped<IHardwareService, HardwareService>();
            services.AddScoped<IFunctionalityService, FunctionalityService>();
            services.AddScoped<IPimService, PimService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IProtocolService, ProtocolService>();
            services.AddScoped<IModelService, ModelService>();

            return services;
        }
    }
}
