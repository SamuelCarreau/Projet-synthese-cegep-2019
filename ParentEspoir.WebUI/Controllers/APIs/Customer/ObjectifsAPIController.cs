using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers.APIs.Customer
{
    [Authorize]
    public class ObjectifsAPIController : BaseController
    {
        // GET: api/Objective
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Objective>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetObjectiveListQuery()));
        }

        // GET api/Objective/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HabilityViewModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetObjectiveQuery { ObjectiveId = id }));
        }

        // POST api/Objective
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateObjectiveCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/Objective/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateObjectiveCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/Objective/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteObjectiveCommand { ObjectiveId = id });

            return NoContent();
        }
    }
}
