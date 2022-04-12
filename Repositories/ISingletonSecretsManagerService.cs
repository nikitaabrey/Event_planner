namespace Event_planner.Models
{
    public interface ISingletonSecretsManagerService
    {
         DbSecretModel getDatabaseCredential(string secretID);
    }
}
