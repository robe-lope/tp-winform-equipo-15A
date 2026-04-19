using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace service
{
    public class ArticuloService
    {
        public List<Articulo> listar() {

            List<Articulo> listaArt = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio FROM ARTICULOS")

                while (datos.Lector.Read()) {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.IdMarca = (int)datos.Lector["IdMarca"];
                    aux.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Precio = Math.Round(aux.Precio, 2);

                    listaArt.Add(aux);
                }

                return listaArt;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {

                datos.cerrarConexion();

            }

        }

        public void agregar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values ('" + art.Codigo + "', '" + art.Nombre + "', '" + art.Descripcion + "', " + art.IdMarca + ", " + art.IdCategoria + ", " + art.Precio + ")");
                datos.ejecutarAccion();


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

        public void modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = '" + art.Codigo + "', Nombre = '" + art.Nombre + "', Descripcion = '" + art.Descripcion + "', IdMarca = " + art.IdMarca + ", IdCategoria = " + art.IdCategoria + ", Precio = " + art.Precio + " where Id = " + art.Id);
                datos.ejecutarAccion(); 
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

        public void eliminar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("delete from ARTICULOS where Id = " + art.Id);
                datos.ejecutarAccion();
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
    }
}
