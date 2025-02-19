using Domain.Product;
using FactoryMethod.Core.Creator;

namespace FactoryMethod.Impl.ConcreteCreator
{
    public class BottleFactory : SodaFactory
    {
        private string _containerSize;
        private string _sodaFlavor;
        private int _sodaQuantity;
        private int _sodaValue;

        public BottleFactory(string containerSize,
                             string sodaFlavor,
                             int sodaQuantity,
                             int sodaValue)
        {
            _containerSize = containerSize;
            _sodaFlavor = sodaFlavor;
            _sodaQuantity = sodaQuantity;
            _sodaValue = sodaValue;
        }

        public override Soda RequestSoda()
        {
            return new Bottle(_containerSize,
                              _sodaFlavor,
                              _sodaQuantity,
                              _sodaValue);
        }
    }

}
