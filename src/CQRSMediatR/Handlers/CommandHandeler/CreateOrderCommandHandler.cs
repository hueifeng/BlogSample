using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRSMediatR.DomainEventHandlers;
using CQRSMediatR.Events;
using MediatR;

namespace CQRSMediatR.Commands
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderRequestModel, string>
    {
        private readonly IMediator _mediator;

        public CreateOrderCommandHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task<string> Handle(CreateOrderRequestModel request, CancellationToken cancellationToken)
        {
            //do something...
            await _mediator.Publish(new OrderCreatedEvent(request.UserId), cancellationToken);
            return request.UserId;
        }
    }
}
