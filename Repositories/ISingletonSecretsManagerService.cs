using Event_planner.Models;

namespace Event_planner.Repositories
{
    public interface ISingletonSecretsManagerService
    {
         DbSecretModel getDatabaseCredential(string secretID);
    }
}
