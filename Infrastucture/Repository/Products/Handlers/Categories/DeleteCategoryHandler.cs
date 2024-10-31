using Application.DTO.Response;
using Application.Service.Products.Commands.Categories;
using Domain.Entities.Products;
using Infrastucture.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repository.Products.Handlers.Categories
{
	public class DeleteCategoryHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<DeleteCategoryCommand, ServiceResponse>
	{
		public async Task<ServiceResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			try
			{
				using var dbContext = contextFactory.CreateDbContext();
				var data = await dbContext.Categories.FirstOrDefaultAsync(i => i.Id.Equals(request.Id), cancellationToken: cancellationToken);
				if (data == null)
					return GeneralDbResponses.ItemNotFound("Категория");
								
				dbContext.Categories.Remove(data);
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
