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
    public class HomeTypeController : BaseController
    {
        // GET api/HomeType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeType>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetProfilOptionQuery<HomeType>()));
        }

        // POST api/HomeType
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateProfilOptionCommand<HomeType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/HomeType/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProfilOptionCommand<HomeType> command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/HomeType/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProfilOptionCommand<HomeType> { Id = id });

            return NoContent();
        }
    }
}