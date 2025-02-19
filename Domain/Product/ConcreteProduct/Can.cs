namespace Domain.Product
{
    public class Can : Soda
    {
        private readonly string _containerType;
        private string _containerSize;
        private string _sodaFlavor;
        private int _sodaQuantity;
        private int _sodaValue;
        private int _totalValue;

        public Can(string containerSize, string sodaFlavor, int sodaQuantity, int sodaValue)
        {
            _containerType = "Can";
            _containerSize = containerSize;
            _sodaFlavor = sodaFlavor;
            _sodaQuantity = sodaQuantity;
            _sodaValue = 5;
            _totalValue = _sodaValue * sodaQuantity;
        }

        public override string ContainerType
        {
            get { return _containerType; }
        }
        public override string ContainerSize
        {
            get { return _containerSize; }
            set { _containerSize = value; }
        }
        public override string SodaFlavor
        {
            get { return _sodaFlavor; }
            set { _sodaFlavor = value; }
        }
        public override int SodaQuantity
        {
            get { return _sodaQuantity; }
            set { _sodaQuantity = value; }
        }
        public override int SodaValue
        {
            get { return _sodaValue; }
        }
        public override int TotalValue
        {
            get { return _totalValue; }
            set { _totalValue = value; }
        }
    }
}
