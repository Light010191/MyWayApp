﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response.Orders
{
	public record GetOrdersCountResponseDTO(int Processing, int Waiting, int Completed, int Canceled);	
}
