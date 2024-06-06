using System;
using System.Linq;
using Microsoft.Extensions.Options;
using AuthorizationWithMiddleware.Data;
using AuthorizationWithMiddleware.Models;
using AuthorizationWithMiddleware.Settings;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly ApiSettings _apiSettings;

    public UserService(AppDbContext context, IOptions<ApiSettings> apiSettings)
    {
        _context = context;
        _apiSettings = apiSettings.Value;
    }

    public User Authenticate(string username, string password)
    {
        return _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
    }

    public ApiKey GenerateApiKey(User user)
    {
        var apiKey = new ApiKey
        {
            Key = Guid.NewGuid().ToString(),
            Expiration = DateTime.UtcNow.AddMinutes(_apiSettings.ApiKeyLifetimeMinutes),
            UserId = user.Id
        };
        _context.ApiKeys.Add(apiKey);
        _context.SaveChanges();
        return apiKey;
    }

    public bool ValidateApiKey(string apiKey)
    {
        var key = _context.ApiKeys.SingleOrDefault(k => k.Key == apiKey);
        return key != null && key.Expiration > DateTime.UtcNow;
    }
}
