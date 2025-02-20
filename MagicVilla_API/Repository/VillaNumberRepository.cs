using System.Linq.Expressions;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Repository;

public class VillaNumberRepository(ApplicationDbContext _db) : IVillaRepositoryNumber
{
    public async Task<List<VillaNumber>> GetVillas(Expression<Func<VillaNumber, bool>> filter = null)
    {
        IQueryable<VillaNumber> query = _db.VillaNumbers;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync();
    }

    public async Task<VillaNumber> GetVillaById(Expression<Func<VillaNumber, bool>>? filter = null)
    {
        IQueryable<VillaNumber> query = _db.VillaNumbers;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task Create(VillaNumber entity)
    {
        await _db.VillaNumbers.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Update(VillaNumber entity)
    {
        _db.VillaNumbers.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Remove(VillaNumber entity)
    {
        _db.VillaNumbers.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Save()
    {
        throw new NotImplementedException();
    }
}