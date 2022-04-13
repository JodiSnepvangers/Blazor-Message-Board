using System.Net.Http.Json;

namespace MailProgram.Client.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _http;
        public List<Message> Messages { get; set; } = new List<Message>();

        public MessageService(HttpClient http)
        {
            _http = http;
        }

        /**
         * 
         * Public Methods
         * 
         */


        public async Task getMessages()
        {
            var result = await _http.GetFromJsonAsync<List<Message>>("api/message");
            if(result != null)
            {
                Messages = result;
            }
        }

        public async Task<Message?> GetSingleMessage(int id)
        {
            var result = new Message();

            try
            {
                result = await _http.GetFromJsonAsync<Message>($"api/message/{id}");
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

        public async Task PostMessage(Message message)
        {
            var result = await _http.PostAsJsonAsync("api/message", message);
            await UpdateMessagesFromHTTPResponse(result);
        }

        public async Task UpdateMessage(Message message)
        {
            var result = await _http.PutAsJsonAsync($"api/message/{message.MessageId}", message);
            await UpdateMessagesFromHTTPResponse(result);
        }

        public async Task DeleteMessage(int id)
        {
            var result = await _http.DeleteAsync($"api/message/{id}");
            await UpdateMessagesFromHTTPResponse(result);
        }

        /**
         * 
         * Private Methods
         * 
         */
        private async Task UpdateMessagesFromHTTPResponse(HttpResponseMessage result)
        {
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<List<Message>>();
                if (response != null)
                {
                    Messages = response;
                }
            }
        }
    }
}
