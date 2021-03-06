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
    public class YearlyIncomeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YearlyIncome>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<YearlyIncome>()));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<YearlyIncome> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<YearlyIncome> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<YearlyIncome> { Id = id });

            return NoContent();
        }
    }
}