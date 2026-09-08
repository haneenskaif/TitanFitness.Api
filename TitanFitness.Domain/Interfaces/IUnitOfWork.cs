using TitanFitness.Domain.Entities;
namespace TitanFitness.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;
    IGenericRepository<Branch> Branches { get; }
    IGenericRepository<Member> Members { get; }
    IGenericRepository<Plan> Plans { get; }
    Task<int> SaveChangesAsync();
}
