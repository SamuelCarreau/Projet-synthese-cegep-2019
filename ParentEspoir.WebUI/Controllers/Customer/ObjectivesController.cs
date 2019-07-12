using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers.Customer
{
    [Authorize]
    [Route("Customer/{id}/Objectives")]
    public class ObjectivesController : ViewControllerBase
    {
        public async Task<IActionResult> Index(int id)
        {
            return View(await Mediator.Send(new GetObjectiveListQuery() {CustomerId = id }));
        }

        [Route("Create")]
        public IActionResult Create(int id)
        {
            ViewBag.CustomerId = id;

            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(int id, [Bind]CreateObjectiveCommand command)
        {
            ViewBag.CustomerId = id;

            try
            {
                await Mediator.Send(command);

                ViewBag.CreationSuccess = true;

                return View();
            }
            catch 
            {
                ViewBag.CreationFailed = true;

                return View(command);
            }
        }
    }
}
