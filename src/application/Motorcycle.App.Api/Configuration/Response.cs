using motorcycle.shared.CreationalBase;

namespace motorcycle.app.api.Configuration
{
    public class Response<T> where T : class
    {
        public T? Data { get; set; }
        public bool IsValid => Error is null;
        public int StatusCode { get; set; }
        public BaseError? Error { get; set; }
        
        public void SetReponseStatusCode(int statusCode) => StatusCode = statusCode;

        public static Response<T> CreateResponse(T? input)
        {
            return new Response<T>
            {
                Data = input
            };
        }

        public static Response<T> CreateResponseWithError(BaseError baseError) => new Response<T>
        {
            Error = baseError,
            StatusCode = 400
        };
    }
}
