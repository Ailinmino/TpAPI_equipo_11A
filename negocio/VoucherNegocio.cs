using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class VoucherNegocio
    {
        public bool existeVoucher(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM VOUCHERS WHERE CodigoVoucher = @codigo");
                datos.setearParametros("@codigo", codigo);

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

        public bool estaCanjeado(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();
            // Verifica si ya fue utilizado (FechaCanje no es null)
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM VOUCHERS WHERE CodigoVoucher = @codigo AND FechaCanje IS NOT NULL");
                datos.setearParametros("@codigo", codigo);

                int cantidad = datos.ejecutarScalar(); // usa tu método personalizado
                return cantidad > 0; // Si hay al menos uno, está canjeado

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
            
        }

        public void modificar(Vouchers vouchers)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.setearConsulta("UPDATE vouchers SET IdCliente = @IdCliente, FechaCanje = GETDATE(), IdArticulo = @articuloSeleccionado WHERE CodigoVoucher = @codigoVoucher");
                accesoDatos.setearParametros("@codigoVoucher", vouchers.CodigoVoucher);
                accesoDatos.setearParametros("@IdCliente", vouchers.Cliente.Id);
                accesoDatos.setearParametros("@articuloSeleccionado", vouchers.Articulo.Id);
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


    }
}
