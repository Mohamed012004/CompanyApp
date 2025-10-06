using Company.Route.BLL.Interfaces;
using Company.Route.BLL.Repositories;
using Company.Route.DAL.Data.Contexts;

namespace Company.Route.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; } // NULL

        public IEmployeeRepository EmployeeRepository { get; } // NULL
        public CompanyDbContext _Context { get; }

        public UnitOfWork(CompanyDbContext context)
        {

            _Context = context;
            EmployeeRepository = new EmployeeRepository(context);
            DepartmentRepository = new DepartmentRepository(context);
        }

        public int Compaated()
        {
            return _Context.SaveChanges();
        }
    }
}
