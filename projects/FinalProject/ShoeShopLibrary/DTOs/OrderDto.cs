namespace ShoeShopLibrary.DTOs
{
    /// <summary>
    /// Dto класс заказа, содержащий краткую информацию о заказе
    /// </summary>
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateOnly? OrderDate { get; set; }
        public DateOnly? DeliveryDate { get; set; }
        public bool IsFinished { get; set; }
    }
}
