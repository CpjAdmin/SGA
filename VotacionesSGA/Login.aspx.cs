using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VotacionesSGA
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //*** #8 - Desencriptar Credenciales
        void Page_LoadComplete(object sender, EventArgs e)
        {
            try
            {   
                if (!IsPostBack)
                {
                    string strReq = "";
                    strReq = Request.RawUrl;
                    int indice = strReq.IndexOf('?');
                    strReq = strReq.Substring(strReq.IndexOf('?') + 1);

                    // Si el string no está vacio y el IndexOf es diferente de -1
                    if (!strReq.Equals("") && indice != -1)
                    {
                        strReq = DecryptQueryString(strReq);
                        string[] arrMsgs = strReq.Split('&');
                        string[] arrIndMsg;

                        string Vusuario = "", Vpin = "";
                        // Usuario
                        arrIndMsg = arrMsgs[0].Split('=');
                        Vusuario = arrIndMsg[1].ToString().Trim();
                        // Clave
                        arrIndMsg = arrMsgs[1].Split('='); 
                        Vpin = arrIndMsg[1].ToString().Trim();
                        // Agrega el valor a los campos Usuario y Clave del HTML
                        this.username.Value = Vusuario;
                        this.pin.Value = Vpin;

                        this.pin.Attributes["type"] = "password";

                        Page.ClientScript.RegisterStartupScript(GetType(), "script", "<script>validar()</script>");
                    }
                    else
                    {
                        this.pin.Attributes["type"] = "password";
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        //*** (8) #9 - Metodo Desencriptar
        private string DecryptQueryString(string strQueryString)
        {
            EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
            return objEDQueryString.Decrypt(strQueryString, "a9y5sm2w");
        }
    }
}
