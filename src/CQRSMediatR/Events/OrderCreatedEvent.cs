using MediatR;

namespace CQRSMediatR.Events
{
    public class OrderCreatedEvent: INotification
    {
        public string UserId { get; set; }

        public OrderCreatedEvent(string orderId)
        {
            this.UserId = orderId;
        }
    }
}
