using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class DocumentApiController : BaseController
    {
        // GET api/Document
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetDocumentListQuery()));
        }

        // GET api/Document/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDocumentModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetDocumentQuery { DocumentId = id }));
        }

        // POST api/Document
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateDocumentCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/Document/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateDocumentCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/Document/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteDocumentCommand { DocumentId = id });

            return NoContent();
        }
    }
}