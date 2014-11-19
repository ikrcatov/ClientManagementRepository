﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ClientManagement.Models;

namespace ClientManagement.Controllers
{
    public class HomeController : Controller
    {
        Boolean admin = false;
        List<CLIENT> clientList = new List<CLIENT>();

        public ActionResult Index()
        {
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
            {
                getAllClientsForUser(context, user.Id);

                return Json(new
                {
                    success = true,
                    viewModel,
                    message = RenderPartialView("ClientList", viewModel)
                });
            }

            else
                return Json(new
                {
                    success = false
                });
        }

        private string RenderPartialView(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");
            }
            this.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
       
        private void getAllClientsForUser(CLIENT_MANAGEMENTEntities context, Int32 Id)
        {
            /*GET ALL USERS*/
            IQueryable<CLIENT> clientsQuery = from client in context.CLIENT
                                            where client.ID_USER == Id
                                            select client;

            CLIENT[] clients = new CLIENT[clientsQuery.Count()];

            for (int i = 0; i < clients.Length; i++)
                clients[i] = new CLIENT();

            int j = 0;

            foreach (var client in clientsQuery)
            {
                clients[j].NAME = client.NAME;
                clients[j].Id = client.Id;
                j++;
            }

            for (int i = 0; i < clients.Length; i++)
                clientList.Add(clients[i]);
        }
    }
}
