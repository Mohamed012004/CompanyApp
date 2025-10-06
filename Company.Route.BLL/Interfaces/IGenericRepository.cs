using Company.Route.DAL.Models;

namespace Company.Route.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T? Get(int id);

        void ADD(T model);
        void Update(T model);
        void Delete(T model);
    }
}
