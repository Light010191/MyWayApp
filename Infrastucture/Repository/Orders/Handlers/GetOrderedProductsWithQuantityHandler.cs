using Application.DTO.Response.Orders;
using Application.Extension;
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
	public class GetOrderedProductsWithQuantityHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequest<GetOrderedProductsWithQuantityResponseDTO>
	{
		public async Task<IEnumerable<GetOrderedProductsWithQuantityResponseDTO>> Handle(GetOrderedProductsWithQuantityQuery request, CancellationToken cancellationToken)
		{
			using var dbContext = contextFactory.CreateDbContext();
			var list = new List<Order>();
			var data = new List<GetOrderedProductsWithQuantityResponseDTO>();
			if (!string.IsNullOrEmpty(request.UserId))
				list = await dbContext.Orders.AsNoTracking().Where(o => o.ClientId.ToString() == request.UserId).ToListAsync(cancellationToken);
			else
				list = await dbContext.Orders.AsNoTracking().ToListAsync(cancellationToken);

			var selectOrdersWithin12Months = list
				.Where(order => order.DataOrdered.Date >= DateTime.Today.AddMonths(-12))
				.GroupBy(order => new { Name = order.ProductId }).ToList();

			foreach (var order in selectOrdersWithin12Months)
			{
				data.Add(new GetOrderedProductsWithQuantityResponseDTO()
				{
					ProductName = (await dbContext.Products.FirstOrDefaultAsync(p => p.Id == order.Key.Name, cancellationToken: cancellationToken)).Name,
					QuatityOrdered = order.Sum(x => x.Quantity),
				});
			}
			return data;
		}
	}
}
