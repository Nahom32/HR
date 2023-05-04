using Microsoft.AspNetCore.SignalR;

namespace Human_Resources.Hubs
{
    public class MessageHub:Hub<IMessageHub>
    {
        
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
        public async Task BroadCastMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);
        }
        public async Task SendToIndividual(string connectionId, string message)
        {
            await Clients.Client(connectionId).ReceiveMessage(message);
        }

    }
}
