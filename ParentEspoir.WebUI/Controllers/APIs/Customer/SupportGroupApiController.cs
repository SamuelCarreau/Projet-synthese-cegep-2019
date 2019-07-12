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
    public class SupportGroupController : BaseController
    {
        // GET api/SupportGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupportGrougModel>>> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetSupportGroupListQuery()));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/SupportGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupportGroup>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetSupportGroupQuery { SupportGroupId = id }));
        }

        // POST api/SupportGroup
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateSupportGroupCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/SupportGroup/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateSupportGroupCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/SupportGroup/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSupportGroupCommand { SupportGroupId = id });

            return NoContent();
        }
    }
}