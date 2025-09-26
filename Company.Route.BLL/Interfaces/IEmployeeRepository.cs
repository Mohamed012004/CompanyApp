using Company.Route.DAL;

namespace Company.Route.BLL.Interfaces
{
    internal interface IEmployeeRepository : IGenericRepository<Employee>
    {

        //public Employee? GetByName(string Name);

        #region Inherited From IGenericRepository
        //IEnumerable<Employee> GetAll();
        //Employee? Get(int id);

        //int ADD(Employee model);
        //int Update(Employee model);
        //int Delete(Employee model); 
        #endregion


    }
}
