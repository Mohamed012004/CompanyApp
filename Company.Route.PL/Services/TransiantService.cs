namespace Company.Route.PL.Services
{
    public class TransiantService : ITransiantService
    {
        public TransiantService()
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
