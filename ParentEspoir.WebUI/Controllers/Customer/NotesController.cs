using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers.Customer
{
    [Authorize]
    [Route("Customer/{id}/Notes")]
    public class NotesController : ViewControllerBase
    {
        private static readonly string DELETE_SUCCESS = "DELETE_SUCCESS";
        private static readonly string UPDATE_SUCCESS = "UPDATE_SUCCESS";
        private static readonly string CREATION_SUCCESS = "CREATION_SUCCESS";
        private static readonly string DELETE_FAILED = "DELETE_FAILED";

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.DeleteSuccess = (TempData.ContainsKey(DELETE_SUCCESS) && (bool)TempData[DELETE_SUCCESS]);
            ViewBag.EditSuccess = (TempData.ContainsKey(UPDATE_SUCCESS) && (bool)TempData[UPDATE_SUCCESS]);
            ViewBag.CreationSuccess = (TempData.ContainsKey(CREATION_SUCCESS) && (bool)TempData[CREATION_SUCCESS]);

            ViewBag.CustomerId = id;

            ViewBag.CustomerName = await Mediator.Send(new GetCustomerNameQuery { CustomerId = id });

            return View(await Mediator.Send(new GetNoteListQuery() { CustomerId = id }));
        }


        [HttpGet]
        [Route("Details/{idNote}")]
        public async Task<IActionResult> Details(int id, int idNote)
        {
            if (TempData.ContainsKey(DELETE_FAILED) && (bool)TempData[DELETE_FAILED])
            {
                ViewBag.DeleteFailed = true;
            }

            var model = await Mediator.Send(new GetNoteQuery
            {
                NoteId = idNote
            });

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.CustomerName = await Mediator.Send(new GetCustomerNameQuery { CustomerId = id });

            return View(new CreateNoteCommand { CustomerId = id });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(int id, [Bind]CreateNoteCommand model)
        {
            try
            {
                await Mediator.Send(model);
                TempData.Add(CREATION_SUCCESS, true);
                return RedirectToAction(nameof(Index), new { id });
            }
            catch
            {
                ViewBag.CreationFailed = true;
                return View(model);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Delete(int id, int idNote)
        {
            try
            {
                await Mediator.Send(new DeleteNoteCommand { NoteId = idNote });

                TempData[DELETE_SUCCESS] = true;

                return RedirectToAction(nameof(Index), new { id });
            }
            catch
            {
                TempData[DELETE_FAILED] = true;

                return RedirectToAction(nameof(Details), new { id, idNote });
            }
        }
    }
}