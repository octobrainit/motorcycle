using motorcycle.shared.CreationalBase;
using Motorcycle.App.Api.Features.v1.CheckRentalValue;
using Motorcycle.App.Api.Features.v1.CreateDriver;
using Motorcycle.App.Api.Features.v1.CreateMotorcycle;
using Motorcycle.App.Api.Features.v1.CreatePlan;
using Motorcycle.App.Api.Features.v1.CreateRental;
using Motorcycle.App.Api.Features.v1.EditVehiclePlate;
using Motorcycle.App.Api.Features.v1.ListPlanActive;
using Motorcycle.App.Api.Features.v1.ListVehicleAndFilterByPlate;
using Motorcycle.App.Api.Features.v1.UploadImageFile;

namespace Motorcycle.App.Api.Context
{
    public static class InversionOfControl
    {
        public static IServiceCollection AddDbConfiuration(this IServiceCollection services, IConfiguration configuration)
        {
            var itemDbContig = configuration.GetSection(nameof(DbConnection)).Get<DbConnection>() ?? 
                                throw BaseError.Create(MessageType.InfraestructureValidation, "DB config not founded, up failed");
            
            services.AddSingleton(itemDbContig);
            services.AddDbContext<Database>();
            services.AddScoped<ICreateMotorcycleRepository, CreateMotorcycleRepository>();
            services.AddScoped<IListVehicleAndFilterByPlateRepository, ListVehicleAndFilterByPlateRepository>();
            services.AddScoped<IEditVehiclePlateRepository, EditVehiclePlateRepository>();
            services.AddScoped<ICreateDriverRepository, CreateDriverRepository>();
            services.AddScoped<ICreatePlanRepository, CreatePlanRepository>();
            services.AddScoped<IListPlanActiveRepository, ListPlanActiveRepository>();
            services.AddScoped<ICreateRentalRepository, CreateRentalRepository>();
            services.AddScoped<ICheckRentalValueRepository, CheckRentalValueRepository>();
            services.AddScoped<IUploadImageFileRepository, UploadImageFileRepository>();

            return services;
        }
    }
}
