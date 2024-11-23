using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebUI.Hubs
{
	public class ConnectionService
	{
		private readonly HubConnection _connection;

		public ConnectionService(NavigationManager navigationManager)
		{
			_connection = new HubConnectionBuilder()
				.WithUrl(navigationManager.ToAbsoluteUri("/localhost"))
				.Build();
			_connection.StartAsync();
		}

		public HubConnection GetHubConnection() => _connection;
	}
}
