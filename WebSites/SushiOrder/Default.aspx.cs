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
    string address = ConfigurationManager.AppSettings["RemoteAddress"];
    orderObj = (IOrders) Activator.GetObject(typeof(IOrders), address);

    
  }

  public override void VerifyRenderingInServerForm(Control control)
  {
   
  }

  protected void Button1_Click(object sender, EventArgs e)
  {
      List<Order> ls;

      ls = orderObj.GetOrders(tbnome.Text);


      for (int i = 0; i < ls.Count; i++)
      {
          GridView gv = new GridView();
          List<Order> tmp = new List<Order>();
          tmp.Add(ls[i]);

          gv.DataSource = tmp;
          Page.Controls.Add(gv);

          GridView gv2 = new GridView();
          gv2.DataSource = ls[i].produtos;
          Page.Controls.Add(gv2);
      }
      Page.DataBind();


      
  }
}