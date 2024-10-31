﻿using Application.DTO.Response.Products;
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

namespace Infrastucture.Repository.Products.Handlers.Locations
{
	public class GetAllLocationsHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : IRequest<IEnumerable<GetLocationResponseDTO>>
	{
		public async Task<IEnumerable<GetLocationResponseDTO>> Handle(GetAllLocationsHandler request, CancellationToken cancellationToken)
		{
			using var dbContext = contextFactory.CreateDbContext();
			var data = await dbContext.Locations.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
			return data.Adapt<List<GetLocationResponseDTO>>();
		}
	}
}