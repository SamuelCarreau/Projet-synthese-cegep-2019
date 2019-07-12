using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize(Roles = "Administrateur")]
    public class ObjectiveController : BaseController
    {
        // GET api/Objective
        [HttpGet]
        public async Task<ActionResult<IDictionary<string, List<ObjectiveModel>>>> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetObjectiveListQuery()));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/Objective/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ObjectiveModel>> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetObjectiveQuery { ObjectiveId = id }));
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/Objective
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateObjectiveCommand command)
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

        // PUT api/Objective/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateObjectiveCommand command)
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

        // DELETE api/Objective/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteObjectiveCommand { ObjectiveId = id });
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}