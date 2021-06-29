using MessagingRealtime.EFCore;
using MessagingRealtime.Models;
using Microsoft.AspNetCore.SignalR;
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
        public async Task SendMessageToAll(string message)
        {
            await AddMessageToDB(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        private async Task AddMessageToDB(string message)
        {
            Message m = new Message
            {
                UserName = "Saif",
                Text = message,

            };
            await _messageRepository.Add(m);
        }
    }
}
