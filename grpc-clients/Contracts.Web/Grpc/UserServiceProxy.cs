using Grpc.Core;
using User;

namespace Contracts.Web.Grpc;

public class UserServiceProxy : UserService.UserServiceBase
{
    private readonly IUserService _userService;

    public UserServiceProxy(IUserService userService)
    {
        _userService = userService;
    }
    
    public override Task<CreateUserResponse> Create(CreateUserRequest request, ServerCallContext context)
    {
        return _userService.CreateUserAsync(request);
    }
}