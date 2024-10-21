using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Orders
{
	public class Order
	{
		[Key]
		public Guid Id				 { get; set; } = Guid.NewGuid();
		public DateTime DataOrdered	 { get; set; } = DateTime.Now;
		public DateTime DataComplete { get; set; }
		public Guid ProductId		 { get; set; }
		public string ClientId		 { get; set; }
		public decimal Price		 { get; set; }
		public string OrderState	 { get; set; }
	}
}
