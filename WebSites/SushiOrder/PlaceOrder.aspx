﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlaceOrder.aspx.cs" Inherits="PlaceOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Place Order</title>
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="http://code.jquery.com/jquery-migrate-1.1.1.min.js"></script>
    <script>
        var id = 1;

        function addFields() {
            id++;
            $("#orderitem" + id).show();
            //'<div class="orderitem"><label>Type: <select><option>Aji</option><option>AmaEbi</option><option>Anago</option><option>Awabi </option><option>Ebi </option><option>Hamachi </option><option> Hirame </option><option>Hokkigai </option><option>Hotate </option><option>Ika </option><option>Ikura</option></select></label><label>Quantity: <input type="text /></label></div>';
            return false;
        }


    </script>
</head>
<body>
    <h1>Place Order</h1>
    <form id="form2" runat="server">
        <asp:Button ID="Button2" runat="server" Text="Place Order" OnClick="Button2_Click" />
        <br />
        <br />
        <label>
            Name:
            <asp:TextBox ID="tbName" runat="server" /></label><br />
        <label>
            Credit Card Number:
            <asp:TextBox ID="tbCCN" runat="server" /></label>
        <br />
        <label>
            Address:
            <asp:TextBox ID="tbAddr" runat="server" /></label><br />
        <div id="orderitems">

            <br />
            <div id="orderitem1">

                <label>
                    Type:
                <asp:DropDownList ID="ddl1" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantity:
                <asp:TextBox ID="tbquant1" runat="server" /></label>
            </div>
            <div id="orderitem2" style="display: none">
                <br />
                <label>
                    Type:
                <asp:DropDownList ID="ddl2" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantity:
                <asp:TextBox ID="tbquant2" runat="server" /></label>
            </div>
            <div id="orderitem3" style="display: none">
                <br />
                <label>
                    Type:
                <asp:DropDownList ID="ddl3" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantity:
                <asp:TextBox ID="tbquant3" runat="server" /></label>
            </div>
            <div id="orderitem4" style="display: none">
                <br />
                <label>
                    Type:
                <asp:DropDownList ID="ddl4" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantity:
                <asp:TextBox ID="tbquant4" runat="server" /></label>
            </div>
            <div id="orderitem5" style="display: none">
                <br />
                <label>
                    Type:
                <asp:DropDownList ID="ddl5" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantity:
                <asp:TextBox ID="tbquant5" runat="server" /></label>
            </div>
            <div id="orderitem6" style="display: none">
                <br />
                <label>
                    Type:
                <asp:DropDownList ID="ddl6" runat="server">
                </asp:DropDownList>
                </label>
                <label>
                    Quantity:
                <asp:TextBox ID="tbquant6" runat="server" /></label>
            </div>

        </div>
        <input type="button" id="Button3" onclick="javascript: addFields()" value="Add" />
        <br />
    </form>
    <br />
    <a href="Default.aspx">Back</a>
</body>
</html>
