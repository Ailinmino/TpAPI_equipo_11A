using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using System.ComponentModel;
using System.Xml.Linq;

namespace TPWeb_equipo_11A
{
    public partial class Clientes : System.Web.UI.Page
    {
        public Cliente Cliente { get; set; }
        public ClienteNegocio Negocio { get; set; }
        public string Documento { get; set; }

        public Vouchers vouchers { get; set; }
        public string codigoVoucher {  get; set; }
        public string codigoArticulo { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            codigoVoucher = codigoVoucher = Session["codigoVoucher"] != null ? Session["codigoVoucher"].ToString() : null;
            codigoArticulo = codigoArticulo = Session["articuloSeleccionado"] != null ? Session["articuloSeleccionado"].ToString() : null;
        }

        protected void btnParticipar_Click(object sender, EventArgs e)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            Documento = txtDNI.Text;

            //validacion de campos vacios
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtCiudad.Text) ||
                string.IsNullOrWhiteSpace(txtCP.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Todos los campos son obligatorios.');", true);
                return;
            }

            //validar Nombre
            if (txtNombre.Text.Length > 50)
            {
                lblNombre.ForeColor = System.Drawing.Color.Red;
                lblNombre.Text = "Excediste los 50 caracteres permitidos.";
                txtNombre.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                return;
            }
            else
            {
                lblNombre.ForeColor = System.Drawing.Color.Green;
                lblNombre.Text = "✓ Campo válido.";
                txtNombre.CssClass = "form-control is-valid";
            }

            //validacion de apellido 50
            if (txtApellido.Text.Length > 50)
            {
                lblApellido.ForeColor = System.Drawing.Color.Red;
                lblApellido.Text = "Excediste los 50 caracteres permitidos.";
                txtApellido.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                return;
            }
            else
            {
                lblApellido.ForeColor = System.Drawing.Color.Green;
                lblApellido.Text = "✓ Campo válido.";
                txtApellido.CssClass = "form-control is-valid";
            }

