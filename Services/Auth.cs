using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

namespace Event_planner.Services
{
    public class Auth : IJwtAuth

    {

        private readonly string key;
        public Auth(string key)
        {
            this.key = key;
        }
        public   string Authentication(string username, string password)
        {


            var user = new CognitoUser { 
                UserName = username,
                Password = password
            };

            bool result =  SignInUserAsync(user).GetAwaiter().GetResult();

            if (!result)
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }







        public async Task<bool> SignInUserAsync(CognitoUser user)
        {
            var provider = new AmazonCognitoIdentityProviderClient(Amazon.RegionEndpoint.USEast1);

            try
            {
                var authReq = new AdminInitiateAuthRequest
                {
                    AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
                    UserPoolId = "us-east-1_0o3L3xJcG",
                    ClientId = "3kcatb2pis217osd3tsobh8ed2"
                };
                authReq.AuthParameters.Add("USERNAME", user.UserName);
                authReq.AuthParameters.Add("PASSWORD", user.Password);

                AdminInitiateAuthResponse authResp = await provider.AdminInitiateAuthAsync(authReq);

                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}
