using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositorio;
using rrhhGestion.Models;
using rrhhGestion.Models.ViewModels;

namespace rrhhGestion.Controllers
{
    public class BaseController : Controller
    {
        private DatosEmpleadosEntities _db;

        private IRepositorio<ProjectViewModel, Proyecto> _repositorio;

        public IRepositorio<ProjectViewModel, Proyecto> RepositorioProyecto
        {
            get
            {
                if(_repositorio == null)
                {
                    _repositorio = new Repositorio<ProjectViewModel, Proyecto>(_db);

                }
                return _repositorio;
            }
        }

        public BaseController()
        {
            _db = new DatosEmpleadosEntities();
        }


    }
}