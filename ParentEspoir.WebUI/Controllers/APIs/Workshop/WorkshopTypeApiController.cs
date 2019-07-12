using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class WorkshopTypeApiController : BaseController
    {
        // GET api/WorkshopType
        [HttpGet]
        public async Task<ActionResult<WorkshopTypeModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetWorkshopTypeListQuery()));
        }

        // POST api/WorkshopType
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateWorkshopTypeCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/WorkshopType/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateWorkshopCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/WorkshopType/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteWorkshopTypeCommand { Id = id });

                return Ok();
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