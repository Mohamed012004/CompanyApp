using Company.Route.DAL.Models;

namespace Company.Route.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {

        public List<Employee> GetByName(string Name);

        #region Inherited From IGenericRepository
        //IEnumerable<Employee> GetAll();
        //Employee? Get(int id);

        //int ADD(Employee model);
        //int Update(Employee model);
        //int Delete(Employee model); 
        #endregion


    }
}
