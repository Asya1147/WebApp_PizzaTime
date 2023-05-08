using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp_PizzaTime.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        WebAppPizzatimeContext db = new WebAppPizzatimeContext();
        List<Order> orderList = new List<Order>();
        string jsonstring;

        async void createjson(Order o)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            jsonstring += JsonSerializer.Serialize<Order>(o, options);
        }

        // GET: api/<OrderController>
        [HttpGet]
        public string Get()
        {
            jsonstring = "[";
            orderList = db.Orders.ToList();
            for (int i = 0; i < orderList.Count; i++)
            {
                createjson(orderList[i]);
                if (0 < orderList.Count - 1) { jsonstring += ","; }
            }
            jsonstring += "]";
            return jsonstring;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            jsonstring = "[";
            orderList = db.Orders.ToList();
            for (int i = 0; i < orderList.Count; i++)
            {
                if (orderList[i].Id == id) { createjson(orderList[i]); }
            }
            jsonstring += "]";
            return jsonstring;
        }

        // POST api/<OrderController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}
        

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id,string new_customer)
        {
            orderList = db.Orders.ToList();
            for (int i = 0; i < orderList.Count; i++)
            {
                if (orderList[i].Id == id)
                {
                    orderList[i].Customer = new_customer;
                    db.SaveChanges();
                }
            }
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            orderList = db.Orders.ToList();
            for (int i = 0; i < orderList.Count; i++)
            {
                if (orderList[i].Id == id)
                {
                    
                    //удаляем объект
                    db.Orders.Remove(orderList[i]);
                    db.SaveChanges();
                }
            }
        }
    }
}
