using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers.APIs
{
    [Authorize(Roles = "Administrateur")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController : BaseController
    {
        
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetUsersQuery()));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpdatePermission/{id}")]
        public async Task<IActionResult> UpdatePermission(string id, [FromBody]string[] userRoles)
        {
            try
            {
                await Mediator.Send(new UpdateUserRolesCommand
                {
                    UserId = id,
                    Roles = userRoles
                });

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}