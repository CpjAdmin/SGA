using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    //estructura para pasar por parametro en el api EnviarSms
    public class SmsRequest
    {
        public string Login { get; set; }
        public string Indgenera { get; set; }
        public string Usuario { get; set; }
        public string Tipo { get; set; }
        public string TelefonoAux { get; set; }

        public SmsRequest()
        {
            Login = "";
            Indgenera = "";
            Usuario = "";
            Tipo = "I";
            TelefonoAux = "";
        }

        public SmsRequest(string Login, string Indgenera, string Usuario, string Tipo)
        {
            this.Login = Login;
            this.Indgenera = Indgenera;
            this.Usuario = Usuario;
            this.Tipo = Tipo;
        }
    }
}
