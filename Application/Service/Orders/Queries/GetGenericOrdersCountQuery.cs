using Application.DTO.Response;
using MediatR;

namespace Application.Service.Orders.Queries
{
    public record GetGenericOrdersCountQuery(string UserId, bool IsAdmin = false) : IRequest<ServiceResponse>;
	
}
