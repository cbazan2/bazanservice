using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cotizaciones.Services;

namespace Cotizaciones.Controllers
{
	 public class ClienteController: Controller
	 {
			private Clientes m_regs;

			public ClienteController()
			{
				 m_regs = new Clientes();
			}

			public ActionResult Agregar()
			{
				 return View();
			}

			public ActionResult Buscar()
			{
				 ViewBag.Title = "Busqueda de Clientes";
				 return View();
			}

			public ActionResult VerDatos()
			{
				 ViewBag.Title = "Lista de Clientes";
				 m_regs.buscar("*", "");
				 var model = m_regs.getData();
				 return View(model);
			}
	 }
}