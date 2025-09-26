using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Data.Contexts;
using Company.Route.DAL.Models;

namespace Company.Route.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        // ASK From CLR To Create Or Injuct Object From CompanyDbContext

        public DepartmentRepository(CompanyDbContext context) : base(context)
        {

        }

        #region Implemented By Class GenericRepository

        //public readonly CompanyDbContext _context;

        //ASK From CLR To Create Object From CompanyDbContext
        //public DepartmentRepository(CompanyDbContext context)
        //{
        //    _context = context;
        //}

        //public IEnumerable<Department> GetAll()
        //{
        //    return _context.Departments.ToList();
        //}

        //public Department? Get(int id)
        //{
        //    return _context.Departments.Find(id);
        //}
        //public int ADD(Department model)
        //{
        //    _context.Add(model);
        //    return _context.SaveChanges();
        //}

        //public int Update(Department model)
        //{
        //    _context.Update(model);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department model)
        //{
        //    _context.Remove(model);
        //    return _context.SaveChanges();
        //}

        #endregion


        public Department? GetByName(string Name)
        {
            throw new NotImplementedException();
        }

    }
}
