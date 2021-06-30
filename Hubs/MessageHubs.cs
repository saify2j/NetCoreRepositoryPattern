using MessagingRealtime.EFCore;
using MessagingRealtime.Helpers;
using MessagingRealtime.Models;
using MessagingRealtime.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MessagingRealtime.Hubs
{
    public class MessageHubs : Hub
    {
        private readonly MessageRepository _messageRepository;
        public MessageHubs(MessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task SendMessageToAll(MessageViewModel message)
        {
            await AddMessageToDB(message);
            await Clients.All.SendAsync("ReceiveMessage", message.UserName + ": " + message.Message);
            //await Clients.User(userId).SendAsync("ReceiveSingleMessage", message.Message);
        }

        public static ConcurrentDictionary<string, string> MyUsers = new ConcurrentDictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            AppUser appUserConnected = JsonConvert.DeserializeObject<AppUser>(Context.GetHttpContext().Session.GetString(Constants.LoggedInUser));
            MyUsers.TryAdd(appUserConnected.UserName, Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        private async Task AddMessageToDB(MessageViewModel message)
        {
            Message m = new Message
            {
                UserName = message.UserName,
                Text = message.Message,

            };
            await _messageRepository.Add(m);
        }
    }
}
