


/*

using Amazon.CognitoIdentityProvider;
using Microsoft.AspNet.Identity;
using Event_planner.Services;
using Amazon.CognitoIdentityProvider.Model;

public class CognitoUserStore : IUserStore<CognitoUser>,
                                IUserLockoutStore<CognitoUser,string>,
                                IUserTwoFactorStore<CognitoUser, string>
{
    private readonly AmazonCognitoIdentityProviderClient _client =
        new AmazonCognitoIdentityProviderClient();
    private readonly string _clientId = "";
    private readonly string _poolId = "";

    public Task CreateAsync(CognitoUser user)
    {
        // Register the user using Cognito
        var signUpRequest = new SignUpRequest
        {
            ClientId = "",
            Password = user.Password,
            Username = user.Email

        };

        var emailAttribute = new AttributeType
        {
            Name = "email",
            Value = user.Email
        };
        signUpRequest.UserAttributes.Add(emailAttribute);

        return _client.SignUpAsync(signUpRequest);
    }

    public Task DeleteAsync(CognitoUser user)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<CognitoUser> FindByIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<CognitoUser> FindByNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetAccessFailedCountAsync(CognitoUser user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetLockoutEnabledAsync(CognitoUser user)
    {
        throw new NotImplementedException();
    }

    public Task<DateTimeOffset> GetLockoutEndDateAsync(CognitoUser user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetTwoFactorEnabledAsync(CognitoUser user)
    {
        throw new NotImplementedException();
    }

    public Task<int> IncrementAccessFailedCountAsync(CognitoUser user)
    {
        throw new NotImplementedException();
    }

    public Task ResetAccessFailedCountAsync(CognitoUser user)
    {
        throw new NotImplementedException();
    }

    public Task SetLockoutEnabledAsync(CognitoUser user, bool enabled)
    {
        throw new NotImplementedException();
    }

    public Task SetLockoutEndDateAsync(CognitoUser user, DateTimeOffset lockoutEnd)
    {
        throw new NotImplementedException();
    }

    public Task SetTwoFactorEnabledAsync(CognitoUser user, bool enabled)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CognitoUser user)
    {
        throw new NotImplementedException();
    }
}
*/