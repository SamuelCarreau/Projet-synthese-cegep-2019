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
    public class ReferenceTypeController : BaseController
    {
        // GET api/ReferenceType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferenceType>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<ReferenceType>()));
        }

        // POST api/ReferenceType
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<ReferenceType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/ReferenceType/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<ReferenceType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/ReferenceType/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<ReferenceType> { Id = id });

            return NoContent();
        }
    }
}