using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class StatisticController : BaseController
    {
        // GET api/Statistic
        [HttpGet]
        public async Task<ActionResult<GetStatisticListModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetStatisticListQuery()));
        }

        // GET api/Statistic/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetStatisticModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetStatisticQuery { StatisticId = id }));
        }

        // POST api/Statistic
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateStatisticCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/Statistic/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateStatisticCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/Statistic/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteStatisticCommand { StatisticId = id });

            return NoContent();
        }
    }
}