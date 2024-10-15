using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Application.Extension.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.DataAccess
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : 
		IdentityDbContext<ApplicationUser>(options)
	{
	}
}
