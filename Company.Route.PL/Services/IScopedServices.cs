namespace Company.Route.PL.Services
{
    public interface IScopedServices
    {
        public Guid Guid { get; set; }
        public string GetGguid();
    }
}
