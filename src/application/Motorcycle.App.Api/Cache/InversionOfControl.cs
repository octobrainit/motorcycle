using motorcycle.shared.CreationalBase;

namespace Motorcycle.App.Api.Cache
{
    public static class InversionOfControl
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            var itemCacheContig = configuration.GetSection(nameof(CacheConfig)).Get<CacheConfig>() ??
                                  throw BaseError.Create(MessageType.InfraestructureValidation, "Cache config not founded, up failed");
            
            services.AddSingleton(itemCacheContig);
            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }
}
