using BemobiX.Domain.Entities;

namespace BemobiX.Domain.Interfaces;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(Guid id);
    Task<IEnumerable<Subscription>> GetActiveByUserIdAsync(Guid userId);
    Task AddAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
}