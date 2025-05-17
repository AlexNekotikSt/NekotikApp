using Proxy;

namespace FactoryMethod
{
    public class ConsoleUserContext : IUserContext
    {
        public bool HasAccess(string operation)
        {
            // Просте правило: дозвіл на читання для всіх, інші операції обмежені
            return operation == "read";
        }
    }
}