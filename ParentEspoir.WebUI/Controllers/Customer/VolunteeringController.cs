using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers.Customer
{
    [Authorize]
    [Route("Customer/{id}/Volunteering")]
    public class VolunteeringController : ViewControllerBase
    {
        private const string CREATION_SUCCESS = "CREATIONSUCCESS";
        private static readonly string UPDATESUCCESS = "key to success!";
        private static readonly string DELETE_SUCCESS = "Key to";
        private static readonly string DELETE_FAILED = "Key to failure!";

        [HttpGet]
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

            ViewBag.customerId = id;

            ViewBag.CustomerName = await Mediator.Send(new GetCustomerNameQuery { CustomerId = id });

            return View(await Mediator.Send(new GetVolunteeringListQuery { CustomerId = id }));
        }

        // GET Volunteering
        [HttpGet]
        [Route("Details/{idvolunteering}")]
        public async Task<IActionResult> Details(int id, int idvolunteering)
        {
            if (TempData.ContainsKey(DELETE_FAILED) && (bool)TempData[DELETE_FAILED])
            {
                ViewBag.DeleteFailed = true;
            }

            var model = await Mediator.Send(new GetVolunteeringQuery
            {
                VolunteeringId = idvolunteering
            });

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create(int id)
        {
            if (TempData.ContainsKey(CREATION_SUCCESS) && (bool)TempData[CREATION_SUCCESS])
            {
                ViewBag.OperationSuccess = true;
            }

            ViewBag.CustomerName = await Mediator.Send(new GetCustomerNameQuery { CustomerId = id });

            return View(new CreateVolunteeringCommand { CustomerId = id });
        }

        // Create Volunteering
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(int id, [Bind]CreateVolunteeringCommand model)
        {
            try
            {
                await Mediator.Send(model);

                TempData.Add(CREATION_SUCCESS, true);

                return RedirectToAction(nameof(Create), new { id });
            }
            catch
            {
                ViewBag.OperationFailed = true;

                return View(model);
            }

        }

        [HttpGet]
        [Route("Update/{idvolunteering}")]
        public async Task<IActionResult> Update(int id, int idVolunteering)
        {
            var model = await Mediator.Send(new GetVolunteeringQuery { VolunteeringId = idVolunteering });

            var updateModel = new UpdateVolunteeringCommand
            {
                CustomerId = id,
                Acknowledgment = model.Acknowledgment,
                Amount = Math.Round(model.Amount, 2).ToString(),
                Date = model.Date,
                Details = model.Details,
                HourCount = model.HourCount,
                Title = model.Title,
                VolonteeringTypeName = model.VolunteeringTypeName,
                VolunteeringTypeId = model.VolunteeringTypeId,
                VolunteeringId = model.VolunteeringId
            };

            return View(updateModel);
        }

        [HttpPost]
        [Route("Update/{idVolunteering}")]
        public async Task<IActionResult> Update(int id, int idVolunteering, [Bind]UpdateVolunteeringCommand command)
        {
            try
            {
                await Mediator.Send(command);

                TempData[UPDATESUCCESS] = true;

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
        public async Task<IActionResult> Delete(int id, int idVolunteering)
        {
            try
            {
                await Mediator.Send(new DeleteVolunteeringCommand { VolunteeringId = idVolunteering });

                TempData[DELETE_SUCCESS] = true;

                return RedirectToAction(nameof(Index), new { id });
            }
            catch
            {
                TempData[DELETE_FAILED] = true;

                return RedirectToAction(nameof(Details), new { id, idVolunteering });
            }
        }
    }
}