using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class SessionController : BaseController
    {
        private const string DEFAULT_ERROR = "Oh! Une erreur c'est produite";

        // GET api/Session
        [HttpGet]
        public async Task<ActionResult<SessionListViewModel>> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetSessionListQuery()));
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/Session
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateSessionCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            catch
            {
                return BadRequest(new string[] { DEFAULT_ERROR });
            }
            return NoContent();
        }

        // PUT api/Session/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateSessionCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            catch
            {
                return BadRequest(new string[] { DEFAULT_ERROR });
            }
            return NoContent();
        }

        // DELETE api/Session/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteSessionCommand { SessionId = id });
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            catch
            {
                return BadRequest(new string[] { DEFAULT_ERROR });
            }
            return NoContent();
        }
    }
}