#region Using

using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;
using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

#endregion

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        //
        // GET: /menu/agencias
        public ActionResult Agencias()
        {
            return View();
        }

        // GET: /menu/chat
        public ActionResult Chat()
        {
            return View();
        }

        // GET: /menu/correo
        public ActionResult Correo()
        {
            return View();
        }

        // GET: /menu/avisos
        public ActionResult Avisos()
        {
            return View();
        }

        // GET: /menu/busqueda
        public ActionResult Busqueda()
        {
            return View();
        }

        // GET: /menu/directorio
        public ActionResult Directorio()
        {
            return View();
        }

        // GET: /menu/addAviso
        public ActionResult AddAviso()
        {
            return View();
        }

        private string strVerifica(string strVerif)
        {
            if (strVerif == null)
                return "";
            return strVerif;
        }

        [HttpPost]
        public string arbolCarpetas()
        {
            DirectoryInfo dI = new DirectoryInfo(string.Format("{0}manuales\\", Server.MapPath(@"\")));
            string html = "<ul>";
            foreach (DirectoryInfo d in dI.GetDirectories())
            {
                string strTemp = subCarpetas(d, "manuales\\" + d + "\\");
                if (strTemp.Contains("li"))
                {
                    html += "<li><span class='onclick' data-nombre='" + d.Name + "' data-ruta='manuales\\'><i class='fa fa-lg fa-plus-circle'></i> " + d.Name + "</span>";
                    html += strTemp;
                    html += "</li>";
                }
                else
                    html += "<li><span class='onclick' data-nombre='" + d.Name + "' data-ruta='manuales\\'><i class='icon-leaf'></i> " + d.Name + "</span></li>";
            }
            html += "</ul>";
            return html;
        }

        private string subCarpetas(DirectoryInfo dTemp, string padre)
        {
            string html = "<ul>";
            foreach (DirectoryInfo d in dTemp.GetDirectories())
            {
                string strTemp = subCarpetas(d, padre + d + "\\");
                if (strTemp.Contains("li"))
                {
                    html += "<li style='display:none'><span class='onclick' data-nombre='" + d.Name + "' data-ruta='" + padre +"'><i class='fa fa-lg fa-plus-circle'></i>" + d.Name + "</span>";
                    html += strTemp;
                    html += "</li>";
                }
                else
                    html += "<li style='display:none'><span class='onclick' data-nombre='" + d.Name + "' data-ruta='" + padre +"'><i class='icon-leaf'></i> " + d.Name + "</span></li>";
            }
            html += "</ul>";
            return html;
        }

        [HttpPost]
        public bool creaCarpeta(string nombre, string ruta)
        {
            try
            {
                DirectoryInfo dI = new DirectoryInfo(string.Format("{0}" + ruta, Server.MapPath(@"\")));
                dI.CreateSubdirectory(nombre.ToUpper());
                return true;
            }
            catch(Exception X)
            {
                return false;
            }
        }

        [HttpPost]
        public bool eliminaCarpeta(string ruta)
        {
            try
            {
                DirectoryInfo dI = new DirectoryInfo(string.Format("{0}" + ruta , Server.MapPath(@"\")));
                dI.Delete(true);
                return true;
            }
            catch (Exception X)
            {
                return false;
            }
        }

        public class attribArchivos
        {
            public string filename { get; set; }
            public string size { get; set; }
            public string ext { get; set; }
        }

        [HttpPost]
        public JsonResult mostrarArchivos(string ruta)
        {
            string strDirectorio = Server.MapPath(@"\");
            strDirectorio += ruta;
            bool isExists = System.IO.Directory.Exists(strDirectorio);
            string[] strArchivos = null;
            if (isExists)
            {
                strArchivos = Directory.GetFiles(strDirectorio);
                List<attribArchivos> atributos = new List<attribArchivos>();
                foreach (string s in strArchivos)
                {
                    FileInfo fI = new FileInfo(s);
                    string host = Request.Url.GetLeftPart(UriPartial.Authority);
                    atributos.Add(new attribArchivos { filename = host + "/" + ruta.Replace(char.Parse("\\"),char.Parse("/")) + "/" + Path.GetFileName(s), ext = Path.GetExtension(s), size = fI.Length.ToString() });
                }
                return Json(atributos);
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult eliminarArchivo(attribArchivos atFile)
        {
            try
            {
                Uri urlArchivo = new Uri(atFile.filename);
                string strUbicacion = "~/" + urlArchivo.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
                string strDirectorio = Server.MapPath(strUbicacion);
                bool isExists = System.IO.File.Exists(strDirectorio);
                if (isExists)
                {
                    System.IO.File.Delete(strDirectorio);
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch (System.IO.IOException e)
            {
                return Json(new { success = false, mensaje = e.Message });
            }
        }

        [HttpPost]
        public ActionResult subeManual(string ruta)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    string[] fileName1 = file.FileName.Split(char.Parse("."));
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}" + ruta, Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString());
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                        var path = string.Format("{0}\\{1}", pathString, fName);
                        file.SaveAs(path);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error guardando archivo" });
            }
        }

        [HttpPost]
        public JsonResult guardaAviso(detailAviso detAviso)
        {
            try
            {
                string filename = "";
                if (detAviso.loadImage)
                {
                    filename = "AVISO" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".png";
                    string path = Path.Combine(Server.MapPath("~/content/img/avisos"), filename);
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(detAviso.file);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                }
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string strCmd = "";
                if (detAviso.idAviso == 0)
                    strCmd = "INSERT_AVISO";
                else
                    strCmd = "UPDATE_AVISO";
                SqlCommand cmd = new SqlCommand(strCmd, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("UNEGOCIO_ID", SqlDbType.Int);
                cmd.Parameters.Add("ASUNTO", SqlDbType.Text);
                cmd.Parameters.Add("MENSAJE", SqlDbType.Text);
                cmd.Parameters.Add("IMAGEN", SqlDbType.Text);
                cmd.Parameters.Add("FECHAINI", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHAFIN", SqlDbType.DateTime);
                cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
                cmd.Parameters.Add("REALIZO", SqlDbType.Int);
                if (detAviso.idAviso > 0)
                {
                    cmd.Parameters.Add("AVISO_ID", SqlDbType.Int);
                    cmd.Parameters["AVISO_ID"].Value = detAviso.idAviso;
                }
                cmd.Parameters["UNEGOCIO_ID"].Value = detAviso.uneg_id;
                cmd.Parameters["ASUNTO"].Value = detAviso.asunto;
                cmd.Parameters["MENSAJE"].Value = detAviso.mensaje;
                cmd.Parameters["IMAGEN"].Value = filename;
                cmd.Parameters["FECHAINI"].Value = detAviso.fechaini;
                cmd.Parameters["FECHAFIN"].Value = detAviso.fechafin;
                cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
                cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true, mensaje = "Se guardo correctamente" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verListAvisos()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_AVISO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", SqlDbType.Int);
                cmd.Parameters["ID"].Value = Session["intUneg"].ToString();
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    List<detailAviso> detAviso = new List<detailAviso>();
                    while (sqlDR.Read())
                    {
                        detAviso.Add(new detailAviso { idAviso = sqlDR.GetInt32(0), asunto = sqlDR.GetString(1).Trim(), mensaje = sqlDR.GetString(2).Trim(), file = sqlDR.GetString(3).Trim(), fechaini = sqlDR.GetDateTime(4).ToShortDateString(), fechafin = sqlDR.GetDateTime(5).ToShortDateString(), uneg_id = sqlDR.GetInt32(6) });
                    }
                    con.Close();
                    return Json(detAviso);
                }
                con.Close();
                return Json(new { success = false, mensaje = "No existen datos" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult eliminarAvisos(detailAviso detAviso)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("DELETE_AVISO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("AVISO_ID", SqlDbType.Int);
                cmd.Parameters["AVISO_ID"].Value = detAviso.idAviso;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json(new { success = true, mensaje = "Borrado correcto" });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        public class detailAviso
        {
            public int idAviso { get; set; }
            public string file { get; set; }
            public int uneg_id { get; set; }
            public string asunto { get; set; }
            public string mensaje { get; set; }
            public string fechaini { get; set; }
            public string fechafin { get; set; }
            public bool loadImage { get; set; }
        }
	}
}