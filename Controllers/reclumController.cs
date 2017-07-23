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
    public class reclumController : Controller
    {
        //private dbSIPEEntities db = new dbSIPEEntities();

        //
        // GET: /reclum/perReclum
        public ActionResult PerReclum()
        {
            return View();
        }

        // GET: /reclum/periodos
        public ActionResult Periodos()
        {
            return View();
        }
        public ActionResult _Periodos()
        {
            return View();
        }

        // GET: /reclum/prospectos
        public ActionResult Prospectos()
        {
            return View();
        }

        public ActionResult _rprospeccion()
        {
            return View();
        }

        public ActionResult _rplantrabajo()
        {
            return View();
        }

        // GET: /reclum/regProspecto
        public ActionResult regProspecto()
        {
            return View();
        }

        // GET: /reclum/etapas
        public ActionResult Calendario()
        {
            return View();
        }

        // GET: /reclum/evaluacion
        public ActionResult Evaluacion()
        {
            return View();
        }

        // GET: /reclum/evaluacion
        public ActionResult Capacita()
        {
            return View();
        }

        // GET: /reclum/documentacion
        public ActionResult Documentacion()
        {
            return View();
        }

        public ActionResult listaProsp()
        {
            return View();
        }

        public ActionResult _recreportes()
        {
            return View();
        }

        public class DetallesAgente
        {
            public string AGENTE_CLAVE { get; set; }
            public string AESTATUS_ID { get; set; }
            public string AGENTE_NOMBRE { get; set; }
            public string AGENTE_APELLIDOPATERNO { get; set; }
            public string AGENTE_APELLIDOMATERNO { get; set; }
            public string AGENTE_FECHANACIMIENTO { get; set; }
            public string AGENTE_CURP { get; set; }
            public int ECIVIL_ID { get; set; }
            public int NACIONALIDAD_ID { get; set; }
            public int SEXO_ID { get; set; }
            public string ADGENERAL_RFC { get; set; }
            public string ADGENERAL_CORREOELECTRONICO { get; set; }
            public string ADGENERAL_PAGINWEB { get; set; }
            public string ADIRECCION_CALLE { get; set; }
            public string ADIRECCION_NUMEXTERIOR { get; set; }
            public string ADIRECCION_NUMINTERIOR { get; set; }
            public string ADIRECCION_COLONIA { get; set; }
            public int EFEDERATIVA_ID { get; set; }
            public int DELEGMPIO_ID { get; set; }
            public string ADIRECCION_CODIGOPOSTAL { get; set; }
            public string ATELEFONO_CASCLVLADA { get; set; }
            public string ATELEFONO_CASNUMERO { get; set; }
            public string ATELEFONO_CASRECADO { get; set; }
            public string ATELEFONO_OFCNUMERO { get; set; }
            public string ATELEFONO_OFCCLVLADA { get; set; }
            public string ATELEFONO_OFCEXTENSION { get; set; }
            public string ATELEFONO_CELULAR { get; set; }
            public int intIDUNegocio { get; set; }
            public string STRTIPOSANGRE { get; set; }
            public string STRHOSPITAL { get; set; }
            public string STRMEDICO { get; set; }
            public string STRPCONTACTO { get; set; }
            public string STRLADAEMER { get; set; }
            public string STRNUMEMER { get; set; }
            public string STREXTEMER { get; set; }
            public string STRCELEMER { get; set; }
            public string STRALERGIAS { get; set; }
            public string STRGASTOS { get; set; }
            public bool BOOLACTIVO { get; set; }
            public string STRFECHAALTA { get; set; }
            public string STRREALIZOALTA { get; set; }
            public string STRFECHAMODIF { get; set; }
            public string STRREALIZOMODIF { get; set; }
            public int INTMATENCIONID { get; set; }
            public int INTFATRACCIONID { get; set; }
            public string STREDAD { get; set; }
            public string FOTO { get; set; }
            public string DESCRESTATUS { get; set; }
            public int INTPERIODORECLUT { get; set; }
            public int RPERIODO_ID { get; set; }
        }

        private string strVerifica(string strVerif)
        {
            if (strVerif == null)
                return "";
            return strVerif;
        }

        [HttpPost]
        public JsonResult GuardaAgente(DetallesAgente detallesAgente)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_IDUNEGOCIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detallesAgente.intIDUNegocio;
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
                string strClave = strNome + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                cmd = new SqlCommand("INSERT_AGENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("AGENTE_CLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("AESTATUS_ID", SqlDbType.Int);
                cmd.Parameters.Add("AGENTE_NOMBRE", SqlDbType.VarChar);
                cmd.Parameters.Add("AGENTE_APELLIDOPATERNO", SqlDbType.VarChar);
                cmd.Parameters.Add("AGENTE_APELLIDOMATERNO", SqlDbType.VarChar);
                cmd.Parameters.Add("AGENTE_FECHANACIMIENTO", SqlDbType.DateTime);
                cmd.Parameters.Add("AGENTE_CURP", SqlDbType.NChar);
                cmd.Parameters.Add("ECIVIL_ID", SqlDbType.Int);
                cmd.Parameters.Add("NACIONALIDAD_ID", SqlDbType.Int);
                cmd.Parameters.Add("SEXO_ID", SqlDbType.Int);
                cmd.Parameters.Add("ADGENERAL_RFC", SqlDbType.NChar);
                cmd.Parameters.Add("ADGENERAL_CORREOELECTRONICO", SqlDbType.Text);
                cmd.Parameters.Add("ADGENERAL_PAGINWEB", SqlDbType.Text);
                cmd.Parameters.Add("ADIRECCION_CALLE", SqlDbType.NChar);
                cmd.Parameters.Add("ADIRECCION_NUMEXTERIOR", SqlDbType.NChar);
                cmd.Parameters.Add("ADIRECCION_NUMINTERIOR", SqlDbType.NChar);
                cmd.Parameters.Add("ADIRECCION_COLONIA", SqlDbType.NChar);
                cmd.Parameters.Add("EFEDERATIVA_ID", SqlDbType.NChar);
                cmd.Parameters.Add("DELEGMPIO_ID", SqlDbType.NChar);
                cmd.Parameters.Add("ADIRECCION_CODIGOPOSTAL", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_CASCLVLADA", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_CASNUMERO", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_CASRECADO", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_OFCNUMERO", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_OFCCLVLADA", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_OFCEXTENSION", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_CELULAR", SqlDbType.NChar);
                cmd.Parameters.Add("STRTIPOSANGRE", SqlDbType.NChar);
                cmd.Parameters.Add("STRHOSPITAL", SqlDbType.VarChar);
                cmd.Parameters.Add("STRMEDICO", SqlDbType.VarChar);
                cmd.Parameters.Add("STRPCONTACTO", SqlDbType.VarChar);
                cmd.Parameters.Add("STRLADAEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRNUMEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STREXTEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRCELEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRALERGIAS", SqlDbType.Text);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRGASTOS", SqlDbType.VarChar);
                cmd.Parameters.Add("STREDAD", SqlDbType.NChar);
                cmd.Parameters.Add("MATENCION", SqlDbType.Int);
                cmd.Parameters.Add("FATRACCION", SqlDbType.Int);
                cmd.Parameters.Add("FOTO", SqlDbType.VarChar);
                cmd.Parameters.Add("PERREC", SqlDbType.Int);
                cmd.Parameters["AGENTE_CLAVE"].Value = strClave;
                cmd.Parameters["AESTATUS_ID"].Value = detallesAgente.AESTATUS_ID;
                cmd.Parameters["AGENTE_NOMBRE"].Value = detallesAgente.AGENTE_NOMBRE.ToUpper();
                cmd.Parameters["AGENTE_APELLIDOPATERNO"].Value = detallesAgente.AGENTE_APELLIDOPATERNO.ToUpper();
                cmd.Parameters["AGENTE_APELLIDOMATERNO"].Value = detallesAgente.AGENTE_APELLIDOMATERNO.ToUpper();
                if(detallesAgente.AGENTE_FECHANACIMIENTO.Length <= 2)
                    cmd.Parameters["AGENTE_FECHANACIMIENTO"].Value = DateTime.Now.ToShortDateString();
                else
                    cmd.Parameters["AGENTE_FECHANACIMIENTO"].Value = detallesAgente.AGENTE_FECHANACIMIENTO.ToUpper();
                cmd.Parameters["AGENTE_CURP"].Value = strVerifica(detallesAgente.AGENTE_CURP).ToUpper();
                cmd.Parameters["ECIVIL_ID"].Value = detallesAgente.ECIVIL_ID;
                cmd.Parameters["NACIONALIDAD_ID"].Value = detallesAgente.NACIONALIDAD_ID;
                cmd.Parameters["SEXO_ID"].Value = detallesAgente.SEXO_ID;
                cmd.Parameters["ADGENERAL_RFC"].Value = strVerifica(detallesAgente.ADGENERAL_RFC).ToUpper();
                cmd.Parameters["ADGENERAL_CORREOELECTRONICO"].Value = strVerifica(detallesAgente.ADGENERAL_CORREOELECTRONICO);
                cmd.Parameters["ADGENERAL_PAGINWEB"].Value = strVerifica(detallesAgente.ADGENERAL_PAGINWEB);
                cmd.Parameters["ADIRECCION_CALLE"].Value = detallesAgente.ADIRECCION_CALLE.ToUpper();
                cmd.Parameters["ADIRECCION_NUMEXTERIOR"].Value = detallesAgente.ADIRECCION_NUMEXTERIOR.ToUpper();
                cmd.Parameters["ADIRECCION_NUMINTERIOR"].Value = strVerifica(detallesAgente.ADIRECCION_NUMINTERIOR).ToUpper();
                cmd.Parameters["ADIRECCION_COLONIA"].Value = detallesAgente.ADIRECCION_COLONIA.ToUpper();
                cmd.Parameters["EFEDERATIVA_ID"].Value = detallesAgente.EFEDERATIVA_ID;
                cmd.Parameters["DELEGMPIO_ID"].Value = detallesAgente.DELEGMPIO_ID;
                cmd.Parameters["ADIRECCION_CODIGOPOSTAL"].Value = detallesAgente.ADIRECCION_CODIGOPOSTAL.ToUpper();
                cmd.Parameters["ATELEFONO_CASCLVLADA"].Value = strVerifica(detallesAgente.ATELEFONO_CASCLVLADA).ToUpper();
                cmd.Parameters["ATELEFONO_CASNUMERO"].Value = strVerifica(detallesAgente.ATELEFONO_CASNUMERO).ToUpper();
                cmd.Parameters["ATELEFONO_CASRECADO"].Value = strVerifica(detallesAgente.ATELEFONO_CASRECADO).ToUpper();
                cmd.Parameters["ATELEFONO_OFCNUMERO"].Value = strVerifica(detallesAgente.ATELEFONO_OFCNUMERO).ToUpper();
                cmd.Parameters["ATELEFONO_OFCCLVLADA"].Value = strVerifica(detallesAgente.ATELEFONO_OFCCLVLADA).ToUpper();
                cmd.Parameters["ATELEFONO_OFCEXTENSION"].Value = strVerifica(detallesAgente.ATELEFONO_OFCEXTENSION).ToUpper();
                cmd.Parameters["ATELEFONO_CELULAR"].Value = strVerifica(detallesAgente.ATELEFONO_CELULAR).ToUpper();
                cmd.Parameters["STRTIPOSANGRE"].Value = strVerifica(detallesAgente.STRTIPOSANGRE).ToUpper();
                cmd.Parameters["STRHOSPITAL"].Value = strVerifica(detallesAgente.STRHOSPITAL).ToUpper();
                cmd.Parameters["STRMEDICO"].Value = strVerifica(detallesAgente.STRMEDICO).ToUpper();
                cmd.Parameters["STRPCONTACTO"].Value = strVerifica(detallesAgente.STRMEDICO).ToUpper();
                cmd.Parameters["STRLADAEMER"].Value = strVerifica(detallesAgente.STRLADAEMER).ToUpper();
                cmd.Parameters["STRNUMEMER"].Value = strVerifica(detallesAgente.STRNUMEMER).ToUpper();
                cmd.Parameters["STRCELEMER"].Value = strVerifica(detallesAgente.STRCELEMER).ToUpper();
                cmd.Parameters["STREXTEMER"].Value = strVerifica(detallesAgente.STREXTEMER).ToUpper();
                cmd.Parameters["STRALERGIAS"].Value = strVerifica(detallesAgente.STRALERGIAS).ToUpper();
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["STRGASTOS"].Value = strVerifica(detallesAgente.STRGASTOS).ToUpper();
                cmd.Parameters["STREDAD"].Value = strVerifica(detallesAgente.STREDAD).ToUpper();
                cmd.Parameters["MATENCION"].Value = detallesAgente.INTMATENCIONID;
                cmd.Parameters["FATRACCION"].Value = detallesAgente.INTFATRACCIONID;
                cmd.Parameters["FOTO"].Value = strVerifica(detallesAgente.FOTO).ToUpper();
                cmd.Parameters["PERREC"].Value = detallesAgente.INTPERIODORECLUT;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true, mensaje = strClave });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult ActualizaAgente(DetallesAgente detallesAgente)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_AGENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("AGENTE_CLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("AESTATUS_ID", SqlDbType.Int);
                cmd.Parameters.Add("AGENTE_NOMBRE", SqlDbType.VarChar);
                cmd.Parameters.Add("AGENTE_APELLIDOPATERNO", SqlDbType.VarChar);
                cmd.Parameters.Add("AGENTE_APELLIDOMATERNO", SqlDbType.VarChar);
                cmd.Parameters.Add("AGENTE_FECHANACIMIENTO", SqlDbType.DateTime);
                cmd.Parameters.Add("AGENTE_CURP", SqlDbType.NChar);
                cmd.Parameters.Add("ECIVIL_ID", SqlDbType.Int);
                cmd.Parameters.Add("NACIONALIDAD_ID", SqlDbType.Int);
                cmd.Parameters.Add("SEXO_ID", SqlDbType.Int);
                cmd.Parameters.Add("ADGENERAL_RFC", SqlDbType.NChar);
                cmd.Parameters.Add("ADGENERAL_CORREOELECTRONICO", SqlDbType.Text);
                cmd.Parameters.Add("ADGENERAL_PAGINWEB", SqlDbType.Text);
                cmd.Parameters.Add("ADIRECCION_CALLE", SqlDbType.NChar);
                cmd.Parameters.Add("ADIRECCION_NUMEXTERIOR", SqlDbType.NChar);
                cmd.Parameters.Add("ADIRECCION_NUMINTERIOR", SqlDbType.NChar);
                cmd.Parameters.Add("ADIRECCION_COLONIA", SqlDbType.NChar);
                cmd.Parameters.Add("EFEDERATIVA_ID", SqlDbType.NChar);
                cmd.Parameters.Add("DELEGMPIO_ID", SqlDbType.NChar);
                cmd.Parameters.Add("ADIRECCION_CODIGOPOSTAL", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_CASCLVLADA", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_CASNUMERO", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_CASRECADO", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_OFCNUMERO", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_OFCCLVLADA", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_OFCEXTENSION", SqlDbType.NChar);
                cmd.Parameters.Add("ATELEFONO_CELULAR", SqlDbType.NChar);
                cmd.Parameters.Add("STRTIPOSANGRE", SqlDbType.NChar);
                cmd.Parameters.Add("STRHOSPITAL", SqlDbType.VarChar);
                cmd.Parameters.Add("STRMEDICO", SqlDbType.VarChar);
                cmd.Parameters.Add("STRPCONTACTO", SqlDbType.VarChar);
                cmd.Parameters.Add("STRLADAEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRNUMEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STREXTEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRCELEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRALERGIAS", SqlDbType.Text);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRGASTOS", SqlDbType.VarChar);
                cmd.Parameters.Add("STREDAD", SqlDbType.NChar);
                cmd.Parameters.Add("MATENCION", SqlDbType.Int);
                cmd.Parameters.Add("FATRACCION", SqlDbType.Int);
                cmd.Parameters.Add("FOTO", SqlDbType.VarChar);
                cmd.Parameters.Add("PERREC", SqlDbType.Int);
                cmd.Parameters["AGENTE_CLAVE"].Value = detallesAgente.AGENTE_CLAVE;
                cmd.Parameters["AESTATUS_ID"].Value = detallesAgente.AESTATUS_ID;
                cmd.Parameters["AGENTE_NOMBRE"].Value = detallesAgente.AGENTE_NOMBRE.ToUpper();
                cmd.Parameters["AGENTE_APELLIDOPATERNO"].Value = detallesAgente.AGENTE_APELLIDOPATERNO.ToUpper();
                cmd.Parameters["AGENTE_APELLIDOMATERNO"].Value = detallesAgente.AGENTE_APELLIDOMATERNO.ToUpper();
                cmd.Parameters["AGENTE_FECHANACIMIENTO"].Value = detallesAgente.AGENTE_FECHANACIMIENTO.ToUpper();
                cmd.Parameters["AGENTE_CURP"].Value = strVerifica(detallesAgente.AGENTE_CURP).ToUpper();
                cmd.Parameters["ECIVIL_ID"].Value = detallesAgente.ECIVIL_ID;
                cmd.Parameters["NACIONALIDAD_ID"].Value = detallesAgente.NACIONALIDAD_ID;
                cmd.Parameters["SEXO_ID"].Value = detallesAgente.SEXO_ID;
                cmd.Parameters["ADGENERAL_RFC"].Value = strVerifica(detallesAgente.ADGENERAL_RFC).ToUpper();
                cmd.Parameters["ADGENERAL_CORREOELECTRONICO"].Value = strVerifica(detallesAgente.ADGENERAL_CORREOELECTRONICO);
                cmd.Parameters["ADGENERAL_PAGINWEB"].Value = strVerifica(detallesAgente.ADGENERAL_PAGINWEB);
                cmd.Parameters["ADIRECCION_CALLE"].Value = detallesAgente.ADIRECCION_CALLE.ToUpper();
                cmd.Parameters["ADIRECCION_NUMEXTERIOR"].Value = detallesAgente.ADIRECCION_NUMEXTERIOR.ToUpper();
                cmd.Parameters["ADIRECCION_NUMINTERIOR"].Value = strVerifica(detallesAgente.ADIRECCION_NUMINTERIOR).ToUpper();
                cmd.Parameters["ADIRECCION_COLONIA"].Value = detallesAgente.ADIRECCION_COLONIA.ToUpper();
                cmd.Parameters["EFEDERATIVA_ID"].Value = detallesAgente.EFEDERATIVA_ID;
                cmd.Parameters["DELEGMPIO_ID"].Value = detallesAgente.DELEGMPIO_ID;
                cmd.Parameters["ADIRECCION_CODIGOPOSTAL"].Value = detallesAgente.ADIRECCION_CODIGOPOSTAL.ToUpper();
                cmd.Parameters["ATELEFONO_CASCLVLADA"].Value = strVerifica(detallesAgente.ATELEFONO_CASCLVLADA).ToUpper();
                cmd.Parameters["ATELEFONO_CASNUMERO"].Value = strVerifica(detallesAgente.ATELEFONO_CASNUMERO).ToUpper();
                cmd.Parameters["ATELEFONO_CASRECADO"].Value = strVerifica(detallesAgente.ATELEFONO_CASRECADO).ToUpper();
                cmd.Parameters["ATELEFONO_OFCNUMERO"].Value = strVerifica(detallesAgente.ATELEFONO_OFCNUMERO).ToUpper();
                cmd.Parameters["ATELEFONO_OFCCLVLADA"].Value = strVerifica(detallesAgente.ATELEFONO_OFCCLVLADA).ToUpper();
                cmd.Parameters["ATELEFONO_OFCEXTENSION"].Value = strVerifica(detallesAgente.ATELEFONO_OFCEXTENSION).ToUpper();
                cmd.Parameters["ATELEFONO_CELULAR"].Value = strVerifica(detallesAgente.ATELEFONO_CELULAR).ToUpper();
                cmd.Parameters["STRTIPOSANGRE"].Value = strVerifica(detallesAgente.STRTIPOSANGRE).ToUpper();
                cmd.Parameters["STRHOSPITAL"].Value = strVerifica(detallesAgente.STRHOSPITAL).ToUpper();
                cmd.Parameters["STRMEDICO"].Value = strVerifica(detallesAgente.STRMEDICO).ToUpper();
                cmd.Parameters["STRPCONTACTO"].Value = strVerifica(detallesAgente.STRMEDICO).ToUpper();
                cmd.Parameters["STRLADAEMER"].Value = strVerifica(detallesAgente.STRLADAEMER).ToUpper();
                cmd.Parameters["STRNUMEMER"].Value = strVerifica(detallesAgente.STRNUMEMER).ToUpper();
                cmd.Parameters["STRCELEMER"].Value = strVerifica(detallesAgente.STRCELEMER).ToUpper();
                cmd.Parameters["STREXTEMER"].Value = strVerifica(detallesAgente.STREXTEMER).ToUpper();
                cmd.Parameters["STRALERGIAS"].Value = strVerifica(detallesAgente.STRALERGIAS).ToUpper();
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString().ToUpper();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString().ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["STRGASTOS"].Value = strVerifica(detallesAgente.STRGASTOS).ToUpper();
                cmd.Parameters["STREDAD"].Value = strVerifica(detallesAgente.STREDAD).ToUpper();
                cmd.Parameters["MATENCION"].Value = detallesAgente.INTMATENCIONID;
                cmd.Parameters["FATRACCION"].Value = detallesAgente.INTFATRACCIONID;
                cmd.Parameters["FOTO"].Value = strVerifica(detallesAgente.FOTO);
                cmd.Parameters["PERREC"].Value = detallesAgente.INTPERIODORECLUT;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true, mensaje = detallesAgente.AGENTE_CLAVE });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult cargaAgenteId(DetallesAgente detallesAgente)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_AGENTEID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["CLAVE"].Value = detallesAgente.AGENTE_CLAVE;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<DetallesAgente>();
                    while (sqlDR.Read())
                    {
                        int intPer = 1;
                        if (!sqlDR.IsDBNull(45))
                            intPer = sqlDR.GetInt32(45);
                        etiqJson.Add(new DetallesAgente { AGENTE_CLAVE = sqlDR.GetString(0).Trim(), AGENTE_NOMBRE = sqlDR.GetString(1).Trim(), AGENTE_APELLIDOPATERNO = sqlDR.GetString(2).Trim(), AGENTE_APELLIDOMATERNO = sqlDR.GetString(3).Trim(), AGENTE_FECHANACIMIENTO = sqlDR.GetDateTime(4).ToShortDateString(), AGENTE_CURP = sqlDR.GetString(5).Trim(), BOOLACTIVO = sqlDR.GetBoolean(6), STRFECHAALTA = sqlDR.GetDateTime(7).ToString(), STRFECHAMODIF = sqlDR.GetDateTime(8).ToString(), ECIVIL_ID = sqlDR.GetInt32(9), NACIONALIDAD_ID = sqlDR.GetInt32(10), SEXO_ID = sqlDR.GetInt32(11), ADGENERAL_RFC = sqlDR.GetString(12).Trim(), ADGENERAL_CORREOELECTRONICO = sqlDR.GetString(13).Trim(), ADGENERAL_PAGINWEB = sqlDR.GetString(14).Trim(), ADIRECCION_CALLE = sqlDR.GetString(15).Trim(), ADIRECCION_NUMEXTERIOR = sqlDR.GetString(16).Trim(), ADIRECCION_NUMINTERIOR = sqlDR.GetString(17).Trim(), ADIRECCION_COLONIA = sqlDR.GetString(18).Trim(), EFEDERATIVA_ID = sqlDR.GetInt32(19), DELEGMPIO_ID = sqlDR.GetInt32(20), ADIRECCION_CODIGOPOSTAL = sqlDR.GetString(21).Trim(), ATELEFONO_CASCLVLADA = sqlDR.GetString(22).Trim(), ATELEFONO_CASNUMERO = sqlDR.GetString(23).Trim(), ATELEFONO_CASRECADO = sqlDR.GetString(24).Trim(), ATELEFONO_OFCNUMERO = sqlDR.GetString(25).Trim(), ATELEFONO_OFCCLVLADA = sqlDR.GetString(26).Trim(), ATELEFONO_OFCEXTENSION = sqlDR.GetString(27).Trim(), ATELEFONO_CELULAR = sqlDR.GetString(28).Trim(), STRTIPOSANGRE = sqlDR.GetString(29).Trim(), STRHOSPITAL = sqlDR.GetString(30).Trim(), STRMEDICO = sqlDR.GetString(31).Trim(), STRLADAEMER = sqlDR.GetString(32).Trim(), STRNUMEMER = sqlDR.GetString(33).Trim(), STREXTEMER = sqlDR.GetString(34).Trim(), STRCELEMER = sqlDR.GetString(35).Trim(), STRALERGIAS = sqlDR.GetString(36).Trim(), STRGASTOS = sqlDR.GetString(37).Trim(), STRREALIZOALTA = sqlDR.GetString(38).Trim(), STRREALIZOMODIF = sqlDR.GetString(39).Trim(), INTMATENCIONID = sqlDR.GetInt32(40), INTFATRACCIONID = sqlDR.GetInt32(41), STREDAD = sqlDR.GetString(42).Trim(), FOTO = sqlDR.GetString(43).Trim(), AESTATUS_ID = sqlDR.GetInt32(44).ToString(), STRPCONTACTO = sqlDR.GetString(31).Trim(), INTPERIODORECLUT = intPer });
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
        public JsonResult verListaProsp()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_AGENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", SqlDbType.Int);
                cmd.Parameters["ID"].Value = Session["intUneg"].ToString();
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<DetallesAgente>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new DetallesAgente { AGENTE_CLAVE = sqlDR.GetString(0).Trim(), AGENTE_NOMBRE = sqlDR.GetString(1).Trim() + " " + sqlDR.GetString(2).Trim() + " " + sqlDR.GetString(3).Trim(), AGENTE_FECHANACIMIENTO = sqlDR.GetDateTime(4).ToShortDateString(), DESCRESTATUS = sqlDR.GetString(5).ToUpper().Trim(), RPERIODO_ID = sqlDR.GetInt32(6) });
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

        public JsonResult GetGanttData()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_RECPER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTEST", SqlDbType.Int);
                cmd.Parameters.Add("STRANIO", SqlDbType.Char);
                cmd.Parameters.Add("INTUNID", SqlDbType.Int);
                if(this.Request.QueryString["idPer"] == null)
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
                    con.Close();
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

        [HttpPost]
        public JsonResult guardarRecPeriodo(GanttTask ganttTask)
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
                cmd.Parameters["ID"].Value = 1;
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
                    cmd.Parameters["ID"].Value = 1;
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
                    cmd.Parameters["ID"].Value = 1;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                string strNewClave = strNome + "-" + intConsec.ToString("00") + "-" + Session["anioTrab"].ToString();
                cmd = new SqlCommand("INSERT_RECPER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRDESCR", SqlDbType.VarChar);
                cmd.Parameters.Add("STRFECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("STROBSERV", SqlDbType.Text);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("INTESTATUS", SqlDbType.Int);
                cmd.Parameters.Add("INTANIO", SqlDbType.Int);
                cmd.Parameters.Add("INTUNID", SqlDbType.Int);
                cmd.Parameters.Add("STRCLAVE", SqlDbType.VarChar);
                cmd.Parameters["STRDESCR"].Value = ganttTask.text.ToUpper();
                cmd.Parameters["STRFECHAINI"].Value = ganttTask.start_date;
                cmd.Parameters["STRFECHAFIN"].Value = ganttTask.end_date;
                cmd.Parameters["STROBSERV"].Value = strVerifica(ganttTask.strObserv).ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["INTESTATUS"].Value = ganttTask.duration;
                cmd.Parameters["INTANIO"].Value = ganttTask.intAnio;
                cmd.Parameters["INTUNID"].Value = Session["intUneg"].ToString();
                cmd.Parameters["STRCLAVE"].Value = strNewClave;
                con.Open();
                int intID = (Int32)(cmd.ExecuteScalar());
                con.Close();
                if (intID > 0)
                {
                    if (ganttTask.items[0] != null)
                    {
                        foreach (RFAtraccion rfAtrac in ganttTask.items)
                        {
                            cmd = new SqlCommand("INSERT_RPFUENTE_ATRACCION", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("INTPERIODO", SqlDbType.Int);
                            cmd.Parameters.Add("INTFUENTE", SqlDbType.Int);
                            cmd.Parameters.Add("STRFECHAALTA", SqlDbType.DateTime);
                            cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                            cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                            cmd.Parameters["INTPERIODO"].Value = intID;
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
                                cmd.Parameters["STROBSERV"].Value = strVerifica(rfAtrac.observ).ToUpper();
                                cmd.Parameters["STRIMA"].Value = "";
                                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                                cmd.Parameters["BOOLACTIVO"].Value = true;
                                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        return Json(new { success = true, mensaje = strNewClave });
                    }
                    return Json(new { success = true, mensaje = strNewClave });
                }
                return Json(new { success = false, mensaje = "No se pudo insertar las fuentes de atracción" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult actualizaRecPeriodo(GanttTask ganttTask)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_RECPER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters.Add("STRDESCR", SqlDbType.VarChar);
                cmd.Parameters.Add("STRFECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("STRFECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("STROBSERV", SqlDbType.Text);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("INTESTATUS", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = ganttTask.id;
                cmd.Parameters["STRDESCR"].Value = ganttTask.text.ToUpper();
                cmd.Parameters["STRFECHAINI"].Value = ganttTask.start_date;
                cmd.Parameters["STRFECHAFIN"].Value = ganttTask.end_date;
                cmd.Parameters["STROBSERV"].Value = ganttTask.strObserv.ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = true;//ganttTask.open;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["INTESTATUS"].Value = ganttTask.duration;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                foreach (RFAtraccion rfAtrac in ganttTask.items)
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
                }
                return Json(new { success = true });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult seleccionaRecPeriodo(RFAtraccion ganttTask)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_RFATRACCION", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = ganttTask.idPer;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                var rfatraccion = new List<RFAtraccion>();
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        RFAtraccion temp = new RFAtraccion();
                        temp.idRFAPub = sqlDR.GetInt32(0);
                        temp.idRFA = sqlDR.GetInt32(1);
                        temp.idFA = sqlDR.GetInt32(2);
                        temp.idComp = sqlDR.GetInt32(3);
                        temp.idEsta = sqlDR.GetInt32(4);
                        temp.fechaIni = sqlDR.GetDateTime(5).ToShortDateString();
                        temp.fechaPub = sqlDR.GetDateTime(6).ToShortDateString();
                        temp.texto = sqlDR.GetString(7);
                        temp.observ = sqlDR.GetString(8);
                        temp.imagen = sqlDR.GetString(9);
                        rfatraccion.Add(temp);
                    }
                }
                con.Close();
                cmd = new SqlCommand("SELECT_RECPERID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = ganttTask.idPer;
                con.Open();
                sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        return Json(new { success = true, strDescr = sqlDR.GetString(0), strObserv = sqlDR.GetString(1), strInicio = sqlDR.GetDateTime(2).ToShortDateString(), strFin = sqlDR.GetDateTime(3).ToShortDateString(), intEstatus = sqlDR.GetInt32(4), boolActivo = sqlDR.GetBoolean(5), items = rfatraccion });
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
                SqlCommand cmd = new SqlCommand("UPDATE_RECPERDRAG", con);
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
        public JsonResult verPeriodos()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_RECPER", con);
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
                        dataJson.Add(new GanttTask { id = sqlDR.GetInt32(0), text = sqlDR.GetString(1).Trim() });
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
        public JsonResult verCitaAgente(citaAgente datosCita)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CITAAGENTE2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                cmd.Parameters["STRCLAVE"].Value = datosCita.claveAgente;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<citaAgente>();
                    while (sqlDR.Read())
                    {
                        string evalTemp = "CAG" + sqlDR.GetInt32(0).ToString();
                        string descrEval = sqlDR.GetString(2).Trim();
                        string fechaIni = sqlDR.GetDateTime(1).ToShortDateString();
                        string fechaTerm = DateTime.Now.ToShortDateString();
                        decimal calificacion = 0.0M;
                        string react = "";
                        bool seleccion = false;
                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                        SqlCommand cmd1 = new SqlCommand("SELECT_EVALUACIONAGENTE", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("IDCITA", SqlDbType.NChar);
                        cmd1.Parameters["IDCITA"].Value = sqlDR.GetInt32(0);
                        con1.Open();
                        SqlDataReader sqlDR1 = cmd1.ExecuteReader();
                        if (sqlDR1.HasRows)
                        {
                            while (sqlDR1.Read())
                            {
                                evalTemp = "EVA" + sqlDR1.GetInt32(0).ToString();
                                fechaIni = sqlDR1.GetDateTime(1).ToShortDateString();
                                fechaTerm = sqlDR1.GetDateTime(2).ToShortDateString();
                                calificacion = sqlDR1.GetDecimal(3);
                                react = sqlDR1.GetString(4);
                                seleccion = true;
                            }
                        }
                        con1.Close();
                        dataJson.Add(new citaAgente { idEval = evalTemp, observaCita = descrEval, fechaInicio = fechaIni, fechaFin = fechaTerm, decCalif = calificacion, boolSelect = seleccion, reactivo = react });
                    }
                    con.Close();
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo acceder a la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verCriterioEval(citaAgente datosCita)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CRITERIOBYTEXT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRTEVAL", SqlDbType.NChar);
                cmd.Parameters["STRTEVAL"].Value = datosCita.observaCita;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                var dataJson = new List<citaAgente>();
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        string idCriterio = "CRI" + sqlDR.GetInt32(0).ToString();
                        string Reactivo = sqlDR.GetString(1).Trim();
                        decimal calif = 0.0M;
                        bool seleccion = false;
                        if (datosCita.idEval.Contains("EVA"))
                        {
                            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                            SqlCommand cmd1 = new SqlCommand("SELECT_EVALCRITERIOBYID", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("IDEVAL", SqlDbType.Int);
                            cmd1.Parameters.Add("IDCRIT", SqlDbType.Int);
                            cmd1.Parameters["IDEVAL"].Value = int.Parse(datosCita.idEval.Replace("EVA",""));
                            cmd1.Parameters["IDCRIT"].Value = sqlDR.GetInt32(0);
                            con1.Open();
                            SqlDataReader sqlDR1 = cmd1.ExecuteReader();
                            if (sqlDR1.HasRows)
                            {
                                while (sqlDR1.Read())
                                {
                                    idCriterio = "ECR" + sqlDR1.GetInt32(0).ToString();
                                    Reactivo = sqlDR1.GetString(2).Trim();
                                    calif = sqlDR1.GetDecimal(1);
                                    seleccion = true;
                                }
                            }
                            dataJson.Add(new citaAgente { idCri = idCriterio, reactivo = Reactivo, decCalif = calif, boolSelect = seleccion });
                            con1.Close();
                        }
                    }
                    con.Close();
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo acceder a la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult guardaEvaluacion(citaAgente datosCita)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string comando = "";
                if(datosCita.idEval.Contains("CAG"))
                    comando = "INSERT_EVALUACIONAGENTE";
                else if(datosCita.idEval.Contains("EVA"))
                    comando = "UPDATE_EVALUACIONAGENTE";
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (datosCita.idEval.Contains("CAG"))
                {
                    cmd.Parameters.Add("IDCITA", SqlDbType.Int);
                    cmd.Parameters["IDCITA"].Value = datosCita.idEval.Replace("CAG","");
                }
                else if (datosCita.idEval.Contains("EVA"))
                {
                    cmd.Parameters.Add("IDEVAL", SqlDbType.Int);
                    cmd.Parameters["IDEVAL"].Value = datosCita.idEval.Replace("EVA","");
                }
                cmd.Parameters.Add("FECHAPROG", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAREAL", SqlDbType.DateTime);
                cmd.Parameters.Add("CALIF", SqlDbType.Decimal);
                cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters["FECHAPROG"].Value = datosCita.fechaInicio;
                cmd.Parameters["FECHAREAL"].Value = datosCita.fechaFin;
                cmd.Parameters["CALIF"].Value = datosCita.decCalif;
                cmd.Parameters["OBSERV"].Value = strVerifica(datosCita.observaCita).ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                con.Open();
                int intIDNuevo = 0;
                if (datosCita.idEval.Contains("CAG"))
                    intIDNuevo = (Int32)(cmd.ExecuteScalar());
                else if (datosCita.idEval.Contains("EVA"))
                {
                    intIDNuevo = int.Parse(datosCita.idEval.Replace("EVA", ""));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                comando = "";
                foreach (citaAgente reg in datosCita.criterios)
                {
                    if (reg.boolSelect)
                    {
                        if (reg.idCri.Contains("CRI"))
                            comando = "INSERT_EVALCRITERIO";
                        else if (reg.idCri.Contains("ECR"))
                            comando = "UPDATE_EVALCRITERIO";
                        cmd = new SqlCommand(comando, con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (reg.idCri.Contains("CRI"))
                        {
                            cmd.Parameters.Add("IDEVAL", SqlDbType.Int);
                            cmd.Parameters.Add("IDCRIT", SqlDbType.Int);
                            cmd.Parameters["IDEVAL"].Value = intIDNuevo;
                            cmd.Parameters["IDCRIT"].Value = reg.idCri.Replace("CRI", "");
                        }
                        else if (reg.idCri.Contains("ECR"))
                        {
                            cmd.Parameters.Add("IDEVALCRIT", SqlDbType.Int);
                            cmd.Parameters["IDEVALCRIT"].Value = reg.idCri.Replace("ECR", "");
                        }
                        cmd.Parameters.Add("PONDER", SqlDbType.Decimal);
                        cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                        cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                        cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                        cmd.Parameters["PONDER"].Value = datosCita.decCalif;
                        cmd.Parameters["BOOLACTIVO"].Value = true;
                        cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                        cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }

        }

        public class archivosAgente
        {
            public int iddocum { get; set; }
            public string documento { get; set; }
            public string html { get; set; }
        }

        [HttpPost]
        public JsonResult verDocumentosArchivos(string strclave)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.VarChar);
                cmd.Parameters["INTOPC"].Value = 33;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<archivosAgente> etiqJson = new List<archivosAgente>();
                    while (sqlDR.Read())
                    {
                        int intDoc = sqlDR.GetInt32(0);
                        string docto = sqlDR.GetString(1);
                        string htmls = "<input id='DOCU" + sqlDR.GetInt32(0) + "' type='file' class='btn btn-default' />";
                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                        SqlCommand cmd1 = new SqlCommand("SELECT_DOCUMENTOAGENTE", con1);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("DOCUMENTO_ID", SqlDbType.Int);
                        cmd1.Parameters.Add("AGENTE_CLAVE", SqlDbType.NChar);
                        cmd1.Parameters["DOCUMENTO_ID"].Value = sqlDR.GetInt32(0);
                        cmd1.Parameters["AGENTE_CLAVE"].Value = strclave;
                        con1.Open();
                        SqlDataReader sqlDr1 = cmd1.ExecuteReader();
                        if (sqlDr1.HasRows)
                        {
                            while (sqlDr1.Read())
                            {
                                htmls = "<div class='row'><div class='col-sm-10 col-md-10 col-lg-10'><input id='DOCA" + sqlDr1.GetInt32(0) + "' type='file' class='btn btn-default' /></div><div class='col-sm-2 col-md-2 col-lg-2'><a href='/archivos/" + Session["intUneg"].ToString() + "/" + strclave + "/" + sqlDr1.GetString(1) + "' target='_blank'><i class='fa fa-file-pdf-o fa-2x'></i></a></div></div>";
                            }
                        }
                        con1.Close();
                        etiqJson.Add(new archivosAgente { iddocum = intDoc, documento = docto, html = htmls });
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

        public class detallesCapAgente
        {
            public int id { get; set; }
            public string descr { get; set; }
            public string fechaprogr { get; set; }
            public string fechareal { get; set; }
            public decimal calificacion { get; set; }
            public bool activo { get; set; }
        }

        [HttpPost]
        public JsonResult verCapacitaAgente(string strClv, int idPer)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CAPAGENTE2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDPER", SqlDbType.Int);
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["IDPER"].Value = idPer;
                cmd.Parameters["CLAVE"].Value = strClv;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<detallesCapAgente>();
                    while (sqlDR.Read())
                    {
                        string strFechaReal = "";
                        decimal decCalif = 0M;
                        if (!sqlDR.IsDBNull(3))
                            strFechaReal = sqlDR.GetDateTime(3).ToShortDateString();
                        if (!sqlDR.IsDBNull(4))
                            decCalif = sqlDR.GetDecimal(4);
                        dataJson.Add(new detallesCapAgente { id = sqlDR.GetInt32(0), fechaprogr = sqlDR.GetDateTime(2).ToShortDateString(), fechareal = strFechaReal, calificacion = decCalif, descr = sqlDR.GetString(5).Trim(), activo = sqlDR.GetBoolean(6) });
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

        public class detallesTemario
        {
            public int id { get; set; }
            public string clave { get; set; }
            public string descr { get; set; }
            public decimal calif { get; set; }
            public bool activo { get; set; }
        }

        [HttpPost]
        public JsonResult verTemasPrograma(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_TEMAPROGRAMA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDPROG", SqlDbType.Int);
                cmd.Parameters["IDPROG"].Value = id;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var dataJson = new List<detallesTemario>();
                    while (sqlDR.Read())
                    {
                        dataJson.Add(new detallesTemario { id = sqlDR.GetInt32(0), clave = sqlDR.GetString(1), descr = sqlDR.GetString(2), calif = 0M, activo = sqlDR.GetBoolean(3) });
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
        public JsonResult borrarPerReclum(GanttTask detPeriodo)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DELETE_PERRECLUM", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detPeriodo.id;
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
        public JsonResult borrarPerFuente(RFAtraccion detFuente)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DELETE_PERFUENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detFuente.idRFA;
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