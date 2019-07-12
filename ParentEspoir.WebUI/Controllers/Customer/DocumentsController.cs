using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;
using System;
using System.Threading.Tasks;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    [Route("Customer/{id}/Documents")]
    public class DocumentsController : ViewControllerBase
    {
        private static readonly string UPDATESUCCESS = "key to success!";
        private static readonly string DELETE_SUCCESS = "Key to";
        private static readonly string DELETE_FAILED = "Key to failure!";

        public async Task<IActionResult> Index(int id)
        {
            if (TempData.ContainsKey(UPDATESUCCESS) && (bool)TempData[UPDATESUCCESS])
            {
                ViewBag.UpdateSuccess = true;
            }
            if (TempData.ContainsKey(DELETE_SUCCESS) && (bool)TempData[DELETE_SUCCESS])
            {
                ViewBag.DeleteSucceed = true;
            }

            ViewBag.CustomerId = id;

            ViewBag.CustomerName = await Mediator.Send(new GetCustomerNameQuery { CustomerId = id });

            return View(await Mediator.Send(new GetDocumentListQuery { CustomerId = id }));
        }

        [HttpGet]
        [Route("Details/{idDocument}")]
        public async Task<IActionResult> Details(int id, int idDocument)
        {
            if (TempData.ContainsKey(DELETE_FAILED) && (bool)TempData[DELETE_FAILED])
            {
                ViewBag.DeleteFailed = true;
            }

            var model = await Mediator.Send(new GetDocumentQuery
            {
                DocumentId = idDocument
            });

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create(int id)
        {
            var model = new CreateDocumentCommand
            {
                CustomerId = id
            };

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(int id, [Bind]CreateDocumentCommand command)
        {
            try
            {
                await Mediator.Send(command);

                ViewBag.CreationSuccess = true;

                var model = new CreateDocumentCommand
                {
                    CustomerId = id
                };

                return View(model);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);

                ViewBag.CreationFailed = true;

                return View(command);
            }
        }

        [HttpGet]
        [Route("Update/{idDocument}")]
        public async Task<IActionResult> Update(int id, int idDocument)
        {
            var model = await Mediator.Send(new GetDocumentQuery { DocumentId = idDocument });

            var updateModel = new UpdateDocumentCommand
            {
                CustomerId = id,
                DocumentName = model.Name,
                DocumentId = model.DocumentId,
                Description = model.Description,
                DocumentTypeId = model.DocumentTypeId,
            };

            return View(updateModel);
        }

        [HttpPost]
        [Route("Update/{idDocument}")]
        public async Task<IActionResult> Update(int id, int idDocument, [Bind]UpdateDocumentCommand command)
        {
            try
            {
                await Mediator.Send(command);

                TempData[UPDATESUCCESS] = true;

                ViewBag.UpdateSuccess = true;

                return RedirectToAction(nameof(Index), new { id });
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);

                ViewBag.UpdateFailed = true;

                return View(command);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Delete(int id, int idDocument)
        {
            try
            {
                await Mediator.Send(new DeleteDocumentCommand { DocumentId = idDocument });

                TempData[DELETE_SUCCESS] = true;

                return RedirectToAction(nameof(Index), new { id });
            }
            catch
            {
                TempData[DELETE_FAILED] = true;

                return RedirectToAction(nameof(Details), new { id, idDocument });
            }
        }
    }
}
