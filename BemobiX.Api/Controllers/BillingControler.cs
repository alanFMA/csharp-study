using Microsoft.AspNetCore.Mvc;
using BemobiX.Application.Interfaces;
using BemobiX.Application.DTOs;

namespace BemobiX.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillingController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    // Injetamos apenas o Serviço da Aplicação.
    // O Controller não precisa saber que existe um banco ou um sistema VB6.
    public BillingController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost("subscriptions")]
    public async Task<IActionResult> Create([FromBody] CreateSubscriptionDto dto)
    {
        try 
        {
            // Chamamos o Caso de Uso na camada de Application
            var result = await _subscriptionService.CreateSubscriptionAsync(dto);

            // Retornamos 201 Created com o objeto criado
            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            // Se o sistema legado falhar, capturamos a exceção de negócio
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            // Erros inesperados (ex: banco fora do ar) retornam 500
            return StatusCode(500, "Ocorreu um erro interno ao processar a assinatura.");
        }
    }
}