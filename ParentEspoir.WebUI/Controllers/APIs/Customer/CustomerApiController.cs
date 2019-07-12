using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ParentEspoir.Application;
using System.Net;
using MediatR;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ParentEspoir.WebUI.Controllers
{
    [Authorize]
    public class CustomerApiController : BaseController
    {
        //Api pour la recherche du client par nom
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetName(int id)
        {
            return Ok(await Mediator.Send(new GetCustomerNameQuery { CustomerId = id }));
        }

        // GET api/CustomerApi/GetAll
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCustomerListQuery()));
        }

        // GET  
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCustomerQuery { CustomerId = id }));
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CustomerModel model)
        {
            await Mediator.Send(new CreateCustomerCommand { Model = model });

            return NoContent();
        }

        // PUT api/Customer/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody]CustomerModel model)
        {
            await Mediator.Send(new UpdateCustomerCommand { Model = model });

            return NoContent();
        }

        // DELETE api/Customer/5
        [Authorize(Roles = "Administrateur")]
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCustomerCommand { CustomerId = id });

            return NoContent();
        }
    }
}