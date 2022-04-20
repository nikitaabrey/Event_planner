using Amazon.CognitoIdentityProvider;
using Microsoft.AspNetCore.Identity;

namespace Event_planner.Services
{
    public class CognitoUser : IdentityUser
    {
        public string Password { get; set; }
        public UserStatusType Status { get; set; }
    }
}
