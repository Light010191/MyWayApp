using Application.DTO.Request.Products;
using System.Text.Json.Serialization;
using Application.DTO.Response.Products;

namespace Application.DTO.Response.Products
{
	public class GetLocationResponseDTO : UpdateLocationRequestDTO
	{
		[JsonIgnore]
		public virtual ICollection<GetProductResponseDTO> Products { get; set; } = null;
	}
}
