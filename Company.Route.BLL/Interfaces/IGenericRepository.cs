using Company.Route.DAL.Models;

namespace Company.Route.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T? Get(int id);

        int ADD(T model);
        int Update(T model);
        int Delete(T model);
    }
}
