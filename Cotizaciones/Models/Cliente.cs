using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public int IDDisrito { get; set; }
        public string TDocumento { get; set; }
        public string NDocumento { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string DireccionFiscal { get; set; }
        public string DireccionLegal { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}