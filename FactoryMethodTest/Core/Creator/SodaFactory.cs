
using FactoryMethod.Product;

namespace FactoryMethod.Creator
{
    public abstract class SodaFactory
    {
        public abstract Soda RequestSoda();
    }

}
