namespace EStoreAPI.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public int? MemberId { get; set; }

        public DateOnly? OrderDate { get; set; }

        public DateOnly? RequiredDate { get; set; }

        public DateOnly? ShippedDate { get; set; }

        public string? Freight { get; set; }
    }
    public class OrderCreateModel
    {
        public OrderModel Order { get; set; } = new OrderModel();
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
