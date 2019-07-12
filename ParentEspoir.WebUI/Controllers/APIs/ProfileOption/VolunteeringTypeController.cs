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
    public class VolunteeringTypeController : BaseController
    {
        // GET api/VolunteeringType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VolunteeringType>>> GetAll()
        {
            return base.Ok(await Mediator.Send(new Application.GetProfilOptionQuery<VolunteeringType>()));
        }

        // POST api/VolunteeringType
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<VolunteeringType> command)
        {
            try
            {
                await Mediator.Send(command);

                return NoContent();
            }
            catch 
            {
                return BadRequest();
            }
        }

        // PUT api/VolunteeringType/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<VolunteeringType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/VolunteeringType/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<VolunteeringType> { Id = id });

            return NoContent();
        }
    }
}