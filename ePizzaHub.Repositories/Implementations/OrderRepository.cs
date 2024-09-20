using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ePizzaHub.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext db) : base(db)
        {

        }
        public OrderModel GetOrderDetails(string orderId)
        {
            var model = (from order in _db.Orders
                         join payment in _db.PaymentDetails
                         on order.PaymentId equals payment.Id
                         where order.Id == orderId
                         select new OrderModel
                         {
                             Id = order.Id,
                             UserId = order.UserId,
                             CreatedDate = order.CreatedDate,
                             Items = (from orderItem in _db.OrderItems
                                      join item in _db.Items
                                      on orderItem.ItemId equals item.Id
                                      where orderItem.OrderId == orderId
                                      select new ItemModel
                                      {
                                          Id = orderItem.Id,
                                          Quantity = orderItem.Quantity,
                                          UnitPrice = orderItem.UnitPrice,

                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          ItemId = item.Id
                                      }).ToList(),
                             Total = payment.Total,
                             Tax = payment.Tax,
                             GrandTotal = payment.GrandTotal
                         }).FirstOrDefault();
            return model;
        }

        public IEnumerable<Order> GetUserOrders(int UserId)
        {
            return _db.Orders
              .Include(o => o.OrderItems)
              .Where(x => x.UserId == UserId).ToList();
        }
    }
}
