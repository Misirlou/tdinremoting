<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlaceOrder.aspx.cs" Inherits="PlaceOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Place Order</title>
</head>
<body>
    <h1>Place Order</h1>
    <form id="form2" runat="server">
      <asp:Button ID="Button2" runat="server" Text="GetOrders" OnClick="Button2_Click" />
      <br />
      <br />
      <asp:GridView ID="GridView1" runat="server">
      </asp:GridView>
        <asp:GridView ID="GridView2" runat="server">
      </asp:GridView>
      <br />
    </form>
    <br />
    <a href="Default.aspx">Back</a>
</body>
</html>
