using motorcycle.shared.CreationalBase;

namespace Motorcycle.App.Api.Blob
{
    public static class InversionOfControl
    {
        public static IServiceCollection AddBlob(this IServiceCollection services, IConfiguration configuration)
        {
            var itemCacheContig = configuration.GetSection(nameof(BlobConfig)).Get<BlobConfig>() ??
                                  throw BaseError.Create(MessageType.InfraestructureValidation, "Blob config not founded, up failed");

            services.AddSingleton(itemCacheContig);
            services.AddScoped<IBlobService, BlobFileMessageService>();

            return services;
        }
    }
}
