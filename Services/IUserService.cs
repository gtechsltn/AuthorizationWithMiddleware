using AuthorizationWithMiddleware.Models;

public interface IUserService
{
    User Authenticate(string username, string password);
    ApiKey GenerateApiKey(User user);
    bool ValidateApiKey(string apiKey);
}
