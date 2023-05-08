using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp_PizzaTime.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        WebAppPizzatimeContext db = new WebAppPizzatimeContext();
        List<Pizza> pizzaList = new List<Pizza>();
        string jsonstring;

        async void createjson(Pizza p)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            jsonstring += JsonSerializer.Serialize<Pizza>(p, options); 
        }

        // GET: <PizzaController>
        [HttpGet]
        public string Get()
        {
            jsonstring = "[";
            pizzaList = db.Pizzas.ToList();
            for (int i=0; i<pizzaList.Count;i++) {
                createjson(pizzaList[i]);
                if (0 < pizzaList.Count - 1) { jsonstring += ","; }
            }
            jsonstring +="]";
            return jsonstring;
        }


        // GET <PizzasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            jsonstring = "[";
            pizzaList = db.Pizzas.ToList();
            for (int i = 0; i < pizzaList.Count; i++)
            {
                if (pizzaList[i].Id == id) {createjson(pizzaList[i]); }
            }
            jsonstring += "]";
            return jsonstring;
        }

        // POST <PizzasController>
        [HttpPost]
        public void Post(string name, double price)
        {
            Pizza p = new Pizza { Name=name, Price=price };
            db.Pizzas.Add(p);
            db.SaveChanges();

        }

        // PUT <PizzasController>/5
        [HttpPut("{id}")]
        public void Put(int id, string new_name, double new_price)
        {
            pizzaList = db.Pizzas.ToList();
            for (int i = 0; i < pizzaList.Count; i++)
            {
                if (pizzaList[i].Id == id)
                {
                    pizzaList[i].Name = new_name;
                    pizzaList[i].Price = new_price;

                    //обновляем объект
                    //db.Users.Update(user);
                    db.SaveChanges();
                }
            }

            
        }

        // DELETE <PizzasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            pizzaList = db.Pizzas.ToList();
            for (int i = 0; i < pizzaList.Count; i++)
            {
                if (pizzaList[i].Id == id) { 
                
                //удаляем объект
                db.Pizzas.Remove(pizzaList[i]);
                db.SaveChanges();
            } 
            }
            
        }
    }
}
