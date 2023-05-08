using System;
using System.Collections.Generic;
using WebApp_PizzaTime;

namespace WebApp_PizzaTime;

public partial class Pizza
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public virtual ICollection<OrdersPizza> OrdersPizzas { get; set; } = new List<OrdersPizza>();
}


