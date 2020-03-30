using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediatR.Commands
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderRequestModel, string>
    {
        public Task<string> Handle(CreateOrderRequestModel request, CancellationToken cancellationToken)
        {
            //do something...
            return Task.FromResult(request.UserId);
        }
    }
}
