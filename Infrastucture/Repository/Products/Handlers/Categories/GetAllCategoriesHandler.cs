using Application.DTO.Response;
using Application.DTO.Response.Products;
using Application.Service.Products.Commands.Categories;
using Application.Service.Products.Queries.Categories;
using Infrastucture.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Products.Handlers.Categories
{
	public class GetAllCategoriesHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequest<IEnumerable<GetCategoryResponseDTO>>
	{
		public async Task<IEnumerable<GetCategoryResponseDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
		{
			using var dbContext = contextFactory.CreateDbContext();
			var data = await dbContext.Categories.AsNoTracking().ToListAsync(cancellationToken : cancellationToken);
			return data.Adapt<List<GetCategoryResponseDTO>>();
		}
	}
}
