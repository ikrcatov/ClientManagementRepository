using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientManagement.Models;

namespace ClientManagement.Controllers
{
    public class HomeController : Controller
    {
        Boolean admin = false;
        List<USER> clientList = new List<USER>();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Login(RegisterModel model)
        {
            var viewModel = new ClientModel
            {
                clients = clientList
            };

            String username = model.UserName;
            String password = model.Password;

            USER user = new USER();
            CLIENT_MANAGEMENTEntities context = new CLIENT_MANAGEMENTEntities();

            if (username != null && password != null)
            {
                user = (from singleUser in context.USER
                        where (singleUser.USERNAME == username && singleUser.PASSWORD == password)
                        select singleUser).FirstOrDefault();

                if (user != null)
                {
                    if (user.ID_USER_TYPE != null)
                    {
                        if (user.ID_USER_TYPE.Equals(1))
                            admin = true;
                    }
                }
            }

            /*Dohvati sve usere i grupe*/
            if (admin == true)
                return View("ClientList", viewModel);
            else
                return Json("notAdmin");
        }
    }
}
