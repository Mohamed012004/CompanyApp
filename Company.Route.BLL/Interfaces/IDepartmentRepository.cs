using Company.Route.DAL.Models;

namespace Company.Route.BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        public Department? GetByName(string Name);


        #region Inherited From IGenericRepository
        //IEnumerable<Department> GetAll();
        //Department? Get(int id);

        //int ADD(Department model);
        //int Update(Department model);
        //int Delete(Department model); 
        #endregion




    }
}
