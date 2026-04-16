namespace BemobiX.Domain.Interfaces;

/// <summary>
/// Interface que isola a complexidade do sistema VB6.
/// Para o resto do sistema, não importa se os dados vêm de um mainframe ou VB6.
/// </summary>
public interface ILegacyBillingService
{
    Task<bool> ProcessLegacyPayment(Guid userId, decimal amount);
}