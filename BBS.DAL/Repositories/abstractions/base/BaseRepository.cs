using BBS.DAL.Database;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, ISoftDeletable
    {
        protected BankDbContext _context;
        private DbSet<T> _dbset;

        public BaseRepository(BankDbContext ctx)

        {
            _context = ctx;
            _dbset = _context.Set<T>();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await NotDeletedRowsOnly().Where(predicate).AsNoTracking().ToListAsync();
        }

        public async  Task<List<T>> GetAllAsync()
        {
            return await NotDeletedRowsOnly().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task InsertAsync(T obj)
        {
          await _dbset.AddAsync(obj);
          await SaveAsync();
        }

        public async Task UpdateAsync(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await SaveAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var result = await GetByIdAsync(id);
            result.IsDeleted = true;
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }

        private IQueryable<T> NotDeletedRowsOnly()
        {
            return _dbset.Where(t => t.IsDeleted == false).AsQueryable();
        }
    }
}
