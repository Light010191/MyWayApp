using Application.DTO.Response;
using Application.Service.Products.Commands.Locations;
using Application.Service.Products.Commands.Products;
using Domain.Entities.Products;
using Infrastucture.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Products.Handlers.Products
{
	public class UpdateProductHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<UpdateProductCommand, ServiceResponse>
	{
		public async Task<ServiceResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			try
			{
				using var dbContext = contextFactory.CreateDbContext();
				var product = await dbContext.Products.FirstOrDefaultAsync(i => i.Id.Equals(request.ProductModel.Id), cancellationToken: cancellationToken);
				if (product == null)
					return GeneralDbResponses.ItemNotFound(request.ProductModel.Name);

				dbContext.Entry(product).State = EntityState.Detached;
				var adaptData = request.ProductModel.Adapt(new Product());
				dbContext.Products.Update(adaptData);
				await dbContext.SaveChangesAsync(cancellationToken);
				return GeneralDbResponses.ItemUpdate(request.ProductModel.Name);
			}
			catch (Exception ex)
			{
				return new ServiceResponse(true, ex.Message);
			}
		}
	}
}
