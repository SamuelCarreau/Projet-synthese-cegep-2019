using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using ParentEspoir.Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetMemberListQuery()));
        }

        // POST api/Member
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateMemberCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/Member/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateMemberCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/Member/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteMemberCommand { MemberId = id });

            return NoContent();
        }
    }
}