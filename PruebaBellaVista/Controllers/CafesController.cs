using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PruebaBellaVista.Models;
using System.Text;
using System.Text.Json;

namespace PruebaBellaVista.Controllers
{
    public class CafesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CafesController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7093/");
        }

        public async Task<IActionResult> Index()
        {
            List<Producto> listaProductos = new List<Producto>();
            HttpResponseMessage respuesta = await _httpClient.GetAsync("api/Productoes");

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                listaProductos = JsonSerializer.Deserialize<List<Producto>>(contenido, opciones);
            }

            return View(listaProductos);
        }

        public async Task<IActionResult> InventarioCompleto()
        {
            List<Producto> listaProductos = new List<Producto>();
            HttpResponseMessage respuesta = await _httpClient.GetAsync("api/Productoes/Todos");

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                listaProductos = JsonSerializer.Deserialize<List<Producto>>(contenido, opciones);
            }

            return View(listaProductos);
        }

        public IActionResult Create()
        {
            CargarListaTipos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            producto.Activo = true;
            var json = JsonSerializer.Serialize(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Productoes", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Error de API ({response.StatusCode}): {errorMsg}");
            CargarListaTipos();
            return View(producto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = new Producto();
            var response = await _httpClient.GetAsync($"api/Productoes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<Producto>(content, opciones);
            }

            CargarListaTipos();
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.IdProducto) return NotFound();

            var json = JsonSerializer.Serialize(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Productoes/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Error al actualizar ({response.StatusCode}): {errorMsg}");
            CargarListaTipos();
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Reactivar(int id)
        {
            var responseGet = await _httpClient.GetAsync($"api/Productoes/{id}");
            if (responseGet.IsSuccessStatusCode)
            {
                var content = await responseGet.Content.ReadAsStringAsync();
                var producto = JsonSerializer.Deserialize<Producto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                producto.Activo = true;

                var json = JsonSerializer.Serialize(producto);
                var contentPut = new StringContent(json, Encoding.UTF8, "application/json");
                await _httpClient.PutAsync($"api/Productoes/{id}", contentPut);
            }

            return RedirectToAction("InventarioCompleto");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var producto = new Producto();
            var response = await _httpClient.GetAsync($"api/Productoes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<Producto>(content, opciones);
            }

            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Productoes/{id}");
            return RedirectToAction("Index");
        }

        private void CargarListaTipos()
        {
            List<SelectListItem> listaTipos = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Arábica" },
                new SelectListItem { Value = "2", Text = "Robusta" },
                new SelectListItem { Value = "3", Text = "Mezcla" }
            };
            ViewBag.TiposCafe = listaTipos;
        }
    }
}