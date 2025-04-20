using System;
using Contracts.Clients.Clients;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using User;

namespace Contracts.Clients;

public static class DependencyInjection
{
    public static IHttpClientBuilder AddUserServiceClient(this IServiceCollection services, 
        Action<GrpcClientFactoryOptions> configureClient,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        switch (lifetime)
        {
            case ServiceLifetime.Scoped:
                services.AddScoped<IUserService, UserServiceClient>();
                break;
            case ServiceLifetime.Singleton:
                services.AddSingleton<IUserService, UserServiceClient>();
                break;
            case ServiceLifetime.Transient:
                services.AddTransient<IUserService, UserServiceClient>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
        }
        
        return services.AddGrpcClient<UserService.UserServiceClient>(configureClient);
    }
}