using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace DemoChatApp.Client.ChatServices
{
    public class MyHubConnectionService
    {
        public Action? InvokeChatDisplay {  get; set; }

        private readonly HubConnection _hubConection;

        public bool IsConnected { get; set; }
        public MyHubConnectionService(NavigationManager navigationManager)
        {
            _hubConection= new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
            .Build();

            _hubConection.StartAsync();
            GetConnectionState();
        }

        public HubConnection GetHubConnection() => _hubConection;

        private bool GetConnectionState()
        {
           var hubConnection = GetHubConnection();
            IsConnected = hubConnection != null;
            return IsConnected;
        }        
    }
}
