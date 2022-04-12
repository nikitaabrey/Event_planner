using Event_planner.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_planner.Data
{
    public class EventPlannerContext : DbContext
    {
        private ISingletonSecretsManagerService secretsManagerService;
        private IConfiguration configuration;

        public EventPlannerContext( ISingletonSecretsManagerService secretsManagerService, IConfiguration configuration)        {
            this.secretsManagerService = secretsManagerService;
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder ) {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            else {

                DbSecretModel secretModel = this.secretsManagerService.getDatabaseCredentialAsync("prodKey")
                    .GetAwaiter()
                    .GetResult();
                optionsBuilder.UseSqlServer(
                    $"Server='{secretModel.Host}';" +
                    $" Database=EventPlanner;" +
                    $" User Id='{secretModel.Username}'; " +
                    $"Password='{secretModel.Password}';"
                );
                base.OnConfiguring(optionsBuilder);
            }
        }


        private string getConnString() {

            // check if environment has string configured
            // get from secrets manager

            if (configuration.GetSection("ConnectionString").Exists())
            {

            }
            else { 
                
            }
            return "";
        
        }

    }
}
