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
    public class SchoolingController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schooling>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<Schooling>()));
        }

        // POST api/Schooling
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<Schooling> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/Schooling/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<Schooling> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/Schooling/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<Schooling> { Id = id });

            return NoContent();
        }
    }
}