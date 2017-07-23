#region Using

using System;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

#endregion

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: home/index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult underConst()
        {
            return View();
        }

        public ActionResult planeador()
        {
            return View();
        }

        // GET: home/inbox
        public ActionResult Inbox()
        {
            return View();
        }

        // GET: home/calendar
        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult _carga()
        {
            return View();
        }

        public ActionResult _agentes()
        {
            return View();
        }

        public ActionResult _msgcenter()
        {
            return View();
        }

        public ActionResult _perfil()
        {
            return View();
        }

        public ActionResult _directorio()
        {
            return View();
        }

        public ActionResult _biblioteca()
        {
            return View();
        }

        // GET: home/google-map
        public ActionResult GoogleMap()
        {
            return View();
        }

        // GET: home/widgets
        public ActionResult Widgets()
        {
            //[TEST] to initialize the theme setter
            //could be called via jQuery or somewhere...
            Settings.SetValue<string>("config:CurrentTheme", "smart-style-5");

            return View();
        }

        // GET: home/chat
        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult indexbck()
        {
            return View();
        }

        [HttpPost]
        public JsonResult anioTrab()
        {
            if (Session["anioTrab"] != null)
                return Json(new { anioT = Session["anioTrab"].ToString() });
            Session["anioTrab"] = DateTime.Now.Year.ToString();
            return Json(new { anioT = Session["anioTrab"].ToString() });
        }

        [HttpPost]
        public JsonResult cambiaIntUNeg(string intUneg)
        {
            if(intUneg != ""){
                Session["intUneg"] = intUneg;
                return Json(new { error = true });
            }
            return Json(new { error = false, mensaje = "Error al establecer el dato" });
        }

        [HttpPost]
        public JsonResult cambiaAnio(string intAnio)
        {
            if (intAnio != "")
            {
                Session["anioTrab"] = intAnio;
                return Json(new { error = true });
            }
            return Json(new { error = false, mensaje = "Error al establecer el dato" });
        }

        [HttpPost]
        public string cargaAvisos()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_AVISO2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", SqlDbType.Int);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters["ID"].Value = Session["intUneg"].ToString();
                cmd.Parameters["FECHA"].Value = DateTime.Now.ToShortDateString();
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                int count = 0;
                if (sqlDR.HasRows)
                {
                    string html1 = "<ol class='carousel-indicators'>";
                    string html2 = "<div class='carousel-inner'>";
                    while (sqlDR.Read())
                    {
                        if (count == 0)
                        {
                            html1 += "<li data-target='#myCarousel-2' data-slide-to='" + count.ToString() + "' class='active'></li>";
                            html2 += "<div class='item active'><img src='/Content/img/avisos/" + sqlDR.GetString(3).Trim() + "' alt=''><div class='carousel-caption caption-right'><h4>" + sqlDR.GetString(1).Trim() + " </h4></div></div>";
                        }
                        else
                        {
                            html1 += "<li data-target='#myCarousel-2' data-slide-to='" + count.ToString() + "' class=''></li>";
                            html2 += "<div class='item'><img src='/Content/img/avisos/" + sqlDR.GetString(3).Trim() + "' alt=''><div class='carousel-caption caption-left'><h4>" + sqlDR.GetString(1).Trim() + " </h4></div></div>";
                        }
                        count++;
                    }
                    html1 += "</ol>";
                    html2 += "</div><a class='left carousel-control' href='#myCarousel-2' data-slide='prev'> <span class='glyphicon glyphicon-chevron-left'></span> </a><a class='right carousel-control' href='#myCarousel-2' data-slide='next'> <span class='glyphicon glyphicon-chevron-right'></span> </a>";
                    con.Close();
                    return html1 + html2;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return "";
            }
        }
    }
}