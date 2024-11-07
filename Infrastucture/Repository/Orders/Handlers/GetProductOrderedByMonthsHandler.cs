using Application.DTO.Response.Orders;
using Application.Service.Orders.Queries;
using Domain.Entities.Orders;
using Infrastucture.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Orders.Handlers
{
	public class GetProductOrderedByMonthsHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequest<GetProductOrderedByMonthResponseDTO>
	{
		public async Task<IEnumerable<GetProductOrderedByMonthResponseDTO>> Handle(GetProductOrderedByMonthsQuery request, CancellationToken cancellationToken)
		{
			using var dbContext = contextFactory.CreateDbContext();

			var orders = new List<Order>();
			var data = new List<GetProductOrderedByMonthResponseDTO>();
			if (!string.IsNullOrEmpty(request.UserId))
				orders = await dbContext.Orders.AsNoTracking().Where(o => o.ClientId.ToString() == request.UserId).ToListAsync(cancellationToken);
			else
				orders = await dbContext.Orders.AsNoTracking().ToListAsync(cancellationToken);

			var selectOrdersWithin12Months = orders
				.Where(order => order.DataOrdered.Date >= DateTime.Today.AddMonths(-12))
				.GroupBy(order => new { Month = order.DataOrdered.Month }).ToList();

			foreach (var order in selectOrdersWithin12Months.OrderBy(o => o.Key.Month))
			{
				data.Add(new GetProductOrderedByMonthResponseDTO()
				{
					MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(order.Key.Month),
					TotalAmount = order.Sum(x => x.TotalAmount),
				});
			}
			return data;
		}
	} 
}
