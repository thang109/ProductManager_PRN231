namespace EStoreAPI.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public int MemberId { get; set; }

        public DateOnly? OrderDate { get; set; }

        public DateOnly? RequiredDate { get; set; }

        public DateOnly? ShippedDate { get; set; }

        public string? Freight { get; set; }
    }
}
