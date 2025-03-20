using ASC.WEB.Configuration;
using ASC.WEB.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASC.WEB.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")]
    public class DashboardController : BaseController
    {
        private readonly ApplicationSettings _settings;

        public DashboardController(IOptions<ApplicationSettings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
