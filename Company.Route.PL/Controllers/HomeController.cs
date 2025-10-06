using Company.Route.PL.Models;
using Company.Route.PL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Company.Route.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedServices scopedServices01;
        private readonly IScopedServices scopedServices02;
        private readonly ITransiantService transientServices01;
        private readonly ITransiantService transiantServices02;
        public readonly ISingletonServices singeltonServices01;
        public readonly ISingletonServices singeltontServices02;

        public HomeController(ILogger<HomeController> logger,
            IScopedServices scopedServices01,
            IScopedServices scopedServices02,
            ITransiantService transientServices01,
            ITransiantService transiantServices02,
            ISingletonServices singeltonServices01,
            ISingletonServices singeltontServices02
            )
        {
            _logger = logger;
            this.scopedServices01 = scopedServices01;
            this.scopedServices02 = scopedServices02;
            this.transientServices01 = transientServices01;
            this.transiantServices02 = transiantServices02;
            this.singeltonServices01 = singeltonServices01;
            this.singeltontServices02 = singeltontServices02;
        }

        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scopedServices :: {scopedServices01.GetGguid()}\n");
            builder.Append($"scopedServices :: {scopedServices02.GetGguid()}\n\n");
            builder.Append($"scopedServices :: {transientServices01.GetGguid()}\n");
            builder.Append($"scopedServices :: {transiantServices02.GetGguid()}\n\n");
            builder.Append($"scopedServices :: {singeltonServices01.GetGguid()}\n");
            builder.Append($"scopedServices :: {singeltontServices02.GetGguid()}\n\n");

            return builder.ToString();
        }

        public IActionResult Index()
        {
            return View();
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
