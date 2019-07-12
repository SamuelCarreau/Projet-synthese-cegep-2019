using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class PersonnalFollowUpController : BaseController
    {
        // GET api/PersonnalFollowUp
        [HttpGet]
        public async Task<ActionResult<GetPersonnalFollowUpListModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetPersonnalFollowUpListQuery()));
        }

        // GET api/PersonnalFollowUp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPersonnalFollowUpModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPersonnalFollowUpQuery { PersonnalFollowUpId = id }));
        }

        // PUT api/PersonnalFollowUp/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdatePersonnalFollowUpCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/PersonnalFollowUp/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePersonnalFollowUpCommand { PersonnalFollowUpId = id });

            return NoContent();
        }
    }
}