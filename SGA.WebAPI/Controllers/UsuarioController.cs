using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using SGA.Dao.Dapper;
using SGA.Dominio;
using System.Threading.Tasks;

namespace SGA.WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        [ActionName("ValidarUsuario")]
        [HttpPost]
        public HttpResponseMessage ValidarUsuario([FromBody] JObject data)
        {
            //*** Instancia de Usuario
            UsuarioRepositorio urn = new UsuarioRepositorio();
            //*** Generación de Token
            string cToken = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            var ObjUsuario = new Usuario();

            try
            {
                //*** Convertir el objeto DATA tipo JObject
                dynamic dataUsuario = data.ToObject<dynamic>();

                //*** Validar Usuario
                int respuesta = urn.ValidarU(dataUsuario.Login.ToString(), cToken, dataUsuario.Clave.ToString(), dataUsuario.Navegador.ToString());

                //*** Buscar el Usuario
                if (respuesta > 0)
                {
                    ObjUsuario = urn.Buscar(dataUsuario.Login.ToString(), dataUsuario.Clave.ToString());
                };

                //*** Guardar en Bitacora
                if (ObjUsuario.Id_usuario != 0)
                {
                    ProcesoLN.getInstancia().AuditarProceso(17, ObjUsuario.Login, "Login.aspx", "Acceso al sistema - Código: " + respuesta.ToString(), "C", System.Web.HttpContext.Current);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { respuesta, ObjUsuario });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("BuscarUsuario")]
        [HttpPost]
        public HttpResponseMessage BuscarUsuario([FromBody] Usuario objUsuario)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            try
            {
                var respuesta = urn.Buscar(objUsuario.Login, objUsuario.Clave);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            }
        }

        [ActionName("InsertarUsuario")]
        [HttpPost]
        public HttpResponseMessage InsertarUsuario([FromBody] Usuario objUsuario)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(12, objUsuario.User_modifica, "Usuarios.aspx", "Insertar usuario, acceso no autorizado ", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");

                }

                objUsuario.Clave = ProcesoLN.getInstancia().GetPin();
                objUsuario.Terminal_creacion = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = urn.InsertarU(objUsuario);
                ProcesoLN.getInstancia().AuditarProceso(12, objUsuario.User_modifica, "Usuarios.aspx", "Insertar usuario " + objUsuario.Login + ": " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(12, objUsuario.User_modifica, "Usuarios.aspx", "Insertar usuario " + objUsuario.Login + ": " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }


        [ActionName("ModificarUsuario")]
        [HttpPost]
        public HttpResponseMessage ModificarUsuario([FromBody] Usuario objUsuario)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(13, objUsuario.User_modifica, "Usuarios.aspx", "Modificar usuario, acceso no autorizado ", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }


                objUsuario.Terminal_modifica = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = urn.ModificarU(objUsuario);
                ProcesoLN.getInstancia().AuditarProceso(13, objUsuario.User_modifica, "Usuarios.aspx", "Modificar usuario " + objUsuario.Login + ": " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(13, objUsuario.User_modifica, "Usuarios.aspx", "Modificar usuario " + objUsuario.Login + ": " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("EliminarUsuario")]
        [HttpPost]
        public HttpResponseMessage EliminarUsuario([FromBody] Usuario objUsuario)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    ProcesoLN.getInstancia().AuditarProceso(14, objUsuario.User_modifica, "Usuarios.aspx", "Eliminar usuario, acceso no autorizado ", "E", System.Web.HttpContext.Current);
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }


                objUsuario.Terminal_modifica = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = urn.EliminarU(objUsuario);
                if (respuesta.Error == 0)
                {
                    ProcesoLN.getInstancia().AuditarProceso(14, objUsuario.User_modifica, "Usuarios.aspx", "Eliminar usuario " + objUsuario.Login + ": " + respuesta.Mensaje, "C", System.Web.HttpContext.Current);
                }
                else
                {
                    ProcesoLN.getInstancia().AuditarProceso(14, objUsuario.User_modifica, "Usuarios.aspx", "Eliminar usuario " + objUsuario.Login + ": " + respuesta.Mensaje, "E", System.Web.HttpContext.Current);

                }

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(14, objUsuario.User_modifica, "Usuarios.aspx", "Eliminar usuario " + objUsuario.Login + ": " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }
        
        //*** VERIFICADO - ENVÍO DE PIN INDIVIDUAL
        [ActionName("EnviarSms")]
        [HttpPost]
        public HttpResponseMessage EnviarSms([FromBody] SmsRequest objSms)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            Usuario USR = new Usuario();

            string pin = ProcesoLN.getInstancia().GetPin();
            string terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
            string cadena = null;
            string telefonoEnvio = "";
            string telefonoCambio = "";
            string mensajeTelefono = "";

            try
            {
                //*** Validar TOKEN
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                //*** Obtener Usuario por el LOGIN
                USR = urn.BuscarL(objSms.Login);

                if (USR == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "usuario no existe.");
                }
                if ((objSms.Indgenera == "S") && (!urn.ActualizaP(objSms.Login, pin, objSms.Usuario, terminal)))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se pudo generar el pin.");
                }

                //*** Verificar si Regenera el PIN
                USR.Clave = objSms.Indgenera == "S" ? pin : USR.Clave;

                //*** Verificar si se envía la teléfono Oficial o al Modificado
                telefonoEnvio = objSms.TelefonoAux == "" ? USR.Telefono : objSms.TelefonoAux;
                telefonoCambio = objSms.TelefonoAux == USR.Telefono ? "" : "*** CAMBIO ***";
                mensajeTelefono = "Envio de PIN ( Login: " + USR.Login + " - Tel: " + telefonoEnvio + " ) : ";

                var respuesta = urn.EnviarPin(telefonoEnvio, USR.Clave, USR.Login, objSms.Usuario, objSms.Tipo);

                ProcesoLN.getInstancia().AuditarProceso(15, objSms.Usuario, "Usuarios.aspx", mensajeTelefono + respuesta.Mensaje + telefonoCambio, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);

            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(15, objSms.Usuario, "Usuarios.aspx", mensajeTelefono + ex.Message + telefonoCambio, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        //*** VERIFICADO - ENVÍO DE PIN A DISPOSITIVO ALTERNO
        [ActionName("EnviarSmsD")]
        [HttpPost]
        public HttpResponseMessage EnviarSmsD([FromBody] JObject data)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            DispositivoRepositorio dr = new DispositivoRepositorio();


            Usuario USR = new Usuario();
            string pin = ProcesoLN.getInstancia().GetPin();
            string terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);
            string cadena = null;
            dynamic dataSms = data.ToObject<dynamic>();
            string indgenera = dataSms.Indgenera.ToString();
            string dispositivo = dataSms.Id_dispositivo.ToString();
            string mensajeTelefono = "";

            SmsRequest objSms = new SmsRequest
            {
                Login = dataSms.Login.ToString(),
                Indgenera = indgenera,
                Usuario = dataSms.Usuario.ToString(),
                Tipo = dataSms.Tipo.ToString()
            };

            string Telefono = dr.ObtenerTelefono(Int32.Parse(dispositivo));
           
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                USR = urn.BuscarL(objSms.Login);

                if (USR == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Usuario no existe.");
                }
                if ((objSms.Indgenera == "S") && (!urn.ActualizaP(objSms.Login, pin, objSms.Usuario, terminal)))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se pudo generar el pin.");
                }

                USR.Clave = objSms.Indgenera == "S" ? pin : USR.Clave;

                mensajeTelefono = "Envio de PIN ( Login: " + USR.Login + " - Dispositivo " + dispositivo + " : " + Telefono + " ) : ";


                //*** Envio de Pin
                var respuesta = urn.EnviarPinD(Telefono, USR.Clave, USR.Login, USR.Nombre, objSms.Usuario, objSms.Tipo);

                ProcesoLN.getInstancia().AuditarProceso(15, objSms.Usuario, "Usuarios.aspx", mensajeTelefono + respuesta.Mensaje + "*** DISPOSITIVO ***", respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(15, objSms.Usuario, "Usuarios.aspx", mensajeTelefono + ex.Message + "*** DISPOSITIVO ***", "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("ListadoUsuarios")]
        [HttpPost]
        public HttpResponseMessage ListadoUsuarios()
        {
            UsuarioRepositorio urn = new UsuarioRepositorio();
            try
            {

                var respuesta = urn.ListaUsuarios();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("BuscarDelegado")]
        [HttpPost]
        public HttpResponseMessage BuscarDelegado([FromBody] Usuario objUsuario)
        {
            UsuarioRepositorio urn = new UsuarioRepositorio();
            try
            {

                var respuesta = urn.BuscarD(objUsuario.Login);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("BuscarDelegadoI")]
        [HttpPost]
        public HttpResponseMessage BuscarDelegadoI([FromBody] Usuario objUsuario)
        {
            UsuarioRepositorio urn = new UsuarioRepositorio();
            try
            {

                var respuesta = urn.BuscarDI(objUsuario.Login);
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("ListadoRoles")]
        [HttpPost]
        public HttpResponseMessage ListadoRoles()
        {
            UsuarioRepositorio urn = new UsuarioRepositorio();
            try
            {
                var respuesta = urn.ListaRoles();
                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("EnvioSmsMasivo")]
        [HttpGet]
        public HttpResponseMessage EnvioSmsMasivo(string usuario, int idrol, string i_delegado, string i_estado, string genera)
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            string cadena = null;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                var respuesta = urn.EnviarSMSMasivo(usuario, idrol, i_delegado, i_estado, genera, cadena.Split(':')[0], cadena.Split(':')[1]);

                ProcesoLN.getInstancia().AuditarProceso(16, usuario, "Usuarios.aspx", "Enviar SMS masivo: " + respuesta.Mensaje, respuesta.Error == 0 ? "C" : "E", System.Web.HttpContext.Current);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                ProcesoLN.getInstancia().AuditarProceso(16, usuario, "Usuarios.aspx", "Enviar SMS masivo: " + ex.Message, "E", System.Web.HttpContext.Current);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        [ActionName("CargarArchivo")]
        [HttpPost]
        public async Task<HttpResponseMessage> CargarArchivo()
        {

            UsuarioRepositorio urn = new UsuarioRepositorio();
            string cadena = null;
            string localFileName;
            string serverFileName = "";
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/uploads");
            var provider = new MultipartFormDataStreamProvider(root);
            string mensaje = "";
            Usuario usuario;
            try
            {
                if (Request.Headers.TryGetValues("Auth", out IEnumerable<string> headerValues))
                {
                    cadena = headerValues.First();
                }
                if (urn.BuscarT(cadena) == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado a ejecutar este proceso, TOKEN invalido.");
                }

                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    localFileName = file.LocalFileName;
                    serverFileName = Path.Combine(root, DateTime.Now.ToString("ddMMyyyy") + random.Next(1000).ToString() + ".txt");
                    File.Move(localFileName, serverFileName);
                }

                if (serverFileName == "")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se cargó la ruta del archivo, favor intente nuevamente.");
                }

                string[] lines = System.IO.File.ReadAllLines(@serverFileName);
                int cont = 1;
                int inserts = 0;

                foreach (string line in lines)
                {
                    var splLine = line.Split('|');
                    if (splLine.Length != 7)
                    {
                        mensaje = mensaje + "- La línea " + cont.ToString() + " no cumple con las columnas requeridas <br>";
                    }
                    else
                    {
                        usuario = new Usuario
                        {
                            Login = splLine[0],
                            Nombre = splLine[1],
                            Telefono = splLine[2],
                            User_modifica = cadena.Split(':')[0],
                            Id_rol = Int32.Parse(splLine[3]),
                            I_estado = splLine[4].ToUpper(),
                            I_delegado = splLine[5].ToUpper(),
                            Id_delegado = (splLine[6] == null || splLine[6].Trim() == "") ? 0 : Int32.Parse(splLine[6]),
                            Clave = ProcesoLN.getInstancia().GetPin(),
                            Terminal_creacion = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current)
                        };

                        var respuesta = urn.InsertarU(usuario);
                        if (respuesta.Error != 0)
                        {
                            mensaje = mensaje + "- Login " + usuario.Login + " error: " + respuesta.Mensaje + "<br>";
                        }
                        else
                        {
                            ProcesoLN.getInstancia().AuditarProceso(31, usuario.User_modifica, "Usuarios.aspx", "Proceso masivo Insertar usuario " + usuario.Login + " ingresado correctamente.", "C", System.Web.HttpContext.Current);
                            inserts++;
                        }
                    }

                    cont++;
                }

                File.Delete(serverFileName);


                mensaje = mensaje + "<b>Se ingresaron " + inserts.ToString() + " registros.</b><br>";

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        //*** @cod_proceso = 'R' - Validado: SGA.master - Cargar Menú de Usuario
        [ActionName("CargarMenu")]
        [HttpPost]
        public HttpResponseMessage CargarMenu([FromBody] JObject data)
        {
            try
            {
                UsuarioRepositorio urn = new UsuarioRepositorio();

                //*** Validar Login   
                dynamic objMenu = data.ToObject<dynamic>();
                string login = objMenu.Usuario.ToString();

                //*** Validar Terminal   
                string terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                var respuesta = urn.CargarMenu(login, terminal);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }

        //*** @cod_proceso = 'V' - Validado: SGA.master - Validar Permisos por Página
        [ActionName("ValidarPermisosPagina")]
        [HttpPost]
        public HttpResponseMessage ValidarPermisosPagina([FromBody] JObject data)
        {
            try
            {
                UsuarioRepositorio urn = new UsuarioRepositorio();
                Pagina objPagina = new Pagina();

                //*** Validar Login   
                dynamic objMenu = data.ToObject<dynamic>();
                string login = objMenu.Usuario.ToString();
                string pagina = objMenu.Pagina.ToString();

                //*** Validar Terminal   
                string terminal = ProcesoLN.getInstancia().GetIPAddress(System.Web.HttpContext.Current);

                //*** Pagina
                objPagina.Href = pagina;
                objPagina.Usuario = login;
                objPagina.Terminal = terminal;

                var respuesta = urn.ValidarPermisosPagina(objPagina);

                return Request.CreateResponse(HttpStatusCode.OK, respuesta);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException == null ? ex.Message : ex.Message + " --> " + ex.InnerException.Message);
            };
        }
    }
}
