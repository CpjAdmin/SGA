using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using Dapper;
using SGA.Dao.Repositorio;
using SGA.Dominio;


namespace SGA.Dao.Dapper
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        #region Metodos Usuario

        //*** Validar Usuario ( Login )
        public int ValidarU(string Login, string Token, string Clave, string navegador)
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "M";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", Login);
                parametros.Add("@token", Token);
                parametros.Add("@clave", Clave);
                parametros.Add("@navegador", navegador);
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                using (bd)
                {
                    //*** Validaciones de usuario.
                    var resultado = bd.Execute("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);
                    int res = parametros.Get<int>("@result");
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** Buscar Usuario
        public Usuario Buscar(string Login, string Clave)
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "A";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", Login);
                parametros.Add("@clave", Clave);
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    return bd.QuerySingle<Usuario>("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** VALIDADO - GENERAL - Validar TOKEN del Usuario
        public Usuario BuscarT(string cadena)
        {
            try
            {
                string login;
                string token;

                if (cadena == "")
                {
                    return null;
                }
                login = cadena.Split(':')[0];
                token = cadena.Split(':')[1];

                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "V";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", login);
                parametros.Add("@token", token);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    return bd.QuerySingle<Usuario>("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** Insertar Usuario
        public Resultado InsertarU(Usuario objUsuario)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Usuario guardado correctamente." };
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "C";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", objUsuario.Login);
                parametros.Add("@nombre", objUsuario.Nombre);
                parametros.Add("@clave", objUsuario.Clave);
                parametros.Add("@i_estado", objUsuario.I_estado);
                parametros.Add("@usuario_ejecuta", objUsuario.User_modifica);
                parametros.Add("@terminal", objUsuario.Terminal_creacion);
                parametros.Add("@id_rol", objUsuario.Id_rol);
                parametros.Add("@telefono", objUsuario.Telefono);
                parametros.Add("@i_delegado", objUsuario.I_delegado);
                parametros.Add("@id_delegado", objUsuario.Id_delegado);
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar usuario: ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar usuario: " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** Modificar Usuario
        public Resultado ModificarU(Usuario objUsuario)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Usuario guardado correctamente." };
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "U";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", objUsuario.Login);
                parametros.Add("@nombre", objUsuario.Nombre);
                parametros.Add("@i_estado", objUsuario.I_estado);
                parametros.Add("@usuario_ejecuta", objUsuario.User_modifica);
                parametros.Add("@terminal", objUsuario.Terminal_modifica);
                parametros.Add("@id_rol", objUsuario.Id_rol);
                parametros.Add("@telefono", objUsuario.Telefono);
                parametros.Add("@i_delegado", objUsuario.I_delegado);
                parametros.Add("@id_delegado", objUsuario.Id_delegado);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar usuario: ";
                    };
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al modificar usuario: " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** Eliminar Usuario
        public Resultado EliminarU(Usuario objUsuario)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Usuario eliminado correctamente." };
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "D";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", objUsuario.Login);
                parametros.Add("@nombre", "ELIMINAR");
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al eliminar usuario";
                    };
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar usuario: " + ex.Message;
                return resultado;
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** Actualizar PIN de Usuario
        public bool ActualizaP(string Login, string Pin, string usuario, string terminal)
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "P";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", Login);
                parametros.Add("@clave", Pin);
                parametros.Add("@usuario_ejecuta", usuario);
                parametros.Add("@terminal", terminal);
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resultado = bd.Execute("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);

                    return resultado > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** Buscar Usuario por Login
        public Usuario BuscarL(string Login)
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "S";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", Login);
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    return bd.QuerySingle<Usuario>("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** Buscar Delegado con el Login de Usuario cuando este ya existe en usuarios
        public Usuario BuscarD(string Login)
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "W";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", Login);
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    return bd.QuerySingle<Usuario>("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        //*** Buscar Delegado con el Login de Usuario cuando este NO existe en usuarios
        public Usuario BuscarDI(string Login)
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "Y";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", Login);
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    return bd.QuerySingle<Usuario>("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        //*** Enviar PIN
        public Resultado EnviarPin(string telefono, string pin, string login, string usuario, string tipo)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "SMS Enviado con Éxito." };
            string usuarioSMS = ConfigurationManager.AppSettings["usuarioSMS"];
            string claveSMS = ConfigurationManager.AppSettings["claveSMS"];
            string mensajeSms = BuscarParam("MENSAJE_SMS").Descripcion;

            mensajeSms = string.Format(mensajeSms, pin);

            if (mensajeSms == null)
            {
                InsertarBitSMS(usuario, login, telefono, "400", "error", "NO SE PUDO OBTENER EL MENSAJE A ENVIAR", tipo);
                throw new Exception();
            }

            try
            {
                if (telefono == null || telefono == "")
                {
                    resultado.Error = -1;
                    resultado.Mensaje = "El usuario no tiene un número de telefono asignado";
                    InsertarBitSMS(usuario, login, telefono, "400", "error", resultado.Mensaje, tipo);
                    return resultado;
                }

                using (SmsService.WSMSGSoapClient servicio = new SmsService.WSMSGSoapClient())
                {
                    string EstadoServicio = servicio.Ping();
                    string paramEncrypted = EncryptQueryString("login=" + login + "&pin=" + pin);

                    if (EstadoServicio.ToUpper() != "OK")
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "El servicio de envío de SMS no está disponible";
                        InsertarBitSMS(usuario, login, telefono, "400", "error", resultado.Mensaje, tipo);
                        return resultado;
                    }

                    SmsService.Message mensaje = new SmsService.Message()
                    {
                        mobile = telefono,
                        countrycode = "506",
                        MessageMember = mensajeSms + paramEncrypted,
                        idreference = "0"
                    };

                    var respuesta = servicio.SendMessage(usuarioSMS, claveSMS, mensaje);

                    if (respuesta.status.ToUpper() != "SUCCESS")
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error en el envío de SMS. Descripción: " + respuesta.errordes;
                        InsertarBitSMS(usuario, login, telefono, "400", "error", resultado.Mensaje, tipo);
                        return resultado;
                    }
                }
                InsertarBitSMS(usuario, login, telefono, "200", "OK", resultado.Mensaje, tipo);
                return resultado;
            }
            catch (Exception ex)
            {
                InsertarBitSMS(usuario, login, telefono, "400", "error", ex.Message, tipo);
                return null;
                throw new Exception("EnviarPin", ex);
            }
        }

        //*** Enviar PIN
        public Resultado EnviarPinD(string telefono, string pin, string login, string nombre, string usuario, string tipo)
        {
            ParametrosGenerales parametro = new ParametrosGenerales();
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "SMS Enviado con Éxito." };
            string usuarioSMS = ConfigurationManager.AppSettings["usuarioSMS"];
            string claveSMS = ConfigurationManager.AppSettings["claveSMS"];
            //*** Obtener el Paramtro y 
            string nombreParametro = "MENSAJE_SMS_DISPOSITIVO";
            string mensajeSms;
            parametro = BuscarParam(nombreParametro);


            if (parametro != null)
            {
                mensajeSms = parametro.Descripcion;
                mensajeSms = string.Format(mensajeSms, nombre, login, pin);
            }
            else
            {
                InsertarBitSMS(usuario, login, telefono, "400", "error", "MENSAJE NO ENVIADO, NO SE PUDO OBTENER EL PARAMTERO: " + nombreParametro, tipo);
                throw new Exception("Mensaje no enviado, no se logro obtener el parametro: " + nombreParametro);
            }

            try
            {
                if (telefono == null || telefono == "")
                {
                    resultado.Error = -1;
                    resultado.Mensaje = "El dispoistivo no tiene un número de telefono asignado";
                    InsertarBitSMS(usuario, login, telefono, "400", "error", resultado.Mensaje, tipo);
                    return resultado;
                }

                using (SmsService.WSMSGSoapClient servicio = new SmsService.WSMSGSoapClient())
                {
                    string EstadoServicio = servicio.Ping();
                    string paramEncrypted = EncryptQueryString("login=" + login + "&pin=" + pin);

                    if (EstadoServicio.ToUpper() != "OK")
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "El servicio de envío de SMS no está disponible";
                        InsertarBitSMS(usuario, login, telefono, "400", "error", resultado.Mensaje, tipo);
                        return resultado;
                    }

                    SmsService.Message mensaje = new SmsService.Message()
                    {
                        mobile = telefono,
                        countrycode = "506",
                        MessageMember = mensajeSms + paramEncrypted,
                        idreference = "0"
                    };

                    var respuesta = servicio.SendMessage(usuarioSMS, claveSMS, mensaje);

                    if (respuesta.status.ToUpper() != "SUCCESS")
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error en el envío de SMS a dispositivo. Descripción: " + respuesta.errordes;
                        InsertarBitSMS(usuario, login, telefono, "400", "error", resultado.Mensaje, tipo);
                        return resultado;
                    }
                }
                InsertarBitSMS(usuario, login, telefono, "200", "OK", resultado.Mensaje, tipo);
                return resultado;
            }
            catch (Exception ex)
            {
                InsertarBitSMS(usuario, login, telefono, "400", "error", ex.Message, tipo);
                return null;
                throw new Exception("EnviarPinDispositivo", ex);
            }
        }

        //*** Listar Usuarios
        public List<Usuario> ListaUsuarios()
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "L";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    return bd.Query<Usuario>("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** Listar los Roles
        public List<Rol> ListaRoles()
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "X";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    return bd.Query<Rol>("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }

        //*** Enviar PIN Masivo por SMS
        public Resultado EnviarSMSMasivo(string usuario, int idrol, string i_delegado, string i_estado, string genera, string loginApi, string tokenApi)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Proceso ejecutado correctamente." };
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@usuario", usuario);
                parametros.Add("@idrol", idrol);
                parametros.Add("@i_delegado", i_delegado);
                parametros.Add("@i_estado", i_estado);
                parametros.Add("@genera", genera);
                parametros.Add("@loginApi", loginApi);
                parametros.Add("@tokenApi", tokenApi);

                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_envio_masivo_pin", parametros, commandType: CommandType.StoredProcedure, commandTimeout: 0);
                    int valorRetornado = parametros.Get<int>("@result");

                    if (valorRetornado >= 0)
                    {
                        resultado.Mensaje = "Proceso ejecutado correctamente. SMS enviados ( " + valorRetornado + " )";
                    }
                    else
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al ejecutar el proceso de envió masivo de sms ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al ejecutar el proceso de envió masivo de sms: " + ex.Message;
                return resultado;
            }
        }

        //*** Encriptar QueryString
        public string EncryptQueryString(string strQueryString)
        {
            EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
            return objEDQueryString.Encrypt(strQueryString, "a9y5sm2w");
        }

        //*** Buscar Parametro
        public ParametrosGenerales BuscarParam(string nomParam)
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                int cod_proceso = 3;
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@nombreParam", nomParam);
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    return bd.QuerySingleOrDefault<ParametrosGenerales>("sp_sga_auditoria_procesos", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("sp_sga_auditoria_procesos - " + nomParam, ex);
            }
        }

        //*** Insertar Bitacora de SMS
        public void InsertarBitSMS(string usuario, string login, string telefono, string status, string statustext, string responsetext, string tipo)
        {
            try
            {
                if (tipo == "M")
                {
                    return;
                }

                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@usuario", usuario);
                parametros.Add("@login", login);
                parametros.Add("@telefono", telefono);
                parametros.Add("@Status", status);
                parametros.Add("@statusText", statustext);
                parametros.Add("@responseText", responsetext);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_registra_bitacora", parametros, commandType: CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_registra_bitacora", ex);
            }
        }

        //*** @cod_proceso = 'Z' - Validado - GENERAL - Validar ROL del Usuario
        public int ObtenerRol(string cadena)
        {
            try
            {
                string login;
                if (cadena == "")
                {
                    return 0;
                }
                login = cadena.Split(':')[0];


                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "Z";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@login", login);
                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_usuarios", parametros, commandType: CommandType.StoredProcedure);
                    int res = parametros.Get<int>("@result");
                    return res;
                }
            }
            catch (Exception ex)
            {
                return -1;
                throw new Exception("sp_sga_crud_usuarios", ex);
            }
        }
       
        //*** @cod_proceso = 'R' - Validado: SGA.master - Cargar Menú de Usuario
        public List<Menu> CargarMenu(string login, string terminal)
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();

                string cod_proceso = "R";

                //*** Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@login", login);
                parametros.Add("@terminal", terminal);
                parametros.Add("@cod_proceso", cod_proceso);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    return bd.Query<Menu>("sp_sga_crud_menu", parametros, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_menu", ex);
            }
        }
        
        //*** @cod_proceso = 'V' - Validado: SGA.master - Validar Permisos por Página
        public Pagina ValidarPermisosPagina(Pagina objPagina)
        {
            try
            {
                if (objPagina.Usuario != "" && objPagina.Href != "")
                {

                    IDbConnection bd = ConexionRepositorio.ConexionSQL();

                    string cod_proceso = "V";
                    //Parametros
                    var parametros = new DynamicParameters();
                    parametros.Add("@cod_proceso", cod_proceso);
                    parametros.Add("@login", objPagina.Usuario);
                    parametros.Add("@terminal", objPagina.Terminal);
                    parametros.Add("@pagina", objPagina.Href);

                    using (bd)
                    {
                        //Retorna la lista de candidatos
                        return bd.QueryFirstOrDefault<Pagina>("sp_sga_crud_menu", parametros, commandType: CommandType.StoredProcedure);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_menu", ex);
            }
        }

        //*** PENDIENTE - CRUED DE PERMISOS POR ROL Y USUARIO

        #endregion
    }
}
