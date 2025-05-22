<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="TPWeb_equipo_11A.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <div class="container mt-5">
        <%if (codigoArticulo != null || codigoVoucher != null)
            { %>
        <h2 class="text-center mb-4">Ingresá tus datos</h2>

        <div class="row mb-3">

            <!--DNI-->
            <div class="col-md-4">
                <label for="txtDNI" class="form-label">DNI</label>
                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="99888777" type="number" BorderWidth="2" />
                <div>
                    <asp:Label ID="lblDni" runat="server" Text=""></asp:Label>
                </div>

                <asp:Button ID="btnValidarDni" runat="server" Text="Validar DNI" CssClass="btn btn-primary mt-2" OnClick="btnValidarDni_Click" />
            </div>
        </div>
        <div class="row mb-3">

            <!--NOMBRE-->
            <div class="col-md-4">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre" Enabled="false" BorderWidth="2" />
                <asp:Label ID="lblNombre" runat="server" Text="" CssClass="form-text"></asp:Label>
            </div>

            <!--APELLIDO-->
            <div class="col-md-4">
                <label for="txtApellido" class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido" Enabled="false" BorderWidth="2" />
                <asp:Label ID="lblApellido" runat="server" Text="" CssClass="form-text"></asp:Label>
            </div>

            <!--EMAIL-->
            <div class="col-md-4">
                <label for="txtEmail" class="form-label">Email</label>
                <div class="input-group">
                    <span class="input-group-text">@</span>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="nombre@mail.com" Enabled="false" BorderWidth="2" />
                </div>
                <asp:Label ID="lblEmail" runat="server" Text="" CssClass="form-text"></asp:Label>
            </div>
        </div>

        <div class="row mb-3">
            <!--DIRECCION-->
            <div class="col-md-4">
                <label for="txtDireccion" class="form-label">Dirección</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Calle 123" Enabled="false" BorderWidth="2" />
                <asp:Label ID="lblDireccion" runat="server" Text="" CssClass="form-text"></asp:Label>
            </div>

            <!--CIUDAD-->
            <div class="col-md-4">
                <label for="txtCiudad" class="form-label">Ciudad</label>
                <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" placeholder="Ciudad" Enabled="false" BorderWidth="2" />
                <asp:Label ID="lblCiudad" runat="server" Text="" CssClass="form-text"></asp:Label>
            </div>

            <!--CP-->
            <div class="col-md-4">
                <label for="txtCP" class="form-label">CP</label>
                <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" placeholder="xxxx" Enabled="false" BorderWidth="2" />
                <asp:Label ID="lblCP" runat="server" Text="" CssClass="form-text"></asp:Label>
            </div>
        </div>


        <div class="row mb-3">
            <div class="col-md-4">
                <!--TyC-->
                <div class="form-check">
                    <asp:CheckBox ID="CheckBoxTerminos" runat="server" />
                    <asp:Label ID="lblTerminos" runat="server" Text="Acepto los términos y condiciones"></asp:Label>
                </div>
            </div>

        </div>
        <asp:ScriptManager ID="Registro" runat="server"></asp:ScriptManager>
        <asp:Button ID="btnParticipar" runat="server" Text="¡Participar!" CssClass="btn btn-primary" OnClick="btnParticipar_Click" />
        <%}
            else
            { %>
                <div class="text-center">
                    <h2>Aún no deberias estar acá</h2>
                    <p>Para volver al inicio <a href="/Default.aspx">click aqui</a></p>
                </div>
        <%  } %>
    </div>

</asp:Content>
