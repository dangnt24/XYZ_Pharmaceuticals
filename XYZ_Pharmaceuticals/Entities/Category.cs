namespace XYZ_Pharmaceuticals.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        // Navigation property for related products
        public ICollection<Product> Products { get; set; }
    }
}
