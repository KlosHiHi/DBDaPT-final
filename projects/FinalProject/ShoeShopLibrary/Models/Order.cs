using System.ComponentModel.DataAnnotations;

namespace ShoeShopLibrary.Models;

public partial class Order
{
    [Display(Name = "Номер заказа")]
    public int OrderId { get; set; }
    public int UserId { get; set; }
    [Display(Name = "Дата заказа")]
    public DateOnly? OrderDate { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public int ReceiveCode { get; set; }
    public bool IsFinished { get; set; }

    public virtual ICollection<ShoeOrder> ShoeOrders { get; set; } = new List<ShoeOrder>();
    public virtual User User { get; set; } = null!;
}
