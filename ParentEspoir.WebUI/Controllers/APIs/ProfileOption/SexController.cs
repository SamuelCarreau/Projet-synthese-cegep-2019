using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class SexController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sex>>> GetAll()
        {
            var result = await Mediator.Send(new GetProfilOptionQuery<Sex>());
            return Ok(result.ToArray());
        }

        // POST api/Sex
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<Sex> command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        // PUT api/Sex/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<Sex> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/Sex/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<Sex> { Id = id });

            return NoContent();
        }
    }
}