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
    public class NoteTypeController : BaseController
    {
        // GET api/NoteType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteType>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<NoteType>()));
        }

        // POST api/NoteType
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<NoteType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/NoteType/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<NoteType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/NoteType/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<NoteType> { Id = id });

            return NoContent();
        }
    }
}