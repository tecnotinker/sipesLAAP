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
    public class organizadorController : Controller
    {
        //
        // GET: /organizador/lista_prospectos_clientes
        public ActionResult listaProspCli()
        {
            return View();
        }

        public ActionResult _listaProspCli()
        {
            return View();
        }

        public ActionResult _plantrabajo()
        {
            return View();
        }

        public ActionResult _ordentrabajo()
        {
            return View();
        }

        public ActionResult _polizas()
        {
            return View();
        }

        public ActionResult _cobranza()
        {
            return View();
        }

        public ActionResult _estadisticas()
        {
            return View();
        }

        public ActionResult _capacitacion()
        {
            return View();
        }

        public ActionResult _prospeccion()
        {
            return View();
        }

        public ActionResult _ServCli()
        {
            return View();
        }

        // GET: /organizador/registro_prospectos_clientes
        public ActionResult regCliente()
        {
            return View();
        }

        // GET: /organizador/califica_clientes
        public ActionResult califica()
        {
            return View();
        }

        // GET: /organizador/orden_trabajo
        public ActionResult ordentrab()
        {
            return View();
        }

        // GET: /organizador/orientacion
        public ActionResult orientacion()
        {
            return View();
        }

        public ActionResult _orientacion()
        {
            return View();
        }

        // GET: /organizador/metas
        public ActionResult metas()
        {
            return View();
        }

        public ActionResult _metasAgente()
        {
            return View();
        }

        // GET: /organizador/necesidades
        public ActionResult necesidades()
        {
            return View();
        }

        // GET: /organizador/planeador
        public ActionResult planeador()
        {
            return View();
        }

        // GET: /organizador/seguimiento
        public ActionResult seguimiento()
        {
            return View();
        }

        // GET: /organizador/doctos
        public ActionResult doctosgrales()
        {
            return View();
        }

        // GET: /organizador/calpagos
        public ActionResult calpagos()
        {
            return View();
        }

        public class DetallesAgente
        {
            public string CLIENTE_CLAVE { get; set; }
            public string CESTATUS_ID { get; set; }
            public string CLIENTE_NOMBRE { get; set; }
            public string CLIENTE_APELLIDOPATERNO { get; set; }
            public string CLIENTE_APELLIDOMATERNO { get; set; }
            public string CLIENTE_FECHANACIMIENTO { get; set; }
            public string CLIENTE_CURP { get; set; }
            public int ECIVIL_ID { get; set; }
            public int NACIONALIDAD_ID { get; set; }
            public int SEXO_ID { get; set; }
            public string CGENERAL_RFC { get; set; }
            public string CGENERAL_CORREOELECTRONICO { get; set; }
            public string CGENERAL_PAGINAWEB { get; set; }
            public string CDIRECCION_CALLE { get; set; }
            public string CDIRECCION_NUMEXTERIOR { get; set; }
            public string CDIRECCION_NUMINTERIOR { get; set; }
            public string CDIRECCION_COLONIA { get; set; }
            public int EFEDERATIVA_ID { get; set; }
            public int DELEGMPIO_ID { get; set; }
            public string CDIRECCION_CODIGOPOSTAL { get; set; }
            public string CTELEFONO_CASCLVLADA { get; set; }
            public string CTELEFONO_CASNUMERO { get; set; }
            public string CTELEFONO_CASRECADO { get; set; }
            public string CTELEFONO_OFCNUMERO { get; set; }
            public string CTELEFONO_OFCCLVLADA { get; set; }
            public string CTELEFONO_OFCEXTENSION { get; set; }
            public string CTELEFONO_CELULAR { get; set; }
            public int intIDUNegocio { get; set; }
            public string STRTIPOSANGRE { get; set; }
            public string STRHOSPITAL { get; set; }
            public string STRMEDICO { get; set; }
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
            public string CALIFICACION { get; set; }
        }

        private string strVerifica(string strVerif)
        {
            if (strVerif == null)
                return "";
            return strVerif;
        }

        [HttpPost]
        public JsonResult GuardaCliente(DetallesAgente detallesCliente)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_IDUNEGOCIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = detallesCliente.intIDUNegocio;
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
                string strClave = "CLI" + strNome + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                cmd = new SqlCommand("INSERT_CLIENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLIENTE_CLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("CESTATUS_ID", SqlDbType.Int);
                cmd.Parameters.Add("CLIENTE_NOMBRE", SqlDbType.VarChar);
                cmd.Parameters.Add("CLIENTE_APELLIDOPATERNO", SqlDbType.VarChar);
                cmd.Parameters.Add("CLIENTE_APELLIDOMATERNO", SqlDbType.VarChar);
                cmd.Parameters.Add("CLIENTE_FECHANACIMIENTO", SqlDbType.DateTime);
                cmd.Parameters.Add("CLIENTE_CURP", SqlDbType.NChar);
                cmd.Parameters.Add("ECIVIL_ID", SqlDbType.Int);
                cmd.Parameters.Add("NACIONALIDAD_ID", SqlDbType.Int);
                cmd.Parameters.Add("SEXO_ID", SqlDbType.Int);
                cmd.Parameters.Add("CGENERAL_RFC", SqlDbType.NChar);
                cmd.Parameters.Add("CGENERAL_CORREOELECTRONICO", SqlDbType.Text);
                cmd.Parameters.Add("CGENERAL_PAGINAWEB", SqlDbType.Text);
                cmd.Parameters.Add("CDIRECCION_CALLE", SqlDbType.NChar);
                cmd.Parameters.Add("CDIRECCION_NUMEXTERIOR", SqlDbType.NChar);
                cmd.Parameters.Add("CDIRECCION_NUMINTERIOR", SqlDbType.NChar);
                cmd.Parameters.Add("CDIRECCION_COLONIA", SqlDbType.NChar);
                cmd.Parameters.Add("EFEDERATIVA_ID", SqlDbType.NChar);
                cmd.Parameters.Add("DELEGMPIO_ID", SqlDbType.NChar);
                cmd.Parameters.Add("CDIRECCION_CODIGOPOSTAL", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_CASCLVLADA", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_CASNUMERO", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_CASRECADO", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_OFCNUMERO", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_OFCCLVLADA", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_OFCEXTENSION", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_CELULAR", SqlDbType.NChar);
                cmd.Parameters.Add("STRTIPOSANGRE", SqlDbType.NChar);
                cmd.Parameters.Add("STRHOSPITAL", SqlDbType.VarChar);
                cmd.Parameters.Add("STRMEDICO", SqlDbType.VarChar);
                cmd.Parameters.Add("STRLADAEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRNUMEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STREXTEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRCELEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRALERGIAS", SqlDbType.Text);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STREDAD", SqlDbType.NChar);
                cmd.Parameters.Add("MATENCION", SqlDbType.Int);
                cmd.Parameters.Add("FOTO", SqlDbType.VarChar);
                cmd.Parameters["CLIENTE_CLAVE"].Value = strClave;
                cmd.Parameters["CESTATUS_ID"].Value = detallesCliente.CESTATUS_ID;
                cmd.Parameters["CLIENTE_NOMBRE"].Value = detallesCliente.CLIENTE_NOMBRE.ToUpper();
                cmd.Parameters["CLIENTE_APELLIDOPATERNO"].Value = detallesCliente.CLIENTE_APELLIDOPATERNO.ToUpper();
                cmd.Parameters["CLIENTE_APELLIDOMATERNO"].Value = detallesCliente.CLIENTE_APELLIDOMATERNO.ToUpper();
                if (detallesCliente.CLIENTE_FECHANACIMIENTO.Length <= 2)
                    cmd.Parameters["CLIENTE_FECHANACIMIENTO"].Value = DateTime.Now.ToShortDateString();
                else
                    cmd.Parameters["CLIENTE_FECHANACIMIENTO"].Value = detallesCliente.CLIENTE_FECHANACIMIENTO.ToUpper();
                cmd.Parameters["CLIENTE_CURP"].Value = strVerifica(detallesCliente.CLIENTE_CURP);
                cmd.Parameters["ECIVIL_ID"].Value = detallesCliente.ECIVIL_ID;
                cmd.Parameters["NACIONALIDAD_ID"].Value = detallesCliente.NACIONALIDAD_ID;
                cmd.Parameters["SEXO_ID"].Value = detallesCliente.SEXO_ID;
                cmd.Parameters["CGENERAL_RFC"].Value = strVerifica(detallesCliente.CGENERAL_RFC);
                cmd.Parameters["CGENERAL_CORREOELECTRONICO"].Value = strVerifica(detallesCliente.CGENERAL_CORREOELECTRONICO);
                cmd.Parameters["CGENERAL_PAGINAWEB"].Value = strVerifica(detallesCliente.CGENERAL_CORREOELECTRONICO);
                cmd.Parameters["CDIRECCION_CALLE"].Value = detallesCliente.CDIRECCION_CALLE.ToUpper();
                cmd.Parameters["CDIRECCION_NUMEXTERIOR"].Value = detallesCliente.CDIRECCION_NUMEXTERIOR.ToUpper();
                cmd.Parameters["CDIRECCION_NUMINTERIOR"].Value = strVerifica(detallesCliente.CDIRECCION_NUMINTERIOR).ToUpper();
                cmd.Parameters["CDIRECCION_COLONIA"].Value = detallesCliente.CDIRECCION_COLONIA.ToUpper();
                cmd.Parameters["EFEDERATIVA_ID"].Value = detallesCliente.EFEDERATIVA_ID;
                cmd.Parameters["DELEGMPIO_ID"].Value = detallesCliente.DELEGMPIO_ID;
                cmd.Parameters["CDIRECCION_CODIGOPOSTAL"].Value = detallesCliente.CDIRECCION_CODIGOPOSTAL.ToUpper();
                cmd.Parameters["CTELEFONO_CASCLVLADA"].Value = strVerifica(detallesCliente.CTELEFONO_CASCLVLADA);
                cmd.Parameters["CTELEFONO_CASNUMERO"].Value = strVerifica(detallesCliente.CTELEFONO_CASNUMERO);
                cmd.Parameters["CTELEFONO_CASRECADO"].Value = strVerifica(detallesCliente.CTELEFONO_CASRECADO);
                cmd.Parameters["CTELEFONO_OFCNUMERO"].Value = strVerifica(detallesCliente.CTELEFONO_OFCNUMERO);
                cmd.Parameters["CTELEFONO_OFCCLVLADA"].Value = strVerifica(detallesCliente.CTELEFONO_OFCCLVLADA);
                cmd.Parameters["CTELEFONO_OFCEXTENSION"].Value = strVerifica(detallesCliente.CTELEFONO_OFCEXTENSION);
                cmd.Parameters["CTELEFONO_CELULAR"].Value = strVerifica(detallesCliente.CTELEFONO_CELULAR);
                cmd.Parameters["STRTIPOSANGRE"].Value = strVerifica(detallesCliente.STRTIPOSANGRE);
                cmd.Parameters["STRHOSPITAL"].Value = strVerifica(detallesCliente.STRHOSPITAL);
                cmd.Parameters["STRMEDICO"].Value = strVerifica(detallesCliente.STRMEDICO);
                cmd.Parameters["STRLADAEMER"].Value = strVerifica(detallesCliente.STRLADAEMER);
                cmd.Parameters["STRNUMEMER"].Value = strVerifica(detallesCliente.STRNUMEMER);
                cmd.Parameters["STRCELEMER"].Value = strVerifica(detallesCliente.STRCELEMER);
                cmd.Parameters["STREXTEMER"].Value = strVerifica(detallesCliente.STREXTEMER);
                cmd.Parameters["STRALERGIAS"].Value = strVerifica(detallesCliente.STRALERGIAS);
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["STREDAD"].Value = strVerifica(detallesCliente.STREDAD);
                cmd.Parameters["MATENCION"].Value = detallesCliente.INTMATENCIONID;
                cmd.Parameters["FOTO"].Value = strVerifica(detallesCliente.FOTO);
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
        public JsonResult ActualizaCliente(DetallesAgente detallesCliente)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE_CLIENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLIENTE_CLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("CESTATUS_ID", SqlDbType.Int);
                cmd.Parameters.Add("CLIENTE_NOMBRE", SqlDbType.VarChar);
                cmd.Parameters.Add("CLIENTE_APELLIDOPATERNO", SqlDbType.VarChar);
                cmd.Parameters.Add("CLIENTE_APELLIDOMATERNO", SqlDbType.VarChar);
                cmd.Parameters.Add("CLIENTE_FECHANACIMIENTO", SqlDbType.DateTime);
                cmd.Parameters.Add("CLIENTE_CURP", SqlDbType.NChar);
                cmd.Parameters.Add("ECIVIL_ID", SqlDbType.Int);
                cmd.Parameters.Add("NACIONALIDAD_ID", SqlDbType.Int);
                cmd.Parameters.Add("SEXO_ID", SqlDbType.Int);
                cmd.Parameters.Add("CGENERAL_RFC", SqlDbType.NChar);
                cmd.Parameters.Add("CGENERAL_CORREOELECTRONICO", SqlDbType.Text);
                cmd.Parameters.Add("CGENERAL_PAGINAWEB", SqlDbType.Text);
                cmd.Parameters.Add("CDIRECCION_CALLE", SqlDbType.NChar);
                cmd.Parameters.Add("CDIRECCION_NUMEXTERIOR", SqlDbType.NChar);
                cmd.Parameters.Add("CDIRECCION_NUMINTERIOR", SqlDbType.NChar);
                cmd.Parameters.Add("CDIRECCION_COLONIA", SqlDbType.NChar);
                cmd.Parameters.Add("EFEDERATIVA_ID", SqlDbType.NChar);
                cmd.Parameters.Add("DELEGMPIO_ID", SqlDbType.NChar);
                cmd.Parameters.Add("CDIRECCION_CODIGOPOSTAL", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_CASCLVLADA", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_CASNUMERO", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_CASRECADO", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_OFCNUMERO", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_OFCCLVLADA", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_OFCEXTENSION", SqlDbType.NChar);
                cmd.Parameters.Add("CTELEFONO_CELULAR", SqlDbType.NChar);
                cmd.Parameters.Add("STRTIPOSANGRE", SqlDbType.NChar);
                cmd.Parameters.Add("STRHOSPITAL", SqlDbType.VarChar);
                cmd.Parameters.Add("STRMEDICO", SqlDbType.VarChar);
                cmd.Parameters.Add("STRLADAEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRNUMEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STREXTEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRCELEMER", SqlDbType.NChar);
                cmd.Parameters.Add("STRALERGIAS", SqlDbType.Text);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("STREDAD", SqlDbType.NChar);
                cmd.Parameters.Add("MATENCION", SqlDbType.Int);
                cmd.Parameters.Add("FOTO", SqlDbType.VarChar);
                cmd.Parameters["CLIENTE_CLAVE"].Value = detallesCliente.CLIENTE_CLAVE;
                cmd.Parameters["CESTATUS_ID"].Value = detallesCliente.CESTATUS_ID;
                cmd.Parameters["CLIENTE_NOMBRE"].Value = detallesCliente.CLIENTE_NOMBRE.ToUpper();
                cmd.Parameters["CLIENTE_APELLIDOPATERNO"].Value = detallesCliente.CLIENTE_APELLIDOPATERNO.ToUpper();
                cmd.Parameters["CLIENTE_APELLIDOMATERNO"].Value = detallesCliente.CLIENTE_APELLIDOMATERNO.ToUpper();
                cmd.Parameters["CLIENTE_FECHANACIMIENTO"].Value = detallesCliente.CLIENTE_FECHANACIMIENTO.ToUpper();
                cmd.Parameters["CLIENTE_CURP"].Value = strVerifica(detallesCliente.CLIENTE_CURP).ToUpper();
                cmd.Parameters["ECIVIL_ID"].Value = detallesCliente.ECIVIL_ID;
                cmd.Parameters["NACIONALIDAD_ID"].Value = detallesCliente.NACIONALIDAD_ID;
                cmd.Parameters["SEXO_ID"].Value = detallesCliente.SEXO_ID;
                cmd.Parameters["CGENERAL_RFC"].Value = strVerifica(detallesCliente.CGENERAL_RFC).ToUpper();
                cmd.Parameters["CGENERAL_CORREOELECTRONICO"].Value = strVerifica(detallesCliente.CGENERAL_CORREOELECTRONICO);
                cmd.Parameters["CGENERAL_PAGINAWEB"].Value = strVerifica(detallesCliente.CGENERAL_PAGINAWEB);
                cmd.Parameters["CDIRECCION_CALLE"].Value = detallesCliente.CDIRECCION_CALLE.ToUpper();
                cmd.Parameters["CDIRECCION_NUMEXTERIOR"].Value = detallesCliente.CDIRECCION_NUMEXTERIOR.ToUpper();
                cmd.Parameters["CDIRECCION_NUMINTERIOR"].Value = strVerifica(detallesCliente.CDIRECCION_NUMINTERIOR).ToUpper();
                cmd.Parameters["CDIRECCION_COLONIA"].Value = detallesCliente.CDIRECCION_COLONIA.ToUpper();
                cmd.Parameters["EFEDERATIVA_ID"].Value = detallesCliente.EFEDERATIVA_ID;
                cmd.Parameters["DELEGMPIO_ID"].Value = detallesCliente.DELEGMPIO_ID;
                cmd.Parameters["CDIRECCION_CODIGOPOSTAL"].Value = detallesCliente.CDIRECCION_CODIGOPOSTAL.ToUpper();
                cmd.Parameters["CTELEFONO_CASCLVLADA"].Value = strVerifica(detallesCliente.CTELEFONO_CASCLVLADA).ToUpper();
                cmd.Parameters["CTELEFONO_CASNUMERO"].Value = strVerifica(detallesCliente.CTELEFONO_CASNUMERO).ToUpper();
                cmd.Parameters["CTELEFONO_CASRECADO"].Value = strVerifica(detallesCliente.CTELEFONO_CASRECADO).ToUpper();
                cmd.Parameters["CTELEFONO_OFCNUMERO"].Value = strVerifica(detallesCliente.CTELEFONO_OFCNUMERO).ToUpper();
                cmd.Parameters["CTELEFONO_OFCCLVLADA"].Value = strVerifica(detallesCliente.CTELEFONO_OFCCLVLADA).ToUpper();
                cmd.Parameters["CTELEFONO_OFCEXTENSION"].Value = strVerifica(detallesCliente.CTELEFONO_OFCEXTENSION).ToUpper();
                cmd.Parameters["CTELEFONO_CELULAR"].Value = strVerifica(detallesCliente.CTELEFONO_CELULAR).ToUpper();
                cmd.Parameters["STRTIPOSANGRE"].Value = strVerifica(detallesCliente.STRTIPOSANGRE).ToUpper();
                cmd.Parameters["STRHOSPITAL"].Value = strVerifica(detallesCliente.STRHOSPITAL).ToUpper();
                cmd.Parameters["STRMEDICO"].Value = strVerifica(detallesCliente.STRMEDICO).ToUpper();
                cmd.Parameters["STRLADAEMER"].Value = strVerifica(detallesCliente.STRLADAEMER).ToUpper();
                cmd.Parameters["STRNUMEMER"].Value = strVerifica(detallesCliente.STRNUMEMER).ToUpper();
                cmd.Parameters["STRCELEMER"].Value = strVerifica(detallesCliente.STRCELEMER).ToUpper();
                cmd.Parameters["STREXTEMER"].Value = strVerifica(detallesCliente.STREXTEMER).ToUpper();
                cmd.Parameters["STRALERGIAS"].Value = strVerifica(detallesCliente.STRALERGIAS).ToUpper();
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString().ToUpper();
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString().ToUpper();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["STREDAD"].Value = strVerifica(detallesCliente.STREDAD).ToUpper();
                cmd.Parameters["MATENCION"].Value = detallesCliente.INTMATENCIONID;
                cmd.Parameters["FOTO"].Value = strVerifica(detallesCliente.FOTO);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true, mensaje = detallesCliente.CLIENTE_CLAVE });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult cargaClienteId(DetallesAgente detallesCLIENTE)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CLIENTEID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["CLAVE"].Value = detallesCLIENTE.CLIENTE_CLAVE;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<DetallesAgente>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new DetallesAgente { CLIENTE_CLAVE = sqlDR.GetString(0).Trim(), CLIENTE_NOMBRE = sqlDR.GetString(1).Trim(), CLIENTE_APELLIDOPATERNO = sqlDR.GetString(2).Trim(), CLIENTE_APELLIDOMATERNO = sqlDR.GetString(3).Trim(), CLIENTE_FECHANACIMIENTO = sqlDR.GetDateTime(4).ToShortDateString(), CLIENTE_CURP = sqlDR.GetString(5).Trim(), BOOLACTIVO = sqlDR.GetBoolean(6), STRFECHAALTA = sqlDR.GetDateTime(7).ToShortDateString(), STRFECHAMODIF = sqlDR.GetDateTime(8).ToShortDateString(), ECIVIL_ID = sqlDR.GetInt32(9), NACIONALIDAD_ID = sqlDR.GetInt32(10), SEXO_ID = sqlDR.GetInt32(11), CGENERAL_RFC = sqlDR.GetString(12).Trim(), CGENERAL_CORREOELECTRONICO = sqlDR.GetString(13).Trim(), CGENERAL_PAGINAWEB = sqlDR.GetString(14).Trim(), CDIRECCION_CALLE = sqlDR.GetString(15).Trim(), CDIRECCION_NUMEXTERIOR = sqlDR.GetString(16).Trim(), CDIRECCION_NUMINTERIOR = sqlDR.GetString(17).Trim(), CDIRECCION_COLONIA = sqlDR.GetString(18).Trim(), EFEDERATIVA_ID = sqlDR.GetInt32(19), DELEGMPIO_ID = sqlDR.GetInt32(20), CDIRECCION_CODIGOPOSTAL = sqlDR.GetString(21).Trim(), CTELEFONO_CASCLVLADA = sqlDR.GetString(22).Trim(), CTELEFONO_CASNUMERO = sqlDR.GetString(23).Trim(), CTELEFONO_CASRECADO = sqlDR.GetString(24).Trim(), CTELEFONO_OFCNUMERO = sqlDR.GetString(25).Trim(), CTELEFONO_OFCCLVLADA = sqlDR.GetString(26).Trim(), CTELEFONO_OFCEXTENSION = sqlDR.GetString(27).Trim(), CTELEFONO_CELULAR = sqlDR.GetString(28).Trim(), STRTIPOSANGRE = sqlDR.GetString(29).Trim(), STRHOSPITAL = sqlDR.GetString(30).Trim(), STRMEDICO = sqlDR.GetString(31).Trim(), STRLADAEMER = sqlDR.GetString(32).Trim(), STRNUMEMER = sqlDR.GetString(33).Trim(), STREXTEMER = sqlDR.GetString(34).Trim(), STRCELEMER = sqlDR.GetString(35).Trim(), STRALERGIAS = sqlDR.GetString(36).Trim(), STRREALIZOALTA = sqlDR.GetString(37).Trim(), STRREALIZOMODIF = sqlDR.GetString(38).Trim(), INTMATENCIONID = sqlDR.GetInt32(39), STREDAD = sqlDR.GetString(40).Trim(), FOTO = sqlDR.GetString(41).Trim(), CESTATUS_ID = sqlDR.GetInt32(42).ToString() });
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
        public JsonResult verListaProspCliente()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CLIENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<DetallesAgente>();
                    while (sqlDR.Read())
                    {
                        string strEstrella = "";
                        string[] strColores = { "#ff0000", "#ff7200", "#fff200", "#008cff", "#13b21e" };
                        int intColor = 0;
                        if(!sqlDR.IsDBNull(1))
                        {
                            if (sqlDR.GetString(1) != "")
                            {
                                strEstrella += "<i class='fa fa-star' style='color:" + strColores[intColor] + "'></i>";
                                intColor++;
                            }
                        }
                        if (!sqlDR.IsDBNull(2))
                        {
                            if (sqlDR.GetString(2) != "")
                            {
                                strEstrella += "<i class='fa fa-star' style='color:" + strColores[intColor] + "'></i>";
                                intColor++;
                            }
                        }
                        if (!sqlDR.IsDBNull(4))
                        {
                            if (sqlDR.GetDateTime(4) <= DateTime.Now)
                            {
                                strEstrella += "<i class='fa fa-star' style='color:" + strColores[intColor] + "'></i>";
                                intColor++;
                            }
                        }
                        if (!sqlDR.IsDBNull(5))
                        {
                            if (sqlDR.GetString(5) != "")
                            {
                                strEstrella += "<i class='fa fa-star' style='color:" + strColores[intColor] + "'></i>";
                                intColor++;
                            }
                        }
                        if (!sqlDR.IsDBNull(6))
                        {
                            if (sqlDR.GetString(6) != "")
                            {
                                strEstrella += "<i class='fa fa-star' style='color:" + strColores[intColor] + "'></i>";
                                intColor++;
                            }
                        }
                        etiqJson.Add(new DetallesAgente { CLIENTE_CLAVE = sqlDR.GetString(0).Trim(), CLIENTE_NOMBRE = sqlDR.GetString(1).Trim() + " " + sqlDR.GetString(2).Trim() + " " + sqlDR.GetString(3).Trim(), CLIENTE_FECHANACIMIENTO = sqlDR.GetDateTime(4).ToShortDateString(), CALIFICACION = strEstrella });
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

        public class DetallesClienteCalif
        {
            public string strClave { get; set; }
            public List<DetallesCalificacion> detCalif { get; set; }
            public string strObserva { get; set; }
        }

        public class DetallesCalificacion
        {
            public int intIDCri { get; set; }
            public bool blSeleccion { get; set; }
        }

        public class DetallesTCalificacion
        {
            public string fecha { get; set; }
            public string edad { get; set; }
            public string edocivil { get; set; }
            public string depend { get; set; }
            public string ocupa { get; set; }
            public string ingresos { get; set; }
            public string acces { get; set; }
            public string observacion { get; set; }
            public string calificacion { get; set; }
            public int id { get; set; }
        }

        [HttpPost]
        public JsonResult guardaCalificacion(DetallesClienteCalif detCliCalif)
        {
            try{
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_CLIENTECALIFICA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters["STRCLAVE"].Value = detCliCalif.strClave;
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                con.Open();
                int intId = (Int32)(cmd.ExecuteScalar());
                con.Close();
                foreach (DetallesCalificacion detCal in detCliCalif.detCalif)
                {
                    cmd = new SqlCommand("INSERT_CALIFCRIT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("IDCRICAL", SqlDbType.Int);
                    cmd.Parameters.Add("IDCCAL", SqlDbType.Int);
                    cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                    cmd.Parameters.Add("BITSELECCION", SqlDbType.Bit);
                    cmd.Parameters.Add("BITACTIVO", SqlDbType.Bit);
                    cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                    cmd.Parameters["IDCRICAL"].Value = detCal.intIDCri;
                    cmd.Parameters["IDCCAL"].Value = intId;
                    cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["BITACTIVO"].Value = true;
                    cmd.Parameters["BITSELECCION"].Value = detCal.blSeleccion;
                    cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                cmd = new SqlCommand("INSERT_CALIFOBSERV", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("IDCCAL", SqlDbType.Int);
                cmd.Parameters.Add("STROBSERV", SqlDbType.Text);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("BITACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters["IDCCAL"].Value = intId;
                cmd.Parameters["STROBSERV"].Value = strVerifica(detCliCalif.strObserva.ToUpper());
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["BITACTIVO"].Value = true;
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
        public JsonResult verListaCalifCliente(DetallesClienteCalif detCliCalif)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_OLDCALIFCLI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                cmd.Parameters["STRCLAVE"].Value = detCliCalif.strClave;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sqlDR);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    int intId = 0;
                    var etiqJson = new List<DetallesTCalificacion>();
                    DetallesTCalificacion temp = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if(intId == int.Parse(dt.Rows[i][0].ToString()))
                        {
                            switch (dt.Rows[i][5].ToString())
                            {
                                case "1": temp.edad = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "2": temp.edocivil = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "3": temp.depend = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "4": temp.ocupa = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "5": temp.ingresos = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "6": temp.acces = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                            }
                        }
                        else
                        {
                            if (temp != null)
                                etiqJson.Add(temp);
                            temp = new DetallesTCalificacion();
                            temp.fecha = DateTime.Parse(dt.Rows[i][1].ToString()).ToShortDateString();
                            temp.id = int.Parse(dt.Rows[i][0].ToString());
                            temp.observacion = dt.Rows[i][6].ToString();
                            switch (dt.Rows[i][5].ToString())
                            {
                                case "1": temp.edad = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "2": temp.edocivil = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "3": temp.depend = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "4": temp.ocupa = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "5": temp.ingresos = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                                case "6": temp.acces = dt.Rows[i][4].ToString();
                                    if (bool.Parse(dt.Rows[i][3].ToString()))
                                        temp.calificacion += "<i class='fa fa-star'></i>";
                                    break;
                            }
                        }
                        intId = int.Parse(dt.Rows[i][0].ToString());
                    }
                    etiqJson.Add(temp);
                    return Json(etiqJson, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, mensaje = "No se pudo consultar la base de datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        public class DetallesOrdenTrabajo
        {
            public string strOrdenTrab { get; set; }
            public int intInsCert { get; set; }
            public int intTipoCert { get; set; }
            public string strClaveCli { get; set; }
            public string strClaveMov { get; set; }
            public string strFechaEnvio { get; set; }
            public string strFechaActiv { get; set; }
            public string strFechaDesact { get; set; }
            public string strFechaEmision { get; set; }
            public string strActiv { get; set; }
            public string strObserv { get; set; }
            public int intDefinicion { get; set; }
            public int intOrdenTrab { get; set; }
            public string strDescDef { get; set; }
            public string strArchivo { get; set; }
            public string strClaveAuten { get; set; }
            public int intPOTDocum { get; set; }
        }

        private string folioOrdenTrabajo()
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
            string strClave = strNome + "-OT-" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            return strClave;
        } 

        [HttpPost]
        public JsonResult guardarOrdenTrabajo(DetallesOrdenTrabajo detOrdTrab)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = null;
                string strFolio = "";
                if (detOrdTrab.intOrdenTrab == 0){
                    cmd = new SqlCommand("INSERT_ORDENTRAB", con);
                    strFolio = folioOrdenTrabajo();
                    cmd.Parameters.Add("CLAVECLIENTE", SqlDbType.NChar);
                    cmd.Parameters["CLAVECLIENTE"].Value = detOrdTrab.strClaveCli;
                }
                else
                {
                    cmd = new SqlCommand("UPDATE_ORDENTRAB", con);
                    cmd.Parameters.Add("INTID", SqlDbType.Int);
                    cmd.Parameters["INTID"].Value = detOrdTrab.intOrdenTrab;
                }
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add("IDDEFINICION", SqlDbType.Int);
                cmd.Parameters.Add("IDINSTCERT", SqlDbType.Int);
                cmd.Parameters.Add("IDTIPOCERT", SqlDbType.Int);
                cmd.Parameters.Add("NUMERO", SqlDbType.NChar);
                cmd.Parameters.Add("FECHAENVIO", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAACTIV", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHADESACT", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAEMISION", SqlDbType.DateTime);
                cmd.Parameters.Add("CLAVEMOV", SqlDbType.NChar);
                cmd.Parameters.Add("CLAVEAUTEN", SqlDbType.NChar);
                cmd.Parameters.Add("BITACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHAMODIF", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZOMODIF", SqlDbType.Int);
                cmd.Parameters["CLAVEAUTEN"].Value = strFolio;
                cmd.Parameters["IDDEFINICION"].Value = detOrdTrab.intDefinicion;
                cmd.Parameters["IDINSTCERT"].Value = detOrdTrab.intInsCert;
                cmd.Parameters["IDTIPOCERT"].Value = detOrdTrab.intTipoCert;
                cmd.Parameters["NUMERO"].Value = detOrdTrab.strOrdenTrab;
                cmd.Parameters["FECHAENVIO"].Value = detOrdTrab.strFechaEnvio;
                cmd.Parameters["FECHAACTIV"].Value = detOrdTrab.strFechaActiv;
                cmd.Parameters["FECHADESACT"].Value = detOrdTrab.strFechaDesact;
                cmd.Parameters["FECHAEMISION"].Value = detOrdTrab.strFechaEmision;
                cmd.Parameters["CLAVEMOV"].Value = detOrdTrab.strClaveMov;
                cmd.Parameters["BITACTIVO"].Value = true;
                cmd.Parameters["FECHAMODIF"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZOMODIF"].Value = Session["intID"].ToString();
                con.Open();
                int intIdOT = 0;
                if (detOrdTrab.intOrdenTrab == 0)
                    intIdOT = (Int32)(cmd.ExecuteScalar());
                else
                {
                    cmd.ExecuteNonQuery();
                    intIdOT = detOrdTrab.intOrdenTrab;
                }
                con.Close();
                if (detOrdTrab.strArchivo != null)
                {
                    if (detOrdTrab.intPOTDocum == 0)
                    {
                        cmd = new SqlCommand("INSERT_OTDOCUMENTACION", con);
                        cmd.Parameters.Add("INTIDPOT", SqlDbType.Int);
                        cmd.Parameters["INTIDPOT"].Value = intIdOT;
                    }
                    else
                    {
                        cmd = new SqlCommand("UPDATE_OTDOCUMENTACION", con);
                        cmd.Parameters.Add("INTID", SqlDbType.Int);
                        cmd.Parameters["INTID"].Value = detOrdTrab.intPOTDocum;
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                    cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                    cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                    cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["BOOLACTIVO"].Value = true;
                    cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                    con.Open();
                    int intIdOTDoc = 0;
                    if (detOrdTrab.intPOTDocum == 0)
                    {
                        intIdOTDoc = (Int32)(cmd.ExecuteScalar());
                    }
                    con.Close();
                    if (detOrdTrab.intPOTDocum == 0)
                    {
                        cmd = new SqlCommand("INSERT_OTARCHIVO", con);
                        cmd.Parameters.Add("INTIDOTDOC", SqlDbType.Int);
                        cmd.Parameters["INTIDOTDOC"].Value = intIdOTDoc;
                    }
                    else
                    {
                        cmd = new SqlCommand("UPDATE_OTARCHIVO", con);
                        cmd.Parameters.Add("INTIDOTDOC", SqlDbType.Int);
                        cmd.Parameters["INTIDOTDOC"].Value = detOrdTrab.intPOTDocum;
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("NOMBARCH", SqlDbType.VarChar);
                    cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                    cmd.Parameters.Add("EXTENSION", SqlDbType.NChar);
                    cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                    cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                    cmd.Parameters["NOMBARCH"].Value = strNombExt(detOrdTrab.strArchivo, false);
                    cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["BOOLACTIVO"].Value = true;
                    cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                    cmd.Parameters["EXTENSION"].Value = strNombExt(detOrdTrab.strArchivo, true);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (detOrdTrab.intOrdenTrab == 0)
                    cmd = new SqlCommand("INSERT_POTOBSERVA", con);
                else
                    cmd = new SqlCommand("UPDATE_POTOBSERVA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTIDPOT", SqlDbType.Int);
                cmd.Parameters["INTIDPOT"].Value = intIdOT;                
                cmd.Parameters.Add("STROBSERV", SqlDbType.VarChar);
                cmd.Parameters.Add("STRFECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("INTREALIZO", SqlDbType.Int);
                cmd.Parameters.Add("BOOLACTIVO", SqlDbType.Bit);
                cmd.Parameters["STROBSERV"].Value = strVerifica(detOrdTrab.strObserv);
                cmd.Parameters["STRFECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["BOOLACTIVO"].Value = true;
                cmd.Parameters["INTREALIZO"].Value = Session["intID"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true, mensaje = strFolio });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        private string strNombExt(string Nombre, bool Exten)
        {
            if (Nombre == null)
                return "";
            else
            {
                string[] strTemp = Nombre.Split(char.Parse("."));
                if (Exten)
                    return strTemp[1];
                else
                    return strTemp[0];
            }
        }

        [HttpPost]
        public JsonResult verOrdenesTrabajo(DetallesOrdenTrabajo detOrdTrab)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_POTRABAJO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                cmd.Parameters["STRCLAVE"].Value = detOrdTrab.strClaveCli;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<DetallesOrdenTrabajo>();
                    while (sqlDR.Read())
                    {
                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                        SqlCommand cmd1 = new SqlCommand("SELECT_DOCUMENTOOT", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("IDORDTRABA", SqlDbType.Int);
                        cmd1.Parameters["IDORDTRABA"].Value = sqlDR.GetInt32(0);
                        con1.Open();
                        SqlDataReader sqlDR1 = cmd1.ExecuteReader();
                        string strArchivoTemp = "";
                        int intArchivTemp = 0;
                        if (sqlDR1.HasRows)
                        {
                            while (sqlDR1.Read())
                            {
                                strArchivoTemp = sqlDR1.GetString(4).Trim() + "." + sqlDR1.GetString(5).Trim();
                                intArchivTemp = sqlDR1.GetInt32(7);
                            }
                            con1.Close();
                        }
                        etiqJson.Add(new DetallesOrdenTrabajo { intOrdenTrab = sqlDR.GetInt32(0), strOrdenTrab = sqlDR.GetString(1).Trim(), intDefinicion = sqlDR.GetInt32(2), intInsCert = sqlDR.GetInt32(3), intTipoCert = sqlDR.GetInt32(4), strFechaEnvio = sqlDR.GetDateTime(5).ToShortDateString(), strFechaActiv = sqlDR.GetDateTime(6).ToShortDateString(), strFechaDesact = sqlDR.GetDateTime(7).ToShortDateString(), strFechaEmision = sqlDR.GetDateTime(8).ToShortDateString(), strClaveMov = sqlDR.GetString(9).Trim(), strClaveAuten = sqlDR.GetString(10).Trim(), strDescDef = sqlDR.GetString(11).Trim(), strArchivo = strArchivoTemp, strObserv = sqlDR.GetString(12).Trim(), intPOTDocum = intArchivTemp });
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

        public ActionResult verDecalogoAgente(detallesPlaneacion detPlanea)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_AGENTEORIENTA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.VarChar);
                cmd.Parameters["STRCLAVE"].Value = Session["strClaveAg"].ToString();
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    while (sqlDR.Read())
                    {
                        return Json(new { success = true, Mision = sqlDR.GetString(0), Vision = sqlDR.GetString(1), Valores = sqlDR.GetString(2), Objetivo = sqlDR.GetString(3), Meta = sqlDR.GetString(4), Estrategia = sqlDR.GetString(5), PlanAccion = sqlDR.GetString(6), FechaAlta = sqlDR.GetDateTime(7).ToString(), RealizoAlta = sqlDR.GetString(8), FechaModif = sqlDR.GetDateTime(9).ToString(), RealizoModif = sqlDR.GetString(10), intId = sqlDR.GetInt32(11) });
                    }
                }
                con.Close();
                return Json(new { success = false, mensaje = "No hay decalogo del agente configurado" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        public ActionResult verDecalogoUNegocio(detallesPlaneacion detPlanea)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_PLANEACIONACTIVA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.NChar);
                cmd.Parameters["INTID"].Value = Session["intUneg"].ToString();
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
        }

        [HttpPost]
        public JsonResult guardaDecalogoAgente(detallesPlaneacion detailsPlan)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT_AGENTEORIENTA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("MISION", SqlDbType.Text);
                cmd.Parameters.Add("VISION", SqlDbType.Text);
                cmd.Parameters.Add("VALORES", SqlDbType.Text);
                cmd.Parameters.Add("OBJETIVO", SqlDbType.Text);
                cmd.Parameters.Add("META", SqlDbType.Text);
                cmd.Parameters.Add("ESTRATEGIA", SqlDbType.Text);
                cmd.Parameters.Add("PLANACCION", SqlDbType.Text);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters["STRCLAVE"].Value = Session["strClaveAg"].ToString();
                cmd.Parameters["MISION"].Value = strVerifica(detailsPlan.strMision).ToUpper();
                cmd.Parameters["VISION"].Value = strVerifica(detailsPlan.strVision).ToUpper();
                cmd.Parameters["VALORES"].Value = strVerifica(detailsPlan.strValores).ToUpper();
                cmd.Parameters["OBJETIVO"].Value = strVerifica(detailsPlan.strObjetivo).ToUpper();
                cmd.Parameters["META"].Value = strVerifica(detailsPlan.strMeta).ToUpper();
                cmd.Parameters["ESTRATEGIA"].Value = strVerifica(detailsPlan.strEstrategia).ToUpper();
                cmd.Parameters["PLANACCION"].Value = strVerifica(detailsPlan.strPlanAccion).ToUpper();
                cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
                cmd.Parameters["ACTIVO"].Value = true;
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

        public class detallesMetas
        {
            public int idMeta { get; set; }
            public string fechaInicio { get; set; }
            public string fechaFin { get; set; }
            public string descrMeta { get; set; }
            public int idRamo { get; set; }
            public string ramo { get; set; }
            public int anio { get; set; }
            public string eficiencia { get; set; }
            public List<detallesNecesidades> detalNeces { get; set; }
            public List<detallesMetasRealizar> detalMetReal { get; set; }
            public int tipoNec { get; set; }
        }

        [HttpPost]
        public JsonResult guardaMetaAgente(detallesMetas detMeta)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = null;
                string comand = "";
                if (detMeta.idMeta == 0)
                    comand = "INSERT_AGENTEMETA";
                else
                    comand = "UPDATE_AGENTEMETA";
                cmd = new SqlCommand(comand, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (detMeta.idMeta == 0)
                {
                    cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                    cmd.Parameters["STRCLAVE"].Value = Session["strClaveAg"].ToString();
                    
                }
                else
                {
                    cmd.Parameters.Add("INTID", SqlDbType.NChar);
                    cmd.Parameters["INTID"].Value = detMeta.idMeta;
                }
                cmd.Parameters.Add("DESCRIPCION", SqlDbType.Text);
                cmd.Parameters.Add("FECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHAALTA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZOALTA", SqlDbType.Int);
                cmd.Parameters.Add("ANIOID", SqlDbType.Int);
                cmd.Parameters.Add("RAMOID", SqlDbType.Int);
                cmd.Parameters["DESCRIPCION"].Value = strVerifica(detMeta.descrMeta).ToUpper();
                cmd.Parameters["FECHAINI"].Value = detMeta.fechaInicio;
                cmd.Parameters["FECHAFIN"].Value = detMeta.fechaFin;
                cmd.Parameters["ACTIVO"].Value = true;
                cmd.Parameters["FECHAALTA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZOALTA"].Value = Session["intID"].ToString();
                cmd.Parameters["ANIOID"].Value = Session["anioTrab"].ToString();
                cmd.Parameters["RAMOID"].Value = detMeta.idRamo;
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
        public ActionResult verMetasAgente(detallesMetas detMeta)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_METAAGENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.VarChar);
                cmd.Parameters["STRCLAVE"].Value = Session["strClaveAg"].ToString();
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesMetas> detalMetas = new List<detallesMetas>();
                    while (sqlDR.Read())
                    {
                        detalMetas.Add(new detallesMetas { idMeta = sqlDR.GetInt32(0), anio = sqlDR.GetInt32(1), idRamo = sqlDR.GetInt32(2), descrMeta = sqlDR.GetString(3).Trim(), fechaInicio = sqlDR.GetDateTime(4).ToShortDateString(), fechaFin = sqlDR.GetDateTime(5).ToShortDateString(), ramo = sqlDR.GetString(6).Trim(), eficiencia = "" });
                    }
                    con.Close();
                    return Json(detalMetas, JsonRequestBehavior.AllowGet);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No se pudo consultar la base de datos" });
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
            public int idMeta { get; set; }
        }

        [HttpPost]
        public JsonResult guardaNecesidadMeta(detallesMetas detMetas)
        {
            try
            {
                int contNec = 1;
                foreach (detallesNecesidades detN in detMetas.detalNeces)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    SqlCommand cmd = null;
                    string comand = "";
                    if ((detN.clave == "")||(detN.clave==null))
                        comand = "INSERT_METANECESIDAD";
                    else
                        comand = "UPDATE_METANECESIDAD";
                    cmd = new SqlCommand(comand, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if ((detN.clave == "") || (detN.clave == null))
                    {
                        cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                        cmd.Parameters["STRCLAVE"].Value = Session["strClaveAg"].ToString();
                        cmd.Parameters.Add("IDMETA", SqlDbType.Int);
                        cmd.Parameters.Add("CLAVE", SqlDbType.VarChar);
                        cmd.Parameters["IDMETA"].Value = detMetas.idMeta;
                        cmd.Parameters.Add("IDNECESIDAD", SqlDbType.Int);
                        string[] strTemp = detN.necesidad.Split(char.Parse("|"));
                        cmd.Parameters["IDNECESIDAD"].Value = strTemp[0];
                        if(detMetas.tipoNec == 1)
                            cmd.Parameters["CLAVE"].Value = "CGF" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 2)
                            cmd.Parameters["CLAVE"].Value = "CGS" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 3)
                            cmd.Parameters["CLAVE"].Value = "CGN" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 4)
                            cmd.Parameters["CLAVE"].Value = "CAI" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 5)
                            cmd.Parameters["CLAVE"].Value = "CCO" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 6)
                            cmd.Parameters["CLAVE"].Value = "CMP" + contNec.ToString("00");
                        else if (detMetas.tipoNec == 7)
                            cmd.Parameters["CLAVE"].Value = "CLP" + contNec.ToString("00");
                        contNec++;
                    }
                    else
                    {
                        cmd.Parameters.Add("INTID", SqlDbType.NChar);
                        cmd.Parameters["INTID"].Value = detN.idMeta;
                    }
                    cmd.Parameters.Add("TRIMES", SqlDbType.Decimal);
                    cmd.Parameters.Add("MENSUAL", SqlDbType.Decimal);
                    cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                    cmd.Parameters.Add("FECHAALTA", SqlDbType.DateTime);
                    cmd.Parameters.Add("REALIZOALTA", SqlDbType.Int);
                    cmd.Parameters["TRIMES"].Value = detN.trimestral;
                    cmd.Parameters["MENSUAL"].Value = detN.mensual;
                    cmd.Parameters["ACTIVO"].Value = true;
                    cmd.Parameters["FECHAALTA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["REALIZOALTA"].Value = Session["intID"].ToString();
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
        public JsonResult verNecesidadesDeLaMeta(detallesNecesidades detNec)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_NECESIDADESAGENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.VarChar);
                cmd.Parameters.Add("CLAVE", SqlDbType.VarChar);
                cmd.Parameters["INTID"].Value = detNec.idMeta;
                if(detNec.idNecesidad == 1)
                    cmd.Parameters["CLAVE"].Value = "%CGF%";
                else if (detNec.idNecesidad == 2)
                    cmd.Parameters["CLAVE"].Value = "%CGS%";
                else if (detNec.idNecesidad == 3)
                    cmd.Parameters["CLAVE"].Value = "%CGN%";
                else if (detNec.idNecesidad == 4)
                    cmd.Parameters["CLAVE"].Value = "%CAI%";
                else if (detNec.idNecesidad == 5)
                    cmd.Parameters["CLAVE"].Value = "%CCO%";
                else if (detNec.idNecesidad == 6)
                    cmd.Parameters["CLAVE"].Value = "%CMP%";
                else if (detNec.idNecesidad == 7)
                    cmd.Parameters["CLAVE"].Value = "%CLA%";
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesNecesidades> etiqJson = new List<detallesNecesidades>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesNecesidades { idNecesidad = sqlDR.GetInt32(0), clave = sqlDR.GetString(2), trimestral = sqlDR.GetDecimal(4), mensual = sqlDR.GetDecimal(3), necesidad = sqlDR.GetString(5) });
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
        public JsonResult verResultadosMetas(detallesNecesidades detNec)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_RESNECBAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.VarChar);
                cmd.Parameters.Add("CLAVE", SqlDbType.VarChar);
                cmd.Parameters["INTID"].Value = detNec.idMeta;
                if (detNec.idNecesidad == 0)
                {
                    cmd.Parameters["CLAVE"].Value = "%CGF%";
                    con.Open();
                    SqlDataReader sqlDR = cmd.ExecuteReader();
                    List<detallesNecesidades> etiqJson = new List<detallesNecesidades>();
                    decimal decTrim = 0M, decMen = 0M, decTotalT = 0M, decTotalM = 0M;
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                        {
                            if(!sqlDR.IsDBNull(1))
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
                    cmd.Parameters["CLAVE"].Value = "%CGS%";
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
                    cmd.Parameters["CLAVE"].Value = "%CGN%";
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
                    cmd.Parameters["CLAVE"].Value = "%CAI%";
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
                    cmd.Parameters["CLAVE"].Value = "%CCO%";
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
                    cmd.Parameters["CLAVE"].Value = "%CMP%";
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
                    cmd.Parameters["CLAVE"].Value = "%CLA%";
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

        public class detallesMetasRealizar
        {
            public string indicador { get; set; }
            public int intMetaRealizar { get; set; }
            public int intMeta { get; set; }
            public string metaEsperada { get; set; }
            public string metaReal { get; set; }
            public int intIndicador { get; set; }
            public string clave { get; set; }
            public string trimestral { get; set; }
            public string mensual { get; set; }
            public string semanal { get; set; }
        }

        [HttpPost]
        public JsonResult guardaMetasRealizar(detallesMetas detMetas)
        {
            try
            {
                foreach (detallesMetasRealizar detMR in detMetas.detalMetReal)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    SqlCommand cmd = null;
                    string comand = "";
                    if (detMR.intMetaRealizar == 0)
                        comand = "INSERT_AGENTEMETAREALIZAR";
                    else
                        comand = "UPDATE_AGENTEMETAREALIZAR";
                    cmd = new SqlCommand(comand, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (detMR.intMetaRealizar == 0)
                    {
                        cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                        cmd.Parameters["STRCLAVE"].Value = Session["strClaveAg"].ToString();
                        cmd.Parameters.Add("IDMETA", SqlDbType.Int);
                        cmd.Parameters["IDMETA"].Value = detMetas.idMeta;
                        string [] strTemp = detMR.indicador.Split(char.Parse("|"));
                        cmd.Parameters.Add("IDINDICADOR", SqlDbType.Int);
                        cmd.Parameters["IDINDICADOR"].Value = strTemp[0];
                    }
                    else
                    {
                        cmd.Parameters.Add("INTID", SqlDbType.NChar);
                        cmd.Parameters["INTID"].Value = detMR.intMetaRealizar;
                    }
                    cmd.Parameters.Add("METAESP", SqlDbType.VarChar);
                    cmd.Parameters.Add("METAREAL", SqlDbType.VarChar);
                    cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                    cmd.Parameters.Add("FECHAALTA", SqlDbType.DateTime);
                    cmd.Parameters.Add("REALIZOALTA", SqlDbType.Int);
                    cmd.Parameters["METAESP"].Value = strVerifica(detMR.metaEsperada).ToUpper();
                    cmd.Parameters["METAREAL"].Value = strVerifica(detMR.metaReal).ToUpper();
                    cmd.Parameters["ACTIVO"].Value = true;
                    cmd.Parameters["FECHAALTA"].Value = DateTime.Now.ToString();
                    cmd.Parameters["REALIZOALTA"].Value = Session["intID"].ToString();
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
        public JsonResult verMetasaRealizar(detallesMetasRealizar detMetas)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_AGENTEMETAREALIZAR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.VarChar);
                cmd.Parameters["INTID"].Value = detMetas.intMeta;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesMetasRealizar> etiqJson = new List<detallesMetasRealizar>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesMetasRealizar { intMetaRealizar = sqlDR.GetInt32(0), clave = "ME" + sqlDR.GetInt32(0).ToString("00000"), metaEsperada = sqlDR.GetString(1), metaReal = sqlDR.GetString(2), indicador = sqlDR.GetString(3) });
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
        public JsonResult verPlanesMetas(detallesMetasRealizar detMetas)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_AGENTEMETAREALIZAR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTID", SqlDbType.VarChar);
                cmd.Parameters["INTID"].Value = detMetas.intMeta;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesMetasRealizar> etiqJson = new List<detallesMetasRealizar>();
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
                        etiqJson.Add(new detallesMetasRealizar { intMetaRealizar = sqlDR.GetInt32(0), indicador = sqlDR.GetString(4), trimestral = strTri, mensual = strMen, semanal = strSem });
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

        public class detallesPolizaCliente
        {
            public string strclave { get; set; }
            public int cobid { get; set; }
            public int planid { get; set; }
            public int cpolid { get; set; }
            public int tempid { get; set; }
            public string clave { get; set; }
            public string fecha { get; set; }
            public string concepto { get; set; }
            public string fechaini { get; set; }
            public string fechafin { get; set; }
            public bool activo { get; set; }
            public string observ { get; set; }
            public int anio { get; set; }
            public int intid { get; set; }
            public int idramo { get; set; }
            List<archivosPolizaCliente> listArchivos { get; set; }
        }

        public class archivosPolizaCliente
        {
            public string documento { get; set; }
            public string html { get; set; }
            public int iddocum { get; set; }
        }

        [HttpPost]
        public JsonResult guardarPolizaCliente(detallesPolizaCliente detPC)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = null;
                string comand = "";
                if (detPC.intid == 0)
                    comand = "INSERT_CPOLIZA";
                else
                    comand = "UPDATE_CPOLIZA";
                cmd = new SqlCommand(comand, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (detPC.intid == 0)
                {
                    cmd.Parameters.Add("STRCLAVE", SqlDbType.NChar);
                    cmd.Parameters["STRCLAVE"].Value = detPC.strclave;
                }
                else
                {
                    cmd.Parameters.Add("INTID", SqlDbType.NChar);
                    cmd.Parameters["INTID"].Value = detPC.intid;
                }
                cmd.Parameters.Add("COBID", SqlDbType.Int);
                cmd.Parameters.Add("PLANID", SqlDbType.Int);
                cmd.Parameters.Add("CPOLID", SqlDbType.Int);
                cmd.Parameters.Add("TEMPID", SqlDbType.Int);
                cmd.Parameters.Add("CLAVE", SqlDbType.VarChar);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("CONCEPTO", SqlDbType.VarChar);
                cmd.Parameters.Add("FECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHAALTA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZOALTA", SqlDbType.Int);
                cmd.Parameters.Add("OBSERV", SqlDbType.Text);
                cmd.Parameters.Add("ANIO", SqlDbType.Int);
                cmd.Parameters["COBID"].Value = detPC.cobid;
                cmd.Parameters["PLANID"].Value = detPC.planid;
                cmd.Parameters["CPOLID"].Value = detPC.cpolid;
                cmd.Parameters["TEMPID"].Value = detPC.tempid;
                cmd.Parameters["CLAVE"].Value = strVerifica(detPC.clave).ToUpper();
                cmd.Parameters["FECHA"].Value = detPC.fecha;
                cmd.Parameters["CONCEPTO"].Value = strVerifica(detPC.concepto).ToUpper();
                cmd.Parameters["FECHAINI"].Value = detPC.fechaini;
                cmd.Parameters["FECHAFIN"].Value = detPC.fechafin;
                cmd.Parameters["ACTIVO"].Value = true;
                cmd.Parameters["FECHAALTA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZOALTA"].Value = Session["intID"].ToString();
                cmd.Parameters["OBSERV"].Value = strVerifica(detPC.observ).ToUpper();
                cmd.Parameters["ANIO"].Value = detPC.anio;
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
        public JsonResult verPolizasCliente(detallesPolizaCliente detPC)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CPOLIZA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("STRCLAVE", SqlDbType.VarChar);
                cmd.Parameters["STRCLAVE"].Value = detPC.strclave;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesPolizaCliente> etiqJson = new List<detallesPolizaCliente>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesPolizaCliente { intid = sqlDR.GetInt32(0), cobid = sqlDR.GetInt32(1), planid = sqlDR.GetInt32(2), cpolid = sqlDR.GetInt32(3), tempid = sqlDR.GetInt32(4), clave = sqlDR.GetString(5).Trim(), fecha = sqlDR.GetDateTime(6).ToShortDateString(), concepto = sqlDR.GetString(7), fechaini = sqlDR.GetDateTime(8).ToShortDateString(), fechafin = sqlDR.GetDateTime(9).ToShortDateString(), observ = sqlDR.GetString(10), anio = sqlDR.GetInt32(11), idramo = sqlDR.GetInt32(12) });
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
        public JsonResult verDocumentosArchivos(detallesPolizaCliente detPC)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_CATALOGODROP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("INTOPC", SqlDbType.VarChar);
                if(detPC.intid == 0)
                    cmd.Parameters["INTOPC"].Value = 33;
                else
                    cmd.Parameters["INTOPC"].Value = 34;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<archivosPolizaCliente> etiqJson = new List<archivosPolizaCliente>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new archivosPolizaCliente { iddocum = sqlDR.GetInt32(0), documento = sqlDR.GetString(1), html = "<input id='DOCU" + sqlDR.GetInt32(0) + "' type='file' class='btn btn-default' />" });
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
            public int AMeta_Id { get; set; }
            public string AMPCualitativa_Pregunta { get; set; }
            public string AMRCualitativa_Respuesta { get; set; }
            public int AMPCualitativa_Id { get; set; }
            public int AMRCualitativa_Id { get; set; }
            public string AMRCualitativa_FechaAlta { get; set; }
            public string AMPCualitativa_FechaAlta { get; set; }
        }

        [HttpPost]
        public JsonResult verPreguntas(detallesCualitativa detCual)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_AMPREGUNTAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", SqlDbType.Int);
                cmd.Parameters["ID"].Value = detCual.AMeta_Id;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesCualitativa> etiqJson = new List<detallesCualitativa>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesCualitativa { AMPCualitativa_Id = sqlDR.GetInt32(0), AMPCualitativa_Pregunta = sqlDR.GetString(1), AMPCualitativa_FechaAlta = sqlDR.GetDateTime(2).ToString() });
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
                SqlCommand cmd = new SqlCommand("SELECT_AMRESPUESTAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", SqlDbType.Int);
                cmd.Parameters["ID"].Value = detCual.AMPCualitativa_Id;
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detallesCualitativa> etiqJson = new List<detallesCualitativa>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detallesCualitativa { AMRCualitativa_Id = sqlDR.GetInt32(0), AMRCualitativa_Respuesta = sqlDR.GetString(1), AMRCualitativa_FechaAlta = sqlDR.GetDateTime(2).ToString() });
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
                if (detCual.AMPCualitativa_Id == 0)
                    scmd = "INSERT_AMPREGUNTA_CUALITATIVA";
                else
                    scmd = "UPDATE_AMPREGUNTA_CUALITATIVA";
                SqlCommand cmd = null;
                cmd = new SqlCommand(scmd, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (detCual.AMPCualitativa_Id == 0)
                {
                    cmd.Parameters.Add("AMETA_ID", SqlDbType.Int);
                    cmd.Parameters["AMETA_ID"].Value = detCual.AMeta_Id;
                }
                else
                {
                    cmd.Parameters.Add("ID", SqlDbType.Int);
                    cmd.Parameters["ID"].Value = detCual.AMPCualitativa_Id;
                }
                cmd.Parameters.Add("PREGUNTA", SqlDbType.Text);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["PREGUNTA"].Value = strVerifica(detCual.AMPCualitativa_Pregunta).ToUpper();
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
                SqlCommand cmd = new SqlCommand("INSERT_AMRESPUESTA_CUALITATIVA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("PREGUNTA_ID", SqlDbType.Int);
                cmd.Parameters.Add("RESPUESTA", SqlDbType.Text);
                cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                cmd.Parameters["PREGUNTA_ID"].Value = detCual.AMPCualitativa_Id;
                cmd.Parameters["RESPUESTA"].Value = strVerifica(detCual.AMRCualitativa_Respuesta).ToUpper();
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
	}
}