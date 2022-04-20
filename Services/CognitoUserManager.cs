/*using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Event_planner.Services;
using Microsoft.AspNetCore.Identity;

namespace Event_planner.Services
{
    public class CognitoUserManager : UserManager<CognitoUser>
    {

        private readonly AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient();
        private readonly string _clientId = "";
        private readonly string _poolId = "";

        public CognitoUserManager(IUserStore<CognitoUser> store)
            : base(store,null)
        {
        }

        public override Task<bool> CheckPasswordAsync(CognitoUser user, string password)
        {
            return CheckPasswordAsync(user.UserName, password);
        }

        private async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            try
            {
                var authReq = new AdminInitiateAuthRequest
                {
                    UserPoolId = ConfigurationManager.AppSettings["USERPOOL_ID"],
                    ClientId = ConfigurationManager.AppSettings["CLIENT_ID"],
                    AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
                };
                authReq.AuthParameters.Add("USERNAME", userName);
                authReq.AuthParameters.Add("PASSWORD", password);

                AdminInitiateAuthResponse authResp = await _client.AdminInitiateAuthAsync(authReq);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
*/