using Application.DTO.Response;
using Application.DTO.Response.Orders;
using Application.DTO.Response.Products;
using Application.Extension.Identity;
using Application.Service.Orders.Queries;
using Application.Service.Products.Queries.Categories;
using Infrastucture.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repository.Orders.Handlers
{
	public class GetAllOrdersHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory, UserManager<ApplicationUser> userManager) : IRequestHandler<GetAllOrdersQuery, IEnumerable<GetOrderResponseDTO>>
	{
		public async Task<IEnumerable<GetOrderResponseDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
		{
			using var dbContext = contextFactory.CreateDbContext();
			var orders = await dbContext.Orders.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
			var products = await dbContext.Products.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
			var users = await userManager.Users.ToListAsync();
			return orders.Select(order => new GetOrderResponseDTO
			{
				Id = order.Id,
				ProductName = products.FirstOrDefault(p => p.Id == order.ProductId).Name,
				Price = order.Price,
				OrderedDate = order.DataOrdered,
				OrderComplete = order.DataComplete,
				ProductId = order.ProductId,
				ProductImage = products.FirstOrDefault(p => p.Id == order.ProductId).Base64Image,
				Quantity = order.Quantity,
				State = order.OrderState,
				ClientId = order.ClientId,
				TotalAmount = order.TotalAmount,
				ClientName = users.FirstOrDefault(u => u.Id.Equals(order.ClientId)).Name
			}).ToList();
		}
	}
}
