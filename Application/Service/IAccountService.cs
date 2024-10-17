﻿using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.DTO.Response.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
	public interface IAccountService
	{
		Task<ServiceResponse> LoginAsync(LoginUserRequestDTO model);
		Task<ServiceResponse> CreateUserAsync(CreateUserRequestDTO model);
		Task<IEnumerable<GetUserWithClaimResponseDTO>> GetUserWithClaimsAsync();
		Task SetUpAsync();
		Task<ServiceResponse> UpdateUserAsync(ChangeUserClaimRequestDTO model);
		//Task SaveActivityAsync(ActivityTrackerRequestDTO model);
		//Task<IEnumerable<ActivityTrackerResponseDTO>> GetActivitiesAsync();
	}
}