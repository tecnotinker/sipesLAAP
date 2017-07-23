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
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using SmartAdminMvc;
//using System.Web.Http.Cors;
//using WebApiContrib.Formatting.Jsonp;

namespace SmartAdminMvc.Controllers
{
    public class getDataController : Controller
    {
        public string sql1;
        public string etapa;
        // GET: getData
        public ActionResult Index()
        {
            return View();
        }

        public JObject get()
        //public JsonResult json()
        {
            // Obtener el objeto JSON generico
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();
            var re = new JObject();

            if (jsonString != "")
            {
                // Deserialize it to a dictionary
                var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(jsonString);

                // Arreglos y listas
                List<string> nCampos = new List<string>();
                var registro = new JObject();
                string ls = "";
                List<JObject> rs = new List<JObject>();

                var jr = new JArray();
                

                // Los parametros de la consulta vienen en el objeto (entidad, where, order)
                string tabla;
                string where;
                string order;

                tabla = Convert.ToString(jsonObject["e"]); tabla = tabla.ToUpper();
                where = Convert.ToString(jsonObject["w"]); where = where.ToUpper();
                order = Convert.ToString(jsonObject["o"]); order = order.ToUpper();

                string sw = "";
                string so = "";
                string sql = "";
                if (where != null && where != "")
                {
                    sw = " WHERE " + where;
                }
                if (order != null && order != "")
                {
                    so = " ORDER BY " + order;
                }

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // declare command
                    sql = "select * from " + tabla + sw + so;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    var estructura = new JObject();
                    if (dr1.Read())
                    {
                        // Extraer la lista de campos
                        for (int x = 0; x <= dr1.FieldCount - 1; x++)
                        {
                            nCampos.Add(dr1.GetName(x).ToLower());
                        }

                        // Formatear el objeto json con nombre de campos
                        do
                        {
                            var obj = new JObject();
                            //obj["leaf"] = true;
                            //obj["srv"] = ini;
                            for (int x = 0; x <= dr1.FieldCount - 1; x++)
                            {
                                string fieldType = dr1.GetDataTypeName(x);
                                if (fieldType == "date" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy");
                                }
                                else
                                    if (fieldType == "datetime" && Convert.ToString(dr1.GetValue(x)) != "")
                                    {
                                        DateTime fecha = dr1.GetDateTime(x);
                                        //String soloFecha =
                                        obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                    else
                                        {
                                            obj[nCampos[x]] = Convert.ToString(dr1.GetValue(x));
                                        }
                            }

                            jr.Add(obj);
                            rs.Add(obj);
                            re["data"] = jr;

                        } while (dr1.Read());
                        dr1.Close();
                    }
                    re["success"] = "true";
                    return re;
                    dr1.Close();
                    conn.Close();
                }
                catch (Exception x)
                {
                    re["Error"] = x.Message;
                    return re;

                    conn.Close();
                }
                
            }
            else
            {
                re["Error"] = "No se recibieron parametros para consulta";
                return re;
            }
        }

        public JObject getqs(string e, string w, string o)
        //public JsonResult json()
        {
            // Obtener el objeto JSON generico
            //String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();
            var re = new JObject();

            if (e != "")
            {
                // Deserialize it to a dictionary
                //var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(jsonString);

                // Arreglos y listas
                List<string> nCampos = new List<string>();
                var registro = new JObject();
                string ls = "";
                List<JObject> rs = new List<JObject>();

                var jr = new JArray();


                // Los parametros de la consulta vienen en el objeto (entidad, where, order)
                string tabla;
                string where;
                string order;

                tabla = e; tabla = tabla.ToUpper();
                where = w; where = where.ToUpper();
                order = o; order = order.ToUpper();

                string sw = "";
                string so = "";
                string sql = "";
                if (where != null && where != "")
                {
                    sw = " WHERE " + where;
                }
                if (order != null && order != "")
                {
                    so = " ORDER BY " + order;
                }

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // declare command
                    sql = "select * from " + tabla + sw + so;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    var estructura = new JObject();
                    if (dr1.Read())
                    {
                        // Extraer la lista de campos
                        for (int x = 0; x <= dr1.FieldCount - 1; x++)
                        {
                            nCampos.Add(dr1.GetName(x).ToLower());
                        }

                        // Formatear el objeto json con nombre de campos
                        do
                        {
                            var obj = new JObject();
                            //obj["leaf"] = true;
                            //obj["srv"] = ini;
                            for (int x = 0; x <= dr1.FieldCount - 1; x++)
                            {
                                string fieldType = dr1.GetDataTypeName(x);
                                //obj["campo"+x] = fieldType;

                                if (fieldType == "date" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy");
                                }
                                else
                                if (fieldType == "datetime" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                                }
                                else
                                {
                                    obj[nCampos[x]] = Convert.ToString(dr1.GetValue(x));
                                }
                            }

                            jr.Add(obj);
                            rs.Add(obj);
                            re["data"] = jr;

                        } while (dr1.Read());
                        dr1.Close();
                    }
                    if (jr.Count > 0)
                    {
                        re["success"] = "true";
                        return re;
                    }
                    else
                    {
                        re["success"] = "false";
                        re["Error"] = "No existen registros";

                        return re;
                    }
                    dr1.Close();
                    conn.Close();
                }
                catch (Exception x)
                {
                    re["Error"] = x.Message;
                    return re;

                    conn.Close();
                }

            }
            else
            {
                re["Error"] = "No se recibieron parametros para consulta";
                return re;
            }
        }

        public JObject getqsPagina()
        //public JsonResult json()
        {
            // Obtener el objeto JSON generico
            // Convertir el querystring en lista de valores
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);
            var e = nvc["e"];
            var w = nvc["w"];
            var o = nvc["o"];
            var pagesize = nvc["pagesize"];
            var pagenum = nvc["pagenum"];
            var filterfield = nvc["filterdatafield0"];
            var filtervalue = nvc["filtervalue0"];

            var re = new JObject();

