using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Reporte
    {
        public int Id_reporte { get; set; }
        public string Cod_alterno { get; set; }
        public string Nombre { get; set; }
        public string Archivo_rpt { get; set; }
        public string Ubicacion_ssrs { get; set; }
        public string Descripcion { get; set; }
        public string Form_html { get; set; }
        public string Parametros { get; set; }
        public string I_estado { get; set; }
        public string Id_modulo { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }

        public Reporte()
        {
            this.Id_reporte = 0;
            this.Cod_alterno = string.Empty;
            this.Nombre = string.Empty;
            this.Archivo_rpt = string.Empty;
            this.Ubicacion_ssrs = string.Empty;
            this.Descripcion = string.Empty;
            this.Form_html = string.Empty;
            this.Parametros = string.Empty;
            this.I_estado = string.Empty;
            this.Id_modulo = string.Empty;
            this.Usuario = string.Empty;
            this.Terminal = string.Empty;
        }
    }
}
