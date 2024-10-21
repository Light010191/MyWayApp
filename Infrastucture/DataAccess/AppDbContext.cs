using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Application.Extension.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ActivityTracker;

namespace Infrastucture.DataAccess
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : 
		IdentityDbContext<ApplicationUser>(options)
	{
		public DbSet<Tracker> ActivityTracer { get; set; }
	}
}
