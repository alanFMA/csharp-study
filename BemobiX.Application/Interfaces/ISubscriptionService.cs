// Local: BemobiX.Application/Interfaces/ISubscriptionService.cs

using BemobiX.Application.DTOs;
using BemobiX.Domain.Entities;

namespace BemobiX.Application.Interfaces;

public interface ISubscriptionService
{
    Task<Subscription> CreateSubscriptionAsync(CreateSubscriptionDto dto);
}