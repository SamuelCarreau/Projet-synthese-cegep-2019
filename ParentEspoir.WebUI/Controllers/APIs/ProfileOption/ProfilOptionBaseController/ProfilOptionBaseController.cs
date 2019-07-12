using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;
using ParentEspoir.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ParentEspoir.WebUI.Controllers
{
    public abstract class ProfilOptionBaseController<TProfilOption> : BaseController where TProfilOption : class, IProfileOption
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TProfilOption>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<TProfilOption>()));
        }

        // POST api/Availability
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<TProfilOption> command)
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

        // PUT api/Availability/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<TProfilOption> command)
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

        // DELETE api/Availability/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteProfilOptionCommand<TProfilOption> { Id = id }));
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
        }
    }
}
