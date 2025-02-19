using FactoryMethod.ConcreteProduct;
using FactoryMethod.Creator;
using FactoryMethod.Product;


namespace FactoryMethod.ConcreteCreator
{
    public class CanFactory : SodaFactory
    {
        private string _containerSize;
        private string _sodaFlavor;
        private int _sodaQuantity;
        private int _sodaValue;

        public CanFactory(string containerSize, string sodaFlavor, int sodaQuantity, int sodaValue) // Constructor
        {
            this._containerSize = containerSize;
            this._sodaFlavor = sodaFlavor;
            this._sodaQuantity = sodaQuantity;
            this._sodaValue = sodaValue;
        }

        public override Soda RequestSoda()
        {
            return new Can(_containerSize, _sodaFlavor, _sodaQuantity, _sodaValue);
        }
    }

}
