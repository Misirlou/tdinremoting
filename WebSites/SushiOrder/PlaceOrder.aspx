<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlaceOrder.aspx.cs" Inherits="PlaceOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Place Order</title>
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="http://code.jquery.com/jquery-migrate-1.1.1.min.js"></script>
    <script>
        function addFields() {
            $("#orderitems").innerHTML += "";
        }

    </script>
</head>
<body>
    <h1>Place Order</h1>
    <form id="form2" runat="server">
      <asp:Button ID="Button2" runat="server" Text="Place Order" OnClick="Button2_Click" />
      <br />
      <br />
        <input type="text" id="Nome"  />
      <div id="orderitems">
          <br />
         <label>Type: <select><option>Aji</option><option>AmaEbi</option><option>Anago</option><option>Awabi </option><option>Ebi </option><option>Hamachi </option><option> Hirame </option><option>Hokkigai </option><option>Hotate </option><option>Ika </option><option>Ikura</option></select></label>
         <label>Quantity: <input type="text /></label> 
      </div>
        <input id="Button3" onclick="javascript:addFields()" value="Add" />
      <br />
    </form>
    <br />
    <a href="Default.aspx">Back</a>
</body>
</html>
