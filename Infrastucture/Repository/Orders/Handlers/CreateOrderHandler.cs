using Application.DTO.Response;
using Application.Extension;
using Application.Service.Orders.Commands;
using Domain.Entities.Orders;
using Infrastucture.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Orders.Handlers
{
	public class CreateOrderHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<CreateOrderCommand, ServiceResponse>
	{
		public async Task<ServiceResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				using var dbContext = contextFactory.CreateDbContext();
				var product = await dbContext.Products.FirstOrDefaultAsync(p=>p.Id == request.Model.ProductId, cancellationToken: cancellationToken);
				var data = request.Model.Adapt<Order>();
				data.TotalAmount = product.Price * data.Quantity;
				data.OrderState = OrderState.Processing;
				data.Price = product.Price;
				await dbContext.SaveChangesAsync(cancellationToken);
				return new ServiceResponse(true, "Заказ создан");
			}
			catch (Exception ex)
			{
				return new ServiceResponse(true, ex.Message);
			}
		}
	}
}
