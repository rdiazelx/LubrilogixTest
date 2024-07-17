using LubrilogixTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.SecurityNamespace;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;


namespace WebApp_OpenIDConnect_DotNet.Controllers
{

    [Authorize]
    public class LubrilogixController : Controller
    {

        private readonly ILogger<LubrilogixController> _logger;
        private readonly LubrilogixDbContext _context;
        private readonly IMemoryCache _cache;

        public LubrilogixController(ILogger<LubrilogixController> logger, LubrilogixDbContext context, IMemoryCache cache)
        {
            _logger = logger;
            _context = context;
            _cache = cache;
        }

        protected async Task<List<string>> GetGroupClaimsAsync()
        {
            if (User.Identity.IsAuthenticated)
            {

                var cacheKey = "proveedoresCache";
                if (!_cache.TryGetValue(cacheKey, out List<Proveedore> proveedores))
                {
                    proveedores = await _context.GetProveedoresAsync();
                    _cache.Set(cacheKey, proveedores, TimeSpan.FromMinutes(30)); // Set expiration as needed
                }


                var groupClaims = await ClaimsHelper.GetUserClaimsAsync(HttpContext);
                return groupClaims;

         
            }

            return new List<string>(); // Return an empty list or handle accordingly if user is not authenticated
        }
        public IActionResult Inicio()
        {
            return View();
        }

        public async Task<IActionResult> vistaInventario()
        {
            ViewBag.GroupClaims = await GetGroupClaimsAsync();

            return View();
        }

        public async Task<IActionResult> Proveedores()
        {
            ViewBag.GroupClaims = await GetGroupClaimsAsync();

            _logger.LogInformation("Fetching providers."); // Logging the action

            // Cache key for proveedores
            var cacheKey = "proveedoresCache";

            // Check if the data is in cache
            if (!_cache.TryGetValue(cacheKey, out List<Proveedore> proveedores))
            {
                // If not in cache, fetch from the database and cache it
                proveedores = await _context.GetProveedoresAsync();
                _cache.Set(cacheKey, proveedores, TimeSpan.FromMinutes(30)); // Set expiration as needed
            }

            return View(proveedores);
        }

        public async Task<IActionResult> Productos()
        {
            ViewBag.GroupClaims = await GetGroupClaimsAsync();

            _logger.LogInformation("Fetching products."); // Logging the action

            var productos = await _context.GetProductosAsync();
            return View(productos);
        }

        public async Task<IActionResult> Sucursales()
        {
            ViewBag.GroupClaims = await GetGroupClaimsAsync();

            var sucursales = await _context.GetSucursalesAsync();
            return View(sucursales);
        }

        public async Task<IActionResult> Inventario()
        {
            ViewBag.GroupClaims = await GetGroupClaimsAsync();

            var inventario = await _context.spLeerInventario();

            return View(inventario);
        }

        public async Task<IActionResult> TotalInventory()
        {
            ViewBag.GroupClaims = await GetGroupClaimsAsync();

            var inventario = await _context.spLeerInventario();

            // Filter and calculate the total inventory
            var totalInventory = inventario
                .Where(i => i.TC_Estado == "Aprobado")
                .GroupBy(i => i.TN_IdProducto)
                .Select(g => new
                {
                    ProductoNombre = g.FirstOrDefault().ProductoNombre,
                    TotalCantidad = g.Sum(i => i.TN_IDTipoOperacion == 1 ? i.TN_Cantidad: 0) -  // Compras
                                    g.Sum(i => i.TN_IDTipoOperacion == 2 ? i.TN_Cantidad : 0) +  // Ventas
                                    g.Sum(i => i.TN_IDTipoOperacion == 3 ? i.TN_Cantidad : 0) +  // Devoluciones
                                    g.Sum(i => i.TN_IDTipoOperacion == 4 ? i.TN_Cantidad : 0)      // Ajustes
                })
                .ToList();

            return View(totalInventory);
        }


        public async Task<IActionResult> inventarioPendiente()
        {
            ViewBag.GroupClaims = await GetGroupClaimsAsync();

            var inventario = await _context.spLeerInventario();

            // Filter the inventory for "pendiente" status
            var pendingInventory = inventario
                .Where(i => i.TC_Estado == "Pendiente")
                .Select(i => new
                {
                    i.TN_IdOrden,
                    i.TF_Fecha,
                    i.SucursalNombre,
                    i.ProductoNombre,
                    i.TN_Cantidad
                })
                .ToList();

            return View(pendingInventory);
        }



    }
}
