using NetcodeHub.Packages.Extensions.Attributes.RequiredGuid;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response.Products
{
	public class ProductBaseDTO
	{
		[Required]
		public string Name { get; set; }
		//[Required]
		//[Display(Name = "Serial number")]
		//public string SerialNumber { get; set; }
		[NetcodeHubRequiredGuid(ErrorMessage = "Product Location is required")]
		public Guid LocationId { get; set; }

		[NetcodeHubRequiredGuid(ErrorMessage = "Product Category is required")]
		public Guid CategoryId { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[RegularExpression(@"^\d+(\.\d{1,2})?$")]
				
		public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Required]
		[MinLength(10)]
		[MaxLength(5000)]
		public string Description { get; set; }

		[Required]
		[Display(Name ="Product Image")]
		public string Base64Image {  get; set; }
	}
}
