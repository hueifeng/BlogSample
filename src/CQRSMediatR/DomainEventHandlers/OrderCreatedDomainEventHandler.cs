using CQRSMediatR.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediatR.DomainEventHandlers
{
    public class OrderCreatedDomainEventHandler :
        INotificationHandler<OrderCreatedEvent>
    {
        private readonly ILogger _logger;

        public OrderCreatedDomainEventHandler(ILogger<OrderCreatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Order has found by userid id: {notification.UserId} from publisher");
            return Task.CompletedTask;
        }
    }
}
