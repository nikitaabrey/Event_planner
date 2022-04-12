using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Extensions.Caching;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json.Linq;
using Event_planner.Models;
using Amazon.Runtime.CredentialManagement;

namespace Event_planner.Services
{

    public class SecretsManagerService : ISingletonSecretsManagerService
    {




        private readonly IAmazonSecretsManager secretsManager;
        private readonly SecretsManagerCache cache;
/*      
        public class SecretCacheHook : ISecretCacheHook
        {
            private readonly IEncryptionProvider _encryptionProvider;

            public SecretCacheHook(IEncryptionProvider encryptionProvider)
            {
                _encryptionProvider = encryptionProvider;
            }

            public object Get(object obj)
            {
                return _encryptionProvider.Decrypt(obj);
            }

            public object Put(object obj)
            {
                return _encryptionProvider.Encrypt(obj);
            }
        }
*/
        public SecretsManagerService()
        {

            this.secretsManager = new AmazonSecretsManagerClient( RegionEndpoint.USEast1);
            this.cache = new SecretsManagerCache(this.secretsManager);

        }

        public async Task<DbSecretModel> getDatabaseCredentialAsync(string secretID)
        {
            
            var response =  this.cache.GetSecretString(secretID).Result;
            JObject jObject = JObject.Parse(response);
            return new DbSecretModel
            {
                Host = jObject["host"].ToObject<string>(),
                Port = jObject["port"].ToObject<string>(),
                Password = jObject["password"].ToObject<string>(),
                Username = jObject["username"].ToObject<string>()
            };

        }



    }
}