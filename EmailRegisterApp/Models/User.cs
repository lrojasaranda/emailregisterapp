using EmailRegisterApp.DB.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailRegisterApp.Models
{
    public class User
    {
        public User()
        {
            Emails = new List<UserEmail>();
        }
        [Required(ErrorMessage ="El campo Nombre es requerido.")]
        [Display(Name="Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo Correo es requerido.")]
        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage = "Dirección de correo invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo Compañia es requerido.")]
        public string Company { get; set; }
        public List<UserEmail> Emails { get; set; }

        public UserEntity ToEntity() {
            return new UserEntity()
            {
                Name = Name,
                Email = Email,
                Company=Company,
                Emails= (from item in Emails select item.ToEntity()).ToList(),
            };
        }


    }

}