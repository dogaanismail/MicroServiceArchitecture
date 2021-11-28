using MediatR;
using MicroServiceArchitecture.Shared.Dtos;
using Order.Application.Dtos;
using System.Collections.Generic;

namespace Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}
