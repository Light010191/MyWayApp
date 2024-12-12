using Application.DTO.Response;
using Application.DTO.Response.Orders;
using Application.Service.Orders.Commands;
using Application.Service.Orders.Queries;
using Application.Service.Products.Commands.Categories;
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
	public class UpdateClientOrderHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<UpdateClientOrderCommand, ServiceResponse>
	{
		public async Task<ServiceResponse> Handle(UpdateClientOrderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				using var dbContext = contextFactory.CreateDbContext();
				var data = await dbContext.Orders.Where(o=>o.Id == request.Model.OrderId).FirstOrDefaultAsync(cancellationToken:cancellationToken);
				if (data == null)
					return new ServiceResponse(false, "Заказ не найден");

				data.OrderState = request.Model.OrderState;
				data.DataComplete = request.Model.DateComplete;
				await dbContext.SaveChangesAsync(cancellationToken);
				return new ServiceResponse(true, "Заказ обновлен");
					
			}
			catch (Exception ex)
			{
				return new ServiceResponse(true, ex.Message);
			}
		}
	}
}
