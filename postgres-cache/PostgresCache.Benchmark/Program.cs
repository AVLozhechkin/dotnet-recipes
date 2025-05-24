using System;
using System.Net.Http;
using NBomber.CSharp;
using NBomber.Http.CSharp;

using var httpClient = new HttpClient();

var setCacheScenario = Scenario.Create("Set value", async context =>
{
    var key = Random.Shared.Next(0, 1000).ToString();
    var randomValue = Random.Shared.Next(0, int.MaxValue).ToString();

    var request =
        Http.CreateRequest("POST", $"http://localhost:5277/set?key={key}&value={randomValue}");
    var response = await Http.Send(httpClient, request);

    return response;
}).WithLoadSimulations(
    Simulation.KeepConstant(copies: 100, during: TimeSpan.FromSeconds(60))
);;

var getCacheScenario = Scenario.Create("Get value", async context =>
{
    var key = Random.Shared.Next(0, 1000).ToString();

    var request =
        Http.CreateRequest("GET", $"http://localhost:5277/get?key={key}");
    var response = await Http.Send(httpClient, request);

    return response;
}).WithLoadSimulations(
    Simulation.KeepConstant(copies: 100, during: TimeSpan.FromSeconds(50))
);;

NBomberRunner.RegisterScenarios(setCacheScenario, getCacheScenario).Run();