using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Data.Contexts;
using Company.Route.DAL.Models;

namespace Company.Route.BLL.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        public readonly CompanyDbContext _context;
        public DepartmentRepository()
        {
            _context = new CompanyDbContext();
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department? GetId(int id)
        {
            return _context.Departments.Find(id);
        }
        public int ADD(Department model)
        {
            _context.Add(model);
            return _context.SaveChanges();
        }

        public int Update(Department model)
        {
            _context.Update(model);
            return _context.SaveChanges();
        }

        public int Delete(Department model)
        {
            _context.Remove(model);
            return _context.SaveChanges();
        }



    }
}
