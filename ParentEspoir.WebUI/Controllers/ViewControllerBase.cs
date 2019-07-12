using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ParentEspoir.WebUI.Controllers
{
    public class ViewControllerBase : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator)));
    }
}
