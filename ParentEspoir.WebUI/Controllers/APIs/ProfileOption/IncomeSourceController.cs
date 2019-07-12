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
    public class IncomeSourceController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncomeSource>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<IncomeSource>()));
        }

        // POST api/IncomeSource
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<IncomeSource> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/IncomeSource/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<IncomeSource> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/IncomeSource/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<IncomeSource> { Id = id });

            return NoContent();
        }
    }
}