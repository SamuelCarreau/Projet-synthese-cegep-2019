using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers.ProfilOption
{
    [Authorize(Roles = "Administrateur")]
    public class ProfilOptionController : ViewControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}