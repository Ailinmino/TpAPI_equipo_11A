using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPWeb_equipo_11A
{
    public partial class Voucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string codigo = voucherInput.Text.Trim();
            VoucherNegocio negocio = new VoucherNegocio();

            if (!negocio.existeVoucher(codigo))
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "window.onload = function() { alert('¡Ups! Código inexistente'); document.getElementById('" + voucherInput.ClientID + "').focus(); };", true);
                //voucherInput.Style.Add(HtmlTextWriterStyle.BorderColor, "#DC3545");
                voucherInput.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                LabelVoucherInput.Text = "¡Ups! Código inexistente";
                LabelVoucherInput.ForeColor = System.Drawing.Color.Red;
                LabelVoucherInput.Visible = true;
                
            }
            else if (negocio.estaCanjeado(codigo))
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertaUsado",
                //    "window.onload = function() { alert('ESTE CÓDIGO YA FUE UTILIZADO'); };", true);
                voucherInput.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                LabelVoucherInput.Text = "¡Ups! Este código ya fue utilizado";
                LabelVoucherInput.ForeColor = System.Drawing.Color.Red;
                LabelVoucherInput.Visible = true;
            }
            else
            {
                // Código válido
                //SI EXISTE EL VOUCHER SE DEBE REDIRIGIR A LA PÁGINA QUE CONTENGA LOS PRODUCTOS QUE SE PUEDEN CANJEAR 
                //ScriptManager.RegisterStartupScript(this, GetType(),
                //        "alertaValido", "window.onload = function() { " + "alert('¡Genial! Tu código se validó EXITOSAMENTE'); " + "window.location.href = 'ListadoArt.aspx';" + "};",
                //true
                // );
                voucherInput.CssClass = "form-control form-control-lg mx-auto form-control is-valid";
                LabelVoucherInput.Text = "¡Genial! Tu código se validó EXITOSAMENTE";
                LabelVoucherInput.ForeColor = System.Drawing.Color.Green;
                LabelVoucherInput.Visible = true;
                //Permite obtener luego la variable del código desde otra ventana. En la otra ventana se invoca con:
                //poniendo como propiedad de la clase por ejemplo cod, y luego la siguiente sintaxis: Se pone TOstring porque devuelve un obj
                //cod = Session["codigovoucher"].ToString(); 
                Session.Add("codigoVoucher", codigo);
                Response.Redirect("ListadoArt.aspx", false);

            }
        }
    }
}


    