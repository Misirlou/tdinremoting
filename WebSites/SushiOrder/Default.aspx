<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check Order</title>
</head>
<body>
    <h1>Check Orders</h1>
    <form id="form1" runat="server">
      <label>Nome: <asp:TextBox runat="server" id="tbnome">nome</asp:TextBox></label>
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
    <a href="PlaceOrder.aspx">Place Order</a>
</body>
</html>
