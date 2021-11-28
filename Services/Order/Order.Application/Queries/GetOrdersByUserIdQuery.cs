using MediatR;
using MicroServiceArchitecture.Shared.Dtos;
using Order.Application.Dtos;
using System.Collections.Generic;

namespace Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
