using Contracts;
using Contracts.Web.Application;
using Contracts.Web.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.MapGrpcService<UserServiceProxy>();

app.Run();