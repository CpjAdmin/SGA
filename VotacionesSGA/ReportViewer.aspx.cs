using Microsoft.Reporting.WebForms;
using SGA.WebAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VotacionesSGA
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                String usuarioRpt =  Request.QueryString["urpt"];

                //***PENDIENTE VALIDAR PERMISOS DE EJECUCIÓN DE REPORTES
                //if (!usuarioActivo.Equals(usuarioRpt))
                //{
                //    string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                //    string mensaje = "El usuario " + usuarioActivo + " no está autorizado para ejecutar este reporte!";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                //}
                //else
                //{
                    String nombreReporte = Request.QueryString["nrpt"];
                    String path = Request.QueryString["irpt"];
                    //Evento Click de ejecución del reporte
                    reporte_ServerClick(usuarioRpt, path, nombreReporte);
                //}
            }
        }

        //Cargar del Reporte
        protected void reporte_ServerClick(String usuarioRpt, String path, String nombreReporte)
        {
            try
            {
                string usuario = usuarioRpt;
                int id_proceso = 36;
                string descripcion = "Ejecución de Reporte: " + nombreReporte + " | Path : " + path;

                if (!String.IsNullOrEmpty(path))
                {
                    //Configurar Nombre del Reporte
                    //txtNombreReporte.Text = nombreReporte;

                    //*** ReportServer 
                    string ReportServer = ConfigurationManager.AppSettings["ReportServer"];

                    if (string.IsNullOrEmpty(ReportServer))
                    {
                        throw new Exception("Falta el ReportServer del archivo web.config");
                    }

                    ReportViewer1.ServerReport.ReportServerUrl = new Uri(ReportServer);
                    ReportViewer1.ServerReport.ReportPath = path;
                    ReportViewer1.ShowCredentialPrompts = false;
                    IReportServerCredentials credenciales = new CustomReportCredentials();
                    ReportViewer1.ServerReport.ReportServerCredentials = credenciales;

                    //***PENDIENTE - DEFINIR USUARIO DE EJECUCIÓN A LOS REPORTES
                    try
                    {
                        //string usuarioRM = Session["Usuario"].ToString();
                        //ReportParameter usuarioParam = new ReportParameter("Usuario", usuarioRM, false);
                        //ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { usuarioParam });
                    }
                    catch (Exception)
                    {
                    }

                    //Visible
                    ReportViewer1.Visible = true;
                    //Estilo del ReportViewer
                    styleReportViewer();
                    //ReportViewer1.DataBind();
                    ReportViewer1.ServerReport.Refresh();

                    //*** Auditoría
                    ProcesoLN.getInstancia().AuditarProceso(id_proceso, usuario, "ReportViewer.aspx", descripcion, "C", System.Web.HttpContext.Current);
                }
                else
                {
                    //*** Auditoría 
                    ProcesoLN.getInstancia().AuditarProceso(id_proceso, usuario, "ReportViewer.aspx", descripcion, "E", System.Web.HttpContext.Current);
                    //*** Mensaje al usuario
                    string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                    string mensaje = "Error al cargar el reporte!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
                }
            }
            catch (Exception ex)
            {
                string titulo = Path.GetFileName(Request.Url.AbsolutePath);
                string mensaje = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "mensajeError('" + mensaje + "', '" + titulo + "');", true);
            }
        }

        //Estilo del ReportViewer
        public void styleReportViewer()
        {
            //Procesar el Reporte
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            // Style Color
            ReportViewer1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            ReportViewer1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            ReportViewer1.SplitterBackColor = System.Drawing.ColorTranslator.FromHtml(htmlColor: "#004680");
            ReportViewer1.Height = Unit.Percentage(100);
            ReportViewer1.Width = Unit.Percentage(100);
            // Opciones
            ReportViewer1.SizeToReportContent = true;
            // Style Zoom
            ReportViewer1.ZoomMode = ZoomMode.Percent;
            ReportViewer1.ZoomPercent = 100;
        }

        // Implementación de CustomReportCredentials (Serializble para convertir el objeto en una secuencia de bytes y conservarlo en memoria)
        [Serializable]
        public class CustomReportCredentials : IReportServerCredentials
        {
            public WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }
            public ICredentials NetworkCredentials
            {

                get
                {

                    // Usuario
                    string usuario = ConfigurationManager.AppSettings["ReportViewerUsuario"];

                    if (string.IsNullOrEmpty(usuario))
                        throw new Exception(
                            "Falta el usuario del archivo web.config");
                    // Clave
                    string clave = ConfigurationManager.AppSettings["ReportViewerClave"];

                    if (string.IsNullOrEmpty(clave))
                        throw new Exception(
                            "Falta la contraseña del archivo web.config");
                    // Dominio
                    string dominio = ConfigurationManager.AppSettings["ReportViewerDominio"];

                    if (string.IsNullOrEmpty(dominio))
                        throw new Exception(
                            "Falta el dominio del archivo web.config");

                    return new NetworkCredential(usuario, clave, dominio);
                }
            }
            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;

                // No se usan credenciales de formulario
                return false;
            }
        }


    } // Partial Class END 
} // namespace END
