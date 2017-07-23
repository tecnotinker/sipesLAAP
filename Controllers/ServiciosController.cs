using PusherServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using SmartAdminMvc.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SmartAdminMvc.Controllers
{
    public class ServiciosController : Controller
    {
        // GET: Servicios
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Plantilla()
        {
            return View();
        }

        public ActionResult _agentesEdit()
        {
            return View();
        }

        public ActionResult _actividades()
        {
            return View();
        }

        public JObject enviarCorreoTest()
        {
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            // Convertir a objecto JObject
            var jsonObject = JObject.Parse(jsonString);

            string para = Convert.ToString(jsonObject["para"]);
            string asunto = Convert.ToString(jsonObject["asunto"]);
            string mensaje = Convert.ToString(jsonObject["mensaje"]);

            var re = new JObject();
            try {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(para);
                mail.From = new MailAddress("noreply@sipeslaap.com", "Servidor SIPES LAAP", System.Text.Encoding.UTF8);
                mail.Subject = asunto;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = mensaje;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                /* servidor gmail
                client.Credentials = new System.Net.NetworkCredential("lmdiazm@gmail.com", "makakogmail");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                 */
                //client.Credentials = new System.Net.NetworkCredential("postmaster@sandboxf651aab082da466d96c6f38e06821d74.mailgun.org", "sipeslaap2015");
                client.Credentials = new System.Net.NetworkCredential("sipes@laasesores.com.mx", "LozanoA$e$ore$2016");
                client.Port = 465;
                client.Host = "secure.emailsrvr.com";
                client.EnableSsl = true;
            
                client.Send(mail);
                re["success"] = true;
                re["mensaje"] = "Mensaje enviado";
                return re;                
            }
            catch (Exception ex) {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null) {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }

                re["success"] = false;
                re["Error"] = ex.Message;
                return re;                    
            }
        }



        [HttpPost]
        public JObject publicaNotificacion() {
            var re = new JObject();
            string sql = "";
            string[] dest;
            string[] destid;

            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            // Convertir a objecto JObject
            var jsonObject = JObject.Parse(jsonString);

            string id = Convert.ToString(jsonObject["id"]);
            // Recuperar notificacion con el ID enviado
            if (id != "")
            { 
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // declare command
                    sql = "select * from notifica where notifica_id = " + id;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    var estructura = new JObject();
                    if (dr1.Read())
                    {
                        int tiponotifica_id = 0;
                        string destinatarios = "";
                        string destinatariosid = "";
                        string contenido = "";
                        string remite = Session["UsuarioActivo"].ToString();
                        string remiteEMail = Convert.ToString(dr1.GetValue(dr1.GetOrdinal("notifica_email")));
                        string fechaini = "";
                        string fechafin = "";
                        string resAviso = "";

                        tiponotifica_id = Convert.ToInt32(dr1.GetValue(dr1.GetOrdinal("tiponotifica_id")));
                        contenido = Convert.ToString(dr1.GetValue(dr1.GetOrdinal("notifica_contenido")));
                        destinatarios = Convert.ToString(dr1.GetValue(dr1.GetOrdinal("notifica_destinatarios")));
                        destinatariosid = Convert.ToString(dr1.GetValue(dr1.GetOrdinal("notifica_destinatariosid")));
                        fechaini = Convert.ToString(dr1.GetValue(dr1.GetOrdinal("notifica_fechaini")));
                        fechafin = Convert.ToString(dr1.GetValue(dr1.GetOrdinal("notifica_fechafin")));

                        // DEscomponer la lista de destinatarios
                        dest = destinatarios.Split(',');
                        destid = destinatariosid.Split(',');

                        // Para cada destinatario
                        foreach (string d in dest)
                        {
                            // Enviar notificacion
                            if (d != "") {
                                switch (tiponotifica_id)
                                {
                                    case 2:
                                        // 
                                        enviarCorreo(remite, d, "Notificación SIPES", contenido, remiteEMail);
                                        break;
                                    case 4:
                                        // 
                                        enviarCorreo(remite, d, "Notificación SIPES", contenido, remiteEMail);
                                        break;
                                }
                            }
                        }

                        foreach (string d in destid)
                        {
                            // Enviar notificacion
                            if (d != "")
                            {
                                switch (tiponotifica_id)
                                {
                                    case 1:
                                        // Crear un aviso de sistema
                                        int x = enviarAviso(remite, d, fechaini, fechafin, "Notificación SIPES", contenido);

                                        resAviso = resAviso + " " + x.ToString();
                                        break;
                                }
                            }
                        }
                        // Reportar exito
                        re["destinatarios"] = destinatarios;
                        re["tiponotifica"] = tiponotifica_id;
                        re["sql"] = sql;
                        re["id"] = id;
                        re["resaviso"] = resAviso;
                        re["success"] = true;
                    }
                }
                catch
                {
                    re["success"] = false;
                    re["sql"] = sql;
                    re["id"] = id;
                    re["mensaje"] = "Falló enviar notificación";
                }
            }
       
            return re;

        }

        public int enviarCorreo(string de, string para, string asunto, string mensaje, string demail)
        {            
            var re = new JObject();
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(para);
                //mail.From = new MailAddress("notificaciones@sipeslaap.com", de, System.Text.Encoding.UTF8);
                if (demail != "") { mail.From = new MailAddress(demail, de, System.Text.Encoding.UTF8); } else { mail.From = new MailAddress("notificaciones@sipeslaap.com", de, System.Text.Encoding.UTF8); }
                
                mail.Subject = asunto;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = mensaje;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                /* servidor gmail
                client.Credentials = new System.Net.NetworkCredential("lmdiazm@gmail.com", "makakogmail");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                 */
                client.Credentials = new System.Net.NetworkCredential("sipes@laasesores.com.mx", "LozanoA$e$ore$2016");
                client.Port = 465;
                client.Host = "secure.emailsrvr.com";
                client.EnableSsl = true;

                client.Send(mail);
                re["success"] = true;
                re["mensaje"] = "Mensaje enviado";
                return 1;
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }

                re["success"] = false;
                re["Error"] = ex.Message;
                return 0;
            }
        }


        public int enviarAviso(string de, string para, string fini, string ffin, string asunto, string mensaje) {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                // declare command
                string sql = "insert into avisos (aviso_fechaini, aviso_fechafin, unegocio_id, aviso_asunto, aviso_mensaje) values (CONVERT(DATETIME, '" + fini + "', 103), CONVERT(DATETIME, '" + ffin + "', 103), "+ de + ", '" + asunto + "', '" + mensaje + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch
            {
                return 1;
            }
            conn.Close();
            return 0;
        }

        [HttpPost]
        public JsonResult adjuntoAgente(string idagente, string desc)
        //public async Task<JsonResult> subeArchivo(string id)
        {
            String fName = "";
            String pathString = "";
            String path = "";
            String tuputamadre = "";
            String rutaImagen = "../archivos/adjuntosagente/" + Convert.ToString(idagente) + "/";
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];

                    string[] fileName1 = fileContent.FileName.Split(char.Parse("."));
                    fName = "AA" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "." + fileName1[1].ToLower();

                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = Path.GetFileName(file);

                        // Verifica la ruta de carga temporal
                        var uploadPath = Server.MapPath("~/uploads");
                        bool isExists = System.IO.Directory.Exists(uploadPath);
                        if (!isExists) System.IO.Directory.CreateDirectory(uploadPath);

                        // Verifica la ruta de carga de archivos
                        pathString = Path.Combine(Server.MapPath("~/archivos/adjuntosagente"), Convert.ToString(idagente));
                        isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists) System.IO.Directory.CreateDirectory(pathString);

                        // Integra el nombre unico del archivo a la ruta de archivos y haz la carga
                        path = Path.Combine(pathString, fName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }

                }
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // El adjunto es nuevo
                    
                    // Crear Objeto query
                    String sql = "INSERT INTO AgenteAdjunto (agente_id, aadjunto_ruta, aadjunto_desc) VALUES (" + idagente + ",' " + rutaImagen + fName + "', '" + desc.ToUpper() + "')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();

                    // Obtener siguiente idadjunto
                    sql = "SELECT max(aadjunto_id) from agenteadjunto";
                    SqlCommand query1 = new SqlCommand(sql, conn);
                    query1.CommandType = System.Data.CommandType.Text;
                    SqlDataReader dr1 = query1.ExecuteReader();

                    if (dr1.Read())
                    {
                        tuputamadre = Convert.ToString(dr1.GetValue(0));
                    }
                    dr1.Close();
                    
                    cmd.Dispose();
                    query1.Dispose();
                }
                catch (Exception)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Message = "Falló la carga del archivo sql", archivo = rutaImagen + fName });
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Falló la carga del archivo carga", archivo = rutaImagen + fName });
            }
            //return Json("File uploaded successfully");
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            return Json(new { Message = "Archivo cargado", archivo = rutaImagen + fName, aadjunto_id = tuputamadre });

        }


         [HttpPost]
         public ActionResult pusherMsg() {
             String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

             // Convertir a objecto JObject
             var jsonObject = JObject.Parse(jsonString);

             string canal = Convert.ToString(jsonObject["canal"]);
             string msg = Convert.ToString(jsonObject["mensaje"]);
             string remite = Convert.ToString(jsonObject["remitente"]);

             var pusher = new Pusher("241561", "9b6180e29b4b1879bfc0", "19e18865f47302d9ac11");
             var result = pusher.Trigger(canal, "notificacion", new { mensaje = msg, nombre = remite} );
             return new HttpStatusCodeResult((int)HttpStatusCode.OK);
         }
    }

}