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
    Order ord;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        string address = ConfigurationManager.AppSettings["RemoteAddress"];
        orderObj = (IOrders)Activator.GetObject(typeof(IOrders), address);
        if (!IsPostBack)
        {
            Hashtable ht = GetEnumForBind(typeof(MenuItem));

            ddl1.DataSource = ht;
            ddl1.DataTextField = "value";
            ddl1.DataValueField = "key";
            ddl1.DataBind();
            ddl1.SelectedIndex = 0;

            ddl2.DataSource = ht;
            ddl2.DataTextField = "value";
            ddl2.DataValueField = "key";
            ddl2.DataBind();
            ddl2.SelectedIndex = 0;

            ddl3.DataSource = ht;
            ddl3.DataTextField = "value";
            ddl3.DataValueField = "key";
            ddl3.DataBind();
            ddl3.SelectedIndex = 0;

            ddl4.DataSource = ht;
            ddl4.DataTextField = "value";
            ddl4.DataValueField = "key";
            ddl4.DataBind();
            ddl4.SelectedIndex = 0;

            ddl5.DataSource = ht;
            ddl5.DataTextField = "value";
            ddl5.DataValueField = "key";
            ddl5.DataBind();
            ddl5.SelectedIndex = 0;

            ddl6.DataSource = ht;
            ddl6.DataTextField = "value";
            ddl6.DataValueField = "key";
            ddl6.DataBind();
            ddl6.SelectedIndex = 0;
        }
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
                
                items.Add((MenuItem)Enum.Parse(typeof(MenuItem),dr.SelectedValue));
                
                
            }
            foreach(TextBox nrstxt in this.Page.Form.Controls.OfType<TextBox>())
            {
                if (nrstxt.ID.StartsWith("tbquant"))
                {
                    try
                    {
                        nrs.Add(int.Parse(nrstxt.Text));
                    }
                    catch 
                    {
                        items.RemoveAt(nrs.Count);
                    }
                }
            }

            
            ord= new Order(tbName.Text, tbCCN.Text, tbAddr.Text,0,items,nrs);
            
            labelpreco.Text = "<b>Preço final: " + ord.price.ToString() + "€</b>";
            confirmPreco.Visible = true;
        }
    }
    protected void btPreco_Click(object sender, EventArgs e)
    {
        Button bt = (Button)sender;
        if (bt.ID.Equals("btSim"))
        {
            List<MenuItem> items = new List<MenuItem>();
            List<int> nrs = new List<int>();

            foreach (DropDownList dr in this.Page.Form.Controls.OfType<DropDownList>())
            {

                items.Add((MenuItem)Enum.Parse(typeof(MenuItem), dr.SelectedValue));


            }
            foreach (TextBox nrstxt in this.Page.Form.Controls.OfType<TextBox>())
            {
                if (nrstxt.ID.StartsWith("tbquant"))
                {
                    try
                    {
                        nrs.Add(int.Parse(nrstxt.Text));
                    }
                    catch
                    {
                        items.RemoveAt(nrs.Count);
                    }
                }
            }


            orderObj.Add(tbName.Text, tbCCN.Text, tbAddr.Text, items, nrs);
            Response.Redirect("Default.aspx");

        }
        confirmPreco.Visible = false;
    }
}