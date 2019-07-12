using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class TransportTypeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<System.Collections.Generic.IEnumerable<TransportType>>> GetAll()
        {
            return base.Ok(await Mediator.Send(new Application.GetProfilOptionQuery<TransportType>()));
        }

        // POST api/TransportType
        [HttpPost]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<TransportType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/TransportType/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<TransportType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/TransportType/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<TransportType> { Id = id });

            return NoContent();
        }
    }
}