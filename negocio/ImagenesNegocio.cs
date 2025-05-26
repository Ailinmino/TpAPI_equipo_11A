using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenesNegocio
    {

        public void agregarImagenes(int idArticulo, List<string> urls)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                foreach (var url in urls)
                {
                    datos = new AccesoDatos();
                    datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                    datos.setearParametros("@IdArticulo", idArticulo);
                    datos.setearParametros("@ImagenUrl", url);
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
    }
}
