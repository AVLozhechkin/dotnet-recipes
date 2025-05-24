using System;
using System.Text;
using Community.Microsoft.Extensions.Caching.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

const string connectionString =
    "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=cacheDataBase;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=50;Connection Lifetime=3600;";

builder.Services.AddDistributedPostgreSqlCache(options =>
{
    options.ConnectionString = connectionString;
    options.SchemaName = "cache_schema";
    options.TableName = "cache_entries";
    options.DisableRemoveExpired = false;
    options.CreateInfrastructure = true;
    options.DefaultSlidingExpiration = TimeSpan.FromMinutes(1);
    options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(5);
    options.UpdateOnGetCacheItem = false;
    options.ReadOnlyMode = false;
});

var app = builder.Build();

app.MapPost("/set", async (
        [FromQuery] string key, 
        [FromQuery] string value, IDistributedCache cache) =>
    {
        try
        {
            await cache.SetAsync(key, Encoding.UTF8.GetBytes(value), new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(120),
            });
            return Results.Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.InternalServerError();
        }
    })
    .WithName("set entry");

app.MapGet("/get", async ([FromQuery] string key, IDistributedCache cache) =>
    {
        try
        {
            var value = await cache.GetAsync(key);

            if (value is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(Encoding.UTF8.GetString(value));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.InternalServerError();
        }
    })
    .WithName("get entry");

app.Run();