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
    public class LegalCustodyController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LegalCustody>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<LegalCustody>()));
        }

        // POST api/LegalCustody
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<LegalCustody> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/LegalCustody/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<LegalCustody> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/LegalCustody/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<LegalCustody> { Id = id });

            return NoContent();
        }
    }
}