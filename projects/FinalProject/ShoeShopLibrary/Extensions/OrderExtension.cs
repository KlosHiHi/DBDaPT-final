using ShoeShopLibrary.DTOs;
using ShoeShopLibrary.Models;

namespace ShoeShopLibrary.Extensions
{
    /// <summary>
    /// Класс с методами расширения для модели заказа
    /// </summary>
    public static class OrderExtension
    {
        extension(Order order)
        {
            // Приведение объекта заказа к объекту DTO, содержащему краткую информацию
            public OrderDto? ToDto()
                => order is null ? null : new OrderDto
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate,
                    IsFinished = order.IsFinished,
                };

            public OrderInfo? ToDtoInfo()
                => order is null ? null : new OrderInfo
                {
                    OrderId = order.OrderId,
                    TotalPrice = order.ShoeOrders.Sum(so => so.Shoe.DiscountPrice * so.Quantity), // Подсчёт итоговой цены всего заказа, с учётом скидки
                    OrderDate = order.OrderDate,
                    ShoeArticles = String.Join(", ", order.ShoeOrders.Select(so => string.Format("[{0}] {1} - {2} шт.", so.Shoe.Article, so.Shoe.Maker.Name, so.Quantity))), // соединение информации о товарах в заказе
                    User = order.User
                };
        }

        extension(IEnumerable<Order> orders)
        {
            // Приведение массива объектов заказов, к массиву объектов содержащих краткую инфомрацию о заказе
            public IEnumerable<OrderDto?> ToDtos()
                => orders.Select(ToDto);

            public IEnumerable<OrderInfo?> ToDtoInfos()
                => orders.Select(ToDtoInfo);
        }
    }
}
