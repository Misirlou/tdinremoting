using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PlaceOrder : System.Web.UI.Page
{
    IOrders orderObj;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        string address = ConfigurationManager.AppSettings["RemoteAddress"];
        orderObj = (IOrders)Activator.GetObject(typeof(IOrders), address);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (true)
        {



            orderObj.Add(tbName.Text,tbCCN.Text,tbAddr.Text);

        }
    }
}