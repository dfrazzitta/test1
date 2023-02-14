using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using Polly;

namespace app.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Privacy()
    {

          using (var httpClient = new HttpClient())
            {
                var maxRetryAttempts = 3;
                var pauseBetweenFailures = TimeSpan.FromSeconds(2);

                var retryPolicy = Policy
                    .Handle<HttpRequestException>()
                    .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);

                await retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await httpClient
                    .GetAsync("http://172.17.0.4/weatherforecast");
                    //response.EnsureSuccessStatusCode();
                    var resData = response.Content.ReadAsStringAsync();
                    ViewData["json"] = response;
                    ViewData["resData"] = resData.Result;
                });
            }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
