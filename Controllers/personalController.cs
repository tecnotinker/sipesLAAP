using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.SessionState;
//using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Threading.Tasks;
using System.Drawing;
using System.Web.Helpers;

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class personalController : Controller
    {
        private string strSalt = "golsystemsSoftware2014";
        //
        // GET: /personal/

        public class etiqDetails
        {
            public string strEcus { get; set; }
            public int intID { get; set; }
            public int intNum { get; set; }
            public string strDescr { get; set; }
            public bool boolActivo { get; set; }
        }

        public JsonResult verEtiquetas(etiqDetails etiquetasEcus)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT_ETIQUETA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ECUS_DESC", SqlDbType.Char);
            cmd.Parameters["@ECUS_DESC"].Value = etiquetasEcus.strEcus;
            con.Open();
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                var etiqJson = new List<etiqDetails>();
                while (sqlDR.Read())
                {
                    etiqJson.Add(new etiqDetails { intID = sqlDR.GetInt32(0), strDescr = sqlDR.GetString(1).Trim(), intNum = sqlDR.GetInt32(2), boolActivo = sqlDR.GetBoolean(3) });
                }
                con.Close();
                return Json(etiqJson, JsonRequestBehavior.AllowGet);
            }
            con.Close();
            return null;
        }

        [HttpPost]
        public JsonResult guardarEtiquetas(etiqDetails[] etiquetasEcus)
        {
            int intEcus = 0;
            SqlCommand cmd;
            foreach (etiqDetails ed in etiquetasEcus)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                if (intEcus == 0)
                {
                    cmd = new SqlCommand("SELECT ECUS_ID FROM ECUS WHERE ECUS_DESCRIPCION = '" + ed.strEcus + "'", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader sqlDR = cmd.ExecuteReader();
                    if (sqlDR.HasRows)
                    {
                        while (sqlDR.Read())
                            intEcus = sqlDR.GetInt32(0);
                    }
                    con.Close();
                }
                cmd = null;
                cmd = new SqlCommand("UPDATE_ETIQUETAS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ECUSID", SqlDbType.Int);
                cmd.Parameters.Add("INTNUM", SqlDbType.Int);
                cmd.Parameters.Add("STRDESCR", SqlDbType.Text);
                cmd.Parameters["ECUSID"].Value = intEcus;
                cmd.Parameters["INTNUM"].Value = ed.intNum;
                cmd.Parameters["STRDESCR"].Value = ed.strDescr;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return Json("'Success':'true'");
        }

        public class classLogin
        {
            public string strUsername { get; set; }
            public string strPassword { get; set; }
        }

        private static string CreateSHAHash(string Password, string Salt)
        {
            System.Security.Cryptography.SHA512Managed HashTool = new System.Security.Cryptography.SHA512Managed();
            Byte[] PasswordAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(Password, Salt));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PasswordAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(classLogin loginLogin)
        {
            string strHash = CreateSHAHash(loginLogin.strPassword, strSalt);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT_USUARIO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("USERNAME", SqlDbType.VarChar);
            cmd.Parameters.Add("PASSWORD", SqlDbType.VarChar);
            cmd.Parameters["USERNAME"].Value = loginLogin.strUsername;
            cmd.Parameters["PASSWORD"].Value = strHash;
            con.Open();
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                while (sqlDR.Read())
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(loginLogin.strUsername, false);
                    string userData = sqlDR.GetInt32(0) + "|" + sqlDR.GetString(1) + "|" + sqlDR.GetString(2) + "|" + sqlDR.GetString(3) + "|" + sqlDR.GetString(4) + "|" + sqlDR.GetInt32(5) + "|" + sqlDR.GetString(6) + "|" + sqlDR.GetInt32(7) + "|" + sqlDR.GetInt32(8) + "|" + sqlDR.GetString(9);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, loginLogin.strUsername, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData, FormsAuthentication.FormsCookiePath);
                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    Session["intID"] = sqlDR.GetInt32(0);
                    Session["intUneg"] = sqlDR.GetInt32(5);
                    Session["intUnegBase"] = sqlDR.GetInt32(5);
                    Session["strClaveAg"] = sqlDR.GetString(6);
                    Session["rolID"] = sqlDR.GetInt32(7);
                    Session["AgenteId"] = sqlDR.GetInt32(8);
                    Session["UsuarioActivo"] = sqlDR.GetString(1) + " " + sqlDR.GetString(2) + " " + sqlDR.GetString(3);
                    Session["strCorreoElectronico"] = sqlDR.GetString(9);
                    Session["intUnegCobertura"] = sqlDR.GetInt32(10);
                    string strNombre = sqlDR.GetString(1);
                    string strAPaterno = sqlDR.GetString(2);
                    string strAMaterno = sqlDR.GetString(3);
                    string strFoto = sqlDR.GetString(4);
                    string strCorreoElectronico = sqlDR.GetString(9);
                    con.Close();
                    return Json(new { boolAcceso = true, intID = int.Parse(Session["intID"].ToString()), strNombre = strNombre, strAPaterno = strAPaterno, strAMaterno = strAMaterno, strFotografia = strFoto, intUneg = Session["intUneg"].ToString(), intRolId = Session["rolID"], strCorreoElectronico = strCorreoElectronico, intUnegCobertura = Session["intUnegCobertura"].ToString() });
                }
            }
            con.Close();
            return Json(new { boolAcceso = false });
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult currentUser()
        {
            string cookie = FormsAuthentication.FormsCookieName;
            HttpCookie httpcookie = HttpContext.Request.Cookies.Get(cookie);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(httpcookie.Value);
            if (ticket == null || ticket.Expired)
                return Json(new { error = false });
            FormsIdentity identity = new FormsIdentity(ticket);
            //get user roles
            string[] Roles = ticket.UserData.Split('|');
            if ((Roles.Length > 0) && (Roles.Length == 10))
            {
                if ((Session["intUneg"] == null) && (Session["intID"] == null))
                {
                    Session["intID"] = Roles[0];
                    Session["intUneg"] = Roles[5];
                    Session["strClaveAg"] = Roles[6];
                    Session["intRolId"] = Roles[7];
                    Session["AgenteId"] = Roles[8];
                    Session["strCorreoElectronico"] = Roles[9];
                    Session["intUnegCobertura"] = Roles[10];
                }
                return Json(new { error = true, intID = Roles[0], strNombre = Roles[1], strAPaterno = Roles[2], strAMaterno = Roles[3], strFotografia = Roles[4], intUneg = Session["intUneg"].ToString(), intUnegBase = Session["intUnegBase"].ToString(), intRolId = Roles[7], strClaveAg = Roles[6], AgenteId = Roles[8], strCorreoElectronico = Roles[9], intUnegCobertura = Session["intUnegCobertura"].ToString() });
            }
            return Json(new { error = false });
        }

        public class subirArchivos
        {
            public object objArchivo { get; set; }
            public string strNombre { get; set; }
        }

        [HttpPost]
        public virtual string UploadFiles(subirArchivos subirFiles)
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    WebImage img = new WebImage(file.InputStream);
                    if (img.Width > 120)
                        img.Resize(120, 120);
                    string path = Path.Combine(Server.MapPath("~/content/img/avatars"), subirFiles.strNombre + "-big" + Path.GetExtension(file.FileName));
                    img.Save(path);
                    if (img.Width > 50)
                        img.Resize(50, 50);
                    path = Path.Combine(Server.MapPath("~/content/img/avatars"), subirFiles.strNombre + Path.GetExtension(file.FileName));
                    img.Save(path);
                    return string.Format(subirFiles.strNombre + Path.GetExtension(file.FileName));
                }
            }
            return string.Format("No se ha podido guardar la foto");
        }

        [HttpPost]
        public virtual string UploadFilesClientes(subirArchivos subirFiles)
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    WebImage img = new WebImage(file.InputStream);
                    if (img.Width > 200)
                        img.Resize(200, 200);
                    string path = Path.Combine(Server.MapPath("~/content/img/clientes"), subirFiles.strNombre + Path.GetExtension(file.FileName));
                    img.Save(path);
                    return string.Format(subirFiles.strNombre + Path.GetExtension(file.FileName));
                }
            }
            return string.Format("No se ha podido guardar la foto");
        }

        [HttpPost]
        public ActionResult SaveUploadedFile(string strUNeg, string strClave, string idDoc)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    string[] fileName1 = file.FileName.Split(char.Parse("."));
                    fName = "OT" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "." + fileName1[1].ToLower();
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}archivos\\" + Session["intUneg"].ToString() + "\\", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), strClave);
                        //Path.GetFileName(file.FileName).ToString();
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
        public JsonResult subeArchivo(string id)
        //public async Task<JsonResult> subeArchivo(string id)
        {
            String fName = "";
            String pathString = "";
            String path = "";
            String rutaImagen = "../archivos/img/" + Convert.ToString(id) + "/";
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    
                    string[] fileName1 = fileContent.FileName.Split(char.Parse("."));
                    fName = "OT" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "." + fileName1[1].ToLower();

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
                        pathString = Path.Combine(Server.MapPath("~/archivos/img"), Convert.ToString(id));
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
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Falló la carga del archivo", archivo = rutaImagen + fName });
            }

            //return Json("File uploaded successfully");
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            return Json(new { Message = "Archivo cargado", archivo = rutaImagen + fName });
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult subeAdjunto(string id, string desc)
        //public async Task<JsonResult> subeArchivo(string id)
        {
            String fName = "";
            String pathString = "";
            String path = "";
            String rutaImagen = "../archivos/adjuntos/" + Convert.ToString(id) + "/";
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];

                    string[] fileName1 = fileContent.FileName.Split(char.Parse("."));
                    fName = "UN" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "." + fileName1[1].ToLower();

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
                        pathString = Path.Combine(Server.MapPath("~/archivos/adjuntos"), Convert.ToString(id));
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
                    String sql = "INSERT INTO unegocioADJUNTO (unegocio_id, unegocioADJUNTO_RUTA, unegocioADJUNTO_DESC) VALUES (" + id + ",' " + rutaImagen + fName + "', '" + desc.ToUpper() + "')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                }
                catch (Exception)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Message = "Falló la carga del archivo", archivo = rutaImagen + fName });
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Falló la carga del archivo", archivo = rutaImagen + fName });
            }
            //return Json("File uploaded successfully");
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            return Json(new { Message = "Archivo cargado", archivo = rutaImagen + fName });   
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult subeAdjuntoRuta(string id, string desc, string ruta)
        //public async Task<JsonResult> subeArchivo(string id)
        {
            String fName = "";
            String pathString = "";
            String path = "";
            //String rutaImagen = "../archivos/adjuntos/" + Convert.ToString(id) + "/";
            String rutaImagen = ruta;
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];

                    string[] fileName1 = fileContent.FileName.Split(char.Parse("."));
                    fName = "UN" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "." + fileName1[1].ToLower();

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
                        //pathString = Path.Combine(Server.MapPath("~/archivos/adjuntos"), Convert.ToString(id));
                        //pathString = Path.Combine(Server.MapPath("~/archivos"), ruta);
                        pathString = Server.MapPath("~/archivos") + ruta;
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
                /*
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // El adjunto es nuevo
                    // Crear Objeto query
                    String sql = "INSERT INTO unegocioADJUNTO (unegocio_id, unegocioADJUNTO_RUTA, unegocioADJUNTO_DESC) VALUES (" + id + ",' " + rutaImagen + fName + "', '" + desc.ToUpper() + "')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                }
                catch (Exception)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Message = "Falló la carga del archivo", archivo = rutaImagen + fName });
                }*/
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Falló la carga del archivo", archivo = rutaImagen + fName });
            }
            //return Json("File uploaded successfully");
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            //return Json(new { Message = "Archivo cargado ruta", archivo = pathString + fName });
            return Json(new { Message = "Archivo cargado ruta", archivo = fName });

        }


        [HttpPost]
        [AllowAnonymous]

        public JsonResult subeAvatar()
        {
            String fName = "";
            String pathString = "";
            String path = "";
            String rutaImagen = "~/archivos/img/avatars/";
            //String rutaImagen = ruta;
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];

                    string[] fileName1 = fileContent.FileName.Split(char.Parse("."));
                    fName = "UN" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "." + fileName1[1].ToLower();

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
                        //pathString = Path.Combine(Server.MapPath("~/archivos/adjuntos"), Convert.ToString(id));
                        //pathString = Path.Combine(Server.MapPath("~/archivos"), ruta);
                        pathString = Server.MapPath("~/archivos/img/avatars") ;
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
                
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Falló la carga del archivo", archivo = rutaImagen + fName });
            }
            //return Json("File uploaded successfully");
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            Response.TrySkipIisCustomErrors = true; 
            return Json(new { Message = "Archivo cargado ruta", archivo = fName });

        }


        [HttpPost]
        [AllowAnonymous]

        public JsonResult subeImagen()
        {
            String fName = "";
            //String pathString = "";
            String path = "";
            String rutaImagen = "~/archivos/img/avatars/";
            //String rutaImagen = ruta;
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];

                    string[] fileName1 = fileContent.FileName.Split(char.Parse("."));
                    fName = "IMG" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "." + fileName1[1].ToLower();

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

                        // Integra el nombre unico del archivo a la ruta de archivos y haz la carga
                        path = Path.Combine(uploadPath, fName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }

                }

            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Falló la carga del archivo", archivo = rutaImagen + fName });
            }
            //return Json("File uploaded successfully");
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            Response.TrySkipIisCustomErrors = true;
            return Json(new { Message = "Archivo cargado ruta", archivo = fName });

        }

        [HttpPost]
        [AllowAnonymous]

        public JsonResult subeArchivoTipo(int tipo)
        {
            String fName = "";
            String files = "";
            //String pathString = "";
            String path = "";
            String rutaArchivo = "";
            String prefix = "";
            int cuentaArchivos = 0;
            //String rutaImagen = ruta;

            switch (tipo)
            {
                case 1:
                    rutaArchivo = "~/uploads/img/";
                    prefix = "IMG";
                    break;
                case 2:
                    rutaArchivo = "~/uploads/adjunto/";
                    prefix = "ADJUNTO";
                    break;
                case 3:
                    rutaArchivo = "~/uploads/doc/";
                    prefix = "DOC";
                    break;
                default:
                    rutaArchivo = "~/uploads/otros/";
                    prefix = "FILE";
                    break;
            }


            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    

                    string[] fileName1 = fileContent.FileName.Split(char.Parse("."));
                    fName = fileName1[0].Trim().ToLower() + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Millisecond.ToString("000") + "." + fileName1[1].ToLower();
                    if (files == "")
                    {
                        files = fName;
                    }
                    else
                    {
                        files = files + "," + fName;
                    }
                    

                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = Path.GetFileName(file);

                        // Verifica la ruta de carga temporal
                        var uploadPath = Server.MapPath(rutaArchivo);
                        bool isExists = System.IO.Directory.Exists(uploadPath);
                        if (!isExists) System.IO.Directory.CreateDirectory(uploadPath);

                        // Integra el nombre unico del archivo a la ruta de archivos y haz la carga
                        path = Path.Combine(uploadPath, fName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                        cuentaArchivos++; 
                    }

                }

            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.Accepted;
                return Json(new { Success= false, Message = "Falló la carga del archivo", archivo = rutaArchivo + fName });
            }
            //return Json("File uploaded successfully");
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            Response.TrySkipIisCustomErrors = true;
            return Json(new { Success = true, Message = "Archivo cargado ruta", archivo = files, conteo= cuentaArchivos });

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveUploadedFileOpcional(string strClave)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    string[] fileName1 = file.FileName.Split(char.Parse("."));
                    fName = file.FileName;// "OT" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + "." + fileName1[1].ToLower();
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}archivos\\" + Session["intUneg"].ToString() + "\\", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), strClave, "opc");
                        //Path.GetFileName(file.FileName).ToString();
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
        public ActionResult SaveUploadedFileAgente(string strUNeg, string strClave, string idDoc, int count)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    string[] fileName1 = file.FileName.Split(char.Parse("."));
                    fName = "OT" + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + count.ToString("00") + "." + fileName1[1].ToLower();
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}archivos\\" + Session["intUneg"].ToString() + "\\", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), strClave);
                        //Path.GetFileName(file.FileName).ToString();
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                        var path = string.Format("{0}\\{1}", pathString, fName);
                        file.SaveAs(path);
                        guardaArchivoAgente(strClave, idDoc, fName);
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

        private void guardaArchivoAgente(string strClave, string idDoc, string fname)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            string comando = "";
            if(idDoc.Contains("DOCU"))
                comando = "INSERT_DOCUMAGENTE";
            else if (idDoc.Contains("DOCA"))
                comando = "UPDATE_DOCUMAGENTE";
            SqlCommand cmd = new SqlCommand(comando, con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (idDoc.Contains("DOCU"))
            {
                cmd.Parameters.Add("AGENTE_CLAVE", SqlDbType.NChar);
                cmd.Parameters.Add("DOCUMENTO_ID", SqlDbType.Int);
                cmd.Parameters["AGENTE_CLAVE"].Value = strClave;
                cmd.Parameters["DOCUMENTO_ID"].Value = int.Parse(idDoc.Remove(0, 4));
            }
            else if (idDoc.Contains("DOCA"))
            {
                cmd.Parameters.Add("INTID", SqlDbType.Int);
                cmd.Parameters["INTID"].Value = int.Parse(idDoc.Remove(0, 4));
            }
            cmd.Parameters.Add("ARCHIVO", SqlDbType.VarChar);
            cmd.Parameters.Add("FECHA", SqlDbType.DateTime);
            cmd.Parameters.Add("REALIZO", SqlDbType.Int);
            cmd.Parameters.Add("ACTIVO", SqlDbType.Bit);
            cmd.Parameters["ARCHIVO"].Value = fname;
            cmd.Parameters["FECHA"].Value = DateTime.Now.ToString();
            cmd.Parameters["REALIZO"].Value = Session["intID"].ToString();
            cmd.Parameters["ACTIVO"].Value = true;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public class attribArchivos
        {
            public string filename { get; set; }
            public string size { get; set; }
            public string ext { get; set; }
        }

        [HttpPost]
        public JsonResult mostrarArchivos(string strClave)
        {
            string strPath = "~/archivos/" + Session["intUneg"].ToString() + "/" + strClave + "/opc";
            string strDirectorio = Server.MapPath(strPath);
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
                    atributos.Add(new attribArchivos { filename = host + "/" + strPath.Remove(0, 2) + "/" +  Path.GetFileName(s), ext = Path.GetExtension(s), size = fI.Length.ToString() });
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

        public void ExportToExcel(SqlDataReader sqlDR1, string fileName)
        {
            //Add Response header 
            Response.Clear();
            Response.AddHeader("content-disposition",
               string.Format("attachment;filename={0}.csv", fileName));
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            //GET Data From Database                
            try
            {
                StringBuilder sb = new StringBuilder();
                //Add Header

                for (int count = 0; count < sqlDR1.FieldCount; count++)
                {
                    if (sqlDR1.GetName(count) != null)
                        sb.Append(sqlDR1.GetName(count));
                    if (count < sqlDR1.FieldCount - 1)
                    {
                        sb.Append(",");
                    }
                }
                Response.Write(sb.ToString() + "\n");
                Response.Flush();
                //Append Data

                while (sqlDR1.Read())
                {
                    sb = new StringBuilder();

                    for (int col = 0; col < sqlDR1.FieldCount - 1; col++)
                    {
                        if (!sqlDR1.IsDBNull(col))
                            sb.Append(sqlDR1.GetValue(col).ToString().Replace(",", " "));
                        sb.Append(",");
                    }
                    if (!sqlDR1.IsDBNull(sqlDR1.FieldCount - 1))
                        sb.Append(sqlDR1.GetValue(sqlDR1.FieldCount - 1).ToString().Replace(",", " "));
                    Response.Write(sb.ToString() + "\n");
                    Response.Flush();
                }
                sqlDR1.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            /*finally
            {
                cmd.Connection.Close();
                cn.Close();
            }*/
            Response.End();
        }
	}
}