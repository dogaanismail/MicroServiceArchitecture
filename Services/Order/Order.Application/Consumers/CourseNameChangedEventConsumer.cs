using MassTransit;
using MicroServiceArchitecture.Shared.Messages;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Application.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        #region Fields
        private readonly OrderDbContext _orderDbContext;

        #endregion

        #region Ctor

        public CourseNameChangedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        #endregion

        #region Methods

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var orderItems = await _orderDbContext.OrderItems.Where(x => x.ProductId == context.Message.CourseId).ToListAsync();

            orderItems.ForEach(x =>
            {
                x.UpdateOrderItem(context.Message.UpdatedName, x.PictureUrl, x.Price);
            });

            await _orderDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
