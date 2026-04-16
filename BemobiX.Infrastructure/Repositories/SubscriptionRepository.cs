// Local: BemobiX.Infrastructure/Repositories/SubscriptionRepository.cs
using Microsoft.EntityFrameworkCore;
using BemobiX.Domain.Entities;
using BemobiX.Domain.Interfaces;
using BemobiX.Infrastructure.Data;

namespace BemobiX.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _context;

    // Injetamos o DbContext que configuramos anteriormente
    public SubscriptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Subscription?> GetByIdAsync(Guid id)
    {
        return await _context.Subscriptions
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Subscription>> GetActiveByUserIdAsync(Guid userId)
    {
        return await _context.Subscriptions
            .Where(s => s.UserId == userId && s.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);
        await _context.SaveChangesAsync();
    }
}