using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class SupportGroupsController : ViewControllerBase
    {
        private static readonly string UPDATE_SUCCESS = "UPDATE_SUCCESS";
        private static readonly string DELETE_SUCCESS = "DELETE_SUCCESS";
        private static readonly string DELETE_FAILED = "DELETE_FAILED";
        // GET: SupportGroups
        public async Task<ActionResult> Index()
        {
            if (TempData.ContainsKey(UPDATE_SUCCESS) && (bool)TempData[UPDATE_SUCCESS])
            {
                ViewBag.EditSuccess = true;
            }
            else if (TempData.ContainsKey(DELETE_SUCCESS) && (bool)TempData[DELETE_SUCCESS])
            {
                ViewBag.DeleteSucced = true;
            }

            return View(await Mediator.Send(new GetSupportGroupListQuery()));
        }

        // GET: SupportGroups/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (TempData.ContainsKey(DELETE_FAILED) && (bool)TempData[DELETE_FAILED])
            {
                ViewBag.DeleteFailed = true;
            }

            return View(await Mediator.Send(new GetSupportGroupQuery { SupportGroupId = id }));
        }

        // GET: SupportGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupportGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind]CreateSupportGroupCommand model)
        {
            try
            {
                await Mediator.Send(model);

                ViewBag.OperationSuccess = true;

                return View();
            }
            catch
            {
                ViewBag.OperationFailed = true;

                return View(model);
            }
        }

        // GET: SupportGroups/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var info = await Mediator.Send(new GetSupportGroupQuery { SupportGroupId = id });

            var model = new UpdateSupportGroupCommand
            {
                SupportGroupId = info.SupportGroupId,
                Name = info.Name,
                Description = info.Description,
                Address = info.Address,
                UserId = info.UserId
            };

            return View(model);
        }

        // POST: SupportGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind]UpdateSupportGroupCommand model)
        {
            if (id == model.SupportGroupId)
            {
                try
                {
                    await Mediator.Send(model);

                    TempData.Add(UPDATE_SUCCESS, true);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.OperationFailed = true;

                    return View(model);
                }
            }

            ViewBag.ImpossibleToProcced = true;

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteSupportGroupCommand { SupportGroupId = id });

                TempData[DELETE_SUCCESS] = true;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData[DELETE_FAILED] = true;

                return RedirectToAction(nameof(Details), id);
            }
        }
    }
}