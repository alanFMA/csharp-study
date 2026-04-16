using BemobiX.Application.DTOs;
using BemobiX.Application.Interfaces;
using BemobiX.Domain.Entities;
using BemobiX.Domain.Interfaces;

// É este namespace que o seu Program.cs estava procurando!
namespace BemobiX.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _repository;
    private readonly ILegacyBillingService _legacyBilling;

    // A Injeção de Dependência pede as interfaces (os contratos)
    public SubscriptionService(
        ISubscriptionRepository repository, 
        ILegacyBillingService legacyBilling)
    {
        _repository = repository;
        _legacyBilling = legacyBilling;
    }

    public async Task<Subscription> CreateSubscriptionAsync(CreateSubscriptionDto dto)
    {
        // 1. Regra de Negócio: Instancia a assinatura
        var subscription = new Subscription(dto.UserId, dto.PlanName, dto.Price);

        // 2. Orquestração: Aciona o sistema legado (VB6)
        bool paymentSuccess = await _legacyBilling.ProcessLegacyPayment(dto.UserId, dto.Price);

        if (!paymentSuccess)
        {
            // Lançamos um erro de negócio se o pagamento falhar
            throw new InvalidOperationException("Falha ao processar o pagamento no sistema legado da Bemobi.");
        }

        // 3. Persistência: Salva no banco PostgreSQL
        await _repository.AddAsync(subscription);

        // 4. Retorna a assinatura criada para o Controller
        return subscription;
    }
}