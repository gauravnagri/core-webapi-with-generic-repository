using StarterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterAPI.Repositories
{
    public interface IGenericRepository<T> where T:Entity
    {
        Task<IEnumerable<T>> GetEntities();
        Task<T> GetEntity(int id);
        Task PostEntity(T todo);
        Task<T> UpdateEntity(int id, T todo);
        Task<T> DeleteEntity(int id);
    }
}
