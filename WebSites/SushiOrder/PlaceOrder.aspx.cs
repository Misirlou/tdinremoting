using System;
using System.Collections;
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

        Hashtable ht = GetEnumForBind(typeof(MenuItem));

        ddlasd1.DataSource = ht;
        ddlasd1.DataTextField = "value";
        ddlasd1.DataValueField = "key";
        ddlasd1.DataBind();
        ddlasd1.SelectedValue = "0";
        
    }

    public Hashtable GetEnumForBind(Type enumeration)
    {
        string[] names = Enum.GetNames(enumeration);
        Array values = Enum.GetValues(enumeration);
        Hashtable ht = new Hashtable();
        for (int i = 0; i < names.Length; i++)
        {
            ht.Add(Convert.ToInt32(values.GetValue(i)).ToString(), names[i]);
        }
        return ht;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (true)
        {
            List<MenuItem> items=new List<MenuItem>();
            List<int> nrs=new List<int>();
            
            foreach (DropDownList dr in this.Page.Form.Controls.OfType<DropDownList>())
            {
                
                items.Add((MenuItem)Enum.Parse(typeof(MenuItem),dr.SelectedItem.Text));
                
                
            }
            foreach(TextBox nrstxt in this.Page.Form.Controls.OfType<TextBox>())
            {
                if (nrstxt.ID.StartsWith("tbquant"))
                {
                    nrs.Add(int.Parse(nrstxt.Text));
                }
            }

            
            orderObj.Add(tbName.Text, tbCCN.Text, tbAddr.Text,items,nrs);
        }
    }
}