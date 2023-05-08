using System;
using System.Collections.Generic;

namespace WebApp_PizzaTime;

public partial class Order
{
    public int Id { get; set; }

    public string Customer { get; set; } = null!;

    public float Summa { get; set; }

    public virtual ICollection<OrdersPizza> OrdersPizzas { get; set; } = new List<OrdersPizza>();
}
