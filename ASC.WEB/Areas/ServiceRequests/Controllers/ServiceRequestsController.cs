using Microsoft.AspNetCore.Mvc;

namespace ASC.WEB.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")] // Định nghĩa đây là Area
    public class ServiceRequestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
