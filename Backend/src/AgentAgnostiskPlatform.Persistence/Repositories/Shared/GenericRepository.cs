using Microsoft.EntityFrameworkCore;
using AgentAgnostiskPlatform.Application.Contracts.Interfaces.Persistence;
using AgentAgnostiskPlatform.Domain.Entities;
using AgentAgnostiskPlatform.Persistence.DatabaseContext;

namespace AgentAgnostiskPlatform.Persistence.Repositories.Shared;

public class GenericRepository<T>(AppDatabaseContext context) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<IReadOnlyList<T>> GetAllAsync() => await context
        .Set<T>()
        .AsNoTracking()
        .ToListAsync();


    public async Task<T?> GetByIdAsync(int id) => await context
        .Set<T>()
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);


    public async Task<int> CreateAsync(T entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(T entity)
    {
        // context.Update(entity);
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
    }
}
