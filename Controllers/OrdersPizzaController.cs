using MessagePack;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp_PizzaTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersPizzaController : ControllerBase
    {
        WebAppPizzatimeContext db = new WebAppPizzatimeContext();
        List<OrdersPizza> opList = new List<OrdersPizza>();
        string jsonstring;

        async void createjson(OrdersPizza o)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            jsonstring += JsonSerializer.Serialize<OrdersPizza>(o, options);
        }

        // GET: api/<OrdersPizzaController>
        [HttpGet]
        public string Get()
        {
            jsonstring = "[";
            opList = db.OrdersPizzas.ToList();
            for (int i = 0; i < opList.Count; i++)
            {
                createjson(opList[i]);
                if (0 < opList.Count - 1) { jsonstring += ","; }
            }
            jsonstring += "]";
            return jsonstring;
        }

        // GET api/<OrdersPizzaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            jsonstring = "[";
            opList = db.OrdersPizzas.ToList();
            for (int i = 0; i < opList.Count; i++)
            {
                if (opList[i].Id == id) { createjson(opList[i]); }
            }
            jsonstring += "]";
            return jsonstring;
        }

        // POST api/<OrdersPizzaController>
        [HttpPost]
        public void Post(int id_pizza, string customer,int count_pizza)
        { Order o;
            List<Pizza> pl = db.Pizzas.ToList();
            for (int i = 0; i < pl.Count; i++) { if (id_pizza == pl[i].Id) 
            { o = new Order { Customer = customer, Summa = Convert.ToSingle(count_pizza * pl[i].Price) };
              db.Orders.Add(o);
              db.SaveChanges();
                } }
            List<Order> ol= db.Orders.ToList();
            OrdersPizza op = new OrdersPizza { Orderid = ol[ol.Count-1].Id,Pizzaid=id_pizza, Count=count_pizza};
            db.OrdersPizzas.Add(op);
            db.SaveChanges();

        }

        //// PUT api/<OrdersPizzaController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<OrdersPizzaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            opList = db.OrdersPizzas.ToList();
            for (int i = 0; i < opList.Count; i++)
            {
                if (opList[i].Id == id)
                {

                    //удаляем объект
                    db.OrdersPizzas.Remove(opList[i]);
                    db.SaveChanges();
                }
            }
        }
    }
}
