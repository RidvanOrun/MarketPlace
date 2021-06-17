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
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Category> categories = new List<Category>();

            using (var httpClient = new HttpClient())
            {
                using var request = await httpClient.GetAsync("http://localhost:51887/api/category");
                string response = await request.Content.ReadAsStringAsync();

                categories = JsonConvert.DeserializeObject<List<Category>>(response);

            }

            return View(categories);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {          

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using var request = await httpClient.PostAsync($"http://localhost:51887/api/category", content);

                string response = await content.ReadAsStringAsync();

            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult<Category>> Edit(int id)
        {
            Category category = new Category();

            using (var httpClient = new HttpClient())
            {
                using var request = await httpClient.GetAsync($"http://localhost:51887/api/category/{id}");

                string response = await request.Content.ReadAsStringAsync();

                category = JsonConvert.DeserializeObject<Category>(response);
            }

            return View(category);
        }


        [HttpPost]
        public async Task<ActionResult<Category>> Edit(Category category)
        {            

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using var request = await httpClient.PutAsync($"http://localhost:51887/api/category/{category.id}", content);

                string response = await content.ReadAsStringAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using var request = await httpClient.DeleteAsync($"http://localhost:51887/api/category/{id}");
            }

            return RedirectToAction("Index");
        }

    }
}
