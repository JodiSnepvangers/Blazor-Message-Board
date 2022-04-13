namespace MailProgram.Client.Services.MessageService
{
    public interface IUserService
    {
        User? LoggedInUser { get; set; }
        Task<List<User>?> GetUserList();
        Task<User?> GetUser(int id);
        Task AddUser(User user);
        Task DeleteUser(int id);
        Task UpdateUser(User user);
    }
}
