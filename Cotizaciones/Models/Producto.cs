using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizaciones.Models
{
	 public class Producto
	 {
			public int ID { get; set; }
			public string CBarras { get; set; }
			public string Nombre { get; set; }
			public int Stock { get; set; }
			public decimal PCompra { get; set; }
			public decimal PVenta { get; set; }
	 }
}