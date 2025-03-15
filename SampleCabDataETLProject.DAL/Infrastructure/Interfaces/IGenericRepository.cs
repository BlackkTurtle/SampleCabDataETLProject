using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.Infrastructure.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
}