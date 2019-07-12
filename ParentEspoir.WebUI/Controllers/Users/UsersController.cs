using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;
using ParentEspoir.Application.Exceptions;
using ParentEspoir.WebUI.Areas.Identity.Pages.Account;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize(Roles = "Administrateur")]
    [Route("Users")]
    public class UsersController : ViewControllerBase
    {
        private static readonly string UPDATE_SUCCESS = "Update Success";
        private static readonly string DELETE_SUCCED = "Delete Succed";
        private static readonly string DELETE_FAILED = "Delete failed";

        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey(UPDATE_SUCCESS) && (bool)TempData[UPDATE_SUCCESS])
            {
                ViewBag.OperationSuccess = true;
            }
            if (TempData.ContainsKey(DELETE_SUCCED) && (bool)TempData[DELETE_SUCCED])
            {
                ViewBag.OperationSuccess = true;
            }

            return View(await Mediator.Send(new GetUsersQuery()));
        }

        // what the actual fuck
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind]RegisterModel.InputModel model)
        {
            try
            {
                await Mediator.Send(new CreateUserCommand
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                });

                ViewBag.OperationSuccess = true;

                return View();
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var user = await Mediator.Send(new GetUserQuery { Id = id });

            if (user == null)
            {
                return NoContent();
            }

            return View(user);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await Mediator.Send(new GetUserEditQuery
            {
                Id = id
            });

            if (model == null)
            {
                return NoContent();
            }

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind]UpdateUserCommand model)
        {
            if (id == model.Id)
            {
                try
                {
                    await Mediator.Send(model);
                }
                catch (InvalidPasswordException)
                {
                    ViewBag.BadPassword = true;

                    return View(model);
                }
                catch
                {
                    ViewBag.ErrorSavingChange = true;

                    return View(model);
                }

                TempData[UPDATE_SUCCESS] = true;
                
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ImpossibleToProcced = true;

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await Mediator.Send(new DeleteUserCommand
                {
                    Id = id
                });
            }
            catch
            {
                TempData[DELETE_FAILED] = true;

                RedirectToAction(nameof(Details), new { id });
            }

            TempData[DELETE_SUCCED] = true;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("UserRoles/{id}")]
        public async Task<IActionResult> UserRoles(string id)
        {
            return View(await Mediator.Send(new GetRolesQuery { UserId = id }));
        }
    }
}