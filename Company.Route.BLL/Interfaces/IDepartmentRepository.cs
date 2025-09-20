using Company.Route.DAL.Models;

namespace Company.Route.BLL.Interfaces
{
    internal interface IDepartmentRepository
    {

        IEnumerable<Department> GetAll();
        Department? GetId(int id);

        int ADD(Department model);
        int Update(Department model);
        int Delete(Department model);



    }
}
