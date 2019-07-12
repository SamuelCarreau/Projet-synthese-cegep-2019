using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParentEspoir.WebUI.Controllers
{
    public class ProfilOptionController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<GetAllProfileOptionModel>> GetAll()
        {
            return await Mediator.Send(new GetAllProfileOptionQuery());
        }
    }
}
