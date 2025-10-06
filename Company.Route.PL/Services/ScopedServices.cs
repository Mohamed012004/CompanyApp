
namespace Company.Route.PL.Services
{
    public class ScopedServices : IScopedServices
    {
        public ScopedServices()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string GetGguid()
        {
            return Guid.ToString();
        }
    }
}
