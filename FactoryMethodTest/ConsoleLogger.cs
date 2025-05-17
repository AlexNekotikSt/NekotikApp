using Proxy;

namespace FactoryMethod
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message) => Console.WriteLine($"[LOG] {message}");
    }
}