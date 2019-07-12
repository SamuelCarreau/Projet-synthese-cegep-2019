using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using System.Collections.Generic;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class SeanceController : BaseController
    {
        [HttpGet("{workshopId}")]
        public async Task<ActionResult<SeanceListModel>> GetAll(int workshopId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetSeanceListQuery { WorkshopId = workshopId }));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/Seance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetSeanceModel>> Get(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetSeanceQuery { SeanceId = id }));
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/Seance
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateSeanceCommand command)
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

        // PUT api/Seance/5
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update([FromBody]UpdateSeanceCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE api/Seance/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete([FromBody]DeleteSeanceCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}