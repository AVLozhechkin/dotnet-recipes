using System.Threading.Tasks;
using User;

namespace Contracts;

public interface IUserService
{
    public Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
}