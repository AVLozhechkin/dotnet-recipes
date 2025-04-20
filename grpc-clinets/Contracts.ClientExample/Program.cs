using System;
using Contracts;
using Contracts.Clients;
using Microsoft.Extensions.DependencyInjection;
using User;

var services = new ServiceCollection();

services.AddUserServiceClient(options =>
{
    options.Address = new Uri("https://localhost:7171");
});

var client = services.BuildServiceProvider().GetRequiredService<IUserService>();

var response = await client.CreateUserAsync(new CreateUserRequest()
{
    Name = "ABC",
    Email = "DEF"
});

Console.Write(response.Id);