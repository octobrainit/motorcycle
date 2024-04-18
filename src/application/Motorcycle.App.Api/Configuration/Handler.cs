using FluentValidation;
using MediatR;
using motorcycle.shared.CreationalBase;
using Newtonsoft.Json;

namespace motorcycle.app.api.Configuration
{
    public abstract class Handler<TInput, TOutput> : IRequestHandler<TInput, Response<TOutput>>
        where TInput : IRequest<Response<TOutput>>
        where TOutput : class
    {
        private readonly IValidator<TInput> _validator;
        private readonly ILogger<TInput> _logger;
        public Response<TOutput> Data { get; set; } = new();

        public Handler(
            ILogger<TInput> logger,
            IValidator<TInput> validator)
        {
            _validator = validator;
            _logger = logger;
        }

        public void LogInformation(string log) => _logger.LogInformation(log);

        public abstract Task ExecutionAsync(TInput input, CancellationToken cancellation);

        public void AddValidation(string message)
        {
            throw BaseError.Create(MessageType.ApplicationValidation, message);
        }

        public async Task<Response<TOutput>> Handle(TInput input, CancellationToken cancellation)
        {
            try
            {
                _logger.BeginScope(cancellation);
                _logger.LogInformation("started handler execution");

                var validation = _validator.Validate(input);

                if (!validation.IsValid)
                {
                    throw BaseError.Create(MessageType.ApiValidation, string.Join(',', validation.Errors));
                }

                await ExecutionAsync(input, cancellation);
                _logger.LogInformation("handler executed");
            }
            catch (BaseError er)
            {
                LogInformation("validation occured: " + JsonConvert.SerializeObject(er));
                Data.Error = BaseError.Create(MessageType.ApplicationValidation, er.MessageDescription);
                Data.StatusCode = 400;
            }
            catch (Exception ex)
            {
                LogInformation("Some unexpected error ocurred : " + JsonConvert.SerializeObject(ex));
                Data.Error = BaseError.Create(MessageType.InfraestructureValidation, ex.Message);
                Data.StatusCode = 500;
            }
            return Data;
        }
    }
}