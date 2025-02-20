using System.Linq.Expressions;
using MagicVilla_API.Models;

namespace MagicVilla_API.Repository.IRepository;

public interface IVillaRepository
{
    Task<List<Villa>> GetVillas(Expression<Func<Villa, bool>> filter = null);
    Task<Villa> GetVillaById(Expression<Func<Villa, bool>> filter = null);
    Task Create(Villa entity);
    Task Update(Villa entity);
    Task Remove(Villa entity);
    Task Save();
}