using System;
using System.Collections.Generic;

namespace BusinessLogic;

public partial class Order
{
    public int OrderId { get; set; }

    public int MemberId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? RequiredDate { get; set; }

    public DateOnly? ShippedDate { get; set; }

    public string? Freight { get; set; }

    public virtual Member Member { get; set; } = null!;
}
