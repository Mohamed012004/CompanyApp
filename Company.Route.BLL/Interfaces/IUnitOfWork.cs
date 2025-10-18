namespace Company.Route.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }

        Task<int> ComplatedAsync();


    }

}
