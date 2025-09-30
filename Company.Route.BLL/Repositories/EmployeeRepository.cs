using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Data.Contexts;
using Company.Route.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Route.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private CompanyDbContext _context { get; set; }
        // ASK From CLR To Create Or Injuct Object From CompanyDbContext
        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Employee> GetByName(string Name)
        {
            return _context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(Name.ToLower())).ToList();
        }


        #region Implemented By Class GenericRepository

        // Ask From CLR To Create Object From Company DBContext
        //    private readonly CompanyDbContext _context;
        //    public EmployeeRepository(CompanyDbContext context)
        //    {
        //        _context = context;
        //    }

        //    public IEnumerable<Employee> GetAll()
        //    {
        //        return _context.Employees.ToList();
        //    }

        //    public Employee? Get(int id)
        //    {
        //        return _context.Employees.Find(id);
        //    }


        //    public int ADD(Employee model)
        //    {
        //        var Employee = _context.Employees.Add(model);
        //        var State = _context.SaveChanges();

        //        return State;
        //    }

        //    public int Update(Employee model)
        //    {
        //        var Employee = _context.Employees.Update(model);
        //        var State = _context.SaveChanges();

        //        return State;
        //    }

        //    public int Delete(Employee model)
        //    {
        //        var Employee = _context.Employees.Remove(model);
        //        return _context.SaveChanges();


        //    } 

        #endregion


    }
}
