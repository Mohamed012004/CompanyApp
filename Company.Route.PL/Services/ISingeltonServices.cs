namespace Company.Route.PL.Services
{
    public interface ISingletonServices
    {
        public Guid Guid { get; set; }
        public string GetGguid();

    }
}
