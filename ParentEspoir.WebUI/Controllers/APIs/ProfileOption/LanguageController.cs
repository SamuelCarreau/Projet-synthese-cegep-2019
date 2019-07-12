using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using System.Collections.Generic;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using ParentEspoir.WebUI.Models;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class LanguageController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<Language>()));
        }

        // POST api/Language
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<Language> command)
        {
            try
            {
                await Mediator.Send(command);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/Language/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<Language> command)
        {
            try
            {
                await Mediator.Send(command);

                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/Language/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteProfilOptionCommand<Language> { Id = id });

                return NoContent();
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}