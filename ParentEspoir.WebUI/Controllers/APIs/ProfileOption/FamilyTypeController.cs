using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class FamilyTypeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyType>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<FamilyType>()));
        }

        // POST api/FamilyType
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<FamilyType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/FamilyType/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<FamilyType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/FamilyType/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<FamilyType> { Id = id });

            return NoContent();
        }
    }
}