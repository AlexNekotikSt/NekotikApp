namespace Domain.Product
{
    public abstract class Soda
    {
        public abstract string ContainerType { get; }

        public abstract string ContainerSize { get; set; }

        public abstract string SodaFlavor { get; set; }

        public abstract int SodaQuantity { get; set; }

        public abstract int SodaValue { get; }

        public abstract int TotalValue { get; set; }
    }

}
