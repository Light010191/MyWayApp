﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastucture.DataAccess
{
	public class DbContextFactory<T>(IServiceProvider serviceProvider) : IDbContextFactory<T> where T : DbContext
	{
		public T CreateDbContext()
		{
			return serviceProvider.GetRequiredService<T>();
		}
	}
}
