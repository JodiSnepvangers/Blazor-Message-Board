namespace MailProgram.Client.Services.MessageService
{
    public interface IMessageService
    {
        List<Message> Messages { get; set; }

        Task getMessages();
        Task<Message?> GetSingleMessage(int id);
        Task PostMessage(Message message); 
        Task DeleteMessage(int id);
        Task UpdateMessage(Message message);

    }
}
