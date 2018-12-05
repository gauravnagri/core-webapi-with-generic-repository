using Microsoft.EntityFrameworkCore;
using StarterAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly TodoContext _context;
        public GenericRepository(TodoContext context)
        {
            _context = context;
        }

        public DbSet<T> Entities => _context.Set<T>();

        public async Task<IEnumerable<T>> GetEntities()
        {
            var entities =  await Entities.ToListAsync();
            return entities;
        }

        public async Task<T> GetEntity(int id)
        {
            var entity = await Entities.Where(x => x.Id == id).SingleOrDefaultAsync();
            return entity != null ? entity : null;
        }

        public async Task PostEntity(T todo)
        {
            await Entities.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateEntity(int id, T item)
        {
            var entity = await Entities.Where(x => x.Id == id).SingleOrDefaultAsync();
            if(entity == null)
            {
                return null;
            }

            var type = item.GetType();
            entity.GetType().GetProperty("Title").SetValue(entity, type.GetProperty("Title").GetValue(item));
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteEntity(int id)
        {
            var entity = await Entities.Where(x => x.Id == id).SingleOrDefaultAsync();
            if(entity == null)
            {
                return null;
            }
            Entities.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
