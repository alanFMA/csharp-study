// Local: BemobiX.Infrastructure/ExternalServices/Vb6BillingAdapter.cs

using BemobiX.Domain.Interfaces;

namespace BemobiX.Infrastructure.ExternalServices;

public class Vb6BillingAdapter : ILegacyBillingService
{
    public async Task<bool> ProcessLegacyPayment(Guid userId, decimal amount)
    {
        // Aqui simulamos a "sujeira" do legado. 
        // Talvez o VB6 exija um arquivo texto, uma procedure antiga ou um Socket.
        
        Console.WriteLine($"[LEGADO VB6] Convertendo Guid {userId} para formato legado...");
        
        // Simulação de lógica legada (ex: o VB6 só aceita IDs de 8 caracteres)
        string legacyId = userId.ToString().Substring(0, 8);
        
        // Lógica de comunicação fictícia com o legado
        await Task.Delay(100); // Simulando latência de sistema antigo
        
        return true; 
    }
}