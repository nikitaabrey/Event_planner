using Event_planner.Models;
using Event_planner.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Event_planner.Data
{
    public class EventPlannerContext : DbContext
    {
        private ISingletonSecretsManagerService secretsManagerService;
        private IConfiguration configuration;

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


        private string getConnString() {
            if (configuration.GetSection("ConnectionString").Exists())
            {
                Console.WriteLine("Hello WOrld");
                return configuration.GetSection("ConnectionString").Value.ToString();
            }
            else if (configuration.GetSection("DatabaseSecretID").Exists()) {
                var secretID = configuration.GetSection("DatabaseSecretID").Value.ToString();
                DbSecretModel secretModel = this.secretsManagerService.getDatabaseCredential(secretID);
                return $"Server='{secretModel.Host}';" +
                    $" Database=EventPlanner;" +
                    $" User Id='{secretModel.Username}'; " +
                    $"Password='{secretModel.Password}';";
            } else {
                // error
                throw new Exception();
            }
        
        }

    }
}
