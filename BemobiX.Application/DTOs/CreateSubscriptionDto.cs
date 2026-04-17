// BemobiX.Application/DTOs/CreateSubscriptionDto.cs
using System.ComponentModel.DataAnnotations;

namespace BemobiX.Application.DTOs;

public record CreateSubscriptionDto(
    [Required(ErrorMessage = "O ID do usuário é obrigatório.")] 
    Guid UserId, 
    
    [Required(ErrorMessage = "O nome do plano é obrigatório.")]
    [MinLength(3, ErrorMessage = "O nome do plano deve ter pelo menos 3 caracteres.")]
    string PlanName, 
    
    // AQUI ESTÁ A BARREIRA: O preço deve estar entre 1 centavo e 10.000
    [Range(0.01, 10000.00, ErrorMessage = "O valor da assinatura deve ser maior que zero.")] 
    decimal Price
);