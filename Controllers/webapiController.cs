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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SmartAdminMvc.Controllers
{
    public class webapiController : Controller
    {
        //
        // GET: /webapi/
        private string strSalt = "golsystemsSoftware2014";

        private static string CreateSHAHash(string Password, string Salt)
        {
            System.Security.Cryptography.SHA512Managed HashTool = new System.Security.Cryptography.SHA512Managed();
            Byte[] PasswordAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(Password, Salt));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PasswordAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes);
        }

        public JObject login(string u, string p)
        {
            string strHash = CreateSHAHash(p, strSalt);
            // Obtener el objeto JSON generico
            //String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();
            var re = new JObject();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT_USUARIO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("USERNAME", SqlDbType.VarChar);
            cmd.Parameters.Add("PASSWORD", SqlDbType.VarChar);
            cmd.Parameters["USERNAME"].Value = u;
            cmd.Parameters["PASSWORD"].Value = strHash;
            con.Open();
            SqlDataReader sqlDR = cmd.ExecuteReader();
            if (sqlDR.HasRows)
            {
                var jr = new JArray();

                while (sqlDR.Read())
                {
                    var obj = new JObject();

                    obj["usuario_id"] = sqlDR.GetInt32(0);
                    obj["usuario_nombre"] = sqlDR.GetString(1);
                    obj["usuario_apaterno"] = sqlDR.GetString(2);
                    obj["usuario_amaterno"] = sqlDR.GetString(3);
                    //obj["usuario_fotografia"] = sqlDR.GetString(4);
                    obj["unegocio_id"] = sqlDR.GetInt32(5);
                    obj["usuario_clave"] = sqlDR.GetString(6);
                    obj["rol_id"] = sqlDR.GetInt32(7);
                    obj["agente_id"] = sqlDR.GetInt32(8);
                    obj["usuario_correoelectronico"] = sqlDR.GetString(9);

                    jr.Add(obj);
                    
             
                }
                con.Close();

                if (jr.Count > 0)
                {
                    re["data"] = jr;
                    re["success"] = "true";

                    return re;
                }
                else
                {
                    re["success"] = "false";
                    re["Error"] = "No existen registros";

                    return re;
                }
            }
            else
            {
                re["success"] = "false";
                re["Error"] = "No existen registros";

                return re;
            }
        }
	}
}