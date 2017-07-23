using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class operacionesController : Controller
    {
        //
        // GET: /organizador/lista_prospectos_clientes
        public ActionResult _index()
        {
            return View();
        }

        public ActionResult _otrabajoop()
        {
            return View();
        }
        public ActionResult _agentesop()
        {
            return View();
        }

        public ActionResult _polizasop()
        {
            return View();
        }

    }
    
}