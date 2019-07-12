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
    public class SocialServiceController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocialService>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<SocialService>()));
        }

        // POST api/SocialService
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<SocialService> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/SocialService/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<SocialService> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/SocialService/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<SocialService> { Id = id });

            return NoContent();
        }
    }
}