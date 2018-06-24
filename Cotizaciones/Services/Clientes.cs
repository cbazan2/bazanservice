using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using Cotizaciones.Services;
using Cotizaciones.Models;

namespace Cotizaciones.Services
{
	 public class Clientes
	 {
			private Conexion m_conn;
			private NpgsqlDataReader m_data;
			private List<Cliente> m_Clientees;

			public Clientes()
			{
				 m_conn = new Conexion();
				 m_data = null;
				 m_Clientees = new List<Cliente>();
			}
			/// <summary>
			/// Crea un Objeto Cliente
			/// </summary>
			/// <param name="lData">Es un objeto NpgsqlDataReader que contiene datos para crear el objeto</param>
			/// <returns>Devuelve un Objeto de tipo Cliente</returns>
			private Cliente crearObjeto(NpgsqlDataReader lData)
			{
				 Cliente lObjeto = null;
				 if (lData != null)
				 {
						lObjeto = new Cliente();
						lObjeto.ID = Convert.ToInt32(lData["idCliente"]);
                        lObjeto.IDDisrito  = Convert.ToInt32(lData["iddistrito"]);
                        lObjeto.TDocumento = Convert.ToString(lData["tdoc"]);
						lObjeto.NDocumento = Convert.ToString(lData["ndoc"]);
						lObjeto.RazonSocial = Convert.ToString(lData["razon_social"]);
						lObjeto.NombreComercial = Convert.ToString(lData["nombre_comercial"]);
						lObjeto.DireccionFiscal = Convert.ToString(lData["direccion_fiscal"]);
                        lObjeto.DireccionLegal = Convert.ToString(lData["direccion_legal"]);
                        lObjeto.Telefono = Convert.ToString(lData["telefono"]);
						lObjeto.Correo = Convert.ToString(lData["correo"]);
				 }
				 return lObjeto;
			}
			/// <summary>
			/// Devuelve un Objeto Cliente
			/// </summary>
			/// <param name="pID">Es el id que identifica a un registro en la tabla Cliente</param>
			/// <returns>Devuelve un Objeto de tipo Cliente</returns>
			public Cliente getCliente(int pID)
			{
				 Cliente lObjeto = null;
				 if (buscar("*", "where idcliente=" + pID))
				 {
						lObjeto = crearObjeto(m_data);
				 }
				 return lObjeto;
			}
			/// <summary>
			/// Crea una lista de Clientees
			/// </summary>
			/// <returns>Devuelve una lista de Clientees</returns>
			public List<Cliente> getData()
			{
				 return m_Clientees;
			}
			/// <summary>
			/// Busca en la tabla Cliente segun los parametros especificados
			/// </summary>
			/// <param name="pCampos">Indica los campos a devolver cuando se ejecuta la búsqueda</param>
			/// <param name="pFiltro">Es el filtro que se aplicara al realizar la búsqueda</param>
			/// <returns></returns>
			public bool buscar(string pCampos, string pFiltro)
			{
				 bool lRsp = false;

				 try
				 {
						if (m_conn.activo() == false) m_conn.abrir("");
						using (NpgsqlCommand lCmd = new NpgsqlCommand())
						{
							 lCmd.CommandType = CommandType.Text;
							 lCmd.CommandText = "select " + pCampos + " from cliente " + pFiltro;
							 lCmd.Connection = m_conn.getConexion();
							 m_data = lCmd.ExecuteReader();
							 while (m_data.Read())
							 {
									m_Clientees.Add(crearObjeto(m_data));
							 }
							 lRsp = (m_data != null);
						}
				 }
				 catch (Exception ex)
				 {
						Console.Write(ex.Message);
						throw;
				 }
				 finally
				 {
						if (m_conn.activo()) m_conn.cerrar();
				 }
				 return lRsp;
			}
			/// <summary>
			/// Guarda/Actualiza un registro de Cliente
			/// </summary>
			/// <param name="pObjeto">Objeto tipo Cliente</param>
			/// <param name="pOpc">Parametro que indica si se crear un nuevo registro (INS),
			/// o si se va a actualizar un registro (UPD)</param>
			/// <returns>Entero que es el Id en la tabla de Clientees</returns>
			public int guardar(Cliente pObjeto, string pOpc)
			{
				 int lNuevoId = 0;
				 try
				 {
						if (m_conn.activo() == false) m_conn.abrir("");
						using (NpgsqlCommand lCmd = new NpgsqlCommand())
						{
							 lCmd.CommandType = CommandType.Text;
							 lCmd.CommandText = "fu_Cliente";
							 lCmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                            lCmd.Parameters.Add(new NpgsqlParameter("iddistrito", NpgsqlDbType.Integer));
                            lCmd.Parameters.Add(new NpgsqlParameter("tdoc", NpgsqlDbType.Varchar));
							 lCmd.Parameters.Add(new NpgsqlParameter("ndoc", NpgsqlDbType.Varchar));
							 lCmd.Parameters.Add(new NpgsqlParameter("razon_social", NpgsqlDbType.Varchar));
							 lCmd.Parameters.Add(new NpgsqlParameter("nombre_comercial", NpgsqlDbType.Varchar));
							 lCmd.Parameters.Add(new NpgsqlParameter("direccion_fiscal", NpgsqlDbType.Varchar));
                            lCmd.Parameters.Add(new NpgsqlParameter("direccion_legal", NpgsqlDbType.Varchar));
                            lCmd.Parameters.Add(new NpgsqlParameter("telefono", NpgsqlDbType.Varchar));
							 lCmd.Parameters.Add(new NpgsqlParameter("correo", NpgsqlDbType.Varchar));
							 lCmd.Parameters.Add(new NpgsqlParameter("opc", NpgsqlDbType.Varchar));
							 lCmd.Parameters[0].Value = pObjeto.ID;
                            lCmd.Parameters[1].Value = pObjeto.IDDisrito;
                            lCmd.Parameters[2].Value = pObjeto.TDocumento;
							 lCmd.Parameters[3].Value = pObjeto.NDocumento;
							 lCmd.Parameters[4].Value = pObjeto.RazonSocial;
							 lCmd.Parameters[5].Value = pObjeto.NombreComercial;
							 lCmd.Parameters[6].Value = pObjeto.DireccionFiscal;
                            lCmd.Parameters[7].Value = pObjeto.DireccionLegal;
                            lCmd.Parameters[8].Value = pObjeto.Telefono;
							 lCmd.Parameters[9].Value = pObjeto.Correo;
							 lCmd.Parameters[10].Value = pOpc;
							 using (NpgsqlDataReader lDrd = lCmd.ExecuteReader())
							 {
									if (lDrd != null)
									{
										 lDrd.Read();
										 lNuevoId = Convert.ToInt32(lDrd[0]);
									}
							 }
						}
				 }
				 catch (Exception ex)
				 {
						Console.Write(ex.Message);
						throw;
				 }
				 finally
				 {
						if (m_conn.activo()) m_conn.cerrar();
				 }
				 return lNuevoId;
			}
	 }
}