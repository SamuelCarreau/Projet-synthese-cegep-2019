using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers.WorkshopType
{
    [Authorize(Roles = "Administrateur")]
    [Route("Volets")]
    public class WorkshopTypeController : ViewControllerBase
    {
        private static readonly string UPDATE_SUCCESS = "UPDATE_SUCCESS";

        public async Task<IActionResult> Index()
        {
            if (ViewData.ContainsKey(UPDATE_SUCCESS) && (bool)ViewData[UPDATE_SUCCESS] == true)
            {
                ViewBag.UpdateSuccess = true;
            }

            return View(await Mediator.Send(new GetWorkshopTypeListQuery()));
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind]CreateWorkshopTypeCommand command)
        {
            try
            {
                await Mediator.Send(command);

                ViewBag.OperationSuccess = true;

                return View();
            }
            catch
            {
                return View(command);
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await Mediator.Send(new GetWorkshopTypeQuery { Id = id });

            var command = new UpdateWorkshopTypeCommand
            {
                Code = model.Code,
                Id = model.Id,
                Name = model.Name
            };

            return View(command);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind]UpdateWorkshopTypeCommand command)
        {
            try
            {
                await Mediator.Send(command);

                ViewData[UPDATE_SUCCESS] = true;
            }
            catch
            {
                return View(command);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}