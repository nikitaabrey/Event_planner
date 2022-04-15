using Event_planner.Models;

namespace Event_planner.Services
{
    public interface ISingletonSecretsManagerService
    {
         DbSecretModel getDatabaseCredential(string secretID);
    }
}
