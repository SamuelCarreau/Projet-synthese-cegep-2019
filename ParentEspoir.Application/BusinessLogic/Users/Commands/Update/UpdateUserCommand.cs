using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParentEspoir.Application
{
    public class UpdateUserCommand : IRequest
    {
        public string Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Nom d'utilisateur")]
        public string UserName { get; set; }
        [Display(Name = "Courriel")]
        public string Email { get; set; }
        public string PasswordToken { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }
    }
}
