using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using FluentValidation;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class CitizenStatusController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitizenStatus>>> GetAll()
        {
            return base.Ok(await Mediator.Send(new GetProfilOptionQuery<CitizenStatus>()));
        }

        // POST api/CitizenStatus
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<CitizenStatus> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/CitizenStatus/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<CitizenStatus> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/CitizenStatus/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteProfilOptionCommand<CitizenStatus> { Id = id });

                return NoContent();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
    }
}