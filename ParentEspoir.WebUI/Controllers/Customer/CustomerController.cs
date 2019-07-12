using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers.Customer
{
    [Authorize]
    public class CustomerController : ViewControllerBase
    {
        private static readonly string DELETE_SUCCESS = "DELETE_SUCCESS";
        private static readonly string UPDATE_SUCCESS = "UPDATE_SUCCESS";

        [HttpGet("/")]
        [HttpGet("Customer/Index/{currentPage?}/{sortType?}/{searchFilter?}")]
        public async Task<ActionResult> Index(int? currentPage, string sortOrder, string searchFilter)
        {
            if (TempData.ContainsKey(DELETE_SUCCESS) && (bool)TempData[DELETE_SUCCESS])
            {
                ViewBag.DeleteSuccess = true;
            }
            if (TempData.ContainsKey(UPDATE_SUCCESS) && (bool)TempData[UPDATE_SUCCESS])
            {
                ViewBag.EditSuccess = true;
            }
            if (currentPage == null)
            {
                currentPage = 1;
            }

            return View(await Mediator.Send(new GetCustomerListQuery { CurrentPage = (int)currentPage, SortOrder = sortOrder, SearchFilter = searchFilter }));
        }

        // GET: Customer/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await Mediator.Send(new GetCustomerQuery { CustomerId = id }));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind]CreateCustomerCommand command)
        {
            try
            {
                await Mediator.Send(command);
                ViewBag.CreationSucced = true;
                return View();
            }
            catch
            {
                return View(command);
            }

        }

        // POST: Customer/Update/4
        public async Task<ActionResult> Update(int id)
        {
            var model = await Mediator.Send(new GetCustomerQuery{ CustomerId = id });
            var command = new UpdateCustomerCommand { Model = model };

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update([Bind]UpdateCustomerCommand command)
        {
            try
            {
                await Mediator.Send(command);

                TempData.Add(UPDATE_SUCCESS, true);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(command);
            }

        }

        // Delete: Customer/Delete/2
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteCustomerCommand { CustomerId = id });

                TempData[DELETE_SUCCESS] = true;

                return RedirectToAction(nameof(Index));
            }
            catch
            {

                TempData[DELETE_SUCCESS] = true;

                return RedirectToAction(nameof(Details),id);
            }
        }

    }
}