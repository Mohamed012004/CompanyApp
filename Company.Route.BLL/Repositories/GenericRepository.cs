using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Data.Contexts;
using Company.Route.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Route.BLL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CompanyDbContext _context;

        // ASK From CLR To Create Or Injuct Object From CompanyDbContext
        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Employee))
            {
                return (IEnumerable<TEntity>)await _context.Employees.Include(E => E.Department).ToListAsync();
            }

            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity?> GetAsync(int id)
        {
            if (typeof(TEntity) == typeof(Employee))
            {
                return await _context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.Id == id) as TEntity;
            }

            return await _context.Set<TEntity>().FindAsync(id);
        }


        public async Task AddAsync(TEntity model)
        {
            await _context.Set<TEntity>().AddAsync(model);
            //return _context.SaveChanges();

        }


        public void Update(TEntity model)
        {
            _context.Set<TEntity>().Update(model);
            //return _context.SaveChanges();
        }
        public void Delete(TEntity model)
        {
            _context.Set<TEntity>().Remove(model);
            //return _context.SaveChanges();
        }
    }
}
