#region Using

using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartAdminMvc.Models;
using SmartAdminMvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

#endregion

namespace SmartAdminMvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private string strSalt = "golsystemsSoftware2014";
        
        // TODO: This should be moved to the constructor of the controller in combination with a DependencyResolver setup
        // NOTE: You can use NuGet to find a strategy for the various IoC packages out there (i.e. StructureMap.MVC5)
        private readonly UserManager _manager = UserManager.Create();

        // GET: /account/forgotpassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            return View();
        }

        // GET: /account/login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            // Store the originating URL so we can attach it to a form field
            var viewModel = new AccountLoginModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }

        // POST: /account/login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountLoginModel viewModel)
        {
            // Ensure we have a valid viewModel to work with
            if (!ModelState.IsValid)
                return View(viewModel);

            // Verify if a user exists with the provided identity information
            var user = await _manager.FindByEmailAsync(viewModel.Email);

            // If a user was found
            if (user != null)
            {
                // Then create an identity for it and sign it in
                await SignInAsync(user, viewModel.RememberMe);

                // If the user came from a specific page, redirect back to it
                return RedirectToLocal(viewModel.ReturnUrl);
            }

            // No existing user was found that matched the given criteria
            ModelState.AddModelError("", "Invalid username or password.");

            // If we got this far, something failed, redisplay form
            return View(viewModel);
        }

        // GET: /account/error
        [AllowAnonymous]
        public ActionResult Error()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            return View();
        }

        // GET: /account/register
        [AllowAnonymous]
        public ActionResult Register()
        {
            // We do not want to use any existing identity information
            EnsureLoggedOut();

            return View(new AccountRegistrationModel());
        }

        // POST: /account/register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountRegistrationModel viewModel)
        {
            // Ensure we have a valid viewModel to work with
            if (!ModelState.IsValid)
                return View(viewModel);

            // Prepare the identity with the provided information
            var user = new IdentityUser
            {
                UserName = viewModel.Username ?? viewModel.Email,
                Email = viewModel.Email
            };

            // Try to create a user with the given identity
            try
            {
                var result = await _manager.CreateAsync(user, viewModel.Password);

                // If the user could not be created
                if (!result.Succeeded) {
                    // Add all errors to the page so they can be used to display what went wrong
                    AddErrors(result);

                    return View(viewModel);
                }

                // If the user was able to be created we can sign it in immediately
                // Note: Consider using the email verification proces
                await SignInAsync(user, false);

                return RedirectToLocal();
            }
            catch (DbEntityValidationException ex)
            {
                // Add all errors to the page so they can be used to display what went wrong
                AddErrors(ex);

                return View(viewModel);
            }
        }

        // POST: /account/Logout
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // First we clean the authentication ticket like always
            FormsAuthentication.SignOut();

            // Second we clear the principal to ensure the user does not retain any authentication
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
            // this clears the Request.IsAuthenticated flag since this triggers a new request
            return Json(new { success = true });
        }

        private ActionResult RedirectToLocal(string returnUrl = "")
        {
            // If the return url starts with a slash "/" we assume it belongs to our site
            // so we will redirect to this "action"
            /*if (!returnUrl.IsNullOrWhiteSpace() && Url.IsLocalUrl(returnUrl))
                return Json(Url.Action("Index", "Home"));*/
            // If we cannot verify if the url is local to our host we redirect to a default location
            return Json(Url.Action("index", "home"));
        }

        private void AddErrors(DbEntityValidationException exc)
        {
            foreach (var error in exc.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors.Select(validationError => validationError.ErrorMessage)))
            {
                ModelState.AddModelError("", error);
            }
        }

        private void AddErrors(IdentityResult result)
        {
            // Add all errors that were returned to the page error collection
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        private async Task SignInAsync(IdentityUser user, bool isPersistent)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Create a claims based identity for the current user
            var identity = await _manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(identity.Name, isPersistent);
        }

        // GET: /account/lock
        [AllowAnonymous]
        public ActionResult Lock()
        {
            return View();
        }

        public class detailsUsuario
        {
            public string username { get; set; }
            public string password { get; set; }
            public int unidnego { get; set; }
            public string claveagente { get; set; }
            public int intId { get; set; }
            public string nombre { get; set; }
            public string apaterno { get; set; }
            public string amaterno { get; set; }
            public string siglas { get; set; }
            public string foto { get; set; }
            public bool activo { get; set; }
            public string correo { get; set; }
            public string numext { get; set; }
            public string calle { get; set; }
            public string colonia { get; set; }
            public string numint { get; set; }
            public string codpost { get; set; }
            public int efedid { get; set; }
            public int delmunid { get; set; }
            public string casclvlada { get; set; }
            public string casnumero { get; set; }
            public string casrecado { get; set; }
            public string ofcclvlada { get; set; }
            public string ofcnumero { get; set; }
            public string ofcextension { get; set; }
            public string celular { get; set; }
            public string fechaalta { get; set; }
            public string fechamodif { get; set; }
            public string realizoalta { get; set; }
            public string realizomodif { get; set; }
            public int idUser { get; set; }

        }

        private static string CreateSHAHash(string Password, string Salt)
        {
            System.Security.Cryptography.SHA512Managed HashTool = new System.Security.Cryptography.SHA512Managed();
            Byte[] PasswordAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(Password, Salt));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PasswordAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes);
        }

       

        private string strVerifica(string strVerif)
        {
            if (strVerif == null)
                return "";
            return strVerif;
        }

        [HttpPost]
        public JsonResult guardarUsuario(detailsUsuario datosUsuario)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string comando = "SELECT_AGENTEID";
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["CLAVE"].Value = datosUsuario.claveagente;
                con.Open();
                SqlDataReader sqlDr = cmd.ExecuteReader();
                if (sqlDr.HasRows)
                {
                    while (sqlDr.Read())
                    {
                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                        string comando1 = "";
                        if (datosUsuario.intId == 0)
                            comando1 = "INSERT_USUARIO";
                        else
                            comando1 = "UPDATE_USUARIO";
                        SqlCommand cmd1 = new SqlCommand(comando1, con1);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        if (datosUsuario.intId == 0)
                        {
                            cmd1.Parameters.Add("username", SqlDbType.VarChar);
                            cmd1.Parameters["username"].Value = datosUsuario.username;
                            cmd1.Parameters.Add("agente", SqlDbType.NChar);
                            cmd1.Parameters["agente"].Value = datosUsuario.claveagente;
                        }
                        else
                        {
                            cmd1.Parameters.Add("id", SqlDbType.Int);
                            cmd1.Parameters["id"].Value = datosUsuario.intId;
                            cmd1.Parameters.Add("unidneg", SqlDbType.Int);
                            cmd1.Parameters["unidneg"].Value = datosUsuario.unidnego;
                            cmd1.Parameters.Add("agente", SqlDbType.NChar);
                            cmd1.Parameters["agente"].Value = datosUsuario.claveagente;
                        }
                        cmd1.Parameters.Add("nombre", SqlDbType.VarChar);
                        cmd1.Parameters.Add("apaterno", SqlDbType.VarChar);
                        cmd1.Parameters.Add("amaterno", SqlDbType.VarChar);
                        cmd1.Parameters.Add("siglas", SqlDbType.NChar);
                        cmd1.Parameters.Add("foto", SqlDbType.VarChar);
                        cmd1.Parameters.Add("fecha", SqlDbType.DateTime);
                        cmd1.Parameters.Add("activo", SqlDbType.Bit);
                        cmd1.Parameters.Add("realizo", SqlDbType.Int);
                        cmd1.Parameters.Add("correo", SqlDbType.Text);
                        cmd1.Parameters.Add("calle", SqlDbType.NChar);
                        cmd1.Parameters.Add("numext", SqlDbType.NChar);
                        cmd1.Parameters.Add("numint", SqlDbType.NChar);
                        cmd1.Parameters.Add("colonia", SqlDbType.NChar);
                        cmd1.Parameters.Add("codpost", SqlDbType.NChar);
                        cmd1.Parameters.Add("efedid", SqlDbType.Int);
                        cmd1.Parameters.Add("delmunid", SqlDbType.Int);
                        cmd1.Parameters.Add("casclvlada", SqlDbType.NChar);
                        cmd1.Parameters.Add("casnumero", SqlDbType.NChar);
                        cmd1.Parameters.Add("casrecado", SqlDbType.NChar);
                        cmd1.Parameters.Add("ofcclvlada", SqlDbType.NChar);
                        cmd1.Parameters.Add("ofcnumero", SqlDbType.NChar);
                        cmd1.Parameters.Add("ofcextension", SqlDbType.NChar);
                        cmd1.Parameters.Add("celular", SqlDbType.NChar);
                        cmd1.Parameters.Add("password", SqlDbType.VarChar);
                        cmd1.Parameters["nombre"].Value = sqlDr.GetString(1);
                        cmd1.Parameters["apaterno"].Value = sqlDr.GetString(2);
                        cmd1.Parameters["amaterno"].Value = sqlDr.GetString(3);
                        cmd1.Parameters["siglas"].Value = sqlDr.GetString(1).Substring(0, 1) + sqlDr.GetString(2).Substring(0, 1) + sqlDr.GetString(3).Substring(0, 1);
                        cmd1.Parameters["foto"].Value = sqlDr.GetString(43);
                        cmd1.Parameters["fecha"].Value = DateTime.Now.ToString();
                        cmd1.Parameters["activo"].Value = true;
                        cmd1.Parameters["realizo"].Value = Session["intID"].ToString();
                        cmd1.Parameters["correo"].Value = sqlDr.GetString(13);
                        cmd1.Parameters["calle"].Value = sqlDr.GetString(15);
                        cmd1.Parameters["numext"].Value = sqlDr.GetString(16);
                        cmd1.Parameters["numint"].Value = sqlDr.GetString(17);
                        cmd1.Parameters["colonia"].Value = sqlDr.GetString(18);
                        cmd1.Parameters["codpost"].Value = sqlDr.GetString(21);
                        cmd1.Parameters["efedid"].Value = sqlDr.GetInt32(19);
                        cmd1.Parameters["delmunid"].Value = sqlDr.GetInt32(20);
                        cmd1.Parameters["casclvlada"].Value = sqlDr.GetString(22);
                        cmd1.Parameters["casnumero"].Value = sqlDr.GetString(23);
                        cmd1.Parameters["casrecado"].Value = sqlDr.GetString(24);
                        cmd1.Parameters["ofcclvlada"].Value = sqlDr.GetString(25);
                        cmd1.Parameters["ofcnumero"].Value = sqlDr.GetString(26);
                        cmd1.Parameters["ofcextension"].Value = sqlDr.GetString(27);
                        cmd1.Parameters["celular"].Value = sqlDr.GetString(28);
                        string strTemp = strVerifica(datosUsuario.password);
                        if (strTemp == "")
                            cmd1.Parameters["password"].Value = "";
                        else
                            cmd1.Parameters["password"].Value = CreateSHAHash(strTemp, strSalt);
                        con1.Open();
                        if (datosUsuario.intId == 0)
                        {
                            int intAgente = (Int32)(cmd1.ExecuteScalar());
                            con1.Close();
                            if (intAgente > -1)
                            {
                                cmd1 = new SqlCommand("INSERT_USUARIOUN", con1);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.Add("unidneg", SqlDbType.Int);
                                cmd1.Parameters.Add("usuario", SqlDbType.Int);
                                cmd1.Parameters["unidneg"].Value = datosUsuario.unidnego;
                                cmd1.Parameters["usuario"].Value = intAgente;
                                con1.Open();
                                cmd1.ExecuteNonQuery();
                                con1.Close();
                            }
                            else
                                return Json(new { success = false, mensaje = "No se pudo guardar el usuario" });
                        }
                        else
                        {
                            cmd1.ExecuteNonQuery();
                            con1.Close();
                        }
                    }
                    con.Close();
                    return Json(new { success = true });
                }
                else
                {
                    con.Close();
                    return Json(new { success = false, mensaje = "No se encontro el agente" });
                }
            }
            catch(Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult guarda2Usuario(detailsUsuario datosUsuario)
        {
            try
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string comando1 = "";
                if (datosUsuario.idUser == 0)
                    comando1 = "INSERT_USUARIO";
                else
                    comando1 = "UPDATE_USUARIO";
                SqlCommand cmd1 = new SqlCommand(comando1, con1);
                cmd1.CommandType = CommandType.StoredProcedure;
                if (datosUsuario.idUser == 0)
                {
                    cmd1.Parameters.Add("username", SqlDbType.VarChar);
                    cmd1.Parameters["username"].Value = datosUsuario.username;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("SELECT_IDUNEGOCIO", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("INTID", SqlDbType.Int);
                    cmd.Parameters["INTID"].Value = datosUsuario.unidnego;
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
                    cmd1.Parameters.Add("agente", SqlDbType.NChar);
                    cmd1.Parameters["agente"].Value = datosUsuario.claveagente;
                    //datosUsuario.claveagente = strNome + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
                }
                else
                {
                    cmd1.Parameters.Add("id", SqlDbType.Int);
                    cmd1.Parameters["id"].Value = datosUsuario.idUser;
                    cmd1.Parameters.Add("unidneg", SqlDbType.Int);
                    cmd1.Parameters["unidneg"].Value = datosUsuario.unidnego;
                    cmd1.Parameters.Add("agente", SqlDbType.NChar);
                    cmd1.Parameters["agente"].Value = datosUsuario.claveagente;
                }
                cmd1.Parameters.Add("nombre", SqlDbType.VarChar);
                cmd1.Parameters.Add("apaterno", SqlDbType.VarChar);
                cmd1.Parameters.Add("amaterno", SqlDbType.VarChar);
                cmd1.Parameters.Add("siglas", SqlDbType.NChar);
                cmd1.Parameters.Add("foto", SqlDbType.VarChar);
                cmd1.Parameters.Add("fecha", SqlDbType.DateTime);
                cmd1.Parameters.Add("activo", SqlDbType.Bit);
                cmd1.Parameters.Add("realizo", SqlDbType.Int);
                cmd1.Parameters.Add("correo", SqlDbType.Text);
                cmd1.Parameters.Add("calle", SqlDbType.NChar);
                cmd1.Parameters.Add("numext", SqlDbType.NChar);
                cmd1.Parameters.Add("numint", SqlDbType.NChar);
                cmd1.Parameters.Add("colonia", SqlDbType.NChar);
                cmd1.Parameters.Add("codpost", SqlDbType.NChar);
                cmd1.Parameters.Add("efedid", SqlDbType.Int);
                cmd1.Parameters.Add("delmunid", SqlDbType.Int);
                cmd1.Parameters.Add("casclvlada", SqlDbType.NChar);
                cmd1.Parameters.Add("casnumero", SqlDbType.NChar);
                cmd1.Parameters.Add("casrecado", SqlDbType.NChar);
                cmd1.Parameters.Add("ofcclvlada", SqlDbType.NChar);
                cmd1.Parameters.Add("ofcnumero", SqlDbType.NChar);
                cmd1.Parameters.Add("ofcextension", SqlDbType.NChar);
                cmd1.Parameters.Add("celular", SqlDbType.NChar);
                cmd1.Parameters.Add("password", SqlDbType.VarChar);
                cmd1.Parameters["nombre"].Value = datosUsuario.nombre.ToUpper();
                cmd1.Parameters["apaterno"].Value = datosUsuario.apaterno.ToUpper();
                cmd1.Parameters["amaterno"].Value = datosUsuario.amaterno.ToUpper();
                cmd1.Parameters["siglas"].Value = datosUsuario.nombre.Substring(0, 1).ToUpper() + datosUsuario.apaterno.Substring(0, 1).ToUpper() + datosUsuario.amaterno.Substring(0, 1).ToUpper();
                cmd1.Parameters["foto"].Value = datosUsuario.foto.ToUpper();
                cmd1.Parameters["fecha"].Value = DateTime.Now.ToString();
                cmd1.Parameters["activo"].Value = true;
                cmd1.Parameters["realizo"].Value = Session["intID"].ToString();
                cmd1.Parameters["correo"].Value = strVerifica(datosUsuario.correo);
                cmd1.Parameters["calle"].Value = datosUsuario.calle.ToUpper();
                cmd1.Parameters["numext"].Value = datosUsuario.numext.ToUpper();
                cmd1.Parameters["numint"].Value = strVerifica(datosUsuario.numint).ToUpper();
                cmd1.Parameters["colonia"].Value = datosUsuario.colonia.ToUpper();
                cmd1.Parameters["codpost"].Value = datosUsuario.codpost.ToUpper();
                cmd1.Parameters["efedid"].Value = datosUsuario.efedid;
                cmd1.Parameters["delmunid"].Value = datosUsuario.delmunid;
                cmd1.Parameters["casclvlada"].Value = strVerifica(datosUsuario.casclvlada).ToUpper();
                cmd1.Parameters["casnumero"].Value = strVerifica(datosUsuario.casnumero).ToUpper();
                cmd1.Parameters["casrecado"].Value = strVerifica(datosUsuario.casrecado).ToUpper();
                cmd1.Parameters["ofcclvlada"].Value = strVerifica(datosUsuario.ofcclvlada).ToUpper();
                cmd1.Parameters["ofcnumero"].Value = strVerifica(datosUsuario.ofcnumero).ToUpper();
                cmd1.Parameters["ofcextension"].Value = strVerifica(datosUsuario.ofcextension).ToUpper();
                cmd1.Parameters["celular"].Value = strVerifica(datosUsuario.celular).ToUpper();
                string strTemp = strVerifica(datosUsuario.password);
                if (strTemp == "")
                    cmd1.Parameters["password"].Value = "";
                else
                    cmd1.Parameters["password"].Value = CreateSHAHash(strTemp, strSalt);
                con1.Open();
                if (datosUsuario.idUser == 0)
                {
                    int intAgente = (Int32)(cmd1.ExecuteScalar());
                    con1.Close();
                    if (intAgente > -1)
                    {
                        cmd1 = new SqlCommand("INSERT_USUARIOUN", con1);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("unidneg", SqlDbType.Int);
                        cmd1.Parameters.Add("usuario", SqlDbType.Int);
                        cmd1.Parameters["unidneg"].Value = datosUsuario.unidnego;
                        cmd1.Parameters["usuario"].Value = intAgente;
                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();
                    }
                    else
                        return Json(new { success = false, mensaje = "No se pudo guardar el usuario" });
                }
                else
                {
                    cmd1.ExecuteNonQuery();
                    con1.Close();
                }
                return Json(new { success = true });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verUsuario(detailsUsuario datosUsuario)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_USUARIOAGENTE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("agente", SqlDbType.NChar);
                cmd.Parameters["agente"].Value = datosUsuario.claveagente;
                con.Open();
                SqlDataReader sqldDR = cmd.ExecuteReader();
                if (sqldDR.HasRows)
                {
                    detailsUsuario datUsua = new detailsUsuario();
                    while (sqldDR.Read())
                    {
                        datUsua.username = sqldDR.GetString(1);
                        datUsua.unidnego = sqldDR.GetInt32(2);
                        datUsua.intId = sqldDR.GetInt32(0);
                    }
                    con.Close();
                    return Json(datUsua, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        
        [HttpPost]
        public JsonResult cargaUsuarioId(detailsUsuario datosUsuario)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_USUARIOID3", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ID", SqlDbType.Int);
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["ID"].Value = datosUsuario.idUser;
                cmd.Parameters["CLAVE"].Value = datosUsuario.claveagente;
                //cmd.Parameters["CLAVE"].Value = Session["strClaveAg"].ToString();
                con.Open();
                SqlDataReader sqldDR = cmd.ExecuteReader();
                if (sqldDR.HasRows)
                {
                    var datUsua = new List<detailsUsuario>();
                    while (sqldDR.Read())
                    {
                        datUsua.Add(new detailsUsuario {
                            claveagente = sqldDR.SafeGetString(0).Trim(),
                            username = sqldDR.SafeGetString(25).Trim(),
                            unidnego = sqldDR.SafeGetInt32(26),
                            intId = sqldDR.SafeGetInt32(27),
                            nombre = sqldDR.SafeGetString(1).Trim(),
                            apaterno = sqldDR.SafeGetString(2).Trim(),
                            amaterno = sqldDR.SafeGetString(3).Trim(), 
                            activo = sqldDR.GetBoolean(4), 
                            fechaalta = sqldDR.GetDateTime(5).ToString().Trim(), 
                            fechamodif = sqldDR.GetDateTime(6).ToString().Trim(),
                            correo = sqldDR.SafeGetString(7).Trim(),
                            calle = sqldDR.SafeGetString(8).Trim(),
                            numext = sqldDR.SafeGetString(9).Trim(),
                            numint = sqldDR.SafeGetString(10).Trim(),
                            colonia = sqldDR.SafeGetString(11).Trim(),
                            efedid = sqldDR.SafeGetInt32(12),
                            delmunid = sqldDR.SafeGetInt32(13),
                            codpost = sqldDR.SafeGetString(14).Trim(),
                            casclvlada = sqldDR.SafeGetString(15).Trim(),
                            casnumero = sqldDR.SafeGetString(16).Trim(),
                            casrecado = sqldDR.SafeGetString(17).Trim(),
                            ofcclvlada = sqldDR.SafeGetString(19).Trim(),
                            ofcnumero = sqldDR.SafeGetString(18).Trim(),
                            ofcextension = sqldDR.SafeGetString(20).Trim(),
                            celular = sqldDR.SafeGetString(21).Trim(),
                            realizoalta = sqldDR.SafeGetString(22).Trim(),
                            realizomodif = sqldDR.SafeGetString(23).Trim(),
                            foto = sqldDR.SafeGetString(24).Trim()
                        });
                    }
                    con.Close();
                    return Json(datUsua, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false });
            }
            catch (Exception X)
            {
                return Json(new { success = false, mensaje = X.Message });
            }
        }

        [HttpPost]
        public JsonResult verListaUsuarios()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("SELECT_USUARIOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("CLAVE", SqlDbType.NChar);
                cmd.Parameters["CLAVE"].Value = Session["strClaveAg"].ToString();
                con.Open();
                SqlDataReader sqlDR = cmd.ExecuteReader();
                if (sqlDR.HasRows)
                {
                    var etiqJson = new List<detailsUsuario>();
                    while (sqlDR.Read())
                    {
                        etiqJson.Add(new detailsUsuario { idUser = sqlDR.GetInt32(0), nombre = sqlDR.GetString(1).Trim()+ " " + sqlDR.GetString(2).Trim() + " " + sqlDR.GetString(3).Trim(), activo = sqlDR.GetBoolean(4) });
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
    }
}