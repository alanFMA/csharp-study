namespace BemobiX.Domain.Entities;

public class Subscription
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string PlanName { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }

    // Construtor para garantir que a assinatura nasça em um estado válido
    public Subscription(Guid userId, string planName, decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Uma assinatura não pode ter valor zero ou negativo.");
            
        Id = Guid.NewGuid();
        UserId = userId;
        PlanName = planName;
        Price = price;
        IsActive = true;
    }
}