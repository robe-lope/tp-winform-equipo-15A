using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class ImagenService
    {
            public List<Imagen> listar()
            {

                List<Imagen> listaImg = new List<Imagen>();
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setearConsulta("select Id, ImagenUrl, IdArticulo FROM IMAGENES");
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {
                        Imagen aux = new Imagen();
                        aux.Id = (int)datos.Lector["Id"];
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                        aux.IdArticulo = (int)datos.Lector["IdArticulo"];

                        listaImg.Add(aux);
                    }

                    return listaImg;
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

            public void agregar(Imagen img)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {

                    datos.setearConsulta("insert into IMAGENES (ImagenUrl, IdArticulo) values (@ImagenUrl, @IdArticulo)");
                    datos.setearParametro("@ImagenUrl", img.ImagenUrl);
                    datos.setearParametro("@IdArticulo", img.IdArticulo);
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

            public void modificar(Imagen img)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setearConsulta("update IMAGENES set ImagenUrl = @ImagenUrl, where IdArticulo = @IdArticulo");
                    datos.setearParametro("@ImagenUrl", img.ImagenUrl);
                    datos.setearParametro("@IdArticulo", img.IdArticulo);
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

            public void eliminar(Imagen img)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.setearConsulta("delete from IMAGENES where Id = @Id");
                    datos.setearParametro("@Id", img.Id);
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