            if (e != "")
            {
                // Deserialize it to a dictionary
                //var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(jsonString);

                // Arreglos y listas
                List<string> nCampos = new List<string>();
                var registro = new JObject();
                string ls = "";
                List<JObject> rs = new List<JObject>();

                var jr = new JArray();


                // Los parametros de la consulta vienen en el objeto (entidad, where, order)
                string tabla;
                string where;
                string order;
                Int32 pg;
                Int32 cr;

                tabla = e; tabla = tabla.ToUpper();
                where = w; where = where.ToUpper();
                order = o; order = order.ToUpper();
                pg = Convert.ToInt32(pagenum);
                cr = Convert.ToInt32(pagesize);

                string sw = "";
                string so = "";
                string sql = "";
                string sql2 = "";
                if (where != null && where != "")
                {
                    sw = " WHERE " + where;
                    if (filterfield != null)
                    {
                        sw = sw + " AND " + filterfield + " like '%" + filtervalue + "%' ";
                    }
                }
                if (order != null && order != "")
                {
                    so = " ORDER BY " + order + " OFFSET " + Convert.ToString((pg) * cr) + " ROWS FETCH NEXT " + Convert.ToString(cr) + " rows only";
                } else
                {
                    throw new System.ArgumentException("No hay parametros de paginación", "original"); 
                }

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // Obtener el total de registros
                    sql2 = "select count(*) from " + tabla + sw;
                    //SqlCommand cmd1 = new SqlCommand(sql, conn);
                    SqlCommand query1 = new SqlCommand(sql2, conn);
                    query1.CommandType = CommandType.Text;
                    etapa = "2";
                    SqlDataReader dr2 = query1.ExecuteReader();
                    //jsonObject["sql1"] = sql2;

                    string rowcount = "";
                    if (dr2.Read())
                    {
                        rowcount = Convert.ToString(dr2.GetValue(0));

                        re["TotalRows"] = rowcount;
                    }
                    dr2.Close();


                    // declare command
                    sql = "select * from " + tabla + sw + so;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    var estructura = new JObject();
                    if (dr1.Read())
                    {
                        // Extraer la lista de campos
                        for (int x = 0; x <= dr1.FieldCount - 1; x++)
                        {
                            nCampos.Add(dr1.GetName(x).ToLower());
                        }

                        // Formatear el objeto json con nombre de campos
                        do
                        {
                            var obj = new JObject();
                            //obj["leaf"] = true;
                            //obj["srv"] = ini;
                            for (int x = 0; x <= dr1.FieldCount - 1; x++)
                            {
                                string fieldType = dr1.GetDataTypeName(x);
                                //obj["campo"+x] = fieldType;

                                if (fieldType == "date" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy");
                                }
                                else
                                if (fieldType == "datetime" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                                }
                                else
                                {
                                    obj[nCampos[x]] = Convert.ToString(dr1.GetValue(x));
                                }
                            }

                            jr.Add(obj);
                            rs.Add(obj);
                            re["data"] = jr;

                        } while (dr1.Read());
                        dr1.Close();
                    }
                    if (jr.Count > 0)
                    {
                        re["success"] = "true";
                        return re;
                    }
                    else
                    {
                        re["success"] = "false";
                        re["Error"] = "No existen registros";

                        return re;
                    }
                    dr1.Close();
                    conn.Close();
                }
                catch (Exception x)
                {
                    re["Error"] = x.Message;
                    re["sql"] = sql;
                    return re;

                    conn.Close();
                }

            }
            else
            {
                re["Error"] = "No se recibieron parametros para consulta";
                return re;
            }
        }

        public JObject getsp(string proc, string intopc)
        //public JsonResult json()
        {
            // Obtener el objeto JSON generico
            //String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();
            var re = new JObject();

            if (intopc != "")
            {
                // Deserialize it to a dictionary
                //var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(jsonString);

                // Arreglos y listas
                List<string> nCampos = new List<string>();
                var registro = new JObject();
                string ls = "";
                List<JObject> rs = new List<JObject>();

                var jr = new JArray();

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // declare command
                    
                    SqlCommand cmd = new SqlCommand(proc, conn);
                    //cmd.CommandType = CommandType.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("INTOPC", SqlDbType.Int);
                    cmd.Parameters["INTOPC"].Value = Convert.ToInt32(intopc);
                    SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    var estructura = new JObject();
                    if (dr1.Read())
                    {
                        // Extraer la lista de campos
                        for (int x = 0; x <= dr1.FieldCount - 1; x++)
                        {
                            nCampos.Add(dr1.GetName(x).ToLower());
                        }

                        // Formatear el objeto json con nombre de campos
                        do
                        {
                            var obj = new JObject();
                            //obj["leaf"] = true;
                            //obj["srv"] = ini;
                            for (int x = 0; x <= dr1.FieldCount - 1; x++)
                            {
                                string fieldType = dr1.GetDataTypeName(x);
                                //obj["campo"+x] = fieldType;

                                if (fieldType == "date" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy");
                                }
                                else
                                    if (fieldType == "datetime" && Convert.ToString(dr1.GetValue(x)) != "")
                                    {
                                        DateTime fecha = dr1.GetDateTime(x);
                                        //String soloFecha =
                                        obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                else
                                {
                                    obj[nCampos[x]] = Convert.ToString(dr1.GetValue(x));
                                }
                            }

                            jr.Add(obj);
                            rs.Add(obj);
                            re["data"] = jr;

                        } while (dr1.Read());
                        dr1.Close();
                    }
                    re["success"] = "true";
                    return re;
                    dr1.Close();
                    conn.Close();
                }
                catch (Exception x)
                {
                    re["Error"] = x.Message;
                    return re;

                    conn.Close();
                }

            }
            else
            {
                re["Error"] = "No se recibieron parametros para consulta";
                return re;
            }
        }

        public JObject getSQL()
        //public JsonResult json()
        {
            // Obtener el objeto JSON generico
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();
            var re = new JObject();

            if (jsonString != "")
            {
                // Deserialize it to a dictionary
                var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(jsonString);

                // Arreglos y listas
                List<string> nCampos = new List<string>();
                var registro = new JObject();
                string ls = "";
                List<JObject> rs = new List<JObject>();

                var jr = new JArray();


                // Los parametros de la consulta vienen en el objeto (entidad, where, order)
                string sql;

                sql = Convert.ToString(jsonObject["query"]); sql = sql.ToUpper();
                
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // declare command                    
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    int conteo = 0;
                    var estructura = new JObject();
                    if (dr1.Read())
                    {
                        // Extraer la lista de campos
                        for (int x = 0; x <= dr1.FieldCount - 1; x++)
                        {
                            nCampos.Add(dr1.GetName(x).ToLower());
                        }

                        // Formatear el objeto json con nombre de campos
                        do
                        {
                            var obj = new JObject();
                            //obj["leaf"] = true;
                            //obj["srv"] = ini;
                            for (int x = 0; x <= dr1.FieldCount - 1; x++)
                            {
                                string fieldType = dr1.GetDataTypeName(x);
                                if (fieldType == "date" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy");
                                }
                                else
                                    if (fieldType == "datetime" && Convert.ToString(dr1.GetValue(x)) != "")
                                    {
                                        DateTime fecha = dr1.GetDateTime(x);
                                        //String soloFecha =
                                        obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                    else
                                    {
                                        obj[nCampos[x]] = Convert.ToString(dr1.GetValue(x));
                                    }
                            }

                            jr.Add(obj);
                            rs.Add(obj);
                            re["data"] = jr;
                            conteo++;

                        } while (dr1.Read());
                        dr1.Close();
                    } 
                    re["success"] = "true";
                    re["recordcount"] = conteo;
                    return re;
                    dr1.Close();
                    conn.Close();
                }
                catch (Exception x)
                {
                    re["Error"] = x.Message;
                    return re;

                    conn.Close();
                }

            }
            else
            {
                re["Error"] = "No se recibieron parametros para consulta";
                return re;
            }
        }

        public JObject getSQLqs(String query)
        //public JsonResult json()
        {
            // Obtener el objeto JSON generico
            //String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();
            var re = new JObject();

            if (query != "")
            {
                // Deserialize it to a dictionary
                //var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(jsonString);

                // Arreglos y listas
                List<string> nCampos = new List<string>();
                var registro = new JObject();
                string ls = "";
                List<JObject> rs = new List<JObject>();

                var jr = new JArray();


                // La consulta viene en la variable query
                //string sql;

                //sql = Convert.ToString(jsonObject["query"]); sql = sql.ToUpper();

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // declare command                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    var estructura = new JObject();
                    if (dr1.Read())
                    {
                        // Extraer la lista de campos
                        for (int x = 0; x <= dr1.FieldCount - 1; x++)
                        {
                            nCampos.Add(dr1.GetName(x).ToLower());
                        }

                        // Formatear el objeto json con nombre de campos
                        do
                        {
                            var obj = new JObject();
                            //obj["leaf"] = true;
                            //obj["srv"] = ini;
                            for (int x = 0; x <= dr1.FieldCount - 1; x++)
                            {
                                string fieldType = dr1.GetDataTypeName(x);
                                if (fieldType == "date" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy");
                                }
                                else
                                    if (fieldType == "datetime" && Convert.ToString(dr1.GetValue(x)) != "")
                                    {
                                        DateTime fecha = dr1.GetDateTime(x);
                                        //String soloFecha =
                                        obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                    else
                                    {
                                        obj[nCampos[x]] = Convert.ToString(dr1.GetValue(x));
                                    }
                            }

                            jr.Add(obj);
                            rs.Add(obj);
                            re["data"] = jr;

                        } while (dr1.Read());
                        dr1.Close();
                    }
                    re["success"] = "true";
                    return re;
                    dr1.Close();
                    conn.Close();
                }
                catch (Exception x)
                {
                    re["Error"] = x.Message;
                    return re;

                    conn.Close();
                }

            }
            else
            {
                re["Error"] = "No se recibieron parametros para consulta";
                return re;
            }
        }

        public JObject getagentescap(string proc, string idtema, string idunegocio)
        //public JsonResult json()
        {
            // Obtener el objeto JSON generico
            //String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();
            var re = new JObject();

            if (idtema != "")
            {
                // Deserialize it to a dictionary
                //var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(jsonString);

                // Arreglos y listas
                List<string> nCampos = new List<string>();
                var registro = new JObject();
                string ls = "";
                List<JObject> rs = new List<JObject>();

                var jr = new JArray();

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // declare command

                    SqlCommand cmd = new SqlCommand(proc, conn);
                    //cmd.CommandType = CommandType.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("IDTEMA", SqlDbType.Int);
                    cmd.Parameters["IDTEMA"].Value = Convert.ToInt32(idtema);
                    cmd.Parameters.Add("IDUNEGOCIO", SqlDbType.Int);
                    cmd.Parameters["IDUNEGOCIO"].Value = Convert.ToInt32(idunegocio);
                    SqlDataReader dr1 = cmd.ExecuteReader();

                    var estructura = new JObject();
                    if (dr1.Read())
                    {
                        // Extraer la lista de campos
                        for (int x = 0; x <= dr1.FieldCount - 1; x++)
                        {
                            nCampos.Add(dr1.GetName(x).ToLower());
                        }

                        // Formatear el objeto json con nombre de campos
                        do
                        {
                            var obj = new JObject();
                            //obj["leaf"] = true;
                            //obj["srv"] = ini;
                            for (int x = 0; x <= dr1.FieldCount - 1; x++)
                            {
                                string fieldType = dr1.GetDataTypeName(x);
                                //obj["campo"+x] = fieldType;

                                if (fieldType == "date" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy");
                                }
                                else
                                    if (fieldType == "datetime" && Convert.ToString(dr1.GetValue(x)) != "")
                                    {
                                        DateTime fecha = dr1.GetDateTime(x);
                                        //String soloFecha =
                                        obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                    else
                                    {
                                        obj[nCampos[x]] = Convert.ToString(dr1.GetValue(x));
                                    }
                            }

                            jr.Add(obj);
                            rs.Add(obj);
                            re["data"] = jr;

                        } while (dr1.Read());
                        dr1.Close();
                    }
                    re["success"] = "true";
                    return re;
                    conn.Close();
                }
                catch (Exception x)
                {
                    re["Error"] = x.Message;
                    return re;

                    conn.Close();
                }

            }
            else
            {
                re["Error"] = "No se recibieron parametros para consulta";
                return re;
            }
        }

        public JObject getagentescap2(string proc, string idperiodo, string idunegocio)
        //public JsonResult json()
        {
            // Obtener el objeto JSON generico
            //String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();
            var re = new JObject();

            if (idperiodo != "")
            {
                // Deserialize it to a dictionary
                //var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, dynamic>>(jsonString);

                // Arreglos y listas
                List<string> nCampos = new List<string>();
                var registro = new JObject();
                string ls = "";
                List<JObject> rs = new List<JObject>();

                var jr = new JArray();

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                try
                {
                    conn.Open();
                    // declare command

                    SqlCommand cmd = new SqlCommand(proc, conn);
                    //cmd.CommandType = CommandType.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("IDPERIODO", SqlDbType.Int);
                    cmd.Parameters["IDPERIODO"].Value = Convert.ToInt32(idperiodo);
                    cmd.Parameters.Add("IDUNEGOCIO", SqlDbType.Int);
                    cmd.Parameters["IDUNEGOCIO"].Value = Convert.ToInt32(idunegocio);
                    SqlDataReader dr1 = cmd.ExecuteReader();

                    var estructura = new JObject();
                    if (dr1.Read())
                    {
                        // Extraer la lista de campos
                        for (int x = 0; x <= dr1.FieldCount - 1; x++)
                        {
                            nCampos.Add(dr1.GetName(x).ToLower());
                        }

                        // Formatear el objeto json con nombre de campos
                        do
                        {
                            var obj = new JObject();
                            //obj["leaf"] = true;
                            //obj["srv"] = ini;
                            for (int x = 0; x <= dr1.FieldCount - 1; x++)
                            {
                                string fieldType = dr1.GetDataTypeName(x);
                                //obj["campo"+x] = fieldType;

                                if (fieldType == "date" && Convert.ToString(dr1.GetValue(x)) != "")
                                {
                                    DateTime fecha = dr1.GetDateTime(x);
                                    //String soloFecha =
                                    obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy");
                                }
                                else
                                    if (fieldType == "datetime" && Convert.ToString(dr1.GetValue(x)) != "")
                                    {
                                        DateTime fecha = dr1.GetDateTime(x);
                                        //String soloFecha =
                                        obj[nCampos[x]] = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                    else
                                    {
                                        obj[nCampos[x]] = Convert.ToString(dr1.GetValue(x));
                                    }
                            }

                            jr.Add(obj);
                            rs.Add(obj);
                            re["data"] = jr;

                        } while (dr1.Read());
                        dr1.Close();
                    }
                    re["success"] = "true";
                    return re;
                    conn.Close();
                }
                catch (Exception x)
                {
                    re["Error"] = x.Message;
                    return re;

                    conn.Close();
                }

            }
            else
            {
                re["Error"] = "No se recibieron parametros para consulta";
                return re;
            }
        }

        public JObject post()
        {
            // Obtener los datos recibidos como string
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            // Deserialize it to a dictionary
            var jsonObject = JObject.Parse(jsonString);

            //var json2 = Newtonsoft.Json.JsonConvert.DeserializeObject<

            // Arreglos y listas
            List<string> nCampos = new List<string>();
            List<JObject> rs = new List<JObject>();

            var jr = new JArray();
            var re = new JObject();

            // Los parametros de la consulta vienen en el objeto (entidad, where, order)
            string tabla;
            string sql;
            string sql2;
            var estructura = new JObject();

            tabla = Convert.ToString(jsonObject["tabla"]); tabla = tabla.ToUpper();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                // declare command
                sql = "SELECT c.name 'Column Name', t.Name 'Data type', c.max_length 'Max Length', c.precision, c.scale, c.is_nullable, " +
                    "ISNULL(i.is_primary_key, 0) 'Primary Key' FROM sys.columns c INNER JOIN sys.types t ON c.user_type_id = t.user_type_id " +
                    "LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id LEFT OUTER JOIN " +
                    "sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + tabla + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                etapa = "1";
                SqlDataReader dr1 = cmd.ExecuteReader();
                
                if (dr1.Read())
                {
                    string campo;
                    string tipo;

                    do
                    {
                        campo = Convert.ToString(dr1.GetValue(0)); campo = campo.ToLower();
                        tipo = Convert.ToString(dr1.GetValue(1));

                        estructura[campo] = tipo;
                    } while (dr1.Read());
                }
                dr1.Close();

                // Obtener la PK
                // declare command
                sql2 = "SELECT c.name 'Column Name', ISNULL(i.is_primary_key, 0) 'Primary Key' FROM sys.columns c INNER JOIN sys.types t " +
                    "ON c.user_type_id = t.user_type_id LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id " +
                    "LEFT OUTER JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + tabla +"') and " +
                    "i.is_primary_key = 1";
                //SqlCommand cmd1 = new SqlCommand(sql, conn);
                SqlCommand query1 = new SqlCommand(sql2, conn);                
                query1.CommandType = CommandType.Text;
                etapa = "2";
                SqlDataReader dr2 = query1.ExecuteReader();
                //jsonObject["sql1"] = sql2;

                string pknombre = "";
                if (dr2.Read())
                {
                    pknombre = Convert.ToString(dr2.GetValue(0));
                    pknombre = pknombre.ToLower();

                    jsonObject["pknombre"] = pknombre;
                }
                dr2.Close();

                // Separar el registro recibido dentro del JSON
                //registro = jsonObject["registro"] as JObject;
                string q3 = "";
                string qCampos = "";
                string qValores = "";
                string separador = "";
                string pkvalor = "";
                String s = "";

                // Verificar si viene un valor en el campo PK, si no hay valor es insert, si hay valor es update
                if (Convert.ToString(jsonObject[pknombre]) == "" || Convert.ToString(jsonObject[pknombre]) == "0")
                {
                    // Se omite la pare de generar valor para la PK
                    // Generar un valor de PK para el siguiente registro
                    query1.CommandText = "SELECT IDENT_CURRENT('"+tabla+"') + 1";
                    etapa = "3";
                    dr1 = query1.ExecuteReader();
                    if (dr1.Read())
                    {
                        pkvalor = Convert.ToString(dr1.GetValue(0));
                        
                    }
                    dr1.Close();

                    // Almacen el valor de la nueva PK en el registro
                    // No se porque lo quite !!!???
                    //jsonObject[pknombre] = pkvalor;

                    // Recorrer el registro para formar la sentencia Insert
                    foreach (JToken token in jsonObject.Children())
                    {
                        if (token is JProperty)
                        {
                            var prop = token as JProperty; // Cada token me da : prop.Name | prop.Value
                            //qCampos = qCampos + separador + prop.Name;

                            if (Convert.ToString(prop.Value) != "" && Convert.ToString(estructura[prop.Name]) != "")
                            {
                                switch (Convert.ToString(estructura[prop.Name]))
                                {
                                    case "int":
                                        qValores = qValores + separador + prop.Value;
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "bit":
                                        int valor = 0;
                                        if(Convert.ToString(prop.Value) == "True"){ valor = 1;}
                                        qValores = qValores + separador + Convert.ToString(valor);
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "varchar":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "char":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "nvarchar":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "text":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "nchar":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "datetime":
                                        qValores = qValores + separador + "CONVERT(DATETIME, '" + prop.Value + "')";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "date":
                                        qValores = qValores + separador + "CONVERT(DATETIME, '" + prop.Value + "')";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "time":
                                        qValores = qValores + separador + "CONVERT(DATETIME, '" + prop.Value + "')";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    default:
                                        qValores = qValores + separador + prop.Value;
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                }

                                separador = ",";
                            }
                        }
                    }
                    sql = "INSERT INTO " + tabla + " (" + qCampos + ") VALUES (" + qValores + ")";
                    //jsonObject[pknombre] = pkvalor;
                    this.sql1 = sql;
                }
                else
                {
                    // Recorrer el registro para formar la sentencia update
                    foreach (JToken token in jsonObject.Children())
                    {
                        if (token is JProperty)
                        {
                            var prop = token as JProperty; // Cada token me da : prop.Name | prop.Value
                            if (prop.Name != pknombre) //pknombre)
                            {
                                if (Convert.ToString(prop.Value) != "" && Convert.ToString(estructura[prop.Name]) != "")
                                {
                                    switch (Convert.ToString(estructura[prop.Name]))
                                    {
                                        case "int":
                                            qValores = qValores + separador + prop.Name + "=" + prop.Value;
                                            break;
                                        case "bit":
                                            int valor = 0;
                                            if(Convert.ToString(prop.Value) == "True"){ valor = 1;}
                                            qValores = qValores + separador + prop.Name + "=" + Convert.ToString(valor);
                                            break;
                                        case "varchar":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "char":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "nvarchar":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "text":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "nchar":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "datetime":
                                            qValores = qValores + separador + prop.Name + "=" + " CONVERT(DATETIME, '" + prop.Value + "')";
                                            break;
                                        case "date":
                                            qValores = qValores + separador + prop.Name + "=" + " CONVERT(DATETIME, '" + prop.Value + "')";
                                            break;
                                        case "time":
                                            qValores = qValores + separador + prop.Name + "=" + " CONVERT(DATETIME, '" + prop.Value + "')";
                                            break;
                                        default:
                                            qValores = qValores + separador + prop.Name + "=" + prop.Value;                                            
                                            break;
                                    }
                                } else
                                {
                                    // Verificar que el campo pertenezca a la tabla
                                    if(Convert.ToString(estructura[prop.Name]) != "") qValores = qValores + separador + prop.Name + "= null";
                                }
                                if (qValores != "") separador = ",";
                            }
                            else
                            {
                                if(qValores == "") separador = "";
                            }

                            
                        }
                    }
                    sql = "UPDATE " + tabla + " SET " + qValores + " WHERE " + pknombre + " = " + Convert.ToString(jsonObject[pknombre]);
                    this.sql1 = sql;
                }

                // Cambiar el formato de fecha               
                etapa = "4";
                query1.CommandText = "SET DATEFORMAT DMY;";
                query1.ExecuteNonQuery();                

                // Procesar el SQL generado
                if (sql != "")
                {
                    query1.CommandText = sql;
                    etapa = "5";
                    query1.ExecuteNonQuery();
                    query1.Dispose();
                }

                dr1.Close();
                
                conn.Close();

                jsonObject["sql"] = sql;
                jsonObject["success"] = "true";
                return jsonObject;
                //return estructura;

            }

            catch (Exception x)
            {
                re["Error"] = x.Message;
                re["sql"] = this.sql1;
                re["etapa"] = this.etapa;

                return re;   
            }
            finally
            {
                
                conn.Close();
            }
        }

        // Guardar y regresar el registro con el ID Nuevo
        public JObject postID()
        {
            // Obtener los datos recibidos como string
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            // Deserialize it to a dictionary
            var jsonObject = JObject.Parse(jsonString);

            //var json2 = Newtonsoft.Json.JsonConvert.DeserializeObject<

            // Arreglos y listas
            List<string> nCampos = new List<string>();
            List<JObject> rs = new List<JObject>();

            var jr = new JArray();
            var re = new JObject();

            // Los parametros de la consulta vienen en el objeto (entidad, where, order)
            string tabla;
            string sql;
            string sql2;
            var estructura = new JObject();

            tabla = Convert.ToString(jsonObject["tabla"]); tabla = tabla.ToUpper();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                // declare command
                sql = "SELECT c.name 'Column Name', t.Name 'Data type', c.max_length 'Max Length', c.precision, c.scale, c.is_nullable, " +
                    "ISNULL(i.is_primary_key, 0) 'Primary Key' FROM sys.columns c INNER JOIN sys.types t ON c.user_type_id = t.user_type_id " +
                    "LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id LEFT OUTER JOIN " +
                    "sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + tabla + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                etapa = "1";
                SqlDataReader dr1 = cmd.ExecuteReader();

                if (dr1.Read())
                {
                    string campo;
                    string tipo;

                    do
                    {
                        campo = Convert.ToString(dr1.GetValue(0)); campo = campo.ToLower();
                        tipo = Convert.ToString(dr1.GetValue(1));

                        estructura[campo] = tipo;
                    } while (dr1.Read());
                }
                dr1.Close();

                // Obtener la PK
                // declare command
                sql2 = "SELECT c.name 'Column Name', ISNULL(i.is_primary_key, 0) 'Primary Key' FROM sys.columns c INNER JOIN sys.types t " +
                    "ON c.user_type_id = t.user_type_id LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id " +
                    "LEFT OUTER JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + tabla + "') and " +
                    "i.is_primary_key = 1";
                //SqlCommand cmd1 = new SqlCommand(sql, conn);
                SqlCommand query1 = new SqlCommand(sql2, conn);
                query1.CommandType = CommandType.Text;
                etapa = "2";
                SqlDataReader dr2 = query1.ExecuteReader();
                //jsonObject["sql1"] = sql2;

                string pknombre = "";
                if (dr2.Read())
                {
                    pknombre = Convert.ToString(dr2.GetValue(0));
                    pknombre = pknombre.ToLower();

                    jsonObject["pknombre"] = pknombre;
                }
                dr2.Close();

                // Separar el registro recibido dentro del JSON
                //registro = jsonObject["registro"] as JObject;
                string q3 = "";
                string qCampos = "";
                string qValores = "";
                string separador = "";
                string pkvalor = "";
                String s = "";

                // Verificar si viene un valor en el campo PK, si no hay valor es insert, si hay valor es update
                if (Convert.ToString(jsonObject[pknombre]) == "" || Convert.ToString(jsonObject[pknombre]) == "0")
                {
                    // Se omite la pare de generar valor para la PK
                    // Generar un valor de PK para el siguiente registro
                    query1.CommandText = "SELECT IDENT_CURRENT('" + tabla + "') + 1";
                    etapa = "3";
                    dr1 = query1.ExecuteReader();
                    if (dr1.Read())
                    {
                        pkvalor = Convert.ToString(dr1.GetValue(0));

                    }
                    dr1.Close();

                    // Recorrer el registro para formar la sentencia Insert
                    foreach (JToken token in jsonObject.Children())
                    {
                        if (token is JProperty)
                        {
                            var prop = token as JProperty; // Cada token me da : prop.Name | prop.Value
                            //qCampos = qCampos + separador + prop.Name;

                            if (Convert.ToString(prop.Value) != "" && Convert.ToString(estructura[prop.Name]) != "")
                            {
                                switch (Convert.ToString(estructura[prop.Name]))
                                {
                                    case "int":
                                        qValores = qValores + separador + prop.Value;
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "bit":
                                        int valor = 0;
                                        if (Convert.ToString(prop.Value) == "True") { valor = 1; }
                                        qValores = qValores + separador + Convert.ToString(valor);
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "varchar":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "char":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "nvarchar":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "text":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "nchar":
                                        s = Convert.ToString(prop.Value);
                                        s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "datetime":
                                        qValores = qValores + separador + "CONVERT(DATETIME, '" + prop.Value + "')";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "date":
                                        qValores = qValores + separador + "CONVERT(DATETIME, '" + prop.Value + "')";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "time":
                                        qValores = qValores + separador + "CONVERT(DATETIME, '" + prop.Value + "')";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    default:
                                        qValores = qValores + separador + prop.Value;
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                }

                                separador = ",";
                            }
                        }
                    }
                    sql = "INSERT INTO " + tabla + " (" + qCampos + ") VALUES (" + qValores + ")";
                    //jsonObject[pknombre] = pkvalor;
                    // Almacen el valor de la nueva PK en el registro
                    jsonObject[pknombre] = pkvalor;

                    this.sql1 = sql;
                }
                else
                {
                    // Recorrer el registro para formar la sentencia update
                    foreach (JToken token in jsonObject.Children())
                    {
                        if (token is JProperty)
                        {
                            var prop = token as JProperty; // Cada token me da : prop.Name | prop.Value
                            if (prop.Name != pknombre) //pknombre)
                            {
                                if (Convert.ToString(prop.Value) != "" && Convert.ToString(estructura[prop.Name]) != "")
                                {
                                    switch (Convert.ToString(estructura[prop.Name]))
                                    {
                                        case "int":
                                            qValores = qValores + separador + prop.Name + "=" + prop.Value;
                                            break;
                                        case "bit":
                                            int valor = 0;
                                            if (Convert.ToString(prop.Value) == "True") { valor = 1; }
                                            qValores = qValores + separador + prop.Name + "=" + Convert.ToString(valor);
                                            break;
                                        case "varchar":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "char":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "nvarchar":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "text":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "nchar":
                                            s = Convert.ToString(prop.Value);
                                            s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "datetime":
                                            qValores = qValores + separador + prop.Name + "=" + " CONVERT(DATETIME, '" + prop.Value + "')";
                                            break;
                                        case "date":
                                            qValores = qValores + separador + prop.Name + "=" + " CONVERT(DATETIME, '" + prop.Value + "')";
                                            break;
                                        case "time":
                                            qValores = qValores + separador + prop.Name + "=" + " CONVERT(DATETIME, '" + prop.Value + "')";
                                            break;
                                        default:
                                            qValores = qValores + separador + prop.Name + "=" + prop.Value;
                                            break;
                                    }
                                }
                                if (qValores != "") separador = ",";
                            }
                            else
                            {
                                if (qValores == "") separador = "";
                            }


                        }
                    }
                    sql = "UPDATE " + tabla + " SET " + qValores + " WHERE " + pknombre + " = " + Convert.ToString(jsonObject[pknombre]);
                    this.sql1 = sql;
                }

                // Cambiar el formato de fecha               
                etapa = "4";
                query1.CommandText = "SET DATEFORMAT DMY;";
                query1.ExecuteNonQuery();

                // Procesar el SQL generado
                if (sql != "")
                {
                    query1.CommandText = sql;
                    etapa = "5";
                    query1.ExecuteNonQuery();
                    query1.Dispose();
                }

                dr1.Close();

                conn.Close();

                jsonObject["sql"] = sql;
                jsonObject["success"] = "true";
                return jsonObject;
                //return estructura;

            }

            catch (Exception x)
            {
                re["Error"] = x.Message;
                re["sql"] = this.sql1;
                re["etapa"] = this.etapa;

                return re;
            }
            finally
            {

                conn.Close();
            }
        }

        public JObject postL()
        {
            // Obtener los datos recibidos como string
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            // Deserialize it to a dictionary
            var jsonObject = JObject.Parse(jsonString);

            //var json2 = Newtonsoft.Json.JsonConvert.DeserializeObject<

            // Arreglos y listas
            List<string> nCampos = new List<string>();
            List<JObject> rs = new List<JObject>();

            var jr = new JArray();
            var re = new JObject();

            // Los parametros de la consulta vienen en el objeto (entidad, where, order)
            string tabla;
            string sql;
            string sql2;
            var estructura = new JObject();

            tabla = Convert.ToString(jsonObject["tabla"]); tabla = tabla.ToUpper();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                // declare command
                sql = "SELECT c.name 'Column Name', t.Name 'Data type', c.max_length 'Max Length', c.precision, c.scale, c.is_nullable, " +
                    "ISNULL(i.is_primary_key, 0) 'Primary Key' FROM sys.columns c INNER JOIN sys.types t ON c.user_type_id = t.user_type_id " +
                    "LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id LEFT OUTER JOIN " +
                    "sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + tabla + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                etapa = "1";
                SqlDataReader dr1 = cmd.ExecuteReader();

                if (dr1.Read())
                {
                    string campo;
                    string tipo;

                    do
                    {
                        campo = Convert.ToString(dr1.GetValue(0)); campo = campo.ToLower();
                        tipo = Convert.ToString(dr1.GetValue(1));

                        estructura[campo] = tipo;
                    } while (dr1.Read());
                }
                dr1.Close();

                // Obtener la PK
                // declare command
                sql2 = "SELECT c.name 'Column Name', ISNULL(i.is_primary_key, 0) 'Primary Key' FROM sys.columns c INNER JOIN sys.types t " +
                    "ON c.user_type_id = t.user_type_id LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id " +
                    "LEFT OUTER JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + tabla + "') and " +
                    "i.is_primary_key = 1";
                //SqlCommand cmd1 = new SqlCommand(sql, conn);
                SqlCommand query1 = new SqlCommand(sql2, conn);
                query1.CommandType = CommandType.Text;
                etapa = "2";
                SqlDataReader dr2 = query1.ExecuteReader();
                //jsonObject["sql1"] = sql2;

                string pknombre = "";
                if (dr2.Read())
                {
                    pknombre = Convert.ToString(dr2.GetValue(0));
                    pknombre = pknombre.ToLower();

                    jsonObject["pknombre"] = pknombre;
                }
                dr2.Close();

                // Separar el registro recibido dentro del JSON
                //registro = jsonObject["registro"] as JObject;
                string q3 = "";
                string qCampos = "";
                string qValores = "";
                string separador = "";
                string pkvalor = "";
                String s = "";

                // Verificar si viene un valor en el campo PK, si no hay valor es insert, si hay valor es update
                if (Convert.ToString(jsonObject[pknombre]) == "" || Convert.ToString(jsonObject[pknombre]) == "0")
                {
                    // Se omite la pare de generar valor para la PK
                    // Generar un valor de PK para el siguiente registro
                    query1.CommandText = "SELECT IDENT_CURRENT('" + tabla + "') + 1";
                    etapa = "3";
                    dr1 = query1.ExecuteReader();
                    if (dr1.Read())
                    {
                        pkvalor = Convert.ToString(dr1.GetValue(0));

                    }
                    dr1.Close();

                    // Almacen el valor de la nueva PK en el registro
                    //jsonObject[pknombre] = pkvalor;

                    // Recorrer el registro para formar la sentencia Insert
                    foreach (JToken token in jsonObject.Children())
                    {
                        if (token is JProperty)
                        {
                            var prop = token as JProperty; // Cada token me da : prop.Name | prop.Value
                            //qCampos = qCampos + separador + prop.Name;

                            if (Convert.ToString(prop.Value) != "" && Convert.ToString(estructura[prop.Name]) != "")
                            {
                                switch (Convert.ToString(estructura[prop.Name]))
                                {
                                    case "int":
                                        qValores = qValores + separador + prop.Value;
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "bit":
                                        int valor = 0;
                                        if (Convert.ToString(prop.Value) == "True") { valor = 1; }
                                        qValores = qValores + separador + Convert.ToString(valor);
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "varchar":
                                        s = Convert.ToString(prop.Value);
                                        //s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "char":
                                        s = Convert.ToString(prop.Value);
                                        //s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "nvarchar":
                                        s = Convert.ToString(prop.Value);
                                        //s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "text":
                                        s = Convert.ToString(prop.Value);
                                        //s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "nchar":
                                        s = Convert.ToString(prop.Value);
                                        //s = s.ToUpper();
                                        qValores = qValores + separador + "'" + s + "'";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "datetime":
                                        qValores = qValores + separador + "CONVERT(DATETIME, '" + prop.Value + "')";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "date":
                                        qValores = qValores + separador + "CONVERT(DATETIME, '" + prop.Value + "')";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    case "time":
                                        qValores = qValores + separador + "CONVERT(DATETIME, '" + prop.Value + "')";
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                    default:
                                        qValores = qValores + separador + prop.Value;
                                        qCampos = qCampos + separador + prop.Name;
                                        break;
                                }

                                separador = ",";
                            }
                        }
                    }
                    sql = "INSERT INTO " + tabla + " (" + qCampos + ") VALUES (" + qValores + ")";
                    //jsonObject[pknombre] = pkvalor;
                    this.sql1 = sql;
                }
                else
                {
                    // Recorrer el registro para formar la sentencia update
                    foreach (JToken token in jsonObject.Children())
                    {
                        if (token is JProperty)
                        {
                            var prop = token as JProperty; // Cada token me da : prop.Name | prop.Value
                            if (prop.Name != pknombre) //pknombre)
                            {
                                if (Convert.ToString(prop.Value) != "" && Convert.ToString(estructura[prop.Name]) != "")
                                {
                                    switch (Convert.ToString(estructura[prop.Name]))
                                    {
                                        case "int":
                                            qValores = qValores + separador + prop.Name + "=" + prop.Value;
                                            break;
                                        case "bit":
                                            int valor = 0;
                                            if (Convert.ToString(prop.Value) == "True") { valor = 1; }
                                            qValores = qValores + separador + prop.Name + "=" + Convert.ToString(valor);
                                            break;
                                        case "varchar":
                                            s = Convert.ToString(prop.Value);
                                            //s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "char":
                                            s = Convert.ToString(prop.Value);
                                            //s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "nvarchar":
                                            s = Convert.ToString(prop.Value);
                                            //s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "text":
                                            s = Convert.ToString(prop.Value);
                                            //s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "nchar":
                                            s = Convert.ToString(prop.Value);
                                            //s = s.ToUpper();
                                            qValores = qValores + separador + prop.Name + "=" + "'" + s + "'";
                                            break;
                                        case "datetime":
                                            qValores = qValores + separador + prop.Name + "=" + " CONVERT(DATETIME, '" + prop.Value + "')";
                                            break;
                                        case "date":
                                            qValores = qValores + separador + prop.Name + "=" + " CONVERT(DATETIME, '" + prop.Value + "')";
                                            break;
                                        case "time":
                                            qValores = qValores + separador + prop.Name + "=" + " CONVERT(DATETIME, '" + prop.Value + "')";
                                            break;
                                        default:
                                            qValores = qValores + separador + prop.Name + "=" + prop.Value;
                                            break;
                                    }
                                }
                                if (qValores != "") separador = ",";
                            }
                            else
                            {
                                if (qValores == "") separador = "";
                            }


                        }
                    }
                    sql = "UPDATE " + tabla + " SET " + qValores + " WHERE " + pknombre + " = " + Convert.ToString(jsonObject[pknombre]);
                    this.sql1 = sql;
                }

                // Cambiar el formato de fecha               
                etapa = "4";
                query1.CommandText = "SET DATEFORMAT dmy;";
                query1.ExecuteNonQuery();

                // Procesar el SQL generado
                if (sql != "")
                {
                    query1.CommandText = sql;
                    etapa = "5";
                    query1.ExecuteNonQuery();
                    query1.Dispose();
                }

                dr1.Close();

                conn.Close();

                jsonObject["sql"] = sql;
                jsonObject["success"] = "true";
                return jsonObject;
                //return estructura;

            }

            catch (Exception x)
            {
                re["Error"] = x.Message;
                re["sql"] = this.sql1;
                re["etapa"] = this.etapa;

                return re;
            }
            finally
            {

                conn.Close();
            }
        }
        public JObject delete()
        //public JsonResult json()
        {
            // Obtener el objeto JSON generico
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            // Convertir a objecto JObject
            var jsonObject = JObject.Parse(jsonString);

            // Arreglos y listas
            List<string> nCampos = new List<string>();
            var registro = new JObject();
            string ls = "";
            List<JObject> rs = new List<JObject>();

            var jr = new JArray();
            var re = new JObject();

            // Los parametros de la consulta vienen en el objeto (entidad, where, order)
            string tabla;
            string where;
            //string order;

            tabla = Convert.ToString(jsonObject["e"]); tabla = tabla.ToUpper();
            where = Convert.ToString(jsonObject["w"]); where = where.ToUpper();

            string sw = "";
            string so = "";
            string sql = "";
            if (where != null && where != "")
            {
                sw = " WHERE " + where;
            }
            
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                // declare command
                sql = "delete from " + tabla + sw;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                jsonObject["sql"] = sql;
                jsonObject["success"] = "true";
                return jsonObject;
            }

            catch (Exception x)
            {
                re["sql"] = sql;
                re["success"] = "false";
                re["Error"] = x.Message;
                return re;

                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }

        public JObject estructura(String tabla)
        {
            
            // Arreglos y listas
            List<string> nCampos = new List<string>();
            string registro = "";
            string ls = "";
            List<JObject> rs = new List<JObject>();

            var jr = new JArray();
            var re = new JObject();

            string sql;
            string sql2;
            var estructura = new JObject();

            tabla = tabla.ToUpper();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                conn.Open();
                // declare command
                sql = "SELECT c.name 'Column Name', t.Name 'Data type', c.max_length 'Max Length', c.precision, c.scale, c.is_nullable, " +
                    "ISNULL(i.is_primary_key, 0) 'Primary Key' FROM sys.columns c INNER JOIN sys.types t ON c.user_type_id = t.user_type_id " +
                    "LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id LEFT OUTER JOIN " +
                    "sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + tabla + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                var lista = new JArray();


                if (dr1.Read())
                {
                    string campo;
                    string tipo;

                    do
                    {
                        campo = Convert.ToString(dr1.GetValue(0)); campo = campo.ToLower();
                        tipo = Convert.ToString(dr1.GetValue(1));

                        var obj = new JObject();
                        obj["name"] = campo;
                        obj["type"] = tipo;

                        lista.Add(obj);
                    } while (dr1.Read());
                }
                dr1.Close();
                estructura["data"] = lista;

                conn.Close();
                return estructura;
            }

            catch (Exception x)
            {
                re["Error"] = x.Message;
                return re;

                conn.Close();
            }
            finally
            {
                
                conn.Close();
            }
        }

        public JObject postUPwd()
        {
            // Get json
            // Obtener los datos recibidos como string
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            // Deserialize it to a dictionary
            var jsonObject = JObject.Parse(jsonString);

            // Los parametros de la consulta vienen en el objeto (entidad, where, order)
            string id;
            string pwdOld;
            string pwdNew;
            
            id = Convert.ToString(jsonObject["usuario_id"]); 
            pwdOld = Convert.ToString(jsonObject["passwordold"]); 
            pwdNew = Convert.ToString(jsonObject["passwordnew"]); 

            // Arreglos y listas
            var re = new JObject();
            string sql;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                conn.Open();
                // declare command
                
                sql = "select * from usuario where usuario_id = " + id + " and usuario_password='" + SepiaTools.CreateSHAHash(pwdOld, SepiaTools.strSalt) + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                var lista = new JArray();

                if (dr1.Read())
                {
                    // Si coincide el usuaio y la contraseña, hacer el update
                    sql = "update usuario set usuario_password = '" + SepiaTools.CreateSHAHash(pwdNew, SepiaTools.strSalt) + "' where usuario_id = " + id;
                    SqlCommand cmd2 = new SqlCommand(sql, conn);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                    re["msg"] = "Contraseña actualizada";
                    re["success"] = true;
                }
                else
                {
                    re["msg"] = "La combinación de Usuario/Contraseña no es correcta";
                    re["success"] = false;
                }
                dr1.Close();
                conn.Close();
                
                return re;  
            }

            catch (Exception x)
            {
                re["success"] = false;
                re["msg"] = x.Message;
                return re;

                conn.Close();
            }
            finally
            {

                conn.Close();
            }
        }

        public JObject updateUPwd()
        {
            // Get json
            // Obtener los datos recibidos como string
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            // Deserialize it to a dictionary
            var jsonObject = JObject.Parse(jsonString);

            // Los parametros de la consulta vienen en el objeto (entidad, where, order)
            string id;
            string pwdNew2;
            string pwdNew;

            id = Convert.ToString(jsonObject["usuario_id"]);
            pwdNew2 = Convert.ToString(jsonObject["passwordnew2"]);
            pwdNew = Convert.ToString(jsonObject["passwordnew"]);

            // Arreglos y listas
            var re = new JObject();
            string sql;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                conn.Open();
                // Si coincide el usuaio y la contraseña, hacer el update
                sql = "update usuario set usuario_password = '" + SepiaTools.CreateSHAHash(pwdNew, SepiaTools.strSalt) + "' where usuario_id = " + id;
                SqlCommand cmd2 = new SqlCommand(sql, conn);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();
                re["msg"] = "Contraseña actualizada";
                re["success"] = true;
                
                conn.Close();

                return re;
            }

            catch (Exception x)
            {
                re["success"] = false;
                re["msg"] = x.Message;
                return re;

                conn.Close();
            }
            finally
            {

                conn.Close();
            }
        }

        // Process sql batch
        public JObject sqlBatch()
        {
            // Get json within string parameter
            String jsonString = new StreamReader(this.Request.InputStream).ReadToEnd();

            // Deserialize it to a dictionary
            var jsonObject = JObject.Parse(jsonString);

            // Arreglos y listas
            var re = new JObject();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                conn.Open();
                // declare command

                foreach (string sql in jsonObject["sentences"])
                {
                    // Si coincide el usuaio y la contraseña, hacer el update
                    SqlCommand cmd2 = new SqlCommand(sql, conn);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                    re["sql"] = sql;
                    
                }
                re["msg"] = "Batch processed";
                re["success"] = true;
                conn.Close();

                return re;
            }

            catch (Exception x)
            {
                re["success"] = false;
                re["msg"] = x.Message;
                return re;

                conn.Close();
            }
            finally
            {

                conn.Close();
            }
        }

    }
}