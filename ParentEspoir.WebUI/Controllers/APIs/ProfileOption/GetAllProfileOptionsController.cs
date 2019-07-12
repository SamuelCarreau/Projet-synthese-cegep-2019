using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class GetAllProfileOptionsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IProfileOption>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllProfileOptionQuery()));
        }
    }
}