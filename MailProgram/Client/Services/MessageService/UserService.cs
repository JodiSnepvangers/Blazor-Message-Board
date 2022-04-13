using System.Net.Http.Json;

namespace MailProgram.Client.Services.MessageService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public User? LoggedInUser { get; set; }

        public UserService(HttpClient http)
        {
            _http = http;
        }

        /**
         * 
         * Public Methods
         * 
         */
        
        public async Task<List<User>?> GetUserList()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("api/user");
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<User?> GetUser(int id)
        {
            var result = new User();

            try
            {
                result = await _http.GetFromJsonAsync<User>($"api/user/{id}");
                if (result != null)
                {
                    return result;
                }
            }
            catch (HttpRequestException ex)
            {
            }
            return null;
        }

        public async Task AddUser(User user)
        {
            await _http.PostAsJsonAsync("api/user", user);
        }

        public async Task UpdateUser(User user)
        {
            await _http.PutAsJsonAsync($"api/user/{user.UserId}", user);
        }
        public async Task DeleteUser(int id)
        {
            await _http.DeleteAsync($"api/user/{id}");
        }
    }
}
