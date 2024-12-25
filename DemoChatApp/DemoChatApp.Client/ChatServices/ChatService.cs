using ChatModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace DemoChatApp.Client.ChatServices
{
    public class ChatService
    {
        public Action? InvokeChatDisplay {  get; set; }
        public List<Chat>Chats { get; set; } = [];
        private readonly HubConnection _hubConection;

        public bool IsConnected { get; set; }
        public ChatService(NavigationManager navigationManager)
        {
            _hubConection= new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
            .Build();
        }

        public void ReceiveMessage ()
        {
            _hubConection.On<Chat>("ReceiveMessage", (Chat) =>
            {
                Chats.Add(Chat);
                InvokeChatDisplay?.Invoke();
            });
        }

        public async Task StartConnectionAsync()
        {
            await _hubConection.StartAsync();
            IsConnected = _hubConection!.State== HubConnectionState.Connected;
        }

        public Task SendChat(Chat chat) =>
            _hubConection!.SendAsync("SendMessage",chat);
    }
}
