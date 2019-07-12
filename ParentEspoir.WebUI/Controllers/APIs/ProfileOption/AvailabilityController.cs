using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class AvailabilityController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<Availability>()));
        }

        // POST api/Availability
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<Availability> command)
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

        // PUT api/Availability/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<Availability> command)
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

        // DELETE api/Availability/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteProfilOptionCommand<Availability> { Id = id }));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
    }
}