using System.Linq.Expressions;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Repository;

public class VillaRepository(ApplicationDbContext _db) : IVillaRepository
{
    public async Task<List<Villa>> GetVillas(Expression<Func<Villa, bool>> filter = null)
    {
        IQueryable<Villa> query = _db.Villas;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync();
    }

    public async Task<Villa> GetVillaById(Expression<Func<Villa, bool>>? filter = null)
    {
        IQueryable<Villa> query = _db.Villas;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task Create(Villa entity)
    {
        await _db.Villas.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Update(Villa entity)
    {
        _db.Villas.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Remove(Villa entity)
    {
        _db.Villas.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Save()
    {
        throw new NotImplementedException();
    }
}