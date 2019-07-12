using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class ParentController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parent>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<Parent>()));
        }

        // POST api/Parent
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<Parent> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/Parent/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<Parent> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/Parent/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<Parent> { Id = id });

            return NoContent();
        }
    }
}