using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rrhhGestion.Models.ViewModels;

namespace rrhhGestion.Controllers
{
    public class ProjectsController : BaseController
    {
        // GET: Projects
        public ActionResult Index()
        {
            var data = RepositorioProyecto.Get();
            return View(data);
        }

        public ActionResult NuevoProyecto()
        {
            return View(new ProjectViewModel());
        }

        
    }
}