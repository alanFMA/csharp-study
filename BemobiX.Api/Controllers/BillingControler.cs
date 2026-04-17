using Microsoft.AspNetCore.Mvc;
using BemobiX.Application.Interfaces;
using BemobiX.Application.DTOs;
using BemobiX.Domain.Entities;

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

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Subscription))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("subscriptions")]
    public async Task<IActionResult> Create([FromBody] CreateSubscriptionDto dto)
    {
        // 1. O Controller só faz a chamada
        var result = await _subscriptionService.CreateSubscriptionAsync(dto);

        // 2. E retorna o sucesso.
        // Se der erro de validação ou de banco, o Middleware cuidará disso!
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}