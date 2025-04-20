using User;

namespace Contracts.Web.Application;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;

    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
    }
    
    public Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var response = new CreateUserResponse()
        {
            Id = Random.Shared.Next(1, int.MaxValue)
        };
        
        _logger.LogInformation("User with an ID {UserId} was created.", response.Id);
        
        return Task.FromResult(response);
    }
}