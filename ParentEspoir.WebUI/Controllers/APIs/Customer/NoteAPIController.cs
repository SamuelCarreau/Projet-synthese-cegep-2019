using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class NoteApiController : BaseController
    {
        // GET api/Note
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAll(int idCustomer)
        {
            return Ok(await Mediator.Send(new GetNoteListQuery { CustomerId = idCustomer }));
        }

        // GET api/Note/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetNoteModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetNoteQuery { NoteId = id }));
        }

        // POST api/Note
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateNoteCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/Note/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteNoteCommand { NoteId = id });

            return NoContent();
        }
    }
}