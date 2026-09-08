using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Interfaces;
using TitanFitness.Infrastructure.Persistence;

namespace TitanFitness.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Branches = new GenericRepository<Branch>(_context);
        Members = new GenericRepository<Member>(_context);
        Plans = new GenericRepository<Plan>(_context);
    }
    public IGenericRepository<Branch> Branches { get; }
    public IGenericRepository<T> Repository<T>()
        where T : class
    {
        return new GenericRepository<T>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    public IGenericRepository<Member> Members { get; }
    public IGenericRepository<Plan> Plans { get; }
}