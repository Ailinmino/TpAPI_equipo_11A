using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar() //1. Metodo para que lea los registros de la base de datos
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("select A.Id,A.nombre, A.codigo, A.descripcion, A.IdCategoria as IdCategoria, A.IdMarca as IdMarca, M.Descripcion as Marca, C.Descripcion as Categoria, A.Precio, I.ImagenUrl as ImagenUrl, I.Id as IdImagen\r\nFROM ARTICULOS A \r\nINNER JOIN MARCAS M ON A.IdMarca = M.Id \r\nINNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id \r\nINNER JOIN IMAGENES I ON A.Id = I.IdArticulo");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int id = (int)datos.Lector["Id"];
                    Articulo existente = lista.FirstOrDefault(a => a.Id == id);
                    if (existente == null)
                    {
                        Articulo aux = new Articulo();
                        aux.Id = id;
                        aux.Codigo = (string)datos.Lector["Codigo"];
                        aux.Nombre = (string)datos.Lector["Nombre"];
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                        aux.Marca = new Marca();
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                        aux.Categoria = new Categoria();
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        //aux.ID_Categoria = Convert.ToInt32(datos.Lector["IDCategoria"]);

                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                        aux.Precio = datos.Lector["Precio"] != DBNull.Value ? Convert.ToSingle(datos.Lector["Precio"]) : 0;
                        //aux.Imagen = new Imagen();
                        //aux.Imagen.Url = datos.Lector["ImagenUrl"] != DBNull.Value ? datos.Lector["ImagenUrl"].ToString() : "";
                        aux.Imagen = new List<Imagen>();
                        if (datos.Lector["IdImagen"] != DBNull.Value)
                        {
                            aux.Imagen.Add(new Imagen
                            {
                                Id = (int)datos.Lector["IdImagen"],
                                Url = (string)datos.Lector["ImagenUrl"]
                            });
                        }
                        lista.Add(aux);
                    }
                    else
                    {
                        if (datos.Lector["IdImagen"] != DBNull.Value)
                        {
                            existente.Imagen.Add(new Imagen
                            {
                                Id = (int)datos.Lector["IdImagen"],
                                Url = (string)datos.Lector["ImagenUrl"]
                            });
                        }

                    }

                }

                return lista;

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

        public void agregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IDMarca, @IDCategoria)");
                datos.setearParametros("@Codigo", nuevo.Codigo);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Descripcion", nuevo.Descripcion);
                datos.setearParametros("@Precio", nuevo.Precio);
                datos.setearParametros("@IdMarca", nuevo.Marca.Id);
                datos.setearParametros("@IdCategoria", nuevo.Categoria.Id);

                // Obtener el ID generado del artículo
                nuevo.Id = datos.ejecutarScalar();

                // Si hay imagen, insertarla con el ID del artículo
                if (!string.IsNullOrEmpty(nuevo.Imagen[0].Url))
                {
                    datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                    datos.setearParametros("@IdArticulo", nuevo.Id);
                    datos.setearParametros("@ImagenUrl", nuevo.Imagen[0].Url);
                    datos.ejecutarAccion();
                }
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

        public bool existeCodigo(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE Codigo = @Codigo");
                datos.setearParametros("@Codigo", codigo);

                int cantidad = datos.ejecutarScalar();
                return cantidad > 0;
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

        public void modificar(Articulo articulo)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio where Id = @Id");
                accesoDatos.setearParametros("@Codigo", articulo.Codigo);
                accesoDatos.setearParametros("@Nombre", articulo.Nombre);
                accesoDatos.setearParametros("@Descripcion", articulo.Descripcion);
                accesoDatos.setearParametros("@IdMarca", articulo.Marca.Id);
                accesoDatos.setearParametros("@IdCategoria", articulo.Categoria.Id);
                accesoDatos.setearParametros("@Precio", articulo.Precio);
                accesoDatos.setearParametros("@Id", articulo.Id);
                accesoDatos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public void eliminarLogico(int ID_articulo)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update from ARTICULOS set ACTIVO = 0 where Id = @ID_articulo");
                datos.setearParametros("ID_articulo", ID_articulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void eliminarFisico(int ID_articulo)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where Id = @ID_articulo");
                datos.setearParametros("ID_articulo", ID_articulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Articulo obtenerPorId(int id)
        {
            Articulo articulo = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT A.Id, A.Nombre, A.Descripcion, I.Id AS IdImagen, I.ImagenUrl FROM ARTICULOS A INNER JOIN IMAGENES I ON A.Id = I.IdArticulo WHERE A.Id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    //Se inicializa una sola vez el artículo y se carga el nombre y la descripción
                    if (articulo == null)
                    {
                        articulo = new Articulo();
                        articulo.Id = id;
                        articulo.Nombre = (string)datos.Lector["Nombre"];
                        articulo.Descripcion = (string)datos.Lector["Descripcion"];
                        articulo.Imagen = new List<Imagen>();
                    }

                    //Se recorren las imágenes del artículo evitando que se repitan los txt
                    if (datos.Lector["IdImagen"] != DBNull.Value)
                    {
                        articulo.Imagen.Add(new Imagen
                        {
                            Id = (int)datos.Lector["IdImagen"],
                            Url = (string)datos.Lector["ImagenUrl"]
                        });
                    }
                }

                return articulo;
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
