using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class ChildrenAgeBracketController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChildrenAgeBracket>>> GetAll()
        {
            return base.Ok(await Mediator.Send(new GetProfilOptionQuery<ChildrenAgeBracket>()));
        }

        // POST api/ChildrenAgeBracket
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<ChildrenAgeBracket> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/ChildrenAgeBracket/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<ChildrenAgeBracket> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/ChildrenAgeBracket/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteProfilOptionCommand<ChildrenAgeBracket> { Id = id });

                return NoContent();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
    }
}