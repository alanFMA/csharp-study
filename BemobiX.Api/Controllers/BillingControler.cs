using Microsoft.AspNetCore.Mvc;
using BemobiX.Domain.Interfaces;

namespace BemobiX.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillingController : ControllerBase
{
    private readonly ILegacyBillingService _legacyService;

    // O Framework injeta o adapter automaticamente aqui
    public BillingController(ILegacyBillingService legacyService)
    {
        _legacyService = legacyService;
    }

[HttpPost("process-legacy")]
        public async Task<IActionResult> ProcessLegacy([FromQuery] Guid userId, [FromQuery] decimal amount)
        {
            var result = await _legacyService.ProcessLegacyPayment(userId, amount);
            
            if (result)
                return Ok(new { Message = "Processamento iniciado no legado com sucesso." });
            
            return BadRequest("Falha ao comunicar com o sistema VB6.");
        }
    }