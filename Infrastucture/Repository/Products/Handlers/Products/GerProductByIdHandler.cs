using Application.DTO.Response;
using Application.DTO.Response.Products;
using Application.Service.Products.Queries.Products;
using Infrastucture.DataAccess;
using Infrastucture.Repository.Products.Handlers.Locations;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repository.Products.Handlers.Products
{
	public class GerProductByIdHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequestHandler<GetProductByIdQuery, GetProductResponseDTO>
	{
		public async Task<GetProductResponseDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			using var dbContext = contextFactory.CreateDbContext();
			var product = await dbContext.Products.AsNoTracking().Include(c=>c.Category).Include(l=>l.Location).FirstOrDefaultAsync(p=>p.Id == request.Id, cancellationToken: cancellationToken);
				
			return new GetProductResponseDTO
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
				{Id = product.LocationId, Name = product.Location.Name},
				Category = new GetCategoryResponseDTO
				{ Id = product.CategoryId, Name = product.Category.Name }
			};
		}
	}
}
