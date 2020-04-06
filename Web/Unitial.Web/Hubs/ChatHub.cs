using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Unitial.Data.Models;

namespace Unitial.Web.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string group ,string message)
        {
            await this.Clients.Group(group).SendAsync(
           "NewMessage", new Message()
           {
               Text = message
           }
           ) ;
        }

        public async Task JoinGroup(string Group)
        {
             Groups.AddToGroupAsync(Context.ConnectionId, Group);
        }
    }
}
