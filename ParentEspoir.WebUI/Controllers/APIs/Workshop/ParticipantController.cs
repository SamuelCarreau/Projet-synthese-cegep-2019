using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class ParticipantController : BaseController
    {
        [HttpGet("{idWorkshop}")]
        public async Task<ActionResult<IEnumerable<GetParticipantListQuery>>> GetAll(int idWorkshop)
        {
            return Ok(await Mediator.Send(new GetParticipantListQuery { WorkshopId = idWorkshop }));
        }

        [HttpGet("{idWorkshop}")]
        public async Task<ActionResult<IEnumerable<GetParticipantListQuery>>> GetAllNotInWorkshop(int idWorkshop)
        {
            var test = await Mediator.Send(new GetParticipantsNotInWorkshopQuery { WorkshopId = idWorkshop });
            return Ok(test);
        }

        // POST api/Participant
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateParticipantCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/Participant/5
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update([FromBody]UpdateParticipantCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/Participant/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete([FromBody]DeleteParticipantCommand particiapntCommand)
        {
            await Mediator.Send(particiapntCommand);

            return NoContent();
        }
    }
}