using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response.Orders
{
	public class GetProductOrderedByMonthResponseDTO
	{
		public string MonthName { get; set; }
		public string TotalAmount { get; set; }
		public string FormattedTotalAmount => TotalAmount.ToString("#,##0.00");
	}
}
