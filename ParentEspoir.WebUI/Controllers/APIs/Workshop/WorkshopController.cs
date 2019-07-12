using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using System.Collections.Generic;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class WorkshopController : BaseController
    {
        [HttpGet("{sessionId}")]
        public async Task<ActionResult<IEnumerable<WorkshopListElementModel>>> GetAll(int sessionId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetWorkshopListQuery { SessionId = sessionId }));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/Workshop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetWorkshopModel>> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetWorkshopQuery { WorkshopId = id }));
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/Workshop
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateWorkshopCommand command)
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

        // PUT api/Workshop/5
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update([FromBody]UpdateWorkshopCommand command)
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

        // DELETE api/Workshop/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteWorkshopCommand { WorkshopId = id });
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}