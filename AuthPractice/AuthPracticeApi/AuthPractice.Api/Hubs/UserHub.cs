using Microsoft.AspNetCore.SignalR;

namespace AuthPractice.Api.Hubs;
public class UserHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        string message = $"{Context.GetHttpContext().Session.GetString("Username")} is joined.";
        await Clients.All.SendAsync("UserJoined", message);
    }
}

