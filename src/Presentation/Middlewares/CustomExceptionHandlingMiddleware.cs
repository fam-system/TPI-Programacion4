﻿
using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace Presentation.Middlewares
{
    public class CustomExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<CustomExceptionHandlingMiddleware> _logger;

        public CustomExceptionHandlingMiddleware(ILogger<CustomExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context); // Continúa llamando al método del próximo middleware en el pipeline
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                switch (ex)
                {
                    // Seteamos el codigo de estado y log correcto en cada caso
                    case NotFoundException notFoundEx:
                        _logger.LogWarning(notFoundEx, "Not Found Exception");
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    case ValidationException validationEx:
                        _logger.LogWarning(validationEx, "Validation Exception");
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    default:
                        _logger.LogError(ex, "Undhandled Exception");
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }

                // agregamos el msj de error que lanzamos con la excepción y escribimos la respuesta
                var response = new { error = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
