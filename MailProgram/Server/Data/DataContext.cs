namespace MailProgram.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Name = "Lucy", UserId = 1 }, 
                new User { Name = "Sarah", UserId = 2 
            }); 

            modelBuilder.Entity<Message>().HasData(
                new Message { MessageId = 1, Text = "World", Title = "Hello", Created = DateTime.Now, UserId = 1 },
                new Message { MessageId = 2, Text = "Test", Title = "test message", Created = DateTime.Now, UserId = 1 }
            );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
