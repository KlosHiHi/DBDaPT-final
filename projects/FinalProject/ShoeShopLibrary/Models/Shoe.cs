using System;
using System.Collections.Generic;

namespace ShoeShopLibrary.Models;

public partial class Shoe
{
    public int ShoeId { get; set; }

    public string Article { get; set; } = null!;

    public int Price { get; set; }

    public byte Discount { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public byte? Size { get; set; }

    public string? Color { get; set; }

    public int VendorId { get; set; }

    public int MakerId { get; set; }

    public int CategoryId { get; set; }

    public bool IsFemale { get; set; }

    public string? PhotoName { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Maker Maker { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}
