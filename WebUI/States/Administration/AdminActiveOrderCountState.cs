using Application.Service.Orders;
using Application.Service.Orders.Queries;
using MediatR;

namespace WebUI.States.Administration
{
	public class AdminActiveOrderCountState(IServiceProvider serviceProvider)
	{
		public int ProcessingCount { get; set; }
		public int WaitingCount { get; set; }
		public int CanceledCount { get; set; }
		public int CompletedCount { get; set; }
		public event Action? StateChanged;

		public async Task GetActiveOrdersCount()
		{
			using var scope = serviceProvider.CreateScope();
			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var response = (await mediator.Send(new GetGenericOrdersCountQuery(null, true)));
			ProcessingCount = response.Processing;
			WaitingCount = response.Waiting;
			CanceledCount = response.Canceled;
			CompletedCount = response.Completed;
			StateChanged?.Invoke();
		}

	}
}
