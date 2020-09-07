using FamilyTree.Services.Interfaces;

namespace FamilyTree.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
