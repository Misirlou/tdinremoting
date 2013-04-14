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

namespace DeliveryService
{
    public partial class DeliveryServiceForm : Form
    {
        IOrders lorders;
        AlterEventProxy evproxy;
        string equipa;

        public DeliveryServiceForm()
        {
            InitializeComponent();

            equipa = Prompt.ShowDialog("Nome da equipa", "prompt");
            RemotingConfiguration.Configure("DeliveryService.exe.config", false);
            lorders = (IOrders)RemoteNew.New(typeof(IOrders));
            evproxy = new AlterEventProxy();
            evproxy.alterEvent += new AlterDelegate(doAlterations);
            lorders.alterEvent += new AlterDelegate(evproxy.Repeater);
            this.Text = "Delivery Equipa: " + equipa;
            this.label1.Text = "Ready";
            this.label2.Text = "Em entrega";
            this.button1.Text = "Sem Encomendas";
            this.label3.Text = "Info Detalhada:";

            UpdateDelivering();

        }

        public void doAlterations(OrderState state, Order or)
        {
            switch (state)
            {
                case OrderState.Ready: UpdateReady(); break;
                case OrderState.Delivering: UpdateDelivering(); break;
                case OrderState.Completed: UpdateCompleted(); break;
            }

            if (this.listBox1.Items.Count == 0 && this.listBox2.Items.Count == 0)
                this.button1.Text = "Sem Encomendas";

        }


        public void UpdateReady()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(UpdateReady));
            else
            {
                this.listBox1.DataSource = lorders.GetOrdersByState(OrderState.Ready);
                this.listBox1.DisplayMember = "DisplayMember";
                this.listBox1.ValueMember = "Nr";

                if (this.listBox1.SelectedValue == null && this.listBox2.SelectedValue == null) this.textBox1.Text = "";
            }
        }

        public void UpdateDelivering()
        {

            if (InvokeRequired)
                Invoke(new MethodInvoker(UpdateDelivering));
            else
            {
                this.listBox1.DataSource = lorders.GetOrdersByState(OrderState.Ready);
                this.listBox1.DisplayMember = "DisplayMember";
                this.listBox1.ValueMember = "Nr";

                this.listBox2.DataSource = lorders.GetOrdersByState(OrderState.Delivering,equipa);
                this.listBox2.DisplayMember = "DisplayMember";
                this.listBox2.ValueMember = "Nr";

                if (this.listBox1.SelectedValue == null && this.listBox2.SelectedValue == null) this.textBox1.Text = "";
            }
        }

        public void UpdateCompleted()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(UpdateCompleted));
            else
            {
                this.listBox2.DataSource = lorders.GetOrdersByState(OrderState.Delivering,equipa);
                this.listBox2.DisplayMember = "DisplayMember";
                this.listBox2.ValueMember = "Nr";
                if (this.listBox1.SelectedValue == null && this.listBox2.SelectedValue == null) this.textBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.Text.Equals("Por em entrega"))
            {
                if (this.listBox1.SelectedValue != null)
                {
                    int num = (int)this.listBox1.SelectedValue;
                    lorders.ModifyOrder(num, OrderState.Delivering,equipa);
                }
            }
            else if (this.button1.Text.Equals("Completar"))
            {
                if (this.listBox2.SelectedValue != null)
                {
                    int num = (int)this.listBox2.SelectedValue;
                    lorders.ModifyOrder(num, OrderState.Completed);
                }

            }

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.button1.Text = "Por em entrega";
            updateTextBox((Order)listBox1.SelectedItem);

        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            this.button1.Text = "Completar";
            updateTextBox((Order)listBox2.SelectedItem);
        }

        private void updateTextBox(Order o)
        {
            if (o != null)
            {
                string str = "";
                str += "Nr: " + o.Nr.ToString() + "\r\nNome: " + o.Name + "\r\nMorada: " + o.Address + "\r\nNumero cartao: " + o.CreditCard + "\r\nEstado: " + Enum.GetName(typeof(OrderState), o.Estado) + "\r\n";
                foreach (OrderItem oi in o.produtos)
                {
                    str += "\r\n" + Enum.GetName(typeof(MenuItem), oi.Type) + ": " + oi.Nr.ToString();
                }
                this.textBox1.Text = str;
            }
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


// copiado de http://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
public static class Prompt
{
    public static string ShowDialog(string text, string caption)
    {
        Form prompt = new Form();
        prompt.Width = 500;
        prompt.Height = 150;
        prompt.Text = caption;
        Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
        TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
        Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.Controls.Add(textBox);
        prompt.ShowDialog();
        return textBox.Text;
    }
}