using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PreparationService
{
    public partial class PreparationServiceForm : Form
    {
        IOrders lorders;
        AlterEventProxy evproxy;

        public PreparationServiceForm()
        {
            InitializeComponent();


            RemotingConfiguration.Configure("PreparationService.exe.config", false);
            lorders = (IOrders)RemoteNew.New(typeof(IOrders));
            evproxy = new AlterEventProxy();
            evproxy.alterEvent += new AlterDelegate(doAlterations);
            lorders.alterEvent += new AlterDelegate(evproxy.Repeater);

            this.label1.Text = "Novas Encomendas";
            this.label2.Text = "Em Preparação";
            this.button1.Text = "Por em preparação";

        }

        public void doAlterations(OrderState state, Order or)
        {
            switch (state)
            {
                case OrderState.New: UpdateNew(); break;
                case OrderState.Preparing: UpdatePreparing(); break;
                case OrderState.Ready: UpdateReady(); break;
            }

        }


        public void UpdateNew()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(UpdateNew));
            else
            {
                this.listBox1.DataSource = lorders.GetOrdersByState(OrderState.New);
                this.listBox1.DisplayMember = "DisplayMember";
                this.listBox1.ValueMember = "Nr";
            }
        }

        public void UpdatePreparing()
        {

            if (InvokeRequired)
                Invoke(new MethodInvoker(UpdatePreparing));
            else
            {
                this.listBox1.DataSource = lorders.GetOrdersByState(OrderState.New);
                this.listBox1.DisplayMember = "DisplayMember";
                this.listBox1.ValueMember = "Nr";

                this.listBox2.DataSource = lorders.GetOrdersByState(OrderState.Preparing);
                this.listBox2.DisplayMember = "DisplayMember";
                this.listBox2.ValueMember = "Nr";
            }
        }

        public void UpdateReady()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(UpdateReady));
            else
            {
                this.listBox2.DataSource = lorders.GetOrdersByState(OrderState.Preparing);
                this.listBox2.DisplayMember = "DisplayMember";
                this.listBox2.ValueMember = "Nr";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.Text.Equals("Por em preparação"))
            {
                if (this.listBox1.SelectedValue != null)
                {
                    int num = (int)this.listBox1.SelectedValue;
                    lorders.ModifyOrder(num, OrderState.Preparing);
                }
            }
            else if (this.button1.Text.Equals("Enviar para entrega"))
            {
                if (this.listBox2.SelectedValue != null)
                {
                    int num = (int)this.listBox2.SelectedValue;
                    lorders.ModifyOrder(num, OrderState.Ready);
                }

            }

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.button1.Text = "Por em preparação";
            updateTextBox((Order)listBox1.SelectedItem);

        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            this.button1.Text = "Enviar para entrega";
            updateTextBox((Order)listBox2.SelectedItem);
        }

        private void updateTextBox(Order o)
        {

            string str = "";
            str += "Nr: " + o.Nr.ToString() + "\r\nNome: " + o.Name + "\r\nMorada: " + o.Address + "\r\nNumero cartao: " + o.CreditCard + "\r\nEstado: " + Enum.GetName(typeof(OrderState), o.Estado)+"\r\n";
            foreach (OrderItem oi in o.produtos)
            {
                str += "\r\n" + Enum.GetName(typeof(MenuItem), oi.Type) + ": " + oi.Nr.ToString();
            }
            this.textBox1.Text = str;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

    }
}


/* Mechanism for instanciating a remote object through its interface, using the config file */

class RemoteNew
{
    private static Hashtable types = null;

    private static void InitTypeTable()
    {
        types = new Hashtable();
        foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
            types.Add(entry.ObjectType, entry);
    }

    public static object New(Type type)
    {
        if (types == null)
            InitTypeTable();
        WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry)types[type];
        if (entry == null)
            throw new RemotingException("Type not found!");
        return RemotingServices.Connect(type, entry.ObjectUrl);
    }
}
