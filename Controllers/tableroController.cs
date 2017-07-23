#region
using System.Web.Mvc;
#endregion

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class tableroController : Controller
    {
        //
        // GET: /tablero/tablero
        public ActionResult Tablero()
        {
            return View();
        }

        public ActionResult _organizador()
        {
            return View();
        }
	}
}