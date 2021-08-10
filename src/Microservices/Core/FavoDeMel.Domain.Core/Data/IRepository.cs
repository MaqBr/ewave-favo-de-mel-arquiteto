using System;
using FavoDeMel.Domain.Core.DomainObjects;

namespace FavoDeMel.Domain.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}