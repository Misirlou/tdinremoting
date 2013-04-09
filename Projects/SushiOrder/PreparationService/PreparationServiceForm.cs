﻿using System;
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
        public PreparationServiceForm()
        {
            InitializeComponent();

            
            RemotingConfiguration.Configure("PreparationService.exe.config", false);
            lorders = (IOrders)RemoteNew.New(typeof(IOrders));

            this.label1.Text = "Novas Encomendas";
            this.label2.Text = "Em Preparação";
            
        }


        public void getNewOrders()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.button1.Text.Equals("Por em preparação")){
                int num = (int)this.listBox1.SelectedValue;
                lorders.ModifyOrder(num,OrderState.Preparing);
            }
            this.listBox1.DataSource = lorders.GetOrdersByState(OrderState.New);
            this.listBox1.DisplayMember = "DisplayMember";
            this.listBox1.ValueMember = "Nr";
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.button1.Text = "Por em preparação";
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
