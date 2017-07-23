using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartAdminMvc.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class planeaController : Controller
    {
        //Get planea/planea
        public ActionResult planea()
        {
            return View();
        }

        //Get planea/metas
        public ActionResult metasUNegocio()
        {
            return View();
        }

        public ActionResult _metasUNegocio()
        {
            return View();
        }

        //Get planea/necesidad
        public ActionResult necesidadUNegocio()
        {
            return View();
        }

        //Get planea/planeador
        public ActionResult planeadorUNegocio()
        {
            return View();
        }

        public ActionResult _organigrama()
        {
            return View();
        }

        public ActionResult _metas()
        {
            return View();
        }

        public ActionResult _seguimiento()
        {
            return View();
        }

        public ActionResult verDecalogo(detallesPlaneacion detPlanea)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNEGOCIOPADRE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["CLAVE"].Value = detPlanea.strClaveAgencia;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                int intID = 0;
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        intID = sqlDR.GetInt32(0);
                        if (intID == -1)
                            intID = 1;
                    }
                }
                con.Close();
                if (intID != 0)
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    cmd = new SqlCommand("SELECT_PLANEACIONACTIVA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("INTID", SqlDbType.Int);
                    cmd.Parameters["INTID"].Value = intID;
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        string strMision = "";
                        string strVision = "";
                        string strValores = "";
                        string strObjetivo = "";
                        string strMeta = "";
                        string strEstrategia = "";
                        string strPlanAccion = "";
                        string strFechaAlta = "";
                        string strRealizoAlta = "";
                        string strFechaModif = "";
                        string strRealizoModif = "";
                        while (sqlDR.Read())
                        {
                            strMision = sqlDR.GetString(0);
                            strVision = sqlDR.GetString(1);
                            strValores = sqlDR.GetString(2);
                            strObjetivo = sqlDR.GetString(3);
                            strMeta = sqlDR.GetString(4);
                            strEstrategia = sqlDR.GetString(5);
                            strPlanAccion = sqlDR.GetString(6);
                            strFechaAlta = sqlDR.GetDateTime(7).ToString();
                            strRealizoAlta = sqlDR.GetString(8);
                            strFechaModif = sqlDR.GetDateTime(9).ToString();
                            strRealizoModif = sqlDR.GetString(10);
                        }
                        con.Close();
                        return Json(new { success = true, Mision = strMision, Vision = strVision, Valores = strValores, Objetivo = strObjetivo, Meta = strMeta, Estrategia = strEstrategia, PlanAccion = strPlanAccion, FechaAlta = strFechaAlta, RealizoAlta = strRealizoAlta, FechaModif = strFechaModif, RealizoModif = strRealizoModif });
                    }
                    con.Close();
                    return Json(new { success = false, mensaje = "No hay decalogo de la unidad de negocio superior" });
                }
                return Json(new { success = false, mensaje = "No hay decalogo de la unidad de negocio superior" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        public ActionResult verDecalogo2(detallesPlaneacion detPlanea)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_PLANEACION", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["CLAVE"].Value = detPlanea.strClaveAgencia;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    string strMision = "";
                    string strVision = "";
                    string strValores = "";
                    string strObjetivo = "";
                    string strMeta = "";
                    string strEstrategia = "";
                    string strPlanAccion = "";
                    string strFechaAlta = "";
                    string strRealizoAlta = "";
                    string strFechaModif = "";
                    string strRealizoModif = "";
                    while (sqlDR.Read())
                    {
                        strMision = sqlDR.GetString(0);
                        strVision = sqlDR.GetString(1);
                        strValores = sqlDR.GetString(2);
                        strObjetivo = sqlDR.GetString(3);
                        strMeta = sqlDR.GetString(4);
                        strEstrategia = sqlDR.GetString(5);
                        strPlanAccion = sqlDR.GetString(6);
                        strFechaAlta = sqlDR.GetDateTime(7).ToString();
                        strRealizoAlta = sqlDR.GetString(8);
                        strFechaModif = sqlDR.GetDateTime(9).ToString();
                        strRealizoModif = sqlDR.GetString(10);
                    }
                    con.Close();
                    return Json(new { success = true, Mision = strMision, Vision = strVision, Valores = strValores, Objetivo = strObjetivo, Meta = strMeta, Estrategia = strEstrategia, PlanAccion = strPlanAccion, FechaAlta = strFechaAlta, RealizoAlta = strRealizoAlta, FechaModif = strFechaModif, RealizoModif = strRealizoModif });
                }
                con.Close();
                return Json(new { success = false, mensaje = "No hay decalogo de la unidad de negocios seleccionada" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        public class detallesPlaneacion
        {
            public string strMision { get; set; }
            public string strVision { get; set; }
            public string strValores { get; set; }
            public string strObjetivo { get; set; }
            public string strMeta { get; set; }
            public string strEstrategia { get; set; }
            public string strPlanAccion { get; set; }
            public int intID { get; set; }
            public string strClaveAgencia { get; set; }
        }

        public class detallesPeriodos
        {
            public int UNPeriodo_ID { get; set; }
            public int UNegocio_Id { get; set; }
            public int ATrabajo_Id { get; set; }
            public string UNegocio_Clave { get; set; }
            public string UNPeriodo_Descripcion { get; set; }
            public string UNPeriodo_FechaInicio { get; set; }
            public string UNPeriodo_FechaFin { get; set; }
            public bool UNPeriodo_Activo { get; set; }
            public string UNPeriodo_FechaAlta { get; set; }
            public string UNPeriodo_FechaModif { get; set; }
            public string UNPeriodo_RealizoAlta { get; set; }
            public string UNPeriodo_RealizoModif { get; set; }
            public List<detallesIndicador> detIndicadores { get; set; }
            public List<detallesNecesidades> detalNeces { get; set; }
            public int tipoNec { get; set; }
        }

        public class detallesIndicador
        {
            public int UNPeriodo_ID { get; set; }
            public int UNIndicador_Id { get; set; }
            public string UNIndicador_Descripcion { get; set; }
            public string UNIndicador_MetaEsperada { get; set; }
            public string UNIndicador_MetaReal { get; set; }
            public bool UNIndicador_Activo { get; set; }
            public string UNIndicador_FechaAlta { get; set; }
            public string UNIndicador_FechaModif { get; set; }
            public string UNIndicador_RealizoAlta { get; set; }
            public string UNIndicador_RealizoModif { get; set; }
            public string UNIndicador_Trimestral { get; set; }
            public string UNIndicador_Mensual { get; set; }
            public string UNIndicador_Semanal { get; set; }
            public int Ramo_Id { get; set; }
        }

        private string strVerifica(string strVerif)
        {
            if (strVerif == null)
                return "";
            return strVerif;
        }

        [HttpPost]
        public JsonResult guardaDecalogo(detallesPlaneacion detailsPlan)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNEGOCIOCLV", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["CLAVE"].Value = detailsPlan.strClaveAgencia;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                int intId = 0;
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        intId = sqlDR.GetInt32(0);
                    }
                }
                con.Close();
                if (intId != 0)
                {
                    cmd = new SqlCommand("INSERT_PLANEACION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("INTID", SqlDbType.NChar);
                    cmd.Parameters.Add("STRMISION", SqlDbType.Text);
                    cmd.Parameters.Add("STRVISION", SqlDbType.Text);
                    cmd.Parameters.Add("STRVALORES", SqlDbType.Text);
                    cmd.Parameters.Add("STROBJETIVO", SqlDbType.Text);
                    cmd.Parameters.Add("STRMETA", SqlDbType.Text);
                    cmd.Parameters.Add("STRESTRATEGIA", SqlDbType.Text);
                    cmd.Parameters.Add("STRPLANACCION", SqlDbType.Text);
                    cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                    cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                    cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                    cmd.Parameters["INTID"].Value = intId;
                    cmd.Parameters["STRMISION"].Value = strVerifica(detailsPlan.strMision).ToUpper();
                    cmd.Parameters["STRVISION"].Value = strVerifica(detailsPlan.strVision).ToUpper();
                    cmd.Parameters["STRVALORES"].Value = strVerifica(detailsPlan.strValores).ToUpper();
                    cmd.Parameters["STROBJETIVO"].Value = strVerifica(detailsPlan.strObjetivo).ToUpper();
                    cmd.Parameters["STRMETA"].Value = strVerifica(detailsPlan.strMeta).ToUpper();
                    cmd.Parameters["STRESTRATEGIA"].Value = strVerifica(detailsPlan.strEstrategia).ToUpper();
                    cmd.Parameters["STRPLANACCION"].Value = strVerifica(detailsPlan.strPlanAccion).ToUpper();
                    cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                    cmd.Parameters["BOOLACTIVO"].Value = true;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult guardaPeriodo(detallesPeriodos detMeta)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNEGOCIOCLV", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["CLAVE"].Value = detMeta.UNegocio_Clave;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                int intId = 0;
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        intId = sqlDR.GetInt32(0);
                    }
                }
                con.Close();
                if (intId != 0)
                {
                    string comando = "";
                    if (detMeta.UNPeriodo_ID == 0)
                        comando = "INSERT_UNEGOCIO_PERIODO";
                    else
                        comando = "UPDATE_UNEGOCIO_PERIODO";
                    cmd = new SqlCommand(comando, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (detMeta.UNPeriodo_ID == 0)
                    {
                        cmd.Parameters.Add("UNEGOCIO_ID", SqlDbType.Int);
                        cmd.Parameters.Add("ATRABAJO_ID", SqlDbType.Int);
                        cmd.Parameters["UNEGOCIO_ID"].Value = intId;
                        cmd.Parameters["ATRABAJO_ID"].Value = detMeta.ATrabajo_Id;
                    }
                    else
                    {
                        cmd.Parameters.Add("UNPERIODO_ID", SqlDbType.Int);
                        cmd.Parameters["UNPERIODO_ID"].Value = detMeta.UNPeriodo_ID;
                    }
                    cmd.Parameters.Add("UNPERIODO_DESCRIPCION", SqlDbType.VarChar);
                    cmd.Parameters.Add("UNPERIODO_FECHAINICIO", SqlDbType.DateTime);
                    cmd.Parameters.Add("UNPERIODO_FECHAFIN", SqlDbType.DateTime);
                    cmd.Parameters.Add("UNPERIODO_FECHA", SqlDbType.DateTime);
                    cmd.Parameters.Add("UNPERIODO_REALIZO", SqlDbType.Int);
                    cmd.Parameters.Add("UNPERIODO_ACTIVO", SqlDbType.Bit);
                    cmd.Parameters["UNPERIODO_DESCRIPCION"].Value = detMeta.UNPeriodo_Descripcion.ToUpper();
                    cmd.Parameters["UNPERIODO_FECHAINICIO"].Value = detMeta.UNPeriodo_FechaInicio.ToUpper();
                    cmd.Parameters["UNPERIODO_FECHAFIN"].Value = detMeta.UNPeriodo_FechaFin.ToUpper();
                    cmd.Parameters["UNPERIODO_FECHA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["UNPERIODO_REALIZO"].Value = Session["intID"].ToString();
                    cmd.Parameters["UNPERIODO_ACTIVO"].Value = true;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return Json(new { success = true });
                }
                return Json(new { success = false, mensaje = "No se pudo guardar la meta" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }

        }

        [HttpPost]
        public ActionResult verPeriodo(detallesPeriodos detMeta)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNEGOCIOCLV", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["CLAVE"].Value = detMeta.UNegocio_Clave;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                int intId = 0;
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        intId = sqlDR.GetInt32(0);
                    }
                }
                con.Close();
                cmd = new SqlCommand("SELECT_ANIOTRABAJO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ATRABAJO_ANIO", SqlDbType.Char);
                cmd.Parameters["ATRABAJO_ANIO"].Value = Session["anioTrab"].ToString();
                con.Open();
                sqlDR = cmd.ExecuteReader();
                int intIdAnio = 0;
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        intIdAnio = sqlDR.GetInt32(0);
                    }
                }
                con.Close();
                if (intId != 0 && intIdAnio != 0)
                {
                    cmd = new SqlCommand("SELECT_UNEGOCIO_PERIODO", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("UNEGOCIO_ID", SqlDbType.Int);
                    cmd.Parameters.Add("ATRABAJO_ID", SqlDbType.Int);
                    cmd.Parameters["UNEGOCIO_ID"].Value = intId;
                    cmd.Parameters["ATRABAJO_ID"].Value = intIdAnio;
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        var detPeriodos = new List<detallesPeriodos>();
                        while (sqlDR.Read())
                        {
                            detPeriodos.Add(new detallesPeriodos { UNPeriodo_ID = sqlDR.GetInt32(0), UNPeriodo_Descripcion = sqlDR.GetString(1), UNPeriodo_FechaInicio = sqlDR.GetDateTime(2).ToShortDateString(), UNPeriodo_FechaFin = sqlDR.GetDateTime(3).ToShortDateString() });
                        }
                        con.Close();
                        return Json(detPeriodos, JsonRequestBehavior.AllowGet);
                    }
                    con.Close();
                    return Json(new { success = false, mensaje = "No existen metas de esa unidad de negocio en ese año de trabajo" });
                }
                return Json(new { success = false, mensaje = "No existe esa unidad de negocio" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public ActionResult verPlaneadorUNegocio(detallesIndicador detIndica)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNEGOCIO_PERIODO_INDICADOR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("UNPERIODO_ID", SqlDbType.Int);
                cmd.Parameters.Add("RAMO_ID", SqlDbType.Int);
                cmd.Parameters["UNPERIODO_ID"].Value = detIndica.UNPeriodo_ID;
                cmd.Parameters["RAMO_ID"].Value = detIndica.Ramo_Id;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var detIndicas = new List<detallesIndicador>();
                    while (sqlDR.Read())
                    {
                        detIndicas.Add(new detallesIndicador { UNIndicador_Id = sqlDR.GetInt32(0), UNIndicador_Descripcion = sqlDR.GetString(1), UNIndicador_MetaEsperada = sqlDR.GetString(2), UNIndicador_MetaReal = sqlDR.GetString(3) });
                    }
                    con.Close();
                    return Json(detIndicas, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No existen indicadores de meta" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult guardaIndicador(detallesPeriodos detIndicador)
        {
            try
            {
                foreach (detallesIndicador detInd in detIndicador.detIndicadores)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    string comando = "";
                    if (detInd.UNIndicador_Id == 0)
                        comando = "INSERT_UNEGOCIO_PERIODO_INDICADOR";
                    else
                        comando = "UPDATE_UNEGOCIO_PERIODO_INDICADOR";
                    SqlCommand cmd = new SqlCommand(comando, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (detInd.UNIndicador_Id == 0)
                    {
                        cmd.Parameters.Add("UNPERIODO_ID", SqlDbType.Int);
                        cmd.Parameters["UNPERIODO_ID"].Value = detIndicador.UNPeriodo_ID;
                        cmd.Parameters.Add("INDICADOR_ID", SqlDbType.Int);
                        string[] strTemp = detInd.UNIndicador_Descripcion.Split(char.Parse("|"));
                        cmd.Parameters["INDICADOR_ID"].Value = strTemp[0];
                    }
                    else
                    {
                        cmd.Parameters.Add("UNINDICADOR_ID", SqlDbType.Int);
                        cmd.Parameters["UNINDICADOR_ID"].Value = detInd.UNIndicador_Id;
                    }
                    cmd.Parameters.Add("UNINDICADOR_METAESPERADA", SqlDbType.VarChar);
                    cmd.Parameters.Add("UNINDICADOR_METAREAL", SqlDbType.VarChar);
                    cmd.Parameters.Add("UNINDICADOR_FECHA", SqlDbType.DateTime);
                    cmd.Parameters.Add("UNINDICADOR_REALIZO", SqlDbType.Int);
                    cmd.Parameters.Add("UNINDICADOR_ACTIVO", SqlDbType.Bit);
                    cmd.Parameters["UNINDICADOR_METAESPERADA"].Value = strVerifica(detInd.UNIndicador_MetaEsperada).ToUpper();
                    cmd.Parameters["UNINDICADOR_METAREAL"].Value = strVerifica(detInd.UNIndicador_MetaReal).ToUpper();
                    cmd.Parameters["UNINDICADOR_FECHA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["UNINDICADOR_REALIZO"].Value = Session["intID"].ToString();
                    cmd.Parameters["UNINDICADOR_ACTIVO"].Value = true;
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

        public class detallesNecesidades
        {
            public string clave { get; set; }
            public string necesidad { get; set; }
            public int idNecesidad { get; set; }
            public decimal trimestral { get; set; }
            public decimal mensual { get; set; }
            public int idPeriodo { get; set; }
        }

        [HttpPost]
        public JsonResult verNecesidadesDeLaMeta(detallesNecesidades detNec)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNEGOCIO_PERIODO_NECESIDAD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("UNPERIODO_ID", SqlDbType.VarChar);
                cmd.Parameters.Add("UNNECESIDAD_CLAVE", SqlDbType.VarChar);
                cmd.Parameters["UNPERIODO_ID"].Value = detNec.idPeriodo;
                if (detNec.idNecesidad == 1)
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CGF%";
                else if (detNec.idNecesidad == 2)
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CGS%";
                else if (detNec.idNecesidad == 3)
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CGN%";
                else if (detNec.idNecesidad == 4)
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CAI%";
                else if (detNec.idNecesidad == 5)
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CCO%";
                else if (detNec.idNecesidad == 6)
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CMP%";
                else if (detNec.idNecesidad == 7)
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CLP%";
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesNecesidades> etiqJson = new List<detallesNecesidades>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesNecesidades { idNecesidad = sqlDR.GetInt32(0), clave = sqlDR.GetString(9).Trim(), trimestral = sqlDR.GetDecimal(3), mensual = sqlDR.GetDecimal(2), necesidad = sqlDR.GetString(10) });
                    }
                    con.Close();
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
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
        public JsonResult guardaNecesidadMeta(detallesPeriodos detMetas)
        {
            try
            {
                int contNec = 1;
                foreach (detallesNecesidades detN in detMetas.detalNeces)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    SqlCommand cmd = null;
                    string comand = "";
                    if ((detN.clave == "") || (detN.clave == null))
                        comand = "INSERT_UNEGOCIO_PERIODO_NECESIDAD";
                    else
                        comand = "UPDATE_UNEGOCIO_PERIODO_NECESIDAD";
                    cmd = new SqlCommand(comand, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if ((detN.clave == "") || (detN.clave == null))
                    {
                        cmd.Parameters.Add("UNPERIODO_ID", SqlDbType.Int);
                        cmd.Parameters.Add("UNNECESIDAD_CLAVE", SqlDbType.VarChar);
                        cmd.Parameters["UNPERIODO_ID"].Value = detMetas.UNPeriodo_ID;
                        cmd.Parameters.Add("UNTGASTO_ID", SqlDbType.Int);
                        string[] strTemp = detN.necesidad.Split(char.Parse("|"));
                        cmd.Parameters["UNTGASTO_ID"].Value = strTemp[0];
                        if (detMetas.tipoNec == 1)
                            cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "CGF" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 2)
                            cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "CGS" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 3)
                            cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "CGN" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 4)
                            cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "CAI" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 5)
                            cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "CCO" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 6)
                            cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "CMP" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 7)
                            cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "CLP" + contNec.ToString("00");
                        contNec++;
                    }
                    else
                    {
                        cmd.Parameters.Add("UNNECESIDAD_ID", SqlDbType.NChar);
                        cmd.Parameters["UNNECESIDAD_ID"].Value = detN.idNecesidad;
                    }
                    cmd.Parameters.Add("UNNECESIDAD_MONTOTRIMES", SqlDbType.Decimal);
                    cmd.Parameters.Add("UNNECESIDAD_MONTOMENS", SqlDbType.Decimal);
                    cmd.Parameters.Add("UNNECESIDAD_ACTIVO", SqlDbType.Bit);
                    cmd.Parameters.Add("UNNECESIDAD_FECHA", SqlDbType.DateTime);
                    cmd.Parameters.Add("UNNECESIDAD_REALIZO", SqlDbType.Int);
                    cmd.Parameters.Add("UNNECESIDAD_DESCRIPCION", SqlDbType.VarChar);
                    decimal decTri = detN.trimestral;
                    decimal decMen = detN.mensual; 
                    if ((!detN.mensual.Equals(0)) && (detN.trimestral.Equals(0)))
                        decTri = detN.mensual * 3;
                    else if ((!detN.trimestral.Equals(0)) && (detN.mensual.Equals(0)))
                        decMen = detN.trimestral / 3;
                    cmd.Parameters["UNNECESIDAD_MONTOTRIMES"].Value = decTri;
                    cmd.Parameters["UNNECESIDAD_MONTOMENS"].Value = decMen;
                    cmd.Parameters["UNNECESIDAD_ACTIVO"].Value = true;
                    cmd.Parameters["UNNECESIDAD_FECHA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["UNNECESIDAD_REALIZO"].Value = Session["intID"].ToString();
                    cmd.Parameters["UNNECESIDAD_DESCRIPCION"].Value = "";
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
        public JsonResult verResultadosMetas(detallesNecesidades detNec)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_RESNECPERIODOSBAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("UNPERIODO_ID", SqlDbType.VarChar);
                cmd.Parameters.Add("UNNECESIDAD_CLAVE", SqlDbType.VarChar);
                cmd.Parameters["UNPERIODO_ID"].Value = detNec.idPeriodo;
                if (detNec.idNecesidad == 0)
                {
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CGF%";
                    con.Open();
                    SqlDataReader sqlDR = cmd.ExecuteReader();
                    List<detallesNecesidades> etiqJson = new List<detallesNecesidades>();
                    decimal decTrim = 0M, decMen = 0M, decTotalT = 0M, decTotalM = 0M;
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(1))
                                decTrim = sqlDR.GetDecimal(1);
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    etiqJson.Add(new detallesNecesidades { idNecesidad = 1, necesidad = "GASTOS FIJOS", trimestral = decTrim, mensual = decMen });
                    decTotalT += decTrim;
                    decTotalM += decMen;
                    decTrim = 0M;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CGS%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(1))
                                decTrim = sqlDR.GetDecimal(1);
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    etiqJson.Add(new detallesNecesidades { idNecesidad = 1, necesidad = "GASTOS DE SOSTENIMIENTO", trimestral = decTrim, mensual = decMen });
                    decTotalT += decTrim;
                    decTotalM += decMen;
                    decTrim = 0M;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CGN%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(1))
                                decTrim = sqlDR.GetDecimal(1);
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    etiqJson.Add(new detallesNecesidades { idNecesidad = 1, necesidad = "GASTOS DE NEGOCIO", trimestral = decTrim, mensual = decMen });
                    decTotalT += decTrim;
                    decTotalM += decMen;
                    decTrim = 0M;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CAI%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(1))
                                decTrim = sqlDR.GetDecimal(1);
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    etiqJson.Add(new detallesNecesidades { idNecesidad = 1, necesidad = "AHORROS E INVERSIONES", trimestral = decTrim, mensual = decMen });
                    decTotalT += decTrim;
                    decTotalM += decMen;
                    con.Close();
                    etiqJson.Add(new detallesNecesidades { idNecesidad = 0, necesidad = "SUB-TOTAL", trimestral = decTotalT, mensual = decTotalM });
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<detallesNecesidades> etiqJson = new List<detallesNecesidades>();
                    decimal decTrim = 0M, decMen = 0M, decTotalT = 0M, decTotalM = 0M;
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CCO%";
                    con.Open();
                    SqlDataReader sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(1))
                                decTrim = sqlDR.GetDecimal(1);
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    etiqJson.Add(new detallesNecesidades { idNecesidad = 1, necesidad = "CORTO PLAZO", trimestral = decTrim, mensual = decMen });
                    decTotalT += decTrim;
                    decTotalM += decMen;
                    decTrim = 0M;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CMP%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(1))
                                decTrim = sqlDR.GetDecimal(1);
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    etiqJson.Add(new detallesNecesidades { idNecesidad = 1, necesidad = "MEDIANO PLAZO", trimestral = decTrim, mensual = decMen });
                    decTotalT += decTrim;
                    decTotalM += decMen;
                    decTrim = 0M;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CLA%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(1))
                                decTrim = sqlDR.GetDecimal(1);
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    etiqJson.Add(new detallesNecesidades { idNecesidad = 1, necesidad = "LARGO PLAZO", trimestral = decTrim, mensual = decMen });
                    decTotalT += decTrim;
                    decTotalM += decMen;
                    con.Close();
                    etiqJson.Add(new detallesNecesidades { idNecesidad = 0, necesidad = "SUB-TOTAL", trimestral = decTotalT, mensual = decTotalM });
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verPlanesMetas(detallesPeriodos detMetas)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNEGOCIO_META_INDICADOR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("UNMETA_ID", SqlDbType.Int);
                cmd.Parameters["UNMETA_ID"].Value = detMetas.UNPeriodo_ID;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesIndicador> etiqJson = new List<detallesIndicador>();
                    while (sqlDR.Read())
                    {
                        decimal decME = decimal.Parse(sqlDR.GetString(2).Replace("%", ""));
                        bool blPorc = false;
                        if (sqlDR.GetString(2).Contains("%"))
                            blPorc = true;
                        decimal tri = decME / 4;
                        decimal men = decME / 12;
                        decimal sem = decME / 52;
                        string strTri = "";
                        string strMen = "";
                        string strSem = "";
                        if (!blPorc)
                        {
                            strTri = tri.ToString("$#,000.00");
                            strMen = men.ToString("$#,000.00");
                            strSem = sem.ToString("$#,000.00");
                        }
                        else
                        {
                            strTri = tri.ToString("0.00") + "%";
                            strMen = men.ToString("0.00") + "%";
                            strSem = sem.ToString("0.00") + "%";
                        }
                        etiqJson.Add(new detallesIndicador { UNIndicador_Id = sqlDR.GetInt32(0),  UNIndicador_Descripcion = sqlDR.GetString(1), UNIndicador_Trimestral = strTri, UNIndicador_Mensual = strMen, UNIndicador_Semanal = strSem });
                    }
                    con.Close();
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo consultar la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }

        }

        public class detallesCualitativa
        {
            public int UNMeta_Id { get; set; }
            public string UNMPCualitativa_Pregunta { get; set; }
            public string UNMRCualitativa_Respuesta { get; set; }
            public int UNMPCualitativa_Id { get; set; }
            public int UNMRCualitativa_Id { get; set; }
            public string UNMRCualitativa_FechaAlta { get; set; }
            public string UNMPCualitativa_FechaAlta { get; set; }
        }

        [HttpPost]
        public JsonResult verPreguntas(detallesCualitativa detCual)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNMPREGUNTAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", SqlDbType.Int);
                cmd.Parameters["ID"].Value = detCual.UNMeta_Id;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesCualitativa> etiqJson = new List<detallesCualitativa>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesCualitativa { UNMPCualitativa_Id = sqlDR.GetInt32(0), UNMPCualitativa_Pregunta = sqlDR.GetString(1), UNMPCualitativa_FechaAlta = sqlDR.GetDateTime(2).ToString() });
                    }
                    con.Close();
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
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
        public JsonResult verRespuestas(detallesCualitativa detCual)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNMRESPUESTAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", SqlDbType.Int);
                cmd.Parameters["ID"].Value = detCual.UNMPCualitativa_Id;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesCualitativa> etiqJson = new List<detallesCualitativa>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesCualitativa { UNMRCualitativa_Id = sqlDR.GetInt32(0), UNMRCualitativa_Respuesta = sqlDR.GetString(1), UNMRCualitativa_FechaAlta = sqlDR.GetDateTime(2).ToString() });
                    }
                    con.Close();
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
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
        public JsonResult guardaPreguntas(detallesCualitativa detCual)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string scmd = "";
                if (detCual.UNMPCualitativa_Id == 0)
                    scmd = "INSERT_UNMPREGUNTA_CUALITATIVA";
                else
                    scmd = "UPDATE_UNMPREGUNTA_CUALITATIVA";
                SqlCommand cmd = null;
                cmd = new SqlCommand(scmd, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (detCual.UNMPCualitativa_Id == 0)
                {
                    cmd.Parameters.Add("UNMETA_ID", SqlDbType.Int);
                    cmd.Parameters["UNMETA_ID"].Value = detCual.UNMeta_Id;
                }
                else
                {
                    cmd.Parameters.Add("ID", SqlDbType.Int);
                    cmd.Parameters["ID"].Value = detCual.UNMPCualitativa_Id;
                }
                cmd.Parameters.Add("PREGUNTA", SqlDbType.Text);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["PREGUNTA"].Value = strVerifica(detCual.UNMPCualitativa_Pregunta).ToUpper();
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
        public JsonResult guardaRespuestas(detallesCualitativa detCual)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_UNMRESPUESTA_CUALITATIVA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PREGUNTA_ID", SqlDbType.Int);
                cmd.Parameters.Add("RESPUESTA", SqlDbType.Text);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["PREGUNTA_ID"].Value = detCual.UNMPCualitativa_Id;
                cmd.Parameters["RESPUESTA"].Value = strVerifica(detCual.UNMRCualitativa_Respuesta).ToUpper();
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
        public JsonResult borrarMeta(detallesPeriodos detMeta)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DELETE_UNMETA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detMeta.UNPeriodo_ID;
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
        public JsonResult borrarNecesidad(detallesNecesidades detNecesidades)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DELETE_UNNECESIDAD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detNecesidades.idNecesidad;
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
        public JsonResult borrarCualitativo(detallesCualitativa detCualitativa)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DELETE_UNCUALITATIVO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detCualitativa.UNMPCualitativa_Id;
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
        public JsonResult borrarPlan(detallesIndicador detIndica)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DELETE_UNINDICADOR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detIndica.UNIndicador_Id;
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
        public decimal calculaResultadosMetas(detallesNecesidades detNec)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_RESNECPERIODOSBAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("UNPERIODO_ID", SqlDbType.VarChar);
                cmd.Parameters.Add("UNNECESIDAD_CLAVE", SqlDbType.VarChar);
                cmd.Parameters["UNPERIODO_ID"].Value = detNec.idPeriodo;
                if (detNec.idNecesidad == 0)
                {
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CGF%";
                    con.Open();
                    SqlDataReader sqlDR = cmd.ExecuteReader();
                    decimal decMen = 0M, decTotalM = 0M;
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    decTotalM += decMen;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CGS%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    decTotalM += decMen;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CGN%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    decTotalM += decMen;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CAI%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    decTotalM += decMen;
                    con.Close();
                    return decTotalM;
                }
                else
                {
                    decimal decMen = 0M, decTotalM = 0M;
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CCO%";
                    con.Open();
                    SqlDataReader sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    decTotalM += decMen;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CMP%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    decTotalM += decMen;
                    decMen = 0M;
                    con.Close();
                    cmd.Parameters["UNNECESIDAD_CLAVE"].Value = "%CLA%";
                    con.Open();
                    sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if (!sqlDR.IsDBNull(0))
                                decMen = sqlDR.GetDecimal(0);
                        }
                    }
                    decTotalM += decMen;
                    con.Close();
                    return decTotalM;
                }
            }
            catch (Exception X)
            {
                return 0;
            }
        }

        [HttpPost]
        public decimal calculaPlaneador(int idPeriodo, int idRamo, int idIndicador)
        {
            try
            {
                decimal decResultado = 0;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_METAESPERADA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDPER", SqlDbType.Int);
                cmd.Parameters.Add("IDRAMO", SqlDbType.Int);
                cmd.Parameters.Add("IDINDIC", SqlDbType.Int);
                cmd.Parameters["IDPER"].Value = idPeriodo;
                cmd.Parameters["IDRAMO"].Value = idRamo;
                cmd.Parameters["IDINDIC"].Value = idIndicador;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        if (sqlDR.GetString(0).Trim().Contains("%"))
                            decResultado += decimal.Parse(sqlDR.GetString(0).Trim().Replace("%", "")) / 100;
                        else
                            decResultado += decimal.Parse(sqlDR.GetString(0).Trim().Replace("%", ""));
                    }
                }
                return decResultado;
            }
            catch (SqlException X)
            {
                return 0;
            }
            catch (Exception X)
            {
                return 0;
            }
        }

        public class detallesMetas
        {
            public int idPeriodo { get; set; }
            public int idMetaMeta { get; set; }
            public string descripcionMeta { get; set; }
            public string fechainiMeta { get; set; }
            public string fechafinMeta { get; set; }
            public int idRamo { get; set; }
        }

        [HttpPost]
        public JsonResult guardaMeta(detallesMetas detMeta)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string scmd = "";
                if (detMeta.idMetaMeta == 0)
                    scmd = "INSERT_UNEGOCIOMETA";
                else
                    scmd = "UPDATE_UNEGOCIOMETA";
                SqlCommand cmd = null;
                cmd = new SqlCommand(scmd, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (detMeta.idMetaMeta == 0)
                {
                    cmd.Parameters.Add("IDPER", SqlDbType.Int);
                    cmd.Parameters.Add("IDRAMO", SqlDbType.Int);
                    cmd.Parameters["IDPER"].Value = detMeta.idPeriodo;
                    cmd.Parameters["IDRAMO"].Value = detMeta.idRamo;
                }
                else
                {
                    cmd.Parameters.Add("IDMETA", SqlDbType.Int);
                    cmd.Parameters["IDMETA"].Value = detMeta.idMetaMeta;
                }
                cmd.Parameters.Add("DESCR", SqlDbType.VarChar);
                cmd.Parameters.Add("FECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["DESCR"].Value = strVerifica(detMeta.descripcionMeta).ToUpper();
                cmd.Parameters["FECHAINI"].Value = DateTime.Parse(strVerifica(detMeta.fechainiMeta).ToUpper());
                cmd.Parameters["FECHAFIN"].Value = DateTime.Parse(strVerifica(detMeta.fechafinMeta).ToUpper());
                cmd.Parameters["ACTIVO"].Value = true;
                cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true });
            }
            catch (SqlException X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verMetas(detallesMetas detMeta)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNEGOCIOMETA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDPER", SqlDbType.Int);
                cmd.Parameters.Add("IDRAMO", SqlDbType.Int);
                cmd.Parameters["IDPER"].Value = detMeta.idPeriodo;
                cmd.Parameters["IDRAMO"].Value = detMeta.idRamo;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesMetas> detMetas = new List<detallesMetas>();
                    while (sqlDR.Read())
                    {
                        detMetas.Add(new detallesMetas { idMetaMeta = sqlDR.GetInt32(0), descripcionMeta = sqlDR.GetString(1).Trim(), fechainiMeta = sqlDR.GetDateTime(2).ToShortDateString(), fechafinMeta = sqlDR.GetDateTime(3).ToShortDateString() });
                    }
                    con.Close();
                    return Json(detMetas);
                }
                con.Close();
                return Json(new { success = false });
            }
            catch (SqlException X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }
    }
}
