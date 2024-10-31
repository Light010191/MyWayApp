using Microsoft.EntityFrameworkCore;

namespace Infrastucture.DataAccess
{
	public interface IDbContextFactory<T> where T : DbContext
	{
		T CreateDbContext();
	}
}
