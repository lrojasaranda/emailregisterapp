using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailRegisterApp.DB.Entity
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public List<EmailEntity> Emails { get; set; }
    }
}