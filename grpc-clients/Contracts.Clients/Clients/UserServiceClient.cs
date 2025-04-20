using System;
using System.Threading.Tasks;
using User;

namespace Contracts.Clients.Clients;

internal class UserServiceClient(UserService.UserServiceClient userServiceClient) : IUserService
{
    public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(request.Name));
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("Email cannot be null or whitespace.", nameof(request.Email));
        }
        
        return await userServiceClient.CreateAsync(request);
    }
}