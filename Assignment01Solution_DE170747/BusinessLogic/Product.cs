using System;
using System.Collections.Generic;

namespace BusinessLogic;

public partial class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal? Weight { get; set; }

    public decimal? UnitPrice { get; set; }

    public int UnitInStock { get; set; }

    public virtual Category Category { get; set; } = null!;
}
