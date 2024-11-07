using Application.DTO.Response.Orders;
using Application.Extension;
using Application.Service.Orders.Queries;
using Domain.Entities.Orders;
using Infrastucture.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repository.Orders.Handlers
{
    public class GetGenericOrdersCountHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequest<GetOrdersCountResponseDTO>
	{
		public async Task<GetOrdersCountResponseDTO> Handle(GetGenericOrdersCountQuery request, CancellationToken cancellationToken)
		{
			using var dbContext = contextFactory.CreateDbContext();
			var list = new List<Order>();
			if (!request.IsAdmin)
				list = await dbContext.Orders.AsNoTracking().Where(o => o.ClientId.ToString() == request.UserId).ToListAsync(cancellationToken);
			else
				list = await dbContext.Orders.AsNoTracking().ToListAsync(cancellationToken);
			int ProcessingCount = list.Count(o=>o.OrderState == OrderState.Processing);
			int CancelledCount = list.Count(o => o.OrderState == OrderState.Canceled);
			int WaitingCount = list.Count(o => o.OrderState == OrderState.Waiting);
			int CompletedCount = list.Count(o => o.OrderState == OrderState.Complete);
			return new GetOrdersCountResponseDTO(ProcessingCount,WaitingCount,CompletedCount,CancelledCount);
		}
	}
}
