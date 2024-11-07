using Application.Service.Orders;
using Application.Service.Orders.Queries;
using MediatR;

namespace WebUI.States.User
{
	public class UserActiveOrderCountState(IServiceProvider serviceProvider)
	{
		public int ProcessingCount { get; set; }
		public int WaitingCount { get; set; }
		public int CanceledCount { get; set; }
		public int CompletedCount { get; set; }
		public event Action? StateChanged;

		public async Task GetActiveOrdersCount(string userId)
		{
			using var scope = serviceProvider.CreateScope();
			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var response = (await mediator.Send(new GetGenericOrdersCountQuery(userId, false)));
			ProcessingCount = response.Processing;
			WaitingCount = response.Waiting;
			CanceledCount = response.Canceled;
			CompletedCount = response.Completed;
			StateChanged?.Invoke();
		}

	}
}
