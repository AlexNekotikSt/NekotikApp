using Domain.Product;

namespace FactoryMethod.Core.Creator
{
    public abstract class SodaFactory
    {
        public abstract Soda RequestSoda();
    }
}
