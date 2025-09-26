using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Data.Contexts;
using Company.Route.DAL.Models;

namespace Company.Route.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        // ASK From CLR To Create Or Injuct Object From CompanyDbContext
        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }


        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T? Get(int id)
        {
            return _context.Set<T>().Find(id);
        }


        public int ADD(T model)
        {
            _context.Set<T>().Add(model);
            return _context.SaveChanges();

        }


        public int Update(T model)
        {
            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }
        public int Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }
    }
}
