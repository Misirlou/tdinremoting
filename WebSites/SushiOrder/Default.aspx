<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acme Home Sushi</title>
    <link rel="stylesheet" type="text/css" href="sushiStyle.css">
</head>
<body>
    <h1 id="header">Acme Home Sushi</h1>
    <div id="place">
        Disfruta dos nossos serviços e encomenda já:  <a href="PlaceOrder.aspx">Eu quero sushi!</a>
    </div>
    
    <div id="check">
        Se já encomendaste o teu sushi, verifica o estado da tua encomenda:
        <form id="form1" runat="server">
          <label>Nome: <asp:TextBox runat="server" id="tbnome">Nome:</asp:TextBox></label>
          <asp:Button ID="Button1" runat="server" Text="GetOrders" OnClick="Button1_Click" />
          <br />
          <br />
          <asp:GridView ID="GridView1" runat="server">
          </asp:GridView>
            <asp:GridView ID="GridView2" runat="server">
          </asp:GridView>
          <br />
        </form>
        <br />
    </div>
</body>
</html>
