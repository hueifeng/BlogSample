using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CQRSMediatR.Models
{
    public class GetOrderByIdRequestModel:IRequest<string>
    {
        public string OrderId { get; set; }
    }
}
