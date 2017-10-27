using EmailRegisterApp.DB.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailRegisterApp.Models
{
    public class UserEmail
    {
        [Required(ErrorMessage = "El campo Email es requerido.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Dirección de correo invalido")]
        public string Email { get; set; }

        [Display(Name = "Motivo")]
        public string Motive { get; set; }

        public EmailEntity ToEntity()
        {
            return new EmailEntity() {Email=Email,Motive=Motive };
        }
    }
}