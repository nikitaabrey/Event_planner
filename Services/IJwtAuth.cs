namespace Event_planner.Services
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
