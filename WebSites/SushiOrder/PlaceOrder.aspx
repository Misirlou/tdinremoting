<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlaceOrder.aspx.cs" Inherits="PlaceOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Encomendar</title>
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="http://code.jquery.com/jquery-migrate-1.1.1.min.js"></script>
    <link rel="stylesheet" type="text/css" href="sushiStyle.css">
    <script>
        var id = 1;

        function addFields() {
            id++;
            $("#orderitem" + id).show();
            //'<div class="orderitem"><label>Type: <select><option>Aji</option><option>AmaEbi</option><option>Anago</option><option>Awabi </option><option>Ebi </option><option>Hamachi </option><option> Hirame </option><option>Hokkigai </option><option>Hotate </option><option>Ika </option><option>Ikura</option></select></label><label>Quantity: <input type="text /></label></div>';
            return false;
        }

        $(document).ready(function(){
            $("div#orderitems div label input").each(function(){

              if ($(this).val() != "") {
                    $(this).parent("label").parent("div").show();
                }
                    
            });
        });

    </script>
</head>
<body>
    <h1 id="header">Acme Home Sushi - Encomendar</h1>
    <form id="form2" runat="server">
        <label>
            Nome Cliente:
            <asp:TextBox ID="tbName" runat="server" /></label><br />
        <label>
            Número de Cartão de Crédito:
            <asp:TextBox ID="tbCCN" runat="server" /></label>
        <br />
        <label>
            Morada:
            <asp:TextBox ID="tbAddr" runat="server" /></label><br />
        <div id="orderitems">

            <br />
            <div id="orderitem1">

                <label>
                    Tipo de sushi:
                <asp:DropDownList ID="ddl1" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantidade:
                <asp:TextBox ID="tbquant1" runat="server" /></label>
            </div>
            <div id="orderitem2" style="display: none">
                <br />
                <label>
                    Tipo de sushi:
                <asp:DropDownList ID="ddl2" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantidade:
                <asp:TextBox ID="tbquant2" runat="server" /></label>
            </div>
            <div id="orderitem3" style="display: none">
                <br />
                <label>
                    Tipo de sushi:
                <asp:DropDownList ID="ddl3" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                   Quantidade:
                <asp:TextBox ID="tbquant3" runat="server" /></label>
            </div>
            <div id="orderitem4" style="display: none">
                <br />
                <label>
                   Tipo de sushi:
                <asp:DropDownList ID="ddl4" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantidade:
                <asp:TextBox ID="tbquant4" runat="server" /></label>
            </div>
            <div id="orderitem5" style="display: none">
                <br />
                <label>
                    Tipo de sushi:
                <asp:DropDownList ID="ddl5" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantidade:
                <asp:TextBox ID="tbquant5" runat="server" /></label>
            </div>
            <div id="orderitem6" style="display: none">
                <br />
                <label>
                    Tipo de sushi:
                <asp:DropDownList ID="ddl6" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantidade:
                <asp:TextBox ID="tbquant6" runat="server" /></label>
            </div>

        </div>
        <input type="button" id="Button3" onclick="javascript: addFields()" value="Novo Item" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Encomendar" OnClick="Button2_Click" />
        <asp:Panel ID="confirmPreco" runat="server"
    Visible="False">
    <p><asp:Label ID="labelpreco" runat="server"></asp:Label><br />Confirmar Preço: 
    <asp:Button ID="btSim" runat="server" OnClick="btPreco_Click" Text="Sim" />
    <asp:Button ID="btNao" runat="server" OnClick="btPreco_Click" Text="Não" />
    </p>
</asp:Panel>
    </form>
    
    <br />
    <a href="Default.aspx">Voltar</a>
</body>
</html>
