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
    public class VolunteeringApiController : BaseController
    {
        // GET api/Volunteering
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Volunteering>>> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetVolunteeringListQuery()));
            }
            catch
            {
                return BadRequest();
            }
        }
            

        // GET api/Volunteering/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetVolunteeringModel>> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetVolunteeringQuery { VolunteeringId = id }));
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/Volunteering
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateVolunteeringCommand command)
        {
            try
            {
                 await Mediator.Send(command);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        // PUT api/Volunteering/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateVolunteeringCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/Volunteering/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteVolunteeringCommand { VolunteeringId = id });
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}