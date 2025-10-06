namespace Company.Route.PL.Services
{
    public class SingeltonServices : ISingletonServices
    {
        public SingeltonServices()
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
