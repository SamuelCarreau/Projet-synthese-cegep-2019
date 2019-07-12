using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class WorkshopViewController : Controller
    {
        public IActionResult WorkshopAppView()
        {
            return View();
        }

        public IActionResult WorkshopMembers()
        {
            return View();
        }
        public IActionResult SessionMembers()
        {
            return View();
        }
        public IActionResult AddMembers()
        {
            return View();
        }
    }
}