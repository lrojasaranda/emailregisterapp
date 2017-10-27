using EmailRegisterApp.DB;
using EmailRegisterApp.Helper.Exceptions;
using EmailRegisterApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailRegisterApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var model = new User();
            return View(model);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!user.Emails.Any())
                        throw new Exception("Debe registrar al menos un email");
                    var userPersistence = new UserPersistence();
                    userPersistence.Insert(user.ToEntity());
                    return Json(new { status = true, data = "Se ha registrado correctamente" });
                }
                else {
                    var errors = ModelState.Values.Where(w => w.Errors.Any());
                    throw new Exception(errors.Any() ? errors.First().Errors.First().ErrorMessage : string.Empty);
                }
            }
            catch(Exception ex)
            {
                if (ex.GetType().IsAssignableFrom(typeof(SaveException)))
                {
                    return Json(new { status =false , data = ex.Message });
                }
                return Json(new { status = false, data = "ha ocurrido un error" });
            }
        }
    }
}
