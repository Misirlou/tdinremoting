using System;
using System.Collections.Generic;

[Serializable]
public class Order
{

    public int Nr { get; set; }
    public string Name { get; set; }
    public string CreditCard { get; set; }
    public string Address { get; set; }
    public List<OrderItem> produtos;
    public OrderState Estado { get; set; }
    public String DisplayMember { get; set; }
    public String DeliveryTeam { get; set; }
    public double price { get; set; }

    public Order(string name, string CreditCard, string Address, int nr)
    {
        Name = name;
        price = 0;
        this.CreditCard = CreditCard;
        this.Address = Address;
        produtos = new List<OrderItem>();
        Nr = nr;
        DeliveryTeam = "";
        Estado = OrderState.New;
        DisplayMember = "Nr: " + Nr + " Name: " + Name;
    }

    public void Add(MenuItem name, int nr)
    {
        produtos.Add(new OrderItem(name, nr));
        switch (name)
        {
            case MenuItem.Aji: price+=1.5*nr;break;
            case MenuItem.AmaEbi: price += 2 * nr; break;
            case MenuItem.Anago: price += 1.5 * nr; break;
            case MenuItem.Awabi: price += 0.5 * nr; break;
            case MenuItem.Ebi: price += 0.5 * nr; break;
            case MenuItem.Hamachi: price += 1 * nr; break;
            case MenuItem.Hirame: price += 2.5 * nr; break;
            case MenuItem.Hokkigai: price += 2 * nr; break;
            case MenuItem.Hotate: price += 1 * nr; break;
            case MenuItem.Ika: price += 1.5 * nr; break;
            case MenuItem.Ikura: price += 3 * nr; break; 
        }
    }




}

[Serializable]
public class OrderItem
{
    public MenuItem Type { get; set; }
    public int Nr { get; set; }

    public OrderItem(MenuItem type, int nr)
    {
        Type = type;
        Nr = nr;

    }


}

public enum MenuItem { Aji, AmaEbi, Anago, Awabi, Ebi, Hamachi, Hirame, Hokkigai, Hotate, Ika, Ikura };

public enum OrderState { New, Preparing, Ready, Delivering, Completed };

public delegate void AlterDelegate(OrderState op, Order or);

public interface IOrders
{
    event AlterDelegate alterEvent;

    //void Add(string name, string credit,string address);//deprecated
    void Add(string name, string credit, string address, List<MenuItem> items, List<int> nrs);

    //void AddItem(string name, MenuItem type, int nr);//deprecated
    List<Order> GetOrdersByState(OrderState state);
    List<Order> GetOrdersByState(OrderState state, string equipa);

    List<Order> GetOrders(string name);
    void ModifyOrder(int num, OrderState state);
    void ModifyOrder(int num, OrderState state, String equipa);
}


public class AlterEventProxy : MarshalByRefObject
{
    public event AlterDelegate alterEvent;

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public void Repeater(OrderState op, Order or)
    {
        if (alterEvent != null)
            alterEvent(op, or);
    }
}
