using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
  IOrders orderObj;

  protected void Page_Load(object sender, EventArgs e) {
    if (!IsPostBack)                                        // first load in a session
      GridView1.Visible = false;
    string address = ConfigurationManager.AppSettings["RemoteAddress"];
    orderObj = (IOrders) Activator.GetObject(typeof(IOrders), address);
  }

  protected void Button1_Click(object sender, EventArgs e)
  {
      List<Order> ls;

      ls = orderObj.GetOrders("pete");
      GridView1.DataSource = ls;
      GridView1.DataBind();
      GridView1.Visible = true;
      if (ls.Count > 0)
      {
          List<OrderItem> ls2 = ls[0].produtos;
          GridView2.DataSource = ls2;
          GridView2.DataBind();
          GridView2.Visible = true;
      }
  }
}