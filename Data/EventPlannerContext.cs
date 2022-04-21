using Event_planner.Models;
using Event_planner.Services;
using EventPlanner.FluentConfigs;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_planner.Data
{
    public class EventPlannerContext : DbContext
    {
        private ISingletonSecretsManagerService secretsManagerService;
        private IConfiguration configuration;

        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<RecurringEvents> RecurringEvents { get; set; }
        public virtual DbSet<Users> Users { get; set; }



        public EventPlannerContext(ISingletonSecretsManagerService secretsManagerService, IConfiguration configuration)        {
            this.secretsManagerService = secretsManagerService;
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder ) {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            else {
                string connectionString = getConnString();
                optionsBuilder.UseSqlServer(
                   connectionString
                );
                base.OnConfiguring(optionsBuilder);
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CalendarMap());
            modelBuilder.ApplyConfiguration(new EventMap());
            modelBuilder.ApplyConfiguration(new RecurringEventsMap());
            modelBuilder.ApplyConfiguration(new UsersMap());
        }


        private string getConnString() {
            if (configuration.GetSection("ConnectionString").Exists())
            {
                return configuration.GetSection("ConnectionString").Value.ToString();
            }
            else if (configuration.GetSection("DatabaseSecretID").Exists()) {
                var secretID = configuration.GetSection("DatabaseSecretID").Value.ToString();
                DbSecretModel secretModel = this.secretsManagerService.getDatabaseCredential(secretID);
                return $"Server='{secretModel.Host}';" +
                    $" Database=EventPlannerDB;" +
                    $" User Id='{secretModel.Username}'; " +
                    $"Password='{secretModel.Password}';";
            } else {
                // error
                throw new Exception();
            }
        
        }

    }
}
