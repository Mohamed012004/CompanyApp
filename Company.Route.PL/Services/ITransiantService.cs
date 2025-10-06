namespace Company.Route.PL.Services
{
    public interface ITransiantService
    {
        public Guid Guid { get; set; }
        public string GetGguid();
    }

}
