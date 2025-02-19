using FactoryMethod.Product;

namespace FactoryMethod.ConcreteProduct
{
    public class Bottle : Soda
    {
        private readonly string _containerType;
        private string _containerSize;
        private string _sodaFlavor;
        private int _sodaQuantity;
        private int _sodaValue;
        private int _totalValue;

        public Bottle(string containerSize,
                      string sodaFlavor,
                      int sodaQuantity,
                      int sodaValue)
        {
            this._containerType = "Bottle";
            this._containerSize = containerSize;
            this._sodaFlavor = sodaFlavor;
            this._sodaQuantity = sodaQuantity;
            this._sodaValue = 10;
            this._totalValue = _sodaValue * sodaQuantity;
        }

        // Polymorphism: declaring overrides
        public override string ContainerType
        {
            get { return _containerType; }
        }
        public override string ContainerSize
        {
            get { return this._containerSize; }
            set { _containerSize = value; }
        }
        public override string SodaFlavor
        {
            get { return this._sodaFlavor; }
            set { _sodaFlavor = value; }
        }
        public override int SodaQuantity
        {
            get { return this._sodaQuantity; }
            set { _sodaQuantity = value; }
        }
        public override int SodaValue
        {
            get { return this._sodaValue; }
        }
        public override int TotalValue
        {
            get { return this._totalValue; }
            set { _totalValue = value; }
        }
    }

}

