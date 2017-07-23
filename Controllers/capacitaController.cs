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
    public class capacitaController : Controller
    {
        //
        // GET: /capacita/percapac
        public ActionResult percapac()
        {
            return View();
        }

        // GET: /capacita/percapac
        public ActionResult programaCap()
        {
            return View();
        }

        // GET: /capacita/listcapac
        public ActionResult listCapacitaciones()
        {
            return View();
        }

        public ActionResult _listCapacitaciones()
        {
            return View();
        }

        // GET: /capacita/listprospcapac
        public ActionResult listProspCapac()
        {
            return View();
        }

        public ActionResult _listProspCapac()
        {
            return View();
        }

        public ActionResult _expCapAgente()
        {
            return View();
        }

        public ActionResult _reportes()
        {
            return View();
        }
        public class GanttTask
        {
            public int id { get; set; }
            public string text { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public int duration { get; set; }
            public decimal progress { get; set; }
            public int order { get; set; }
            public string type { get; set; }
            public int parent { get; set; }
            public bool open { get; set; }
            public string color { get; set; }
            public string textColor { get; set; }
            public string strObserv { get; set; }
            public List<RFAtraccion> items { get; set; }
            public int esta { get; set; }
            public int intAnio { get; set; }
        }

        public class RFAtraccion
        {
            public int idPer { get; set; }
            public int idComp { get; set; }
            public int idRFA { get; set; }
            public int idFA { get; set; }
            public int idEsta { get; set; }
            public int idRFAPub { get; set; }
            public string fechaIni { get; set; }
            public string fechaPub { get; set; }
            public string texto { get; set; }
            public string observ { get; set; }
            public string imagen { get; set; }
            public bool boolActivo { get; set; }

        }

        public class ProgramaCapac
        {
            public int id { get; set; }
            public string titulo { get; set; }
            public string inicio { get; set; }
            public string fin { get; set; }
            public bool success { get; set; }
        }
        
        public class ModuloCapac
        {
            public int id { get; set; }
            public string titulo { get; set; }
            public string inicio { get; set; }
            public string fin { get; set; }
            public bool success { get; set; }
            public int duracion { get; set; }
            public int estatus { get; set; }
            public bool activo { get; set; }
        }

        public class TemaCapac
        {
            public int idMod { get; set; }
            public string tituloMod { get; set; }
            public string inicioMod { get; set; }
            public string finMod { get; set; }
            public bool successMod { get; set; }
            public string claveMod { get; set; }
            public bool activoMod { get; set; }
        }

        public JsonResult GetGanttData()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CAPPER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTEST", SqlDbType.Int);
                cmd.Parameters.Add("STRANIO", SqlDbType.Char);
                cmd.Parameters.Add("INTUNID", SqlDbType.Int);
                if (this.Request.QueryString["idPer"] == null)
                    cmd.Parameters["INTEST"].Value = 0;
                else
                    cmd.Parameters["INTEST"].Value = this.Request.QueryString["idPer"];
                cmd.Parameters["STRANIO"].Value = Session["anioTrab"].ToString();
                cmd.Parameters["INTUNID"].Value = Session["intUneg"].ToString();
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<GanttTask>();
                    while (sqlDR.Read())
                    {
                        dataJson.Add(new GanttTask { id = sqlDR.GetInt32(0), text = sqlDR.GetString(1).Trim(), start_date = sqlDR.GetDateTime(2).ToString("dd-MM-yyyy"), end_date = sqlDR.GetDateTime(3).ToString("dd-MM-yyyy"), color = sqlDR.GetString(4), textColor = "#ffffff" });
                    }
                    return Json(new { data = dataJson }, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo consultar la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        private string strVerifica(string strVerif)
        {
            if (strVerif == null)
                return "";
            return strVerif;
        }

        [HttpPost]
        public JsonResult guardarCapPeriodo(GanttTask ganttTask)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_IDUNEGOCIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = Session["intUneg"].ToString();
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                string strNome = "";
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        strNome = sqlDR.GetString(3).Trim();
                    }
                }
                con.Close();
                cmd = new SqlCommand("SELECT_CONSEC", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("NEMO", SqlDbType.VarChar);
                cmd.Parameters.Add("ANIO", SqlDbType.VarChar);
                cmd.Parameters.Add("ID", SqlDbType.Int);
                cmd.Parameters["NEMO"].Value = strNome;
                cmd.Parameters["ANIO"].Value = Session["anioTrab"].ToString();
                cmd.Parameters["ID"].Value = 2;
                con.Open();
                sqlDR = cmd.ExecuteReader();
                int intConsec = 1;
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        intConsec = sqlDR.GetInt32(0);
                    }
                }
                con.Close();
                if (intConsec == 1)
                {
                    cmd = new SqlCommand("INSERT_CONSEC", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("NEMO", SqlDbType.VarChar);
                    cmd.Parameters.Add("ANIO", SqlDbType.VarChar);
                    cmd.Parameters.Add("ID", SqlDbType.Int);
                    cmd.Parameters["NEMO"].Value = strNome;
                    cmd.Parameters["ANIO"].Value = Session["anioTrab"].ToString();
                    cmd.Parameters["ID"].Value = 2;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    cmd = new SqlCommand("UPDATE_CONSEC", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("NEMO", SqlDbType.VarChar);
                    cmd.Parameters.Add("ANIO", SqlDbType.VarChar);
                    cmd.Parameters.Add("ID", SqlDbType.Int);
                    cmd.Parameters["NEMO"].Value = strNome;
                    cmd.Parameters["ANIO"].Value = Session["anioTrab"].ToString();
                    cmd.Parameters["ID"].Value = 2;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                string strNewClave = "CA-" + strNome + "-" + intConsec.ToString("00") + "-" + Session["anioTrab"].ToString();
                cmd = new SqlCommand("INSERT_CPERIODO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CPERIODO_DESCRIPCION", SqlDbType.VarChar);
                cmd.Parameters.Add("CPERIODO_FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("CPERIODO_FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("CPERIODO_OBSERVACION", SqlDbType.Text);
                cmd.Parameters.Add("CPERIODO_ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("CPERIODO_FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("CPERIODO_REALIZO", SqlDbType.Int);
                cmd.Parameters.Add("RPESTATUS_ID", SqlDbType.Int);
                cmd.Parameters.Add("ATRABAJO_ID", SqlDbType.Int);
                cmd.Parameters.Add("UNEGOCIO_ID", SqlDbType.Int);
                cmd.Parameters.Add("CPERIODO_CLAVE", SqlDbType.VarChar);
                cmd.Parameters["CPERIODO_DESCRIPCION"].Value = ganttTask.text.ToUpper();
                cmd.Parameters["CPERIODO_FECHAINICIO"].Value = ganttTask.start_date;
                cmd.Parameters["CPERIODO_FECHAFIN"].Value = ganttTask.end_date;
                cmd.Parameters["CPERIODO_OBSERVACION"].Value = strVerifica(ganttTask.strObserv).ToUpper();
                cmd.Parameters["CPERIODO_ACTIVO"].Value = true;
                cmd.Parameters["CPERIODO_FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["CPERIODO_REALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["RPESTATUS_ID"].Value = ganttTask.duration;
                cmd.Parameters["ATRABAJO_ID"].Value = ganttTask.intAnio;
                cmd.Parameters["UNEGOCIO_ID"].Value = Session["intUneg"].ToString();
                cmd.Parameters["CPERIODO_CLAVE"].Value = strNewClave;
                con.Open();
                int intID = (Int32)(cmd.ExecuteScalar());
                con.Close();
                return Json(new { success = true, mensaje = strNewClave });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult actualizaCapPeriodo(GanttTask ganttTask)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_CPERIODO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CPERIODO_ID", SqlDbType.Int);
                cmd.Parameters.Add("CPERIODO_DESCRIPCION", SqlDbType.VarChar);
                cmd.Parameters.Add("CPERIODO_FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("CPERIODO_FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("CPERIODO_OBSERVACION", SqlDbType.Text);
                cmd.Parameters.Add("CPERIODO_ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("CPERIODO_FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("CPERIODO_REALIZO", SqlDbType.Int);
                cmd.Parameters.Add("RPESTATUS_ID", SqlDbType.Int);
                cmd.Parameters["CPERIODO_ID"].Value = ganttTask.id;
                cmd.Parameters["CPERIODO_DESCRIPCION"].Value = ganttTask.text.ToUpper();
                cmd.Parameters["CPERIODO_FECHAINICIO"].Value = ganttTask.start_date;
                cmd.Parameters["CPERIODO_FECHAFIN"].Value = ganttTask.end_date;
                cmd.Parameters["CPERIODO_OBSERVACION"].Value = ganttTask.strObserv.ToUpper();
                cmd.Parameters["CPERIODO_ACTIVO"].Value = true;//ganttTask.open;
                cmd.Parameters["CPERIODO_FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["CPERIODO_REALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["RPESTATUS_ID"].Value = ganttTask.duration;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                /*foreach (RFAtraccion rfAtrac in ganttTask.items)
                {
                    if (rfAtrac.idRFAPub == 0)
                    {
                        cmd = new SqlCommand("INSERT_RPFUENTE_ATRACCION", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("INTPERIODO", SqlDbType.Int);
                        cmd.Parameters.Add("INTFUENTE", SqlDbType.Int);
                        cmd.Parameters.Add("STRFECHAALTA", SqlDbType.DateTime);
                        cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                        cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                        cmd.Parameters["INTPERIODO"].Value = ganttTask.id;
                        cmd.Parameters["INTFUENTE"].Value = rfAtrac.idFA;
                        cmd.Parameters["STRFECHAALTA"].Value = DateTime.Now.ToString();
                        cmd.Parameters["BOOLACTIVO"].Value = true;
                        cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                        con.Open();
                        int intIDFA = (Int32)(cmd.ExecuteScalar());
                        con.Close();
                        if (intIDFA > -1)
                        {
                            cmd = new SqlCommand("INSERT_RFAPUBLICACION", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("INTFAID", SqlDbType.Int);
                            cmd.Parameters.Add("INTCOMP", SqlDbType.Int);
                            cmd.Parameters.Add("INTFAEST", SqlDbType.Int);
                            cmd.Parameters.Add("STRFECHAENVIO", SqlDbType.DateTime);
                            cmd.Parameters.Add("STRFECHAPUB", SqlDbType.DateTime);
                            cmd.Parameters.Add("STRTEXTO", SqlDbType.Text);
                            cmd.Parameters.Add("STROBSERV", SqlDbType.Text);
                            cmd.Parameters.Add("STRIMA", SqlDbType.VarChar);
                            cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                            cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                            cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                            cmd.Parameters["INTFAID"].Value = intIDFA;
                            cmd.Parameters["INTCOMP"].Value = rfAtrac.idComp;
                            cmd.Parameters["INTFAEST"].Value = rfAtrac.idEsta;
                            cmd.Parameters["STRFECHAENVIO"].Value = rfAtrac.fechaIni;
                            cmd.Parameters["STRFECHAPUB"].Value = rfAtrac.fechaPub;
                            cmd.Parameters["STRTEXTO"].Value = rfAtrac.texto.ToUpper();
                            cmd.Parameters["STROBSERV"].Value = rfAtrac.observ.ToUpper();
                            cmd.Parameters["STRIMA"].Value = "";
                            cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                            cmd.Parameters["BOOLACTIVO"].Value = true;
                            cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        cmd = new SqlCommand("UPDATE_RPFUENTE_ATRACCION", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("INTID", SqlDbType.Int);
                        cmd.Parameters.Add("STRFECHAALTA", SqlDbType.DateTime);
                        cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                        cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                        cmd.Parameters["INTID"].Value = rfAtrac.idRFA;
                        cmd.Parameters["STRFECHAALTA"].Value = DateTime.Now.ToString();
                        cmd.Parameters["BOOLACTIVO"].Value = true;
                        cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        cmd = new SqlCommand("UPDATE_RFAPUBLICACION", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("INTID", SqlDbType.Int);
                        cmd.Parameters.Add("INTCOMP", SqlDbType.Int);
                        cmd.Parameters.Add("INTFAEST", SqlDbType.Int);
                        cmd.Parameters.Add("STRFECHAENVIO", SqlDbType.DateTime);
                        cmd.Parameters.Add("STRFECHAPUB", SqlDbType.DateTime);
                        cmd.Parameters.Add("STRTEXTO", SqlDbType.Text);
                        cmd.Parameters.Add("STROBSERV", SqlDbType.Text);
                        cmd.Parameters.Add("STRIMA", SqlDbType.VarChar);
                        cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                        cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                        cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                        cmd.Parameters["INTID"].Value = rfAtrac.idRFAPub;
                        cmd.Parameters["INTCOMP"].Value = rfAtrac.idComp;
                        cmd.Parameters["INTFAEST"].Value = rfAtrac.idEsta;
                        cmd.Parameters["STRFECHAENVIO"].Value = rfAtrac.fechaIni;
                        cmd.Parameters["STRFECHAPUB"].Value = rfAtrac.fechaPub;
                        cmd.Parameters["STRTEXTO"].Value = rfAtrac.texto;
                        cmd.Parameters["STROBSERV"].Value = rfAtrac.observ;
                        cmd.Parameters["STRIMA"].Value = "";
                        cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                        cmd.Parameters["BOOLACTIVO"].Value = true;
                        cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }*/
                return Json(new { success = true });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult seleccionaCapPeriodo(RFAtraccion ganttTask)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CAPPERID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = ganttTask.idPer;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        return Json(new { success = true, strDescr = sqlDR.GetString(0), strObserv = sqlDR.GetString(1), strInicio = sqlDR.GetDateTime(2).ToShortDateString(), strFin = sqlDR.GetDateTime(3).ToShortDateString(), intEstatus = sqlDR.GetInt32(4), boolActivo = sqlDR.GetBoolean(5) });
                    }
                }
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo consultar la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }

        }

        [HttpPost]
        public JsonResult actualizaFechas(GanttTask ganttTask)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_CAPPERDRAG", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("STRFECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = ganttTask.id;
                cmd.Parameters["STRFECHAINI"].Value = DateTime.Parse(ganttTask.start_date).ToShortDateString();
                cmd.Parameters["STRFECHAFIN"].Value = DateTime.Parse(ganttTask.end_date).ToShortDateString();
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verProgramaCapac(ProgramaCapac capac)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CAPPROGRAMA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDPER", SqlDbType.Int);
                cmd.Parameters["IDPER"].Value = capac.id;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<ProgramaCapac> datos = new List<ProgramaCapac>();
                    while (sqlDR.Read())
                    {
                        datos.Add(new ProgramaCapac { success = true, id = sqlDR.GetInt32(0), titulo = sqlDR.GetString(1), inicio = sqlDR.GetDateTime(3).ToString("dd/MM/yyyy"), fin = sqlDR.GetDateTime(4).ToString("dd/MM/yyyy") });
                    }
                    return Json(datos, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "Error al consultar la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verModuloPrograma(ModuloCapac capac)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_PROGMODULO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDPRO", SqlDbType.Int);
                cmd.Parameters["IDPRO"].Value = capac.id;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<ModuloCapac> datos = new List<ModuloCapac>();
                    while (sqlDR.Read())
                    {
                        datos.Add(new ModuloCapac { success = true, id = sqlDR.GetInt32(0), estatus = sqlDR.GetInt32(1), titulo = sqlDR.GetString(2), inicio = sqlDR.GetDateTime(3).ToString("dd/MM/yyyy"), fin = sqlDR.GetDateTime(4).ToString("dd/MM/yyyy"), duracion = sqlDR.GetInt32(5), activo = sqlDR.GetBoolean(6) });
                    }
                    return Json(datos, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "Error al consultar la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult guardarModuloPrograma(ModuloCapac capac)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_PROGMODULO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CPPROGRAMA_ID", SqlDbType.Int);
                cmd.Parameters.Add("CMESTATUS_ID", SqlDbType.Int);
                cmd.Parameters.Add("CPMODULO_DESCRIPCION", SqlDbType.VarChar);
                cmd.Parameters.Add("CPMODULO_FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("CPMODULO_FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("CPMODULO_DURACION", SqlDbType.Int);
                cmd.Parameters.Add("CPMODULO_ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("CPMODULO_FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("CPMODULO_REALIZO", SqlDbType.Int);
                cmd.Parameters["CPPROGRAMA_ID"].Value = capac.id;
                cmd.Parameters["CMESTATUS_ID"].Value = capac.estatus;
                cmd.Parameters["CPMODULO_DESCRIPCION"].Value = capac.titulo.ToUpper();
                cmd.Parameters["CPMODULO_FECHAINICIO"].Value = capac.inicio;
                cmd.Parameters["CPMODULO_FECHAFIN"].Value = capac.fin;
                cmd.Parameters["CPMODULO_DURACION"].Value = capac.duracion;
                cmd.Parameters["CPMODULO_ACTIVO"].Value = capac.activo;
                cmd.Parameters["CPMODULO_FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["CPMODULO_REALIZO"].Value = Session["intID"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true});
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult actualizarModuloPrograma(ModuloCapac capac)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_PROGMODULO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CPMODULO_ID", SqlDbType.Int);
                cmd.Parameters.Add("CMESTATUS_ID", SqlDbType.Int);
                cmd.Parameters.Add("CPMODULO_DESCRIPCION", SqlDbType.VarChar);
                cmd.Parameters.Add("CPMODULO_FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("CPMODULO_FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("CPMODULO_DURACION", SqlDbType.Int);
                cmd.Parameters.Add("CPMODULO_ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("CPMODULO_FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("CPMODULO_REALIZO", SqlDbType.Int);
                cmd.Parameters["CPMODULO_ID"].Value = capac.id;
                cmd.Parameters["CMESTATUS_ID"].Value = capac.estatus;
                cmd.Parameters["CPMODULO_DESCRIPCION"].Value = capac.titulo.ToUpper();
                cmd.Parameters["CPMODULO_FECHAINICIO"].Value = capac.inicio;
                cmd.Parameters["CPMODULO_FECHAFIN"].Value = capac.fin;
                cmd.Parameters["CPMODULO_DURACION"].Value = capac.duracion;
                cmd.Parameters["CPMODULO_ACTIVO"].Value = capac.activo;
                cmd.Parameters["CPMODULO_FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["CPMODULO_REALIZO"].Value = Session["intID"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verTemaModulo(TemaCapac capac)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_TEMAMODULO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                cmd.Parameters["IDMOD"].Value = capac.idMod;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<TemaCapac> datos = new List<TemaCapac>();
                    while (sqlDR.Read())
                    {
                        datos.Add(new TemaCapac { successMod = true, idMod = sqlDR.GetInt32(0), claveMod = sqlDR.GetString(2), tituloMod = sqlDR.GetString(1), inicioMod = sqlDR.GetDateTime(3).ToString("dd/MM/yyyy"), finMod = sqlDR.GetDateTime(4).ToString("dd/MM/yyyy"), activoMod = sqlDR.GetBoolean(5) });
                    }
                    return Json(datos, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "Error al consultar la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult guardarTemaModulo(TemaCapac capac)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_TEMAMODULO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CPMODULO_ID", SqlDbType.Int);
                cmd.Parameters.Add("CPTEMA_DESCRIPCION", SqlDbType.VarChar);
                cmd.Parameters.Add("CPTEMA_FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("CPTEMA_FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("CPTEMA_CLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("CPTEMA_ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("CPTEMA_FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("CPTEMA_REALIZO", SqlDbType.Int);
                cmd.Parameters["CPMODULO_ID"].Value = capac.idMod;
                cmd.Parameters["CPTEMA_DESCRIPCION"].Value = capac.tituloMod.ToUpper();
                cmd.Parameters["CPTEMA_FECHAINICIO"].Value = capac.inicioMod;
                cmd.Parameters["CPTEMA_FECHAFIN"].Value = capac.finMod;
                cmd.Parameters["CPTEMA_CLAVE"].Value = capac.claveMod.ToUpper();
                cmd.Parameters["CPTEMA_ACTIVO"].Value = capac.activoMod;
                cmd.Parameters["CPTEMA_FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["CPTEMA_REALIZO"].Value = Session["intID"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult actualizarTemaModulo(TemaCapac capac)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_TEMAMODULO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CPTEMA_ID", SqlDbType.Int);
                cmd.Parameters.Add("CPTEMA_DESCRIPCION", SqlDbType.VarChar);
                cmd.Parameters.Add("CPTEMA_FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("CPTEMA_FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("CPTEMA_CLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("CPTEMA_ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("CPTEMA_FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("CPTEMA_REALIZO", SqlDbType.Int);
                cmd.Parameters["CPTEMA_ID"].Value = capac.idMod;
                cmd.Parameters["CPTEMA_DESCRIPCION"].Value = capac.tituloMod.ToUpper();
                cmd.Parameters["CPTEMA_FECHAINICIO"].Value = capac.inicioMod;
                cmd.Parameters["CPTEMA_FECHAFIN"].Value = capac.finMod;
                cmd.Parameters["CPTEMA_CLAVE"].Value = capac.claveMod.ToUpper();
                cmd.Parameters["CPTEMA_ACTIVO"].Value = capac.activoMod;
                cmd.Parameters["CPTEMA_FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["CPTEMA_REALIZO"].Value = Session["intID"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verPeriodos()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_PERCAP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTEST", SqlDbType.Int);
                cmd.Parameters.Add("STRANIO", SqlDbType.Char);
                cmd.Parameters.Add("INTUNID", SqlDbType.Int);
                cmd.Parameters["INTEST"].Value = 0;
                cmd.Parameters["STRANIO"].Value = Session["anioTrab"].ToString();
                cmd.Parameters["INTUNID"].Value = Session["intUneg"].ToString();
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<GanttTask>();
                    while (sqlDR.Read())
                    {
                        dataJson.Add(new GanttTask { id = sqlDR.GetInt32(0), text = sqlDR.GetString(6).Trim() });
                    }
                    con.Close();
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No hay periodos de capacitacion registrados, o no se pudo consultar la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }
	}
}