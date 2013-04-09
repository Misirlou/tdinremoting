﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Orders : MarshalByRefObject, IOrders
{

    public event AlterDelegate alterEvent;
    int nr;
    private List<Order> AOrders;

    public Orders()
    {
        AOrders = new List<Order>();
        nr = (System.DateTime.Now.Month * 100 + System.DateTime.Now.Day) * 1000; //suporta ate 1000 ordens por dia com nr unico

        Console.WriteLine("[Orders] built.");
    }

    public void Add(string name, string credit, string address)
    {
        Order or = new Order(name, credit, address, nr);
        AOrders.Add(or);
        nr++;
        Console.WriteLine("[Add] called.");
        NotifyClients(OrderState.New, or);
    }

    public void Add(string name, string credit, string address,List<MenuItem> states,List<int> nrs)
    {
        Order or = new Order(name, credit, address, nr);
        AOrders.Add(or);
        nr++;
        Console.WriteLine("[Add] called.");
        for (int i=0;i<states.Count;i++)
        {
            AddItem(or,states[i],nrs[i]);
        }
        NotifyClients(OrderState.New, or);
    }

    public List<Order> GetOrdersByState(OrderState state)
    {
        List<Order> result = new List<Order>();
        foreach (Order or in AOrders)
            if (or.Estado == state)
            {

                result.Add(or);
            }
        Console.WriteLine("[GetOrdersByState] called.");
        return result;
    }

    public List<Order> GetOrders(string name)
    {
        List<Order> result = new List<Order>();

        foreach (Order or in AOrders)
            if (or.Name == name)
                result.Add(or);
        Console.WriteLine("[GetOrders] called.");
        return result;
    }

    public void AddItem(Order or, MenuItem type, int nr)
    {

        or.Add(type, nr);
        Console.WriteLine("[AddOrderItem] called");

    }

    public void ModifyOrder(int num, OrderState state)
    {
        foreach (Order or in AOrders)
        {
            if (or.Nr == num)
            {
                or.Estado = state;
                if (state == OrderState.Preparing)
                {
                    Payment(or);
                }
                NotifyClients(state, or);
                return;
            }
        }
    }

    public void Payment(Order or)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter("pagamentos.txt", true))
        {
            Console.WriteLine("Writing to receipt file: {0}, {1}, {2}, {3}", or.Nr, System.DateTime.Now.ToString(), or.Name, or.CreditCard);
            file.WriteLine("{0}, {1}, {2}, {3}", or.Nr, System.DateTime.Now.ToString(), or.Name, or.CreditCard);
        }


    }

    void NotifyClients(OrderState op, Order or)
    {
        if (alterEvent != null)
        {
            Delegate[] invkList = alterEvent.GetInvocationList();

            foreach (AlterDelegate handler in invkList)
            {
                try
                {
                    IAsyncResult ar = handler.BeginInvoke(op, or, null, null);
                    Console.WriteLine("Invoking event handler");
                }
                catch (Exception)
                {
                    alterEvent -= handler;
                }
            }
        }
    }
}
