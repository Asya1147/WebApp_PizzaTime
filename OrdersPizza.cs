using System;
using System.Collections.Generic;

namespace WebApp_PizzaTime;

public partial class OrdersPizza
{
    public int Id { get; set; }

    public int Orderid { get; set; }

    public int Pizzaid { get; set; }

    public int Count { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Pizza Pizza { get; set; } = null!;
}
