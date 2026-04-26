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
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, A.Precio,(SELECT Descripcion FROM MARCAS WHERE Id = A.IdMarca) AS DescripcionMarca,(SELECT Descripcion FROM CATEGORIAS WHERE Id = A.IdCategoria) AS DescripcionCategoria,(SELECT TOP 1 ImagenUrl FROM IMAGENES WHERE IdArticulo = A.Id) AS UrlImagen FROM ARTICULOS A");
                datos.ejecutarLectura(); 

                while (datos.Lector.Read()) {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.UrlImagen = Convert.IsDBNull(datos.Lector["UrlImagen"]) ? "" : datos.Lector["UrlImagen"].ToString();
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

                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");
                datos.setearParametro("@Codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion); 
                datos.setearParametro("@IdMarca", art.IdMarca);
                datos.setearParametro("@IdCategoria", art.IdCategoria);
                datos.setearParametro("@Precio", art.Precio);

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
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio where Id = @Id");
                datos.setearParametro("@Codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@IdMarca", art.IdMarca);
                datos.setearParametro("@IdCategoria", art.IdCategoria);
                datos.setearParametro("@Precio", art.Precio);
                datos.setearParametro("@Id", art.Id);

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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("delete from ARTICULOS where Id = @Id");
                datos.setearParametro("@Id", id);   
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

        public int obtenerUltimoId()
        {
            AccesoDatos datos = new AccesoDatos();
            int id = 0;
            try
            {
                datos.setearConsulta("SELECT MAX(Id) FROM ARTICULOS");
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    id = (int)datos.Lector[0];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
            return id;
        }


    }
}
