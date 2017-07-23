using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class calendarioController : Controller
    {
        //
        // GET: /calendario/
        public ActionResult calendario()
        {
            return View();
        }

        // GET: /calendario/
        public ActionResult calcapacita()
        {
            return View();
        }

        public ActionResult _calcapacita()
        {
            return View();
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

        public class datosCal
        {
            public string id { get; set; }
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string description { get; set; }
            public string backgroundColor { get; set; }
            public string icon { get; set; }
            public bool allDay { get; set; }
            public int idPer { get; set; }
            public int idEst { get; set; }
            public int idTEv { get; set; }
            public bool editable { get; set; }
            public int agente { get; set; }
            public int opc { get; set; }
            public string horaIni { get; set; }
            public string horaFin { get; set; }
        }

        private string strVerifica(string strVerif)
        {
            if (strVerif == null)
                return "";
            return strVerif;
        }

        [HttpPost]
        public JsonResult veretapas(int idPer)
        {
            List<datosCal> datos = new List<datosCal>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT_RECPERID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("INTID", SqlDbType.Int);
            cmd.Parameters["INTID"].Value = idPer;
            con.Open();
            SqlDataReader sqlDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "PER" + idPer.ToString(), title = sqlDR.GetString(0), description = sqlDR.GetString(1), start = sqlDR.GetDateTime(2).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), backgroundColor = sqlDR.GetString(6), allDay = true, editable = false });
                }
            }
            sqlDR.Close();
            //con.Close();
            cmd = new SqlCommand("SELECT_PERETAPA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("INTID", SqlDbType.Int);
            cmd.Parameters["INTID"].Value = idPer;
            //con.Open();
            sqlDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "ETA" + sqlDR.GetInt32(0).ToString(), idEst = sqlDR.GetInt32(1), idTEv = sqlDR.GetInt32(2), title = sqlDR.GetString(3), description = sqlDR.GetString(4), start = sqlDR.GetDateTime(5).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(6).ToString("yyyy-MM-dd HH:mm"), backgroundColor = sqlDR.GetString(7), icon = sqlDR.GetString(8), allDay = sqlDR.GetBoolean(9), editable = true });
                }
            }
            sqlDR.Close();
            
            cmd = new SqlCommand("SELECT_CITAAGENTE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("IDPER", SqlDbType.Int);
            cmd.Parameters["IDPER"].Value = idPer;

            sqlDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CAG" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(4), description = sqlDR.GetString(5), start = sqlDR.GetDateTime(1).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(2).ToString("yyyy-MM-dd HH:mm"), allDay = false, editable = true, backgroundColor = sqlDR.GetString(6) });
                }
            }
            sqlDR.Close();
            cmd = new SqlCommand("SELECT_CITACLIENTE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
            cmd.Parameters["STRCLAVE"].Value = Session["strClaveAg"].ToString();
            
            sqlDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CLL" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(3).AddMinutes(30).ToString("yyyy-MM-dd HH:mm"), allDay = false, editable = true, backgroundColor = sqlDR.GetString(5) });
                }
            }
            sqlDR.Close();
            cmd = new SqlCommand("SELECT_CITAPERSONAL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
            cmd.Parameters["CLAVE"].Value = Session["strClaveAg"].ToString().Trim();

            sqlDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CPE" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(4).AddMinutes(30).ToString("yyyy-MM-dd HH:mm"), allDay = sqlDR.GetBoolean(5), editable = true, backgroundColor = sqlDR.GetString(6) });
                }
            }
            sqlDR.Close();
            con.Close();
            return Json(datos);
        }

        [HttpPost]
        public JsonResult veretapasall()
        {
            List<datosCal> datos = new List<datosCal>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT_RECPERALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("STRANIO", SqlDbType.Char);
            cmd.Parameters.Add("INTUNID", SqlDbType.Int);
            cmd.Parameters["STRANIO"].Value = Session["anioTrab"].ToString();
            cmd.Parameters["INTUNID"].Value = Session["intUneg"].ToString();
            con.Open();
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "PER" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(4).ToString("yyyy-MM-dd HH:mm"), backgroundColor = sqlDR.GetString(7), allDay = true, editable = false });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_PERETAPAALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("STRANIO", SqlDbType.Char);
            cmd.Parameters.Add("INTUNID", SqlDbType.Int);
            cmd.Parameters["STRANIO"].Value = Session["anioTrab"].ToString();
            cmd.Parameters["INTUNID"].Value = Session["intUneg"].ToString();
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "ETA" + sqlDR.GetInt32(0).ToString(), idEst = sqlDR.GetInt32(1), idTEv = sqlDR.GetInt32(2), title = sqlDR.GetString(3), description = sqlDR.GetString(4), start = sqlDR.GetDateTime(5).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(6).ToString("yyyy-MM-dd HH:mm"), backgroundColor = sqlDR.GetString(7), icon = sqlDR.GetString(8), allDay = sqlDR.GetBoolean(9), editable = true });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_CITAAGENTEALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("STRANIO", SqlDbType.Char);
            cmd.Parameters.Add("INTUNID", SqlDbType.Int);
            cmd.Parameters["STRANIO"].Value = Session["anioTrab"].ToString();
            cmd.Parameters["INTUNID"].Value = Session["intUneg"].ToString();
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CAG" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(4), description = sqlDR.GetString(5), start = sqlDR.GetDateTime(1).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(2).ToString("yyyy-MM-dd HH:mm"), allDay = false, editable = true, backgroundColor = sqlDR.GetString(6) });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_CITACLIENTE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
            cmd.Parameters["STRCLAVE"].Value = Session["strClaveAg"].ToString().Trim();
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CLL" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(3).AddMinutes(30).ToString("yyyy-MM-dd HH:mm"), allDay = false, editable = true, backgroundColor = sqlDR.GetString(5) });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_CITAPERSONAL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
            cmd.Parameters["CLAVE"].Value = Session["strClaveAg"].ToString().Trim();
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CPE" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(4).AddMinutes(30).ToString("yyyy-MM-dd HH:mm"), allDay = sqlDR.GetBoolean(5), editable = true, backgroundColor = sqlDR.GetString(6) });
                }
            }
            con.Close();
            return Json(datos);
        }

        [HttpPost]
        public JsonResult veretapas2(int idPer)
        {
            List<datosCal> datos = new List<datosCal>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT_CAPPERID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("INTID", SqlDbType.Int);
            cmd.Parameters["INTID"].Value = idPer;
            con.Open();
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "PER" + idPer.ToString(), title = sqlDR.GetString(0), description = sqlDR.GetString(1), start = sqlDR.GetDateTime(2).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), backgroundColor = sqlDR.GetString(6), allDay = true, editable = false });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_CAPETAPA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("INTID", SqlDbType.Int);
            cmd.Parameters["INTID"].Value = idPer;
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "ETA" + sqlDR.GetInt32(0).ToString(), idEst = sqlDR.GetInt32(1), idTEv = sqlDR.GetInt32(2), title = sqlDR.GetString(3), description = sqlDR.GetString(4), start = sqlDR.GetDateTime(5).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(6).ToString("yyyy-MM-dd HH:mm"), backgroundColor = sqlDR.GetString(7), icon = sqlDR.GetString(8), allDay = sqlDR.GetBoolean(9), editable = true });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_CAPPROGRAMA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("IDPER", SqlDbType.Int);
            cmd.Parameters["IDPER"].Value = idPer;
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CAG" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(4).ToString("yyyy-MM-dd HH:mm"), allDay = false, editable = true, backgroundColor = sqlDR.GetString(5), icon = sqlDR.GetString(6) });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_CAPAGENTE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("IDPER", SqlDbType.Int);
            cmd.Parameters["IDPER"].Value = idPer;
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CLL" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(5), start = sqlDR.GetDateTime(2).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(2).AddMinutes(30).ToString("yyyy-MM-dd HH:mm"), allDay = false, editable = false, backgroundColor = "#b261e8" });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_CITAPERSONAL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
            cmd.Parameters["CLAVE"].Value = Session["strClaveAg"].ToString().Trim();
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CPE" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(4).AddMinutes(30).ToString("yyyy-MM-dd HH:mm"), allDay = sqlDR.GetBoolean(5), editable = true, backgroundColor = sqlDR.GetString(6) });
                }
            }
            con.Close();
            return Json(datos);
        }

        [HttpPost]
        public JsonResult veretapasall2()
        {
            List<datosCal> datos = new List<datosCal>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT_CAPPERALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("STRANIO", SqlDbType.Char);
            cmd.Parameters.Add("INTUNID", SqlDbType.Int);
            cmd.Parameters["STRANIO"].Value = Session["anioTrab"].ToString();
            cmd.Parameters["INTUNID"].Value = Session["intUneg"].ToString();
            con.Open();
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "PER" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(4).ToString("yyyy-MM-dd HH:mm"), backgroundColor = sqlDR.GetString(7), allDay = true, editable = false });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_CAPETAPAALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("STRANIO", SqlDbType.Char);
            cmd.Parameters.Add("INTUNID", SqlDbType.Int);
            cmd.Parameters["STRANIO"].Value = Session["anioTrab"].ToString();
            cmd.Parameters["INTUNID"].Value = Session["intUneg"].ToString();
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "ETA" + sqlDR.GetInt32(0).ToString(), idEst = sqlDR.GetInt32(1), idTEv = sqlDR.GetInt32(2), title = sqlDR.GetString(3), description = sqlDR.GetString(4), start = sqlDR.GetDateTime(5).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(6).ToString("yyyy-MM-dd HH:mm"), backgroundColor = sqlDR.GetString(7), icon = sqlDR.GetString(8), allDay = sqlDR.GetBoolean(9), editable = true });
                }
            }
            con.Close();
            cmd = new SqlCommand("SELECT_CAPPROGRAMAALL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("STRANIO", SqlDbType.Char);
            cmd.Parameters.Add("INTUNID", SqlDbType.Int);
            cmd.Parameters["STRANIO"].Value = Session["anioTrab"].ToString();
            cmd.Parameters["INTUNID"].Value = Session["intUneg"].ToString();
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CAG" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(4), description = sqlDR.GetString(5), start = sqlDR.GetDateTime(1).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(2).ToString("yyyy-MM-dd HH:mm"), allDay = false, editable = true, backgroundColor = sqlDR.GetString(6) });
                }
            }
            con.Close();
            /*cmd = new SqlCommand("SELECT_CITACLIENTE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
            cmd.Parameters["STRCLAVE"].Value = Session["strClaveAg"].ToString().Trim();
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CLL" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(3).AddMinutes(30).ToString("yyyy-MM-dd HH:mm"), allDay = false, editable = true, backgroundColor = sqlDR.GetString(5) });
                }
            }
            con.Close();*/
            cmd = new SqlCommand("SELECT_CITAPERSONAL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
            cmd.Parameters["CLAVE"].Value = Session["strClaveAg"].ToString().Trim();
            con.Open();
            sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "CPE" + sqlDR.GetInt32(0).ToString(), title = sqlDR.GetString(1), description = sqlDR.GetString(2), start = sqlDR.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(4).AddMinutes(30).ToString("yyyy-MM-dd HH:mm"), allDay = sqlDR.GetBoolean(5), editable = true, backgroundColor = sqlDR.GetString(6) });
                }
            }
            con.Close();
            return Json(datos);
        }

        [HttpPost]
        public JsonResult guardaEtapas(datosCal datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_PERETAPA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTIDESTATUS", SqlDbType.Int);
                cmd.Parameters.Add("INTPERIODO", SqlDbType.Int);
                cmd.Parameters.Add("INTIDTEVEN", SqlDbType.Int);
                cmd.Parameters.Add("STRDESCR", SqlDbType.VarChar);
                cmd.Parameters.Add("STRTITULO", SqlDbType.VarChar);
                cmd.Parameters.Add("STRFECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTIDESTATUS"].Value = datCal.idEst;
                cmd.Parameters["INTPERIODO"].Value = datCal.idPer;
                cmd.Parameters["INTIDTEVEN"].Value = datCal.idTEv;
                cmd.Parameters["STRDESCR"].Value = datCal.description.ToUpper();
                cmd.Parameters["STRTITULO"].Value = datCal.title.ToUpper();
                cmd.Parameters["STRFECHAINI"].Value = datCal.start;
                cmd.Parameters["STRFECHAFIN"].Value = datCal.end;
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["INTOPC"].Value = datCal.opc;
                con.Open();
                int intID = (Int32)(cmd.ExecuteScalar());
                con.Close();
                if (intID > -1)
                    return Json(new { success = true });
                else
                    return Json(new { success = false, mensaje = "No se pudo guardar" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult actualizaEtapas(datosCal datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_PERETAPA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("INTIDESTATUS", SqlDbType.Int);
                cmd.Parameters.Add("INTIDTEVEN", SqlDbType.Int);
                cmd.Parameters.Add("STRDESCR", SqlDbType.VarChar);
                cmd.Parameters.Add("STRTITULO", SqlDbType.VarChar);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = datCal.opc;
                cmd.Parameters["INTID"].Value = int.Parse(datCal.id.Remove(0, 3));
                cmd.Parameters["INTIDESTATUS"].Value = datCal.idEst;
                cmd.Parameters["INTIDTEVEN"].Value = datCal.idTEv;
                cmd.Parameters["STRDESCR"].Value = datCal.description.ToUpper();
                cmd.Parameters["STRTITULO"].Value = datCal.title.ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["STRFECHAINI"].Value = datCal.start;
                cmd.Parameters["STRFECHAFIN"].Value = datCal.end;
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
        public JsonResult actualizaEtapas2(datosCal datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string cmdProc = "";
                if (datCal.agente == 1)
                    cmdProc = "UPDATE_CAPETAPA2";
                else if (datCal.agente == 2)
                    cmdProc = "UPDATE_CITAAGENTE2";
                else if (datCal.agente == 3)
                    cmdProc = "UPDATE_CLIENTECITA2";
                else if (datCal.agente == 4)
                    cmdProc = "UPDATE_CITAPERSONAL2";
                SqlCommand cmd = new SqlCommand(cmdProc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("STRFECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("BOOLALLDAY", SqlDbType.Bit);
                cmd.Parameters["INTID"].Value = int.Parse(datCal.id.Remove(0, 3));
                cmd.Parameters["STRFECHAINI"].Value = datCal.start;
                cmd.Parameters["STRFECHAFIN"].Value = datCal.end;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["BOOLALLDAY"].Value = !datCal.allDay;
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

        public class citaAgente
        {
            public int idTipoEval { get; set; }
            public string fechaInicio { get; set; }
            public string fechaFin { get; set; }
            public string horaInicio { get; set; }
            public string horaFin { get; set; }
            public string tiempoCita { get; set; }
            public string toleranciaCita { get; set; }
            public string observaCita { get; set; }
            public int anioTrabajo { get; set; }
            public int idPeriodo { get; set; }
            public int idEstatus { get; set; }
            public int idCita { get; set; }
            public string claveAgente { get; set; }
            public decimal decCalif { get; set; }
            public bool boolSelect { get; set; }
            public int idCriterio { get; set; }
            public string reactivo { get; set; }
            public string idEval { get; set; }
            public string idCri { get; set; }
            public List<citaAgente> criterios { get; set; }
        }

        [HttpPost]
        public JsonResult guardarCitaAgente(citaAgente datosCita)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_CITAAGENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("FECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("TIEMPOCITA", SqlDbType.Int);
                cmd.Parameters.Add("TOLERANCIACITA", SqlDbType.Int);
                cmd.Parameters.Add("FECHAALTA", SqlDbType.DateTime);
                cmd.Parameters.Add("OBSERVACITA", SqlDbType.Text);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("REALIZOALTA", SqlDbType.Int);
                cmd.Parameters.Add("IDPERIODO", SqlDbType.Int);
                cmd.Parameters.Add("IDUNEGOCIO", SqlDbType.Int);
                cmd.Parameters.Add("IDANIOTRAB", SqlDbType.Int);
                cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                cmd.Parameters.Add("IDESTA", SqlDbType.Int);
                cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                cmd.Parameters["FECHAINI"].Value = DateTime.Parse(datosCita.fechaInicio + " " + datosCita.horaInicio).ToString();
                cmd.Parameters["FECHAFIN"].Value = DateTime.Parse(datosCita.fechaFin + " " + datosCita.horaFin).ToString();
                cmd.Parameters["TIEMPOCITA"].Value = datosCita.tiempoCita;
                cmd.Parameters["TOLERANCIACITA"].Value = datosCita.toleranciaCita;
                cmd.Parameters["FECHAALTA"].Value = DateTime.Now.ToString();
                cmd.Parameters["OBSERVACITA"].Value = strVerifica(datosCita.observaCita).ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["REALIZOALTA"].Value = Session["intID"].ToString();
                cmd.Parameters["IDPERIODO"].Value = datosCita.idPeriodo;
                cmd.Parameters["IDUNEGOCIO"].Value = Session["intUneg"].ToString();
                cmd.Parameters["IDANIOTRAB"].Value = datosCita.anioTrabajo;
                cmd.Parameters["IDTEVAL"].Value = datosCita.idTipoEval;
                cmd.Parameters["IDESTA"].Value = datosCita.idEstatus;
                cmd.Parameters["STRCLAVE"].Value = datosCita.claveAgente;
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
        public JsonResult actualizarCitaAgente(citaAgente datosCita)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_CITAAGENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("FECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("TIEMPOCITA", SqlDbType.Int);
                cmd.Parameters.Add("TOLERANCIACITA", SqlDbType.Int);
                cmd.Parameters.Add("FECHAALTA", SqlDbType.DateTime);
                cmd.Parameters.Add("OBSERVACITA", SqlDbType.Text);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("REALIZOALTA", SqlDbType.Int);
                cmd.Parameters.Add("IDESTA", SqlDbType.Int);
                cmd.Parameters.Add("IDCITA", SqlDbType.Int);
                cmd.Parameters["FECHAINI"].Value = DateTime.Parse(datosCita.fechaInicio + " " + datosCita.horaInicio).ToString();
                cmd.Parameters["FECHAFIN"].Value = DateTime.Parse(datosCita.fechaFin + " " + datosCita.horaFin).ToString();
                cmd.Parameters["TIEMPOCITA"].Value = datosCita.tiempoCita;
                cmd.Parameters["TOLERANCIACITA"].Value = datosCita.toleranciaCita;
                cmd.Parameters["FECHAALTA"].Value = DateTime.Now.ToString();
                cmd.Parameters["OBSERVACITA"].Value = strVerifica(datosCita.observaCita).ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["REALIZOALTA"].Value = Session["intID"].ToString();
                cmd.Parameters["IDESTA"].Value = datosCita.idEstatus;
                cmd.Parameters["IDCITA"].Value = datosCita.idCita;
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
        public JsonResult citaAgenteId(citaAgente datosCita)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CITAAGENTEID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDCITA", SqlDbType.Int);
                cmd.Parameters["IDCITA"].Value = datosCita.idCita;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<citaAgente>();
                    while (sqlDR.Read())
                    {
                        dataJson.Add(new citaAgente { idCita = sqlDR.GetInt32(0), idTipoEval = sqlDR.GetInt32(1), fechaInicio = sqlDR.GetDateTime(2).ToShortDateString(), fechaFin = sqlDR.GetDateTime(3).ToShortDateString(), horaInicio = sqlDR.GetDateTime(2).ToString("HH:mm"), horaFin = sqlDR.GetDateTime(3).ToString("HH:mm"), tiempoCita = sqlDR.GetInt32(4).ToString(), toleranciaCita = sqlDR.GetInt32(5).ToString(), observaCita = sqlDR.GetString(6), idEstatus = sqlDR.GetInt32(7) });
                    }
                    con.Close();
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
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
        public JsonResult guardarLlamadaCliente2(citaAgente datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_CLICONTLLAMADA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("IDESTATUS", SqlDbType.Int);
                cmd.Parameters.Add("STRDESCR", SqlDbType.VarChar);
                cmd.Parameters.Add("STROBSERV", SqlDbType.Text);
                cmd.Parameters.Add("STRFECHALLAMDA", SqlDbType.DateTime);
                cmd.Parameters.Add("BITACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters["STRCLAVE"].Value = datCal.claveAgente;
                cmd.Parameters["IDESTATUS"].Value = datCal.idEstatus;
                cmd.Parameters["STRDESCR"].Value = "";
                cmd.Parameters["STROBSERV"].Value = strVerifica(datCal.observaCita).ToUpper();
                cmd.Parameters["STRFECHALLAMDA"].Value = DateTime.Parse(datCal.fechaInicio + " " + datCal.horaInicio).ToString();
                cmd.Parameters["BITACTIVO"].Value = true;
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
        public JsonResult guardarLlamadaCliente(citaAgente datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_CLIENTECITA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CCLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("ACLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("TCITAID", SqlDbType.Int);
                cmd.Parameters.Add("FECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["CCLAVE"].Value = datCal.claveAgente;
                cmd.Parameters["ACLAVE"].Value = Session["strClaveAg"].ToString();
                cmd.Parameters["TCITAID"].Value = datCal.idEstatus;
                cmd.Parameters["FECHAINI"].Value = DateTime.Parse(datCal.fechaInicio + " " + datCal.horaInicio).ToString();
                cmd.Parameters["FECHAFIN"].Value =  DateTime.Parse(datCal.fechaInicio + " " + datCal.horaInicio).AddMinutes(30).ToString();
                cmd.Parameters["OBSERV"].Value = strVerifica(datCal.observaCita).ToUpper();
                cmd.Parameters["ACTIVO"].Value = true;
                cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
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
        public JsonResult actualizarLlamadaCliente(citaAgente datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_CLIENTECITA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("TCITAID", SqlDbType.Int);
                cmd.Parameters.Add("FECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = datCal.idCita;
                cmd.Parameters["TCITAID"].Value = datCal.idEstatus;
                cmd.Parameters["FECHAINI"].Value = DateTime.Parse(datCal.fechaInicio + " " + datCal.horaInicio).ToString();
                cmd.Parameters["FECHAFIN"].Value = DateTime.Parse(datCal.fechaInicio + " " + datCal.horaInicio).ToString();
                cmd.Parameters["OBSERV"].Value = strVerifica(datCal.observaCita).ToUpper();
                cmd.Parameters["ACTIVO"].Value = true;
                cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
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
        public JsonResult citaLlamadaId(citaAgente datosCita)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CITALLAMADAID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDLLAMADA", SqlDbType.Int);
                cmd.Parameters["IDLLAMADA"].Value = datosCita.idCita;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<citaAgente>();
                    while (sqlDR.Read())
                    {
                        dataJson.Add(new citaAgente { idCita = sqlDR.GetInt32(0), idEstatus = sqlDR.GetInt32(1), fechaInicio = sqlDR.GetDateTime(2).ToShortDateString(), fechaFin = sqlDR.GetDateTime(2).ToShortDateString(), horaInicio = sqlDR.GetDateTime(2).ToString("HH:mm"), horaFin = sqlDR.GetDateTime(2).ToString("HH:mm"), observaCita = sqlDR.GetString(3) });
                    }
                    con.Close();
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
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
        public JsonResult citaClienteId(citaAgente datosCita)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CLIENTECITAID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDCITA", SqlDbType.Int);
                cmd.Parameters["IDCITA"].Value = datosCita.idCita;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<citaAgente>();
                    while (sqlDR.Read())
                    {
                        dataJson.Add(new citaAgente { idCita = sqlDR.GetInt32(0), idEstatus = sqlDR.GetInt32(1), fechaInicio = sqlDR.GetDateTime(2).ToShortDateString(), fechaFin = sqlDR.GetDateTime(2).ToShortDateString(), horaInicio = sqlDR.GetDateTime(2).ToString("HH:mm"), horaFin = sqlDR.GetDateTime(2).ToString("HH:mm"), observaCita = sqlDR.GetString(3) });
                    }
                    con.Close();
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo consultar la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        public class EstadisticasLlamadas
        {
            public string label { get; set; }
            public int value { get; set; }
            public string color { get; set; }
            public string highlight { get; set; }
            public string fechaIni { get; set; }
            public string fechaFin { get; set; }
        }

        [HttpPost]
        public JsonResult verEstadisticasLlamadas(EstadisticasLlamadas estadLlam)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_ESTADLLAMADAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("FECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters["FECHAINI"].Value = DateTime.Parse(estadLlam.fechaIni + " 00:00:00");
                cmd.Parameters["FECHAFIN"].Value = DateTime.Parse(estadLlam.fechaFin + " 23:59:59");
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<EstadisticasLlamadas>();
                    while (sqlDR.Read())
                    {
                        string strColor = hex2rgba(sqlDR.GetString(2).Trim(), "0.9");
                        string strColorLight = hex2rgba(sqlDR.GetString(2).Trim(), "0.8");
                        dataJson.Add(new EstadisticasLlamadas { label = sqlDR.GetString(1), value = sqlDR.GetInt32(0), color = strColor , highlight = strColorLight });
                    }
                    sqlDR.Close();
                    con.Close();
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
                }
                sqlDR.Close();
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo contactar la base de datos o no existen registros" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        private string hex2rgba(string color, string alpha)
        {
            //string color = "#FF00FF"; //This would be a parameter
            if (color.StartsWith("#"))
                color = color.Remove(0, 1);
            byte r, g, b;
            if (color.Length == 3)
            {
                r = Convert.ToByte(color[0] + "" + color[0], 16);
                g = Convert.ToByte(color[1] + "" + color[1], 16);
                b = Convert.ToByte(color[2] + "" + color[2], 16);
            }
            else if (color.Length == 6)
            {
                r = Convert.ToByte(color[0] + "" + color[1], 16);
                g = Convert.ToByte(color[2] + "" + color[3], 16);
                b = Convert.ToByte(color[4] + "" + color[5], 16);
            }
            else
            {
                throw new ArgumentException("Hex color " + color + " is invalid.");
            }
            return "rgba(" + r.ToString() + "," + g.ToString() + "," + b.ToString() + "," + alpha + ")";
        }

        [HttpPost]
        public JsonResult borrarEvento(string strID)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string scmd = "";
                string tipo = strID.Remove(3,2);
                string id = strID.Remove(0,3);
                if (tipo == "ETA")
                    scmd = "DELETE FROM RPERIODO_ETAPA WHERE RETAPA_ID=" + id;
                else if (tipo == "CAG")
                    scmd = "DELETE FROM AGENTE_CITA WHERE ACITA_ID=" + id;
                else if(tipo == "CLL")
                    scmd = "DELETE FROM CLIENTE_CONTADOR_LLAMADA WHERE CCLLAMDA_ID=" + id;
                else if(tipo == "CPE")
                    scmd = "DELETE FROM CITA_PERSONAL_USUARIO WHERE CITAPERSONAL_ID=" + id;
                SqlCommand cmd = new SqlCommand(scmd, con);
                cmd.CommandType = CommandType.Text;
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
        public JsonResult programarPagos(datosCal datCal)
        {
            try
            {
                for (int i = 1; i <= datCal.agente; i++)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("INSERT_CALPAGO", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("POLID", SqlDbType.Int);
                    cmd.Parameters.Add("SEGUIID", SqlDbType.Int);
                    cmd.Parameters.Add("SEGEST", SqlDbType.Int);
                    cmd.Parameters.Add("DIACORTE", SqlDbType.Int);
                    cmd.Parameters.Add("FECHAPRO", SqlDbType.DateTime);
                    cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                    cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                    cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                    cmd.Parameters["POLID"].Value = datCal.id;
                    cmd.Parameters["SEGUIID"].Value = datCal.idTEv;
                    cmd.Parameters["SEGEST"].Value = datCal.idEst;
                    cmd.Parameters["DIACORTE"].Value = datCal.idPer;
                    string[] strFecha = datCal.start.Split(char.Parse("/"));
                    DateTime dateFecha = DateTime.Parse(datCal.idPer + "/" + strFecha[1] + "/" + strFecha[2]).AddMonths(i);
                    cmd.Parameters["FECHAPRO"].Value = dateFecha.ToShortDateString();
                    cmd.Parameters["ACTIVO"].Value = true;
                    cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return Json(new { success = true });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verProgramPagosPoliza(int idPol)
        {
            List<datosCal> datos = new List<datosCal>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT_PROGPAGOSPOL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("IDPOL", SqlDbType.Int);
            cmd.Parameters["IDPOL"].Value = idPol;
            con.Open();
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                int count = 1;
                while (sqlDR.Read())
                {
                    datos.Add(new datosCal { id = "PPP" + sqlDR.GetInt32(0).ToString(), title = "PAGO " + count, description = sqlDR.GetString(2), start = sqlDR.GetDateTime(1).ToString("yyyy-MM-dd HH:mm"), end = sqlDR.GetDateTime(1).ToString("yyyy-MM-dd HH:mm"), backgroundColor = sqlDR.GetString(3), allDay = true, editable = true });
                    count++;
                }
            }
            con.Close();
            return Json(datos);
        }

        [HttpPost]
        public JsonResult actualizarProgramPagosPoliza(datosCal datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string cmdProc = "UPDATE_PROGRPAGOPOLIZA2";
                SqlCommand cmd = new SqlCommand(cmdProc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("STRFECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = int.Parse(datCal.id.Remove(0, 3));
                cmd.Parameters["STRFECHAINI"].Value = datCal.start;
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
        public JsonResult verEstadisticaProgPagoPoliza(int idPol)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_GRAFIPAGOPOLIZA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = idPol;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<EstadisticasLlamadas>();
                    while (sqlDR.Read())
                    {
                        string strColor = hex2rgba(sqlDR.GetString(2).Trim(), "0.9");
                        string strColorLight = hex2rgba(sqlDR.GetString(2).Trim(), "0.8");
                        dataJson.Add(new EstadisticasLlamadas { label = sqlDR.GetString(1), value = sqlDR.GetInt32(0), color = strColor, highlight = strColorLight });
                    }
                    con.Close();
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo contactar la base de datos o no existen registros" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verProgrPagos(int idPol)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_PROGPAGOSPOLTABLA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDPOL", SqlDbType.Int);
                cmd.Parameters["IDPOL"].Value = idPol;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<datosCal>();
                    while (sqlDR.Read())
                    {
                        dataJson.Add(new datosCal { id = sqlDR.GetInt32(0).ToString(), start = sqlDR.GetDateTime(1).ToShortDateString(), title = sqlDR.GetString(2), description = "<i class='fa fa-circle' style='color:" + sqlDR.GetString(3) + "'></i>" });
                    }
                    con.Close();
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo contactar la base de datos o no existen registros" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult guardaCitaPersonal(datosCal datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_CITAPERSONAL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("TITULO", SqlDbType.VarChar);
                cmd.Parameters.Add("DESCRIPCION", SqlDbType.Text);
                cmd.Parameters.Add("FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("ALLDAY", SqlDbType.Bit);
                cmd.Parameters.Add("COLOR", SqlDbType.NChar);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["CLAVE"].Value = Session["strClaveAg"].ToString();
                cmd.Parameters["TITULO"].Value = datCal.title.ToUpper();
                cmd.Parameters["DESCRIPCION"].Value = strVerifica(datCal.description).ToUpper();
                cmd.Parameters["FECHAINICIO"].Value = DateTime.Parse(datCal.start).ToString();
                cmd.Parameters["FECHAFIN"].Value = DateTime.Parse(datCal.end).ToString();
                cmd.Parameters["ALLDAY"].Value = false;
                string strColor = strVerifica(datCal.backgroundColor).ToUpper();
                if(strColor == "")
                    strColor = "#FFFFFF";
                cmd.Parameters["COLOR"].Value = strColor;
                cmd.Parameters["ACTIVO"].Value = true;
                cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
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
        public JsonResult actualizaCitaPersonal(datosCal datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_CITAPERSONAL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("TITULO", SqlDbType.VarChar);
                cmd.Parameters.Add("DESCRIPCION", SqlDbType.Text);
                cmd.Parameters.Add("FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("ALLDAY", SqlDbType.Bit);
                cmd.Parameters.Add("COLOR", SqlDbType.NChar);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = datCal.id;
                cmd.Parameters["TITULO"].Value = datCal.title.ToUpper();
                cmd.Parameters["DESCRIPCION"].Value = datCal.description.ToUpper();
                cmd.Parameters["FECHAINICIO"].Value = DateTime.Parse(datCal.start).ToString();
                cmd.Parameters["FECHAFIN"].Value = DateTime.Parse(datCal.end).ToString();
                cmd.Parameters["ALLDAY"].Value = datCal.allDay;
                cmd.Parameters["COLOR"].Value = datCal.backgroundColor;
                cmd.Parameters["ACTIVO"].Value = true;
                cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
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
        public JsonResult actualizaCitaPersonal2(datosCal datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_CITAPERSONAL2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("ALLDAY", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = datCal.id;
                cmd.Parameters["FECHAINICIO"].Value = DateTime.Parse(datCal.start).ToString();
                cmd.Parameters["FECHAFIN"].Value = DateTime.Parse(datCal.end).ToString();
                cmd.Parameters["ALLDAY"].Value = datCal.allDay;
                cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
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
        public JsonResult verCitaPersonalID(datosCal datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CITAPERSONALID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = datCal.id;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var datCitaPersonal = new List<datosCal>();
                    while (sqlDR.Read())
                    {
                        datCitaPersonal.Add(new datosCal { title = sqlDR.GetString(0), description = sqlDR.GetString(1), start = sqlDR.GetDateTime(2).ToShortDateString(), end = sqlDR.GetDateTime(3).ToShortDateString(), horaIni = sqlDR.GetDateTime(2).ToString("HH:mm"), horaFin = sqlDR.GetDateTime(3).ToString("HH:mm"), backgroundColor = sqlDR.GetString(4) });
                    }
                    con.Close();
                    return Json(datCitaPersonal, JsonRequestBehavior.AllowGet);
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
        public JsonResult guardaPrograma(datosCal datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_CPERIODO_PROGRAMA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CPESTATUS_ID", SqlDbType.Int);
                cmd.Parameters.Add("CPERIODO_ID", SqlDbType.Int);
                cmd.Parameters.Add("CTEVENTO_ID", SqlDbType.Int);
                cmd.Parameters.Add("CPPROGRAMA_DESCRIPCION", SqlDbType.VarChar);
                cmd.Parameters.Add("CPPROGRAMA_TITULO", SqlDbType.VarChar);
                cmd.Parameters.Add("CPPROGRAMA_FECHAINICIO", SqlDbType.DateTime);
                cmd.Parameters.Add("CPPROGRAMA_FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("CPPROGRAMA_ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("CPPROGRAMA_FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("CPPROGRAMA_REALIZO", SqlDbType.Int);
                cmd.Parameters["CPESTATUS_ID"].Value = datCal.idEst;
                cmd.Parameters["CPERIODO_ID"].Value = datCal.idPer;
                cmd.Parameters["CTEVENTO_ID"].Value = datCal.idTEv;
                cmd.Parameters["CPPROGRAMA_DESCRIPCION"].Value = datCal.description.ToUpper();
                cmd.Parameters["CPPROGRAMA_TITULO"].Value = datCal.title.ToUpper();
                cmd.Parameters["CPPROGRAMA_FECHAINICIO"].Value = datCal.start;
                cmd.Parameters["CPPROGRAMA_FECHAFIN"].Value = datCal.end;
                cmd.Parameters["CPPROGRAMA_ACTIVO"].Value = true;
                cmd.Parameters["CPPROGRAMA_FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["CPPROGRAMA_REALIZO"].Value = Session["intID"].ToString();
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
        public JsonResult guardarCapacitacionAgente(citaAgente datCal)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_CAPAGENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CPPROGRAMA_ID", SqlDbType.Int);
                cmd.Parameters.Add("AGENTE_CLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("CCAPACITACION_FECHAPROGRAMADA", SqlDbType.DateTime);
                cmd.Parameters.Add("CCAPACITACION_ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("CCAPACITACION_FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("CCAPACITACION_REALIZO", SqlDbType.Int);
                cmd.Parameters["CPPROGRAMA_ID"].Value = datCal.idCita;
                cmd.Parameters["AGENTE_CLAVE"].Value = datCal.claveAgente;
                cmd.Parameters["CCAPACITACION_FECHAPROGRAMADA"].Value = datCal.fechaInicio;
                cmd.Parameters["CCAPACITACION_ACTIVO"].Value = true;
                cmd.Parameters["CCAPACITACION_FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["CCAPACITACION_REALIZO"].Value = Session["intID"].ToString();
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
	}
}