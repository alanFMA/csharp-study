using System.Net;
using System.Text.Json;

namespace BemobiX.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    // O _next representa o "próximo passo" na fila da requisição
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Tenta processar a requisição normalmente (ir para o Controller)
            await _next(context);
        }
        catch (Exception ex)
        {
            // Se qualquer Controller lançar um erro que não foi tratado, cai aqui!
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Regra Sênior: Mapeamos Tipos de Exceção para Status HTTP
        context.Response.StatusCode = exception switch
        {
            InvalidOperationException => (int)HttpStatusCode.BadRequest, // 400
            KeyNotFoundException => (int)HttpStatusCode.NotFound,        // 404
            _ => (int)HttpStatusCode.InternalServerError                 // 500
        };

        // Criamos um formato padrão para todos os erros da API
        var result = JsonSerializer.Serialize(new
        {
            error = exception.Message,
            statusCode = context.Response.StatusCode
        });

        return context.Response.WriteAsync(result);
    }
}