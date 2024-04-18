using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Motorcycle.App.Api.Features.v1.CheckRentalValue;
using Motorcycle.App.Api.Features.v1.CreateDriver;
using Motorcycle.App.Api.Features.v1.CreateMotorcycle;
using Motorcycle.App.Api.Features.v1.CreatePlan;
using Motorcycle.App.Api.Features.v1.CreateRental;
using Motorcycle.App.Api.Features.v1.EditVehiclePlate;
using Motorcycle.App.Api.Features.v1.ListPlanActive;
using Motorcycle.App.Api.Features.v1.ListVehicleAndFilterByPlate;
using Motorcycle.App.Api.Features.v1.UploadImageFile;

namespace Motorcycle.App.Api
{
    public static class Routes
    {

        public static WebApplication AddRoutes(this WebApplication app)
        {
            app.MapGroup("v1/vehicle").AddVehicleRoutes();
            app.MapGroup("v1/driver").AddDriverRoutes();
            app.MapGroup("v1/plan").AddPlanRoutes();
            app.MapGroup("v1/rent").AddRentRoutes();
            app.MapGroup("v1/file").AddFileRoutes();

            return app;
        }

        public static RouteGroupBuilder AddVehicleRoutes(this RouteGroupBuilder routeGroup)
        {
            routeGroup.MapPost("/", async ([FromServices] IMediator mediator, [FromBody] CreateMotorcycleCommand requestBody) =>
            {
                var response = await mediator.Send(requestBody);
                return response.IsValid ? Results.Created("/list", response) : Results.BadRequest(response);
            }).WithName("Vehicle");

            routeGroup.MapGet("/", async ([FromServices] IMediator mediator, [FromQuery] string? plate, [FromQuery] int page = 1, [FromQuery] int pagesize = 10) =>
            {
                var response = await mediator.Send(new ListVehicleAndFilterByPlateQuery { Plate = plate, Page = page, PageSize = pagesize });
                return response.IsValid ? Results.Ok(response) : Results.BadRequest(response);
            }).WithName("Vehicle list");

            routeGroup.MapPatch("/plate", async ([FromServices] IMediator mediator, [FromBody] EditVehiclePlateCommand command) =>
            {
                var response = await mediator.Send(command);
                return response.IsValid ? Results.NoContent() : Results.BadRequest(response);
            }).WithName("Vehicle edit plate");
            
            return routeGroup;
        }

        public static RouteGroupBuilder AddDriverRoutes(this RouteGroupBuilder routeGroup)
        {
            routeGroup.MapPost("/", async ([FromServices] IMediator mediator, [FromBody] CreateDriverCommand requestBody) =>
            {
                var response = await mediator.Send(requestBody);
                return response.IsValid ? Results.Created("/list", response) : Results.BadRequest(response);
            }).WithName("Driver");
            return routeGroup;
        }

        public static RouteGroupBuilder AddPlanRoutes(this RouteGroupBuilder routeGroup)
        {
            routeGroup.MapPost("/", async ([FromServices] IMediator mediator, [FromBody] CreatePlanCommand requestBody) =>
            {
                var response = await mediator.Send(requestBody);
                return response.IsValid ? Results.Created("/list", response) : Results.BadRequest(response);
            }).WithName("Plan");

            routeGroup.MapGet("/", async ([FromServices] IMediator mediator, [FromQuery] bool isActive = true, [FromQuery] int page = 1, [FromQuery] int pagesize = 10) =>
            {
                var response = await mediator.Send(new ListPlanActiveQuery { IsActive = isActive, Page = page, PageSize = pagesize });
                return response.IsValid ? Results.Ok(response) : Results.BadRequest(response);
            }).WithName("Plan list");

            return routeGroup;
        }

        public static RouteGroupBuilder AddRentRoutes(this RouteGroupBuilder routeGroup)
        {
            routeGroup.MapPost("/", async ([FromServices] IMediator mediator, [FromBody] CreateRentalCommand requestBody) =>
            {
                var response = await mediator.Send(requestBody);
                return response.IsValid ? Results.Created("/list", response) : Results.BadRequest(response);
            }).WithName("rental");

            routeGroup.MapGet("/", async ([FromServices] IMediator mediator, [FromQuery] DateOnly date, [FromQuery] Guid driverId) =>
            {
                var response = await mediator.Send(new CheckRentalValueQuery { EndDate = date, DriverId = driverId });
                return response.IsValid ? Results.Ok(response) : Results.BadRequest(response);
            }).WithName("rental checkValue");

            return routeGroup;
        }

        public static RouteGroupBuilder AddFileRoutes(this RouteGroupBuilder routeGroup)
        {

            // routeGroup.MapGet("antiforgery/token", (IAntiforgery forgeryService, HttpContext context) =>
            // {
            //     var tokens = forgeryService.GetAndStoreTokens(context);
            //     var xsrfToken = tokens.RequestToken!;
            //     return TypedResults.Content(xsrfToken, "text/plain");
            // })
                //.RequireAuthorization()
                ;

            routeGroup.MapPost("/", async ([FromServices] IMediator mediator,IFormFile file, [FromQuery] Guid driverId) =>
            {
                var response = await mediator.Send(new UploadImageFileCommand { DriverId = driverId, File = file });
                return response.IsValid ? Results.Created("", response) : Results.BadRequest(response);
            }).DisableAntiforgery().WithName("Upload file");

            return routeGroup;
        }
    }
}
