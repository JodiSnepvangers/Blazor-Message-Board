using Microsoft.AspNetCore.Mvc;

namespace MailProgram.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly DataContext _context;

        public MessageController(DataContext context)
        {
            _context = context;
        }

        /**
         * 
         * HTTP Methods
         * 
         */


        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetMessages()
        {
            return Ok(await GetAllMessagesFromDatabase());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
        {
            var message = await _context.Messages
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MessageId == id);

            if (message == null)
            {
                return NotFound("No message with that ID");
            }
            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult<List<Message>>> AddMessage(Message message)
        {
            if (message == null)
            {
                return BadRequest("Message cannot be null");
            } else if(message.User == null)
            {
                return BadRequest("User cannot be null");
            }

            message.UserId = message.User.UserId;
            message.User = null;

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(await GetAllMessagesFromDatabase());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Message>>> UpdateMessage(Message message, int id)
        {
            if (message == null)
            {
                return BadRequest("Message cannot be null");
            }

            var databaseMessage = await _context.Messages
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            Console.WriteLine(databaseMessage);

            if(databaseMessage == null)
            {
                return NotFound("No message found to update");
            }

            databaseMessage.Title = message.Title;
            databaseMessage.Text = message.Text;

            await _context.SaveChangesAsync();

            return Ok(await GetAllMessagesFromDatabase());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Message>>> DeleteMessageById(int id)
        {
            var messages = await _context.Messages.ToListAsync();
            var internalMessage = messages.FirstOrDefault(x => x.MessageId == id);
            if (internalMessage == null)
            {
                return NotFound("No message found with that ID");
            }
            _context.Messages.Remove(internalMessage);
            await _context.SaveChangesAsync();

            return Ok(await GetAllMessagesFromDatabase());
        }

        /**
         * 
         * Private Methods
         * 
         */
        private async Task<List<Message>> GetAllMessagesFromDatabase()
        {
            return await _context.Messages
                .Include(m => m.User)
                .ToListAsync();
        }

    }
}
