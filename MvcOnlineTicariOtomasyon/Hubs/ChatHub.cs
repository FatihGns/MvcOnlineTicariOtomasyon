using Microsoft.AspNet.SignalR;

namespace MvcOnlineTicariOtomasyon.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Tüm client’lara mesaj gönder
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}