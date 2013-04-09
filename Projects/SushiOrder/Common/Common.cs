using System;
using System.Collections.Generic;

[Serializable]
public class Order {

    public int Nr { get; set; }
    public string Name { get; set; }
    public string CreditCard { get; set; }
    public string Address { get; set; }
    public List<OrderItem> produtos;
	public OrderState Estado{get;set;}

  public Order(string name, string CreditCard,string Address,int nr) {
    Name = name;
    this.CreditCard = CreditCard;
    this.Address = Address;
    produtos = new List<OrderItem>();
    Nr = nr;
	Estado=OrderState.New;
  }

    public void Add(MenuItem name,int nr)
    {
        produtos.Add(new OrderItem(name, nr));
    }

}

[Serializable]
public class OrderItem {
    public MenuItem Type { get; set; }
    public int Nr{ get; set; }

    public OrderItem(MenuItem type, int nr)
    {
        Type = type;
        Nr = nr;

    }


}

public enum MenuItem {Aji,AmaEbi,Anago,Awabi ,Ebi ,Hamachi , Hirame ,Hokkigai ,Hotate ,Ika ,Ikura  };

public enum OrderState {New,Preparing,Ready,Delivering,Completed};

public delegate void AlterDelegate(OrderState op, Order or);

public interface IOrders
{
  event AlterDelegate alterEvent;

  void Add(string name, string credit,string address);
  void AddItem(string name, MenuItem type, int nr);
  List<Order> GetOrdersByState(OrderState state);
  List<Order> GetOrders(string name);
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
