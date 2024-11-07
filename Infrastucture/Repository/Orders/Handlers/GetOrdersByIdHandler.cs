using Application.DTO.Response.Orders;
using Application.Service.Orders.Queries;
using Domain.Entities.Orders;
using Infrastucture.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Orders.Handlers
{
	public class GetOrdersByIdHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequest<GetOrderResponseDTO>
	{
		public async Task<IEnumerable<GetOrderResponseDTO>> Handle(GetOrdersByIdQuery request, CancellationToken cancellationToken)
		{
			using var dbContext = contextFactory.CreateDbContext();
			var orders = await dbContext.Orders.AsNoTracking().Where(o => o.ClientId.ToString() == request.UserId).ToListAsync(cancellationToken);
			var products = await dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);

			return orders.Select(order => new GetOrderResponseDTO
			{
				Id = order.Id,
				ProductName = products.FirstOrDefault(p => p.Id == order.ProductId).Name,
				Price = order.Price,
				OrderedDate = order.DataOrdered,
				ProductId = order.ProductId,
				ProductImage = products.FirstOrDefault(p => p.Id == order.ProductId).Base64Image,
				Quantity = order.Quantity,
				State = order.OrderState
			}).ToList();
		}
	}
}
