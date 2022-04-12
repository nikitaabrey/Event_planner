namespace Event_planner.Models
{
    public interface ISingletonSecretsManagerService
    {
         Task<DbSecretModel> getDatabaseCredentialAsync(string secretID);
    }
}
