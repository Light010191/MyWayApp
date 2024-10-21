using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Application.Extension.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ActivityTracker;
using Domain.Entities.Products;
using Domain.Entities.Orders;

namespace Infrastucture.DataAccess
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : 
		IdentityDbContext<ApplicationUser>(options)
	{
		public DbSet<Tracker> ActivityTracker	{ get; set; }
		public DbSet<Product>  Products			{ get; set; }
		public DbSet<Category> Categories		{ get; set; }
		public DbSet<Location> Locations		{ get; set; }
		public DbSet<Order>	   Orders			{ get; set; }
	}
}
