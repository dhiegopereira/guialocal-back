using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace guialocal.Middlewares
{
    public class ValidateMiddleware<TModel>
    {
        private readonly RequestDelegate _next;
        private readonly IValidator<TModel> _validator;

        public ValidateMiddleware(RequestDelegate next, IValidator<TModel> validator)
        {
            _next = next;
            _validator = validator;
        }

        public Task Invoke(HttpContext httpContext)
        {
            // Aqui você pode usar _validator para validar o modelo TModel

            return _next(httpContext);
        }
    }

    public static class ValidateMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidateMiddleware<TModel>(this IApplicationBuilder builder, IValidator<TModel> validator)
        {
            return builder.UseMiddleware<ValidateMiddleware<TModel>>(validator);
        }
    }
}
