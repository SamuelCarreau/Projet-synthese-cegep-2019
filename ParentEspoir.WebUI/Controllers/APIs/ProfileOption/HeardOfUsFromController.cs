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
    public class HeardOfUsFromController : BaseController
    {
        // GET api/HeardOfUsFrom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeardOfUsFrom>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<HeardOfUsFrom>()));
        }

        // POST api/HeardOfUsFrom
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<HeardOfUsFrom> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/HeardOfUsFrom/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<HeardOfUsFrom> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/HeardOfUsFrom/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<HeardOfUsFrom> { Id = id });

            return NoContent();
        }
    }
}