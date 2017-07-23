using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class catalogosController : Controller
    {
        //
        // GET: /catalogos/catalogos
        public ActionResult catalogos()
        {
            return View();
        }

        // GET: /catalogos/listaUsuarios
        public ActionResult listaUsuarios()
        {
            return View();
        }

        public ActionResult _listaUsuarios()
        {
            return View();
        }

        // GET: /catalogos/regUsuario
        public ActionResult regUsuario()
        {
            return View();
        }

        public ActionResult permisos()
        {
            return View();
        }

        public ActionResult _ajustes()
        {
            return View();
        }

        public ActionResult _propnodo()
        {
            return View();
        }

        public ActionResult _cfgorganizador()
        {
            return View();
        }

        public ActionResult _cfgtablero()
        {
            return View();
        }
        public class classCatalogos
        {
            public int intOpc { get; set; }
            public int intID { get; set; }
            public string strDescr { get; set; }
            public string strFunc { get; set; }
            public string strNemo { get; set; }
            public bool boolActivo { get; set; }
            public string strColor { get; set; }
            public int intIco { get; set; }
            public string strAnio { get; set; }
            public string strFechaIni { get; set; }
            public string strFechaFin { get; set; }
            public decimal flPond { get; set; }
            public bool boolOblig { get; set; }
            public int intEval { get; set; }
        }

        [HttpPost]
        public JsonResult verCatalogo(classCatalogos catOpc)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = catOpc.intOpc;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<classCatalogos>();
                    while (sqlDR.Read())
                    {
                        if ((catOpc.intOpc == 12) || (catOpc.intOpc == 13))
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strNemo = sqlDR.GetString(2).Trim(), boolActivo = sqlDR.GetBoolean(3), strColor = sqlDR.GetString(4).Trim() });
                        else if (catOpc.intOpc == 46)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), boolActivo = sqlDR.GetBoolean(2) }); 
                        else if ((catOpc.intOpc == 33) || (catOpc.intOpc == 34))
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strNemo = sqlDR.GetString(2).Trim(), boolActivo = sqlDR.GetBoolean(4), strColor = sqlDR.GetString(3).Trim() });
                        else if ((catOpc.intOpc == 15) || (catOpc.intOpc == 16))
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), boolActivo = sqlDR.GetBoolean(2) });
                        else if (catOpc.intOpc == 22)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), intIco = sqlDR.GetInt32(1), strDescr = sqlDR.GetString(2).Trim(), strNemo = sqlDR.GetString(3).Trim(), boolActivo = sqlDR.GetBoolean(4) });
                        else if (catOpc.intOpc == 23)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strAnio = sqlDR.GetString(1).Trim(), strFechaIni = sqlDR.GetDateTime(2).ToShortDateString(), strFechaFin = sqlDR.GetDateTime(3).ToShortDateString(), strDescr = sqlDR.GetString(4).Trim(), strNemo = sqlDR.GetString(5).Trim(), boolActivo = sqlDR.GetBoolean(6) });
                        else if ((catOpc.intOpc == 19) || (catOpc.intOpc == 20))
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strColor = sqlDR.GetString(2).Trim(), boolActivo = sqlDR.GetBoolean(3) });
                        else if ((catOpc.intOpc == 24) || (catOpc.intOpc == 29))
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strColor = sqlDR.GetString(2).Trim(), boolActivo = sqlDR.GetBoolean(3) });
                        else if ((catOpc.intOpc == 25) || (catOpc.intOpc == 44))
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), boolActivo = sqlDR.GetBoolean(2) });
                        else if (catOpc.intOpc == 45)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), boolActivo = sqlDR.GetBoolean(2) });
                        else if(catOpc.intOpc == 26)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), flPond = sqlDR.GetDecimal(2), boolOblig = sqlDR.GetBoolean(3), boolActivo = sqlDR.GetBoolean(4), intIco = sqlDR.GetInt32(5), intEval = sqlDR.GetInt32(6) });
                        else if (catOpc.intOpc == 27)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), boolOblig = sqlDR.GetBoolean(2), strNemo = sqlDR.GetString(3).Trim(), boolActivo = sqlDR.GetBoolean(4), intIco = sqlDR.GetInt32(5) });
                        else if (catOpc.intOpc == 31)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), intEval = sqlDR.GetInt32(1), strDescr = sqlDR.GetString(2).Trim(), boolActivo = sqlDR.GetBoolean(3) });
                        else if ((catOpc.intOpc == 36) || (catOpc.intOpc == 37))
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strNemo = sqlDR.GetString(2).Trim(), boolActivo = sqlDR.GetBoolean(3), intEval = sqlDR.GetInt32(4) });
                        else if (catOpc.intOpc == 35)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strNemo = sqlDR.GetString(2).Trim(), boolActivo = sqlDR.GetBoolean(3), intEval = sqlDR.GetInt32(4) });
                        else if (catOpc.intOpc == 38)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), intEval = sqlDR.GetInt32(2), boolActivo = sqlDR.GetBoolean(3) });
                        else if (catOpc.intOpc == 32)
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), intEval = sqlDR.GetInt32(1), strColor = sqlDR.GetString(2).Trim(), intIco = sqlDR.GetInt32(3), boolActivo = sqlDR.GetBoolean(4) });
                        else
                            etiqJson.Add(new classCatalogos { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strNemo = sqlDR.GetString(2).Trim(), boolActivo = sqlDR.GetBoolean(3) });
                    }
                    con.Close();
                    return Json(etiqJson);
                }
                con.Close();
                return Json(new { success = false });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message + catOpc.intOpc });
            }
        }

        [HttpPost]
        public JsonResult GuardaCatalogo(classCatalogos catOpc)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string strCmd = "";
                if ((catOpc.intOpc == 14) || (catOpc.intOpc == 28))
                    strCmd = "INSERT_CATFATRACCION";
                else if ((catOpc.intOpc == 12) || (catOpc.intOpc == 13))
                    strCmd = "INSERT_CATALOGO2";
                else if ((catOpc.intOpc == 20) || (catOpc.intOpc == 21))
                    strCmd = "INSERT_CATALOGO2";
                else if ((catOpc.intOpc == 29) || (catOpc.intOpc == 33))
                    strCmd = "INSERT_CATALOGO2";
                else if (catOpc.intOpc == 34)
                    strCmd = "INSERT_CATALOGO2";
                else if (catOpc.intOpc == 15)
                    strCmd = "INSERT_ETIQUETA";
                else if (catOpc.intOpc == 22)
                    strCmd = "INSERT_RTIPOEVENTO";
                else if (catOpc.intOpc == 23)
                    strCmd = "INSERT_ANIOTRABAJO";
                else if ((catOpc.intOpc == 26) || (catOpc.intOpc == 27))
                    strCmd = "INSERT_CATALOGO3";
                else if ((catOpc.intOpc == 31) ||(catOpc.intOpc == 35))
                    strCmd = "INSERT_CATALOGO3";
                else if ((catOpc.intOpc == 36) || (catOpc.intOpc == 37))
                    strCmd = "INSERT_CATALOGO3";
                else if (catOpc.intOpc == 38)
                    strCmd = "INSERT_CATALOGO3";
                else if (catOpc.intOpc == 32)
                    strCmd = "INSERT_CALIFICACION";
                else
                    strCmd = "INSERT_CATALOGO";
                SqlCommand cmd = new SqlCommand(strCmd, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if ((catOpc.intOpc < 15) || (catOpc.intOpc >= 16))
                {
                    cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                    cmd.Parameters["INTOPC"].Value = catOpc.intOpc;
                }
                else if (catOpc.intOpc == 15)
                {
                    cmd.Parameters.Add("ECUSNUM", SqlDbType.Int);
                    cmd.Parameters.Add("INTNUM", SqlDbType.Int);
                    cmd.Parameters["ECUSNUM"].Value = catOpc.strColor;
                    cmd.Parameters["INTNUM"].Value = Convert.ToInt32(catOpc.strNemo);
                }
                if ((catOpc.intOpc == 19)||(catOpc.intOpc == 24))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = catOpc.strColor.ToUpper();
                }
                else if (catOpc.intOpc == 26)
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Decimal);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = catOpc.flPond;
                    cmd.Parameters["OBLIG"].Value = catOpc.boolOblig;
                    cmd.Parameters["OBSERV"].Value = "";
                    cmd.Parameters["IDMOD"].Value = catOpc.intIco;
                    cmd.Parameters["IDTEVAL"].Value = catOpc.intEval;
                }
                else if (catOpc.intOpc == 27)
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Float);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = 0.0;
                    cmd.Parameters["OBLIG"].Value = catOpc.boolOblig;
                    cmd.Parameters["OBSERV"].Value = catOpc.strNemo.ToUpper();
                    cmd.Parameters["IDMOD"].Value = catOpc.intIco;
                    cmd.Parameters["IDTEVAL"].Value = 0;
                }
                else if ((catOpc.intOpc == 20)||(catOpc.intOpc == 29))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    if (catOpc.strNemo != null)
                        cmd.Parameters["STRNEMO"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["STRNEMO"].Value = "";
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strColor.ToUpper();
                }
                else if ((catOpc.intOpc == 33)||(catOpc.intOpc == 34))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    if (catOpc.strNemo != null)
                        cmd.Parameters["STRNEMO"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["STRNEMO"].Value = "";
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strColor.ToUpper();
                }
                else if (catOpc.intOpc == 21)
                {
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strNemo.ToUpper();
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = "";
                }
                else if (catOpc.intOpc == 22)
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters.Add("IDICON", SqlDbType.Int);
                    if (catOpc.strNemo != null)
                        cmd.Parameters["STRNEMO"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["STRNEMO"].Value = "";
                    cmd.Parameters["IDICON"].Value = catOpc.intIco;
                }
                else if (catOpc.intOpc == 23)
                {
                    cmd.Parameters.Add("STRANIO", SqlDbType.VarChar);
                    cmd.Parameters.Add("STRFECHAINI", SqlDbType.VarChar);
                    cmd.Parameters.Add("STRFECHAFIN", SqlDbType.VarChar);
                    cmd.Parameters.Add("STROBSERV", SqlDbType.VarChar);
                    if (catOpc.strNemo != null)
                        cmd.Parameters["STROBSERV"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["STROBSERV"].Value = "";
                    cmd.Parameters["STRANIO"].Value = catOpc.strAnio.ToUpper().Trim();
                    cmd.Parameters["STRFECHAINI"].Value = catOpc.strFechaIni.ToUpper().Trim();
                    cmd.Parameters["STRFECHAFIN"].Value = catOpc.strFechaFin.ToUpper().Trim();
                }
                else if ((catOpc.intOpc == 25)||(catOpc.intOpc == 44))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = "";
                }
                else if ((catOpc.intOpc == 45) || (catOpc.intOpc == 46))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = "";
                }
                else if ((catOpc.intOpc == 12) || (catOpc.intOpc == 13))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    if (catOpc.strNemo != null)
                        cmd.Parameters["STRNEMO"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["STRNEMO"].Value = "";
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strColor.ToUpper();
                }
                else if ((catOpc.intOpc == 31)||(catOpc.intOpc == 38))
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Decimal);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = 0M;
                    cmd.Parameters["OBLIG"].Value = false;
                    cmd.Parameters["OBSERV"].Value = "";
                    cmd.Parameters["IDMOD"].Value = catOpc.intEval;
                    cmd.Parameters["IDTEVAL"].Value = 0;
                }
                else if ((catOpc.intOpc == 35)||(catOpc.intOpc == 36))
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Decimal);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = 0M;
                    cmd.Parameters["OBLIG"].Value = false;
                    if (catOpc.strNemo != null)
                        cmd.Parameters["OBSERV"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["OBSERV"].Value = "";
                    cmd.Parameters["IDMOD"].Value = catOpc.intEval;
                    cmd.Parameters["IDTEVAL"].Value = 0;
                }
                else if (catOpc.intOpc == 37)
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Decimal);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = 0M;
                    cmd.Parameters["OBLIG"].Value = false;
                    if (catOpc.strNemo != null)
                        cmd.Parameters["OBSERV"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["OBSERV"].Value = "";
                    cmd.Parameters["IDMOD"].Value = catOpc.intEval;
                    cmd.Parameters["IDTEVAL"].Value = 0;
                }
                else if (catOpc.intOpc == 32)
                {
                    cmd.Parameters.Add("INTESTRELLAS", SqlDbType.Int);
                    cmd.Parameters.Add("INTNUMERO", SqlDbType.Int);
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    cmd.Parameters["INTESTRELLAS"].Value = catOpc.intEval;
                    cmd.Parameters["INTNUMERO"].Value = catOpc.intIco;
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strColor.ToUpper();
                }
                else if ((catOpc.intOpc == 14)||(catOpc.intOpc == 28))
                {
                    cmd.Parameters.Add("STROBSERV", SqlDbType.Text);
                    cmd.Parameters["STROBSERV"].Value = catOpc.strNemo.ToUpper();
                }
                else if ((catOpc.intOpc < 12) || (catOpc.intOpc >= 16))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    if (catOpc.strNemo != null)
                        cmd.Parameters["STRNEMO"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["STRNEMO"].Value = "";
                }
                cmd.Parameters.Add("STRDESCR", SqlDbType.VarChar);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                if (catOpc.strDescr != null)
                    cmd.Parameters["STRDESCR"].Value = catOpc.strDescr.ToUpper();
                else
                    cmd.Parameters["STRDESCR"].Value = "";
                cmd.Parameters["BOOLACTIVO"].Value = catOpc.boolActivo;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString().ToUpper();
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
        public JsonResult ActualizaCatalogo(classCatalogos catOpc)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string strCmd = "";
                if ((catOpc.intOpc == 14)||(catOpc.intOpc == 28))
                    strCmd = "UPDATE_CATFATRACCION";
                else if ((catOpc.intOpc == 12) || (catOpc.intOpc == 13))
                    strCmd = "UPDATE_CATALOGO2";
                else if ((catOpc.intOpc == 20) || (catOpc.intOpc == 21))
                    strCmd = "UPDATE_CATALOGO2";
                else if ((catOpc.intOpc == 29) || (catOpc.intOpc == 33))
                    strCmd = "UPDATE_CATALOGO2";
                else if (catOpc.intOpc == 34)
                    strCmd = "UPDATE_CATALOGO2";
                else if (catOpc.intOpc == 15)
                    strCmd = "UPDATE_ETIQUETA";
                else if (catOpc.intOpc == 22)
                    strCmd = "UPDATE_RTIPOEVENTO";
                else if (catOpc.intOpc == 23)
                    strCmd = "UPDATE_ANIOTRABAJO";
                else if ((catOpc.intOpc == 26) || (catOpc.intOpc == 27))
                    strCmd = "UPDATE_CATALOGO3";
                else if ((catOpc.intOpc == 31)||(catOpc.intOpc == 35))
                    strCmd = "UPDATE_CATALOGO3";
                else if ((catOpc.intOpc == 36) || (catOpc.intOpc == 37))
                    strCmd = "UPDATE_CATALOGO3";
                else if (catOpc.intOpc == 38)
                    strCmd = "UPDATE_CATALOGO3";
                else if (catOpc.intOpc == 32)
                    strCmd = "UPDATE_CALIFICACION";
                else
                    strCmd = "UPDATE_CATALOGO";
                SqlCommand cmd = new SqlCommand(strCmd, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (catOpc.intOpc == 21)
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = "";
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strColor.ToUpper();
                }
                else if (catOpc.intOpc == 22)
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters.Add("IDICON", SqlDbType.Int);
                    if (catOpc.strNemo != null)
                        cmd.Parameters["STRNEMO"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["STRNEMO"].Value = "";
                    cmd.Parameters["IDICON"].Value = catOpc.intIco;
                }
                else if (catOpc.intOpc == 23)
                {
                    cmd.Parameters.Add("STRANIO", SqlDbType.VarChar);
                    cmd.Parameters.Add("STROBSERV", SqlDbType.VarChar);
                    cmd.Parameters.Add("STRFECHAINI", SqlDbType.VarChar);
                    cmd.Parameters.Add("STRFECHAFIN", SqlDbType.VarChar);
                    if (catOpc.strNemo != null)
                        cmd.Parameters["STROBSERV"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["STROBSERV"].Value = "";
                    cmd.Parameters["STRANIO"].Value = catOpc.strAnio.ToUpper();
                    cmd.Parameters["STRFECHAINI"].Value = catOpc.strFechaIni.ToUpper();
                    cmd.Parameters["STRFECHAFIN"].Value = catOpc.strFechaFin.ToUpper();
                } 
                else if ((catOpc.intOpc < 15) || (catOpc.intOpc >= 16))
                {
                    cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                    cmd.Parameters["INTOPC"].Value = catOpc.intOpc;
                }
                if ((catOpc.intOpc == 19)|| (catOpc.intOpc == 24))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = catOpc.strColor.ToUpper();
                }
                else if ((catOpc.intOpc == 20)||(catOpc.intOpc == 29))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = "";
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strColor.ToUpper();
                }
                else if ((catOpc.intOpc == 33)||(catOpc.intOpc == 34))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    if (catOpc.strNemo != null)
                        cmd.Parameters["STRNEMO"].Value = catOpc.strNemo.ToUpper();
                    else
                        cmd.Parameters["STRNEMO"].Value = "";
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strColor.ToUpper();
                }
                else if ((catOpc.intOpc == 25)||(catOpc.intOpc == 44))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = "";
                }
                else if ((catOpc.intOpc == 45) || (catOpc.intOpc == 46))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = "";
                }
                else if (catOpc.intOpc == 26)
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Decimal);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = catOpc.flPond;
                    cmd.Parameters["OBLIG"].Value = catOpc.boolOblig;
                    cmd.Parameters["OBSERV"].Value = "";
                    cmd.Parameters["IDMOD"].Value = catOpc.intIco;
                    cmd.Parameters["IDTEVAL"].Value = catOpc.intEval;
                }
                else if (catOpc.intOpc == 27)
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Float);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = 0.0;
                    cmd.Parameters["IDTEVAL"].Value = 0;
                    cmd.Parameters["OBLIG"].Value = catOpc.boolOblig;
                    cmd.Parameters["OBSERV"].Value = catOpc.strNemo.ToUpper();
                    cmd.Parameters["IDMOD"].Value = catOpc.intIco;
                }
                else if ((catOpc.intOpc == 31)||(catOpc.intOpc == 38))
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Decimal);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = 0M;
                    cmd.Parameters["OBLIG"].Value = false;
                    cmd.Parameters["OBSERV"].Value = "";
                    cmd.Parameters["IDMOD"].Value = catOpc.intEval;
                    cmd.Parameters["IDTEVAL"].Value = 0;
                }
                else if ((catOpc.intOpc == 35)||(catOpc.intOpc == 36))
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Decimal);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = 0M;
                    cmd.Parameters["OBLIG"].Value = false;
                    cmd.Parameters["OBSERV"].Value = catOpc.strNemo;
                    cmd.Parameters["IDMOD"].Value = catOpc.intEval;
                    cmd.Parameters["IDTEVAL"].Value = 0;
                }
                else if (catOpc.intOpc == 37)
                {
                    cmd.Parameters.Add("PONDER", SqlDbType.Decimal);
                    cmd.Parameters.Add("OBLIG", SqlDbType.Bit);
                    cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                    cmd.Parameters.Add("IDMOD", SqlDbType.Int);
                    cmd.Parameters.Add("IDTEVAL", SqlDbType.Int);
                    cmd.Parameters["PONDER"].Value = 0M;
                    cmd.Parameters["OBLIG"].Value = false;
                    cmd.Parameters["OBSERV"].Value = catOpc.strNemo;
                    cmd.Parameters["IDMOD"].Value = catOpc.intEval;
                    cmd.Parameters["IDTEVAL"].Value = 0;
                }
                else if (catOpc.intOpc == 32)
                {
                    cmd.Parameters.Add("INTESTRELLAS", SqlDbType.Int);
                    cmd.Parameters.Add("INTNUMERO", SqlDbType.Int);
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    cmd.Parameters["INTESTRELLAS"].Value = catOpc.intEval;
                    cmd.Parameters["INTNUMERO"].Value = catOpc.intIco;
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strColor.ToUpper();
                }
                else if ((catOpc.intOpc == 12) || (catOpc.intOpc == 13))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters.Add("STRCOLOR", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = catOpc.strNemo.ToUpper();
                    cmd.Parameters["STRCOLOR"].Value = catOpc.strColor.ToUpper();
                }
                else if ((catOpc.intOpc == 14)||(catOpc.intOpc == 28))
                {
                    cmd.Parameters.Add("STROBSERV", SqlDbType.Text);
                    cmd.Parameters["STROBSERV"].Value = catOpc.strNemo.ToUpper();

                }
                else if ((catOpc.intOpc < 12) || (catOpc.intOpc >= 16))
                {
                    cmd.Parameters.Add("STRNEMO", SqlDbType.VarChar);
                    cmd.Parameters["STRNEMO"].Value = catOpc.strNemo.ToUpper();
                }
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("STRDESCR", SqlDbType.VarChar);
                if (catOpc.strDescr != null)
                    cmd.Parameters["STRDESCR"].Value = catOpc.strDescr.ToUpper();
                else
                    cmd.Parameters["STRDESCR"].Value = "";
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = catOpc.intID;
                cmd.Parameters["BOOLACTIVO"].Value = catOpc.boolActivo;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString().ToUpper();
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

        public class detallesCatalogoDrop
        {
            public int intOpc { get; set; }
            public int intID { get; set; }
            public int intIcon { get; set; }
            public string strDescr { get; set; }
            public string strClave { get; set; }
            public string strColor { get; set; }
            public string strIcono { get; set; }
        }

        public JsonResult verCatalogoDrop(detallesCatalogoDrop detCatDrop)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = detCatDrop.intOpc;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<detallesCatalogoDrop>();
                    while (sqlDR.Read())
                    {
                        if ((detCatDrop.intOpc == 10)||(detCatDrop.intOpc == 13))
                            etiqJson.Add(new detallesCatalogoDrop { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strColor = sqlDR.GetString(2).Trim() });
                        else if ((detCatDrop.intOpc == 17)||(detCatDrop.intOpc == 22))
                            etiqJson.Add(new detallesCatalogoDrop { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strColor = sqlDR.GetString(2).Trim() });
                        else if ((detCatDrop.intOpc == 35)||(detCatDrop.intOpc == 39))
                            etiqJson.Add(new detallesCatalogoDrop { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), strColor = sqlDR.GetString(2).Trim() });
                        else if ((detCatDrop.intOpc == 12)||(detCatDrop.intOpc == 38))
                            etiqJson.Add(new detallesCatalogoDrop { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), intIcon = sqlDR.GetInt32(2), strIcono = sqlDR.GetString(3).Trim() });
                        else if (detCatDrop.intOpc == 45)
                            etiqJson.Add(new detallesCatalogoDrop { strClave = sqlDR.GetString(0).Trim(), strDescr = sqlDR.GetString(1).Trim() });
                        else
                            etiqJson.Add(new detallesCatalogoDrop { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim() });
                    }
                    sqlDR.Close();
                    con.Close();
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
                sqlDR.Close();
                con.Close();
                return Json(new { success = false, mensaje = "No hay datos en la tabla" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        public JsonResult verCatalogoDropCalif(detallesCatalogoDrop detCatDrop)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROPCALIF", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = detCatDrop.intOpc;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<detallesCatalogoDrop>();
                    while (sqlDR.Read())
                    {
                            etiqJson.Add(new detallesCatalogoDrop { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim() });
                    }
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No hay datos en la tabla" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        public string verCatalogoIcono()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = 11;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strIco = "<select>";
                    while (sqlDR.Read())
                    {
                        strIco += "<option value=" + sqlDR.GetSqlInt32(0).ToString() + ">" + sqlDR.GetString(1) + "</option>";
                    }
                    strIco += "</select>";
                    return strIco;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return X.Message;
            }
        }

        public string verCatalogoModulo()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = 18;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strIco = "<select>";
                    while (sqlDR.Read())
                    {
                        strIco += "<option value=" + sqlDR.GetSqlInt32(0).ToString() + ">" + sqlDR.GetString(1) + "</option>";
                    }
                    strIco += "</select>";
                    return strIco;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return X.Message;
            }
        }

        public string verCatalogoEvaluacion()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = 16;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strIco = "<select>";
                    while (sqlDR.Read())
                    {
                        strIco += "<option value=" + sqlDR.GetSqlInt32(0).ToString() + ">" + sqlDR.GetString(1) + "</option>";
                    }
                    strIco += "</select>";
                    return strIco;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return X.Message;
            }
        }

        public string verCatalogoCalif()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = 21;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strIco = "<select>";
                    while (sqlDR.Read())
                    {
                        strIco += "<option value=" + sqlDR.GetSqlInt32(0).ToString() + ">" + sqlDR.GetString(1) + "</option>";
                    }
                    strIco += "</select>";
                    return strIco;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return X.Message;
            }
        }

        public string verCatalogoNecesidades()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = 27;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strIco = "<select>";
                    while (sqlDR.Read())
                    {
                        strIco += "<option value=" + sqlDR.GetSqlInt32(0).ToString() + ">" + sqlDR.GetSqlInt32(0).ToString() + "|" + sqlDR.GetString(1) + "</option>";
                    }
                    strIco += "</select>";
                    return strIco;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return X.Message;
            }
        }

        public string verCatalogoRamo()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = 26;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strIco = "<select>";
                    while (sqlDR.Read())
                    {
                        strIco += "<option value=" + sqlDR.GetSqlInt32(0).ToString() + ">" + sqlDR.GetString(1) + "</option>";
                    }
                    strIco += "</select>";
                    return strIco;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return X.Message;
            }
        }

        public string verCatalogoMEstatus()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = 40;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strIco = "<select>";
                    while (sqlDR.Read())
                    {
                        strIco += "<option value=" + sqlDR.GetSqlInt32(0).ToString() + ">" + sqlDR.GetSqlInt32(0).ToString() + "|" + sqlDR.GetString(1) + "</option>";
                    }
                    strIco += "</select>";
                    return strIco;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return X.Message;
            }
        }
        
        public string verCatalogoIndicadores(int clv)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATINDICADORES", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = clv;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strIco = "<select>";
                    while (sqlDR.Read())
                    {
                        strIco += "<option value=" + sqlDR.GetSqlInt32(0).ToString() + ">" + sqlDR.GetSqlInt32(0).ToString() + "|" + sqlDR.GetString(1) + "</option>";
                    }
                    strIco += "</select>";
                    return strIco;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return X.Message;
            }
        }

        public string verCatalogoNecesidades2()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = 42;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strIco = "<select>";
                    while (sqlDR.Read())
                    {
                        strIco += "<option value=" + sqlDR.GetSqlInt32(0).ToString() + ">" + sqlDR.GetSqlInt32(0).ToString() + "|" + sqlDR.GetString(1) + "</option>";
                    }
                    strIco += "</select>";
                    return strIco;
                }
                con.Close();
                return "";
            }
            catch (Exception X)
            {
                return X.Message;
            }
        }

        [HttpPost]
        public JsonResult borrarCatalogo(classCatalogos catOpc)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string strCmd = "DELETE_CATALOGO";
                SqlCommand cmd = new SqlCommand(strCmd, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTOPC"].Value = catOpc.intOpc;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = catOpc.intID;
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
        public JsonResult cargaDelMun(int intId)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_DELMUN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = intId;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<detallesCatalogoDrop>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesCatalogoDrop { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim() });
                    }
                    con.Close();
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No hay datos en la tabla" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }

        }

        [HttpPost]
        public JsonResult cargaDropsPoliza(int intOpc, int intId)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_DROPSPOLIZA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = intId;
                cmd.Parameters["INTOPC"].Value = intOpc;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<detallesCatalogoDrop>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesCatalogoDrop { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim() });
                    }
                    con.Close();
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No hay datos en la tabla" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }

        }
	}
}