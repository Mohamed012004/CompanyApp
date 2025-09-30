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


        public IEnumerable<TEntity> GetAll()
        {
            if (typeof(TEntity) == typeof(Employee))
            {
                return (IEnumerable<TEntity>)_context.Employees.Include(E => E.Department).ToList();
            }

            return _context.Set<TEntity>().ToList();
        }
        public TEntity? Get(int id)
        {
            if (typeof(TEntity) == typeof(Employee))
            {
                return _context.Employees.Include(E => E.Department).FirstOrDefault(E => E.Id == id) as TEntity;
            }

            return _context.Set<TEntity>().Find(id);
        }


        public int ADD(TEntity model)
        {
            _context.Set<TEntity>().Add(model);
            return _context.SaveChanges();

        }


        public int Update(TEntity model)
        {
            _context.Set<TEntity>().Update(model);
            return _context.SaveChanges();
        }
        public int Delete(TEntity model)
        {
            _context.Set<TEntity>().Remove(model);
            return _context.SaveChanges();
        }
    }
}
