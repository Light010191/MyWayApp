using Application.DTO.Response.Products;
using Infrastucture.DataAccess;
using Infrastucture.Repository.Products.Handlers.Locations;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repository.Products.Handlers.Products
{
	public class GetProductsHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequest<IEnumerable<GetProductResponseDTO>>
	{
		public async Task<IEnumerable<GetProductResponseDTO>> Handle(GetAllLocationsHandler request, CancellationToken cancellationToken)
		{
			using var dbContext = contextFactory.CreateDbContext();
			var data = await dbContext.Products.AsNoTracking().Include(c => c.Category).Include(l => l.Location).ToListAsync(cancellationToken: cancellationToken);
			return data.Select(product => new GetProductResponseDTO
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Base64Image = product.Base64Image,
				CategoryId = product.CategoryId,
				LocationId = product.LocationId,
				Price = product.Price,
				Quantity = product.Quantity,
				DateAdded = product.DateAdded,
				Location = new GetLocationResponseDTO
				{ Id = product.LocationId, Name = product.Location.Name },
				Category = new GetCategoryResponseDTO
				{ Id = product.CategoryId, Name = product.Category.Name }
			}).ToList();
		}
	}
}
