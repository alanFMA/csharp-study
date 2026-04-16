namespace BemobiX.Application.DTOs;

public record CreateSubscriptionDto(
    Guid UserId, 
    string PlanName, 
    decimal Price
);