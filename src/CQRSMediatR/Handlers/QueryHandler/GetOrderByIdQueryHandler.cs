using CQRSMediatR.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediatR.Commands.QueryHandler
{
    public class GetOrderByIdQueryHandler :
        IRequestHandler<GetOrderByIdRequestModel, string>
    {
        public Task<string> Handle(GetOrderByIdRequestModel request, CancellationToken cancellationToken)
        {
            //do something
            return Task.FromResult(request.OrderId);
        }
    }
}
