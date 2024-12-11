namespace XYZ_Pharmaceuticals.Entities
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string Output { get; set; }
        public string CapsuleSize { get; set; }
        public string MachineDimension { get; set; }
        public float? ShippingWeight { get; set; }
        public string ModelNumber { get; set; }
        public string Dies { get; set; }
        public float? MaxPressure { get; set; }
        public float? MaxDiameter { get; set; }
        public float? MaxDepth { get; set; }
        public int? ProductionCapacity { get; set; }
        public float? AirPressure { get; set; }
        public float? AirVolume { get; set; }
        public int? FillingSpeed { get; set; }
        public float? FillingRange { get; set; }
        public string FillingType { get; set; }
        public string? Image { get; set; }
    }
}
