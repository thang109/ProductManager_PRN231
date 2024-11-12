namespace EStoreAPI.Model
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal? Weight { get; set; }

        public decimal? UnitPrice { get; set; }

        public int UnitInStock { get; set; }
    }
}
