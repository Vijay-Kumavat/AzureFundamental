using AzureScoopyLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace AzureScoopyLogic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static readonly HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SpookyRequest spookyRequest, IFormFile file)
        {
            spookyRequest.Id = Guid.NewGuid().ToString();
            var jsonContent = JsonConvert.SerializeObject(spookyRequest);
            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage httpResponse = await client.PostAsync("https://prod-07.eastus.logic.azure.com:443/workflows/c441f655f0254bd59d3a07e08faa1ed7/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=BnzA8r2km5rfGVooQ5RNIcmsa1t5LGli1QYWpBLllZs", content);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}