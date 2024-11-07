using Application.DTO.Response;
using Application.Extension;
using Application.Service.Orders.Commands;
using Application.Service.Products.Commands.Categories;
using Domain.Entities.Products;
using Infrastucture.DataAccess;
using Infrastucture.Repository.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Orders.Handlers
{
	public class CancelOrderHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<CancelOrderCommand, ServiceResponse>
	{
		public async Task<ServiceResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				using var dbContext = contextFactory.CreateDbContext();
				var order = await dbContext.Orders.Where(o => o.Id == request.OrderId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
				if (order == null)
					return new ServiceResponse(false, "Заказ не найден");

				order.OrderState = OrderState.Canceled;
				await dbContext.SaveChangesAsync(cancellationToken);
				return new ServiceResponse(true, "Заказ отменен");
			}
			catch (Exception ex)
			{
				return new ServiceResponse(true, ex.Message);
			}
		}
	}
}
