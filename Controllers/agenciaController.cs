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
    public class agenciaController : Controller
    {
        // GET: /agencia/unegocio
        public ActionResult uNegocio()
        {
            return View();
        }

        public ActionResult _uNegocio()
        {
            return View();
        }

        public class detallesUNegocio
        {
            public int intID { get; set; }
            public string strUNegocio { get; set; }
            public string strNomen { get; set; }
            public string strClaveUN { get; set; }
            public string strCalle { get; set; }
            public string strNumExt { get; set; }
            public string strNumInt { get; set; }
            public string strColonia { get; set; }
            public string strDelMun { get; set; }
            public string strEstado { get; set; }
            public string strCodPost { get; set; }
            public string strCasaClvLada { get; set; }
            public string strCasaNumero { get; set; }
            public string strCasaRecados { get; set; }
            public string strOfcClvLada { get; set; }
            public string strOfcNumero { get; set; }
            public string strOfcExtension { get; set; }
            public string strCelular { get; set; }
            public string strCorreo { get; set; }
            public string strPagina { get; set; }
            public int intPadre { get; set; }
            public int intAnio { get; set; }
            public string strResponsable { get; set; }
            public string dateFechaAlta { get; set; }
            public string dateFechaModif { get; set; }
            public string strRealizoAlta { get; set; }
            public string strRealizoModif { get; set; }
            public bool boolActivo { get; set; }
            public int intEstado { get; set; }
            public int intDelMun { get; set; }
            public int intPuestoId { get; set; }
            public string strTipo { get; set; }
            [AllowHtml] public string strFunciones { get; set; }
        }

        private string strVerifica(string strVerif)
        {
            if (strVerif == null)
                return "";
            return strVerif;
        }

        private int intVerifica(int intVerif)
        {
            if (intVerif == 0)
                return 1;
            return intVerif;
        }

        [HttpPost]
        
        public JsonResult guardaUNegocio(detallesUNegocio detailsUNegocio)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_UNEGOCIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("STRNOMBRE", SqlDbType.VarChar);
                cmd.Parameters.Add("STRNOMEN", SqlDbType.Char);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("INTPADRE", SqlDbType.Int);
                cmd.Parameters.Add("STRCALLE", SqlDbType.VarChar);
                cmd.Parameters.Add("STRNUMEXT", SqlDbType.Char);
                cmd.Parameters.Add("STRNUMINT", SqlDbType.Char);
                cmd.Parameters.Add("STRCOLONIA", SqlDbType.VarChar);
                cmd.Parameters.Add("STRCODPOS", SqlDbType.VarChar);
                cmd.Parameters.Add("STRRESPONSABLE", SqlDbType.VarChar);
                cmd.Parameters.Add("STRCORREO", SqlDbType.Text);
                cmd.Parameters.Add("STRPAGINA", SqlDbType.Text);
                cmd.Parameters.Add("STRANIO", SqlDbType.Int);
                cmd.Parameters.Add("STRCASACLVLADA", SqlDbType.Char);
                cmd.Parameters.Add("STRCASANUMERO", SqlDbType.Char);
                cmd.Parameters.Add("STRCASARECADO", SqlDbType.Char);
                cmd.Parameters.Add("STROFCCLVLADA", SqlDbType.Char);
                cmd.Parameters.Add("STROFCNUMERO", SqlDbType.Char);
                cmd.Parameters.Add("STROFCEXTENSION", SqlDbType.Char);
                cmd.Parameters.Add("STRCELULAR", SqlDbType.Char);
                cmd.Parameters.Add("INTIDESTADO", SqlDbType.Int);
                cmd.Parameters.Add("INTIDDELMUN", SqlDbType.Int);
                cmd.Parameters.Add("INTPUESTOID", SqlDbType.Int);
                cmd.Parameters.Add("STRTIPO", SqlDbType.Char);
                cmd.Parameters.Add("STRFUNCIONES", SqlDbType.Char);

                //
                cmd.Parameters["STRCLAVE"].Value = detailsUNegocio.strClaveUN.ToUpper();
                cmd.Parameters["STRNOMBRE"].Value = detailsUNegocio.strUNegocio.ToUpper();
                cmd.Parameters["STRNOMEN"].Value = detailsUNegocio.strNomen.ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = detailsUNegocio.boolActivo;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["INTPADRE"].Value = detailsUNegocio.intPadre;
                cmd.Parameters["STRCALLE"].Value = strVerifica(detailsUNegocio.strCalle).ToUpper();
                cmd.Parameters["STRNUMEXT"].Value = strVerifica(detailsUNegocio.strNumExt).ToUpper();
                cmd.Parameters["STRNUMINT"].Value = strVerifica(detailsUNegocio.strNumInt).ToUpper();
                cmd.Parameters["STRCOLONIA"].Value = strVerifica(detailsUNegocio.strColonia).ToUpper();
                cmd.Parameters["STRCODPOS"].Value = strVerifica(detailsUNegocio.strCodPost).ToUpper();
                cmd.Parameters["STRRESPONSABLE"].Value = strVerifica(detailsUNegocio.strResponsable).ToUpper();
                cmd.Parameters["STRCORREO"].Value = strVerifica(detailsUNegocio.strCorreo);
                cmd.Parameters["STRPAGINA"].Value = strVerifica(detailsUNegocio.strPagina);
                cmd.Parameters["STRANIO"].Value = intVerifica(detailsUNegocio.intAnio);
                cmd.Parameters["STRCASACLVLADA"].Value = strVerifica(detailsUNegocio.strCasaClvLada).ToUpper();
                cmd.Parameters["STRCASANUMERO"].Value = strVerifica(detailsUNegocio.strCasaNumero).ToUpper();
                cmd.Parameters["STRCASARECADO"].Value = strVerifica(detailsUNegocio.strCasaRecados).ToUpper();
                cmd.Parameters["STROFCCLVLADA"].Value = strVerifica(detailsUNegocio.strOfcClvLada).ToUpper();
                cmd.Parameters["STROFCNUMERO"].Value = strVerifica(detailsUNegocio.strOfcNumero).ToUpper();
                cmd.Parameters["STROFCEXTENSION"].Value = strVerifica(detailsUNegocio.strOfcExtension).ToUpper();
                cmd.Parameters["STRCELULAR"].Value = strVerifica(detailsUNegocio.strCelular).ToUpper();
                cmd.Parameters["INTIDESTADO"].Value = intVerifica(detailsUNegocio.intEstado);
                cmd.Parameters["INTIDDELMUN"].Value = intVerifica(detailsUNegocio.intDelMun);
                cmd.Parameters["INTPUESTOID"].Value = intVerifica(detailsUNegocio.intPuestoId);
                cmd.Parameters["STRTIPO"].Value = strVerifica(detailsUNegocio.strTipo);
                cmd.Parameters["STRFUNCIONES"].Value = strVerifica(detailsUNegocio.strFunciones);
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

        public JsonResult verUNegocio(int intOpc, int intProf)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_UNEGOCIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(sqlDR);
                    con.Close();
                    var etiqJson = new List<detallesUNegocio>();
                    int intPadreAnt = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[0].ToString() == Session["intUneg"].ToString())
                        {
                            if((intOpc == 0)||(intOpc == 1))
                                etiqJson.Add(new detallesUNegocio { intID = int.Parse(dr[0].ToString()), intPadre = -1, strUNegocio = dr[2].ToString().Trim(), strNomen = dr[3].ToString().Trim(), strClaveUN = dr[4].ToString().Trim() });
                            else if (intOpc == 2)
                            {
                                intPadreAnt = int.Parse(dr[1].ToString());
                                etiqJson.Add(new detallesUNegocio { intID = int.Parse(dr[0].ToString()), intPadre = int.Parse(dr[1].ToString()), strUNegocio = dr[2].ToString().Trim(), strNomen = dr[3].ToString().Trim(), strClaveUN = dr[4].ToString().Trim() });
                            }
                        }
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (intOpc == 1)
                        {
                            if (dr[1].ToString() == Session["intUneg"].ToString())
                            {
                                etiqJson.Add(new detallesUNegocio { intID = int.Parse(dr[0].ToString()), intPadre = int.Parse(dr[1].ToString()), strUNegocio = dr[2].ToString().Trim(), strNomen = dr[3].ToString().Trim(), strClaveUN = dr[4].ToString().Trim() });
                                if (intProf >= 1)
                                {
                                    foreach(DataRow dr1 in dt.Rows)
                                    {
                                        if (int.Parse(dr1[1].ToString()) == int.Parse(dr[0].ToString()))
                                        {
                                            etiqJson.Add(new detallesUNegocio { intID = int.Parse(dr1[0].ToString()), intPadre = int.Parse(dr1[1].ToString()), strUNegocio = dr1[2].ToString().Trim(), strNomen = dr1[3].ToString().Trim(), strClaveUN = dr1[4].ToString().Trim() });
                                            if (intProf >= 2)
                                            {
                                                foreach (DataRow dr2 in dt.Rows)
                                                {
                                                    if (int.Parse(dr2[1].ToString()) == int.Parse(dr1[0].ToString()))
                                                    {
                                                        etiqJson.Add(new detallesUNegocio { intID = int.Parse(dr2[0].ToString()), intPadre = int.Parse(dr2[1].ToString()), strUNegocio = dr2[2].ToString().Trim(), strNomen = dr2[3].ToString().Trim(), strClaveUN = dr2[4].ToString().Trim() });
                                                        if (intProf >= 3)
                                                        {
                                                            foreach (DataRow dr3 in dt.Rows)
                                                            {
                                                                if (int.Parse(dr3[1].ToString()) == int.Parse(dr2[0].ToString()))
                                                                {
                                                                    etiqJson.Add(new detallesUNegocio { intID = int.Parse(dr3[0].ToString()), intPadre = int.Parse(dr3[1].ToString()), strUNegocio = dr3[2].ToString().Trim(), strNomen = dr3[3].ToString().Trim(), strClaveUN = dr3[4].ToString().Trim() });
                                                                    if (intProf >= 4)
                                                                    {
                                                                        foreach (DataRow dr4 in dt.Rows)
                                                                        {
                                                                            if (int.Parse(dr4[1].ToString()) == int.Parse(dr3[0].ToString()))
                                                                            {
                                                                                etiqJson.Add(new detallesUNegocio { intID = int.Parse(dr4[0].ToString()), intPadre = int.Parse(dr4[1].ToString()), strUNegocio = dr4[2].ToString().Trim(), strNomen = dr4[3].ToString().Trim(), strClaveUN = dr4[4].ToString().Trim() });
                                                                                if (intProf >= 5)
                                                                                {
                                                                                    foreach (DataRow dr5 in dt.Rows)
                                                                                    {
                                                                                        if (int.Parse(dr5[1].ToString()) == int.Parse(dr4[0].ToString()))
                                                                                            etiqJson.Add(new detallesUNegocio { intID = int.Parse(dr5[0].ToString()), intPadre = int.Parse(dr5[1].ToString()), strUNegocio = dr5[2].ToString().Trim(), strNomen = dr5[3].ToString().Trim(), strClaveUN = dr5[4].ToString().Trim() });
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (intOpc == 2)
                        {
                            if (int.Parse(dr[0].ToString()) == intPadreAnt)
                                etiqJson.Add(new detallesUNegocio { intID = int.Parse(dr[0].ToString()), intPadre = -1, strUNegocio = dr[2].ToString().Trim(), strNomen = dr[3].ToString().Trim(), strClaveUN = dr[4].ToString().Trim() });
                        }
                    }
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No hay datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult consultaIDAgencia(detallesUNegocio detUnegocio)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_IDUNEGOCIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detUnegocio.intID;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<detallesUNegocio>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesUNegocio { intID = sqlDR.GetInt32(0), intPadre = sqlDR.GetInt32(1), strUNegocio = sqlDR.GetString(2).Trim(), strNomen = sqlDR.GetString(3).Trim(), strClaveUN = sqlDR.GetString(4).Trim(), dateFechaAlta = sqlDR.GetDateTime(5).ToString(), strRealizoAlta = sqlDR.GetString(6), dateFechaModif = sqlDR.GetDateTime(7).ToString(), strRealizoModif = sqlDR.GetString(8), strCalle = sqlDR.GetString(9), strNumExt = sqlDR.GetString(10), strNumInt = sqlDR.GetString(11), strColonia = sqlDR.GetString(12), strCodPost = sqlDR.GetString(13), strResponsable = sqlDR.GetString(14), strCorreo = sqlDR.GetString(15), strPagina = sqlDR.GetString(16), intAnio = sqlDR.GetInt32(17), strCasaClvLada = sqlDR.GetString(18), strCasaNumero = sqlDR.GetString(19), strCasaRecados = sqlDR.GetString(20), strOfcClvLada = sqlDR.GetString(21), strOfcNumero = sqlDR.GetString(22), strOfcExtension = sqlDR.GetString(23), strCelular = sqlDR.GetString(24), boolActivo = sqlDR.GetBoolean(25), intEstado = sqlDR.GetInt32(26), intDelMun = sqlDR.GetInt32(27), intPuestoId = sqlDR.GetInt32(28), strTipo = sqlDR.GetString(29), strFunciones = sqlDR.GetString(30) });
                    }
                    con.Close();
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No hay datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message, parametro = detUnegocio.intID });
            }
        }

        [HttpPost]

        public JsonResult actualizaUNegocio(detallesUNegocio detailsUNegocio)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_UNEGOCIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("STRNOMBRE", SqlDbType.VarChar);
                cmd.Parameters.Add("STRNOMEN", SqlDbType.Char);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("INTPADRE", SqlDbType.Int);
                cmd.Parameters.Add("STRCALLE", SqlDbType.VarChar);
                cmd.Parameters.Add("STRNUMEXT", SqlDbType.Char);
                cmd.Parameters.Add("STRNUMINT", SqlDbType.Char);
                cmd.Parameters.Add("STRCOLONIA", SqlDbType.VarChar);
                cmd.Parameters.Add("STRCODPOS", SqlDbType.VarChar);
                cmd.Parameters.Add("STRRESPONSABLE", SqlDbType.VarChar);
                cmd.Parameters.Add("STRCORREO", SqlDbType.Text);
                cmd.Parameters.Add("STRPAGINA", SqlDbType.Text);
                cmd.Parameters.Add("STRANIO", SqlDbType.Int);
                cmd.Parameters.Add("STRCASACLVLADA", SqlDbType.Char);
                cmd.Parameters.Add("STRCASANUMERO", SqlDbType.Char);
                cmd.Parameters.Add("STRCASARECADO", SqlDbType.Char);
                cmd.Parameters.Add("STROFCCLVLADA", SqlDbType.Char);
                cmd.Parameters.Add("STROFCNUMERO", SqlDbType.Char);
                cmd.Parameters.Add("STROFCEXTENSION", SqlDbType.Char);
                cmd.Parameters.Add("STRCELULAR", SqlDbType.Char);
                cmd.Parameters.Add("INTIDESTADO", SqlDbType.Int);
                cmd.Parameters.Add("INTIDDELMUN", SqlDbType.Int);
                cmd.Parameters.Add("INTPUESTOID", SqlDbType.Int);
                cmd.Parameters.Add("STRTIPO", SqlDbType.Char);
                cmd.Parameters.Add("STRFUNCIONES", SqlDbType.Char);
                //
                cmd.Parameters["STRCLAVE"].Value = detailsUNegocio.strClaveUN.ToUpper();
                cmd.Parameters["STRNOMBRE"].Value = detailsUNegocio.strUNegocio.ToUpper();
                cmd.Parameters["STRNOMEN"].Value = detailsUNegocio.strNomen.ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = detailsUNegocio.boolActivo;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["INTPADRE"].Value = intVerifica(detailsUNegocio.intPadre);
                cmd.Parameters["STRCALLE"].Value = strVerifica(detailsUNegocio.strCalle).ToUpper();
                cmd.Parameters["STRNUMEXT"].Value = strVerifica(detailsUNegocio.strNumExt).ToUpper();
                cmd.Parameters["STRNUMINT"].Value = strVerifica(detailsUNegocio.strNumInt).ToUpper();
                cmd.Parameters["STRCOLONIA"].Value = strVerifica(detailsUNegocio.strColonia).ToUpper();
                cmd.Parameters["STRCODPOS"].Value = strVerifica(detailsUNegocio.strCodPost).ToUpper();
                cmd.Parameters["STRRESPONSABLE"].Value = strVerifica(detailsUNegocio.strResponsable).ToUpper();
                cmd.Parameters["STRCORREO"].Value = strVerifica(detailsUNegocio.strCorreo);
                cmd.Parameters["STRPAGINA"].Value = strVerifica(detailsUNegocio.strPagina);
                cmd.Parameters["STRANIO"].Value = intVerifica(detailsUNegocio.intAnio);
                cmd.Parameters["STRCASACLVLADA"].Value = strVerifica(detailsUNegocio.strCasaClvLada).ToUpper();
                cmd.Parameters["STRCASANUMERO"].Value = strVerifica(detailsUNegocio.strCasaNumero).ToUpper();
                cmd.Parameters["STRCASARECADO"].Value = strVerifica(detailsUNegocio.strCasaRecados).ToUpper();
                cmd.Parameters["STROFCCLVLADA"].Value = strVerifica(detailsUNegocio.strOfcClvLada).ToUpper();
                cmd.Parameters["STROFCNUMERO"].Value = strVerifica(detailsUNegocio.strOfcNumero).ToUpper();
                cmd.Parameters["STROFCEXTENSION"].Value = strVerifica(detailsUNegocio.strOfcExtension).ToUpper();
                cmd.Parameters["STRCELULAR"].Value = strVerifica(detailsUNegocio.strCelular).ToUpper();
                cmd.Parameters["INTIDESTADO"].Value = intVerifica(detailsUNegocio.intEstado);
                cmd.Parameters["INTIDDELMUN"].Value = intVerifica(detailsUNegocio.intDelMun);
                cmd.Parameters["INTPUESTOID"].Value = intVerifica(detailsUNegocio.intPuestoId);
                cmd.Parameters["STRTIPO"].Value = strVerifica(detailsUNegocio.strTipo).ToUpper();
                cmd.Parameters["STRFUNCIONES"].Value = strVerifica(detailsUNegocio.strFunciones).ToUpper();
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
        public JsonResult borrarUNegocio(detallesUNegocio detUnegocio)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DELETE_UNEGOCIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detUnegocio.intID;
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
