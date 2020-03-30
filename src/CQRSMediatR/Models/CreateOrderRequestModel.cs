using MediatR;

namespace CQRSMediatR.Commands
{
    public class CreateOrderRequestModel: 
        IRequest<string>
    {
        public string UserId { get; set; }
        public string CardNumber { get; set; }
    }


}
