using MarketPlace.Host.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Host.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using var request = await httpClient.GetAsync("http://localhost:51887/api/product");
                string response = await request.Content.ReadAsStringAsync();

                products = JsonConvert.DeserializeObject<List<Product>>(response);

            }

            return View(products);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                using var request = await httpClient.PostAsync($"http://localhost:51887/api/product", content);

                string response = await content.ReadAsStringAsync();

            }
            return RedirectToAction("Index");
        }

        
        public async Task<ActionResult<Product>> Edit(int id)
        {
            Product product = new Product();

            using (var httpClient = new HttpClient())
            {
                using var request = await httpClient.GetAsync($"http://localhost:51887/api/product/{id}");

                string response = await request.Content.ReadAsStringAsync();

                product = JsonConvert.DeserializeObject<Product>(response);
            }

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Edit(Product product)
        {

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                using var request = await httpClient.PutAsync($"http://localhost:51887/api/product/{product.id}", content);

                string response = await content.ReadAsStringAsync();
            }

            return RedirectToAction("Index");
        }
        

        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {                
                using var request = await httpClient.DeleteAsync($"http://localhost:51887/api/product/{id}");                
            }
            return RedirectToAction("Index");
        }

    }
}
