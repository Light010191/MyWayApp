using Application.DTO.Request.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response.Orders
{
	public class GetOrderResponseDTO : OrderBaseDTO
	{
		public Guid Id { get; set; }
		public string State { get; set; }
		public string ProductName { get; set; }
		public DateTime OrderedDate { get; set; }
		public DateTime OrderComplete { get; set; }
		public decimal Price { get; set; }
		public string ProductImage { get; set; }
		public string ClientId { get; set; }
		public string ClientName { get; set; }		
	}
}
