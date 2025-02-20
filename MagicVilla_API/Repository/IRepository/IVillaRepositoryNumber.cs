using System.Linq.Expressions;
using MagicVilla_API.Models;

namespace MagicVilla_API.Repository.IRepository;

public interface IVillaRepositoryNumber
{
    Task<List<VillaNumber>> GetVillas(Expression<Func<VillaNumber, bool>> filter = null);
    Task<VillaNumber> GetVillaById(Expression<Func<VillaNumber, bool>> filter = null);
    Task Create(VillaNumber entity);
    Task Update(VillaNumber entity);
    Task Remove(VillaNumber entity);
    Task Save();
}