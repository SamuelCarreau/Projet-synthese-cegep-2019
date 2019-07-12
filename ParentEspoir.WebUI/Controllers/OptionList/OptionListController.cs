using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentEspoir.Application;

namespace ParentEspoir.WebUI.Controllers.OptionList
{
    [Authorize(Roles = "Administrateur")]
    public class OptionListController : ViewControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}