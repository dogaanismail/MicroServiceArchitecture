using MassTransit;
using MicroServiceArchitecture.Shared.Messages;
using Order.Infrastructure;
using System.Threading.Tasks;

namespace Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        #region Fields
        private readonly OrderDbContext _orderDbContext;

        #endregion

        #region Ctor

        public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;   
        }

        #endregion

        #region Methods

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var newAddress = new Domain.OrderAggregate.Address(context.Message.Province, 
                context.Message.District, 
                context.Message.Street, 
                context.Message.ZipCode, 
                context.Message.Line);

            Domain.OrderAggregate.Order order = new(context.Message.BuyerId, newAddress);

            context.Message.OrderItems.ForEach(item =>
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.PictureUrl);
            });

            await _orderDbContext.Orders.AddAsync(order);

            await _orderDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