            //validacion de mail 50
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains(".com"))
            {
                lblEmail.ForeColor = System.Drawing.Color.Red;
                lblEmail.Text = "Formato de mail incorreto.";
                txtEmail.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                return;
            }
            else if (txtEmail.Text.Length > 50)
            {
                lblEmail.ForeColor = System.Drawing.Color.Red;
                lblEmail.Text = "Excediste los 50 caracteres permitidos en el email.";
                txtEmail.CssClass = "form-control is-invalid";
                return;
            }
            else
            {
                lblEmail.ForeColor = System.Drawing.Color.Green;
                lblEmail.Text = "✓ Campo válido.";
                txtEmail.CssClass = "form-control is-valid";
            }

            //validacion de direccion 50
            if (txtDireccion.Text.Length > 50)
            {
                lblDireccion.ForeColor = System.Drawing.Color.Red;
                lblDireccion.Text = "Excediste los 50 caracteres permitidos.";
                txtDireccion.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                return;
            }
            else
            {
                lblDireccion.ForeColor = System.Drawing.Color.Green;
                lblDireccion.Text = "✓ Campo válido.";
                txtDireccion.CssClass = "form-control is-valid";
            }

            //validacion de ciudad 50
            if (txtCiudad.Text.Length > 50)
            {
                lblCiudad.ForeColor = System.Drawing.Color.Red;
                lblCiudad.Text = "Excediste los 50 caracteres permitidos.";
                txtCiudad.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                return;
            }
            else
            {
                lblCiudad.ForeColor = System.Drawing.Color.Green;
                lblCiudad.Text = "✓ Campo válido.";
                txtCiudad.CssClass = "form-control is-valid";
            }

            //validacion de CP
            int CodigoPostal;//variable auxiliar para guardar el contenido del txt.CP

            if (!int.TryParse(txtCP.Text, out CodigoPostal) || CodigoPostal < 0)
            {
                lblCP.ForeColor = System.Drawing.Color.Red;
                lblCP.Text = "Error de formato. Ingresa solo números.";
                txtCP.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                return;
            }
            else
            {
                lblCP.ForeColor = System.Drawing.Color.Green;
                lblCP.Text = "✓ Campo válido.";
                txtCP.CssClass = "form-control is-valid";
            }

            if(CheckBoxTerminos.Checked == false)
            {
                lblTerminos.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                lblTerminos.ForeColor = System.Drawing.Color.Green;
            }

                try
                {
                    Cliente nuevo = negocio.existeCliente(Documento);
                    if (nuevo == null)
                    {
                        nuevo = new Cliente();

                        nuevo.Documento = Documento;
                        nuevo.Nombre = txtNombre.Text;
                        nuevo.Apellido = txtApellido.Text;
                        nuevo.Email = txtEmail.Text;
                        nuevo.Direccion = txtDireccion.Text;
                        nuevo.Ciudad = txtCiudad.Text;
                        nuevo.CodigoPostal = int.Parse(txtCP.Text);

                        negocio.agregarCliente(nuevo);
                        Session.Add("IdCliente", negocio.existeCliente(nuevo.Documento).Id);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('¡Registro exitoso!');", true);
                    }
                    else
                    {
                        nuevo = negocio.existeCliente(Documento);
                        Session.Add("IdCliente", nuevo.Id);



                    }



                    vouchers = new Vouchers();

                    vouchers.CodigoVoucher = (string)Session["codigoVoucher"];
                    vouchers.Articulo = new Articulo();
                    vouchers.Articulo.Id = Convert.ToInt32(Session["articuloSeleccionado"]);
                    vouchers.Cliente = new Cliente();
                    vouchers.Cliente.Id = (int)Session["IdCliente"];
                    VoucherNegocio negocioVouchers = new VoucherNegocio();
                    negocioVouchers.modificar(vouchers);

                    Response.Redirect("ProductoCanjeado.aspx");
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            Response.Redirect("ProductoCanjeado.aspx");
        }

        protected void btnValidarDni_Click(object sender, EventArgs e)
        {
            try
            {
                Documento = txtDNI.Text;
                if (Documento == "")
                {
                    lblDni.ForeColor = System.Drawing.Color.Red;
                    lblDni.Text = "Debe ingresar un DNI para validar!";
                    txtDNI.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                    return;
                }
                else
                if (Documento.Length < 6 || Documento.Length > 8)
                {
                    lblDni.ForeColor = System.Drawing.Color.Red;
                    lblDni.Text = "El DNI ingresado es inválido!";
                    txtDNI.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                    return;
                } 
                else
                if (Convert.ToInt32(Documento) <= 0)
                {
                    lblDni.ForeColor = System.Drawing.Color.Red;
                    lblDni.Text = "El DNI debe ser un numero positivo!";
                    txtDNI.CssClass = "form-control form-control-lg mx-auto form-control is-invalid";
                    return;
                }

                    Negocio = new ClienteNegocio();
                Cliente = new Cliente();
                Cliente = Negocio.existeCliente(Documento);

                if (Cliente != null)
                {
                    txtNombre.Text = Cliente.Nombre;
                    txtApellido.Text = Cliente.Apellido;
                    txtEmail.Text = Cliente.Email;
                    txtDireccion.Text = Cliente.Direccion;
                    txtCiudad.Text = Cliente.Ciudad;
                    txtCP.Text = Cliente.CodigoPostal.ToString();
                    lblDni.ForeColor = System.Drawing.Color.Green;
                    lblDni.Text = "Bienvenido de nuevo " + Cliente.Nombre + "!";
                    txtDNI.CssClass = "form-control form-control-lg mx-auto form-control is-valid";
                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtEmail.Enabled = false;
                    txtDireccion.Enabled = false;
                    txtCiudad.Enabled = false;
                    txtCP.Enabled = false;
                }
                else
                {
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtEmail.Text = "";
                    txtDireccion.Text = "";
                    txtCiudad.Text = "";
                    txtCP.Text = "";
                    lblDni.ForeColor = System.Drawing.Color.Green;
                    lblDni.Text = "El DNI ingreasdo es válido! Debes ingresar todos los datos para registrarte.";
                    txtDNI.CssClass = "form-control form-control-lg mx-auto form-control is-valid";
                    txtNombre.Enabled = true;
                    txtApellido.Enabled = true;
                    txtEmail.Enabled = true;
                    txtDireccion.Enabled = true;
                    txtCiudad.Enabled = true;
                    txtCP.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}