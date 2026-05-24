using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace service
{
	public class CategoriaService
	{
		public List<Categoria> listar()
		{
			List<Categoria> listaCat = new List<Categoria>();
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
					Categoria aux = new Categoria();
					aux.Id = (int)datos.Lector["Id"];
					aux.Descripcion = (string)datos.Lector["Descripcion"];
					listaCat.Add(aux);
				}
				return listaCat;

			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.cerrarConexion();
			}
		}

		public void agregar(Categoria cat)
		{
			AccesoDatos datos = new AccesoDatos();

			try
			{

                datos.setearConsulta("insert into CATEGORIA (Descripcion) values (@Descripcion)");
				datos.setearParametro("@Descripcion", cat.Descripcion);
                datos.ejecutarAccion();


            }
			catch (Exception ex)
			{

				throw;
			}
			finally
			{
				datos.cerrarConexion();
			}
		}

		public void modificar(Categoria cat)
		{
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("update Categorias set Descripcion = @Descripcion where id = @Id");
				datos.setearParametro("@Descripcion", cat.Descripcion);
				datos.setearParametro("@Id", cat.Id);
                datos.ejecutarAccion();
            }
			catch (Exception ex)
			{

				throw;
			}
			finally
			{
				datos.cerrarConexion();

			}
		}

		public void eliminar(Categoria cat)
		{
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.setearConsulta("delete from Categorias where id = @Id");
				datos.setearParametro("@Id", cat.Id);
				datos.ejecutarAccion();
            }
			catch (Exception ex)
			{

				throw;
			}
        }
	}
}
