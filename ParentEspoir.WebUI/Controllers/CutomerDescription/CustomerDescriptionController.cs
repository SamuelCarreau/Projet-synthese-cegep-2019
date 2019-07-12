using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers.CutomerDescription
{
    [Authorize(Roles = "Administrateur")]
    public class CustomerDescriptionController : ViewControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await Mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = (int)id });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await Mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = (int)id });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update([Bind]UpdateCustomerDescriptionCommand command)
        {
            try
            {
                await Mediator.Send(command);
                ViewBag.success = true;
                var model = await Mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = command.CustomerDescriptionId });
                return View(model);
            }
            catch
            {
                
                var model = await Mediator.Send(new GetCustomerDescriptionQuery { CustomerDescriptionId = command.CustomerDescriptionId });
                return View(model);
            }
        }
    }
}


