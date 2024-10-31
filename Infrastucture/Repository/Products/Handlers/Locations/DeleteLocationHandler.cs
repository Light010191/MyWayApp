using Application.DTO.Response;
using Application.Service.Products.Commands.Categories;
using Application.Service.Products.Commands.Locations;
using Infrastucture.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Products.Handlers.Locations
{
	public class DeleteLocationHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<DeleteLocationCommand, ServiceResponse>
	{
		public async Task<ServiceResponse> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
		{
			try
			{
				using var dbContext = contextFactory.CreateDbContext();
				var data = await dbContext.Locations.FirstOrDefaultAsync(i => i.Id.Equals(request.Id), cancellationToken: cancellationToken);
				if (data == null)
					return GeneralDbResponses.ItemNotFound("Локация");

				dbContext.Locations.Remove(data);
				await dbContext.SaveChangesAsync(cancellationToken);
				return GeneralDbResponses.ItemDelete(data.Name);
			}
			catch (Exception ex)
			{
				return new ServiceResponse(true, ex.Message);
			}
		}
	}
}
