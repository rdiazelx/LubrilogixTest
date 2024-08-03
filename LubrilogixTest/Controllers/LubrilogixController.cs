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
using AspNetCore;


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

        public async Task<IActionResult> Prueba()
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


        public async Task<IActionResult> UpdateOrderState(int tnIdOrden)
        {
            if (tnIdOrden <= 0)
            {
                return BadRequest("Invalid order ID.");
            }

            try
            {
                await _context.UpdateEstadoAsync(tnIdOrden);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error updating order state for TN_IdOrden: {TN_IdOrden}", tnIdOrden);

                // Return a detailed error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        public async Task<IActionResult> AgregarProducto([FromBody] Producto producto)
        {
            // Verificar que los datos del producto no sean nulos ni estén vacíos
            if (producto == null || string.IsNullOrWhiteSpace(producto.TcNombre) ||
                string.IsNullOrWhiteSpace(producto.TcCategoria) || string.IsNullOrWhiteSpace(producto.TcSubcategoria))
            {
                return BadRequest("Invalid product data.");
            }

            try
            {
                // Intentar insertar el producto en la base de datos llamando al método asincrónico en el contexto de la base de datos
                await _context.AgregarProductoAsync(producto);
                return Ok("Producto agregado exitosamente.");
            }
            catch (Exception ex)
            {
                // Registrar la excepción con un mensaje de error detallado
                _logger.LogError(ex, "Error inserting product: {Producto}", producto);

                // Retornar una respuesta de error con un código de estado 500
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        public async Task<IActionResult> AgregarSucursal([FromBody] Sucursale sucursal)
        {
            if (sucursal == null)
            {
                return BadRequest("Sucursal data is null.");
            }

            if (string.IsNullOrEmpty(sucursal.TcNombre) ||
                string.IsNullOrEmpty(sucursal.TcProvincia) ||
                string.IsNullOrEmpty(sucursal.TcDireccion) ||
                string.IsNullOrEmpty(sucursal.TcTelefono) ||
                string.IsNullOrEmpty(sucursal.TcCorreo) ||
                string.IsNullOrEmpty(sucursal.TcEstado) ||
                string.IsNullOrEmpty(sucursal.TcComentarios))
            {
                return BadRequest("All fields are required.");
            }

            try
            {
                // Llama al método del contexto para agregar la sucursal
                await _context.AgregarSurcursalAsync(sucursal);

                return Ok("Sucursal agregada exitosamente.");
            }
            catch (Exception ex)
            {
                // Loguea el error
                _logger.LogError(ex, "Error al agregar la sucursal.");

                // Devuelve una respuesta de error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        public async Task<IActionResult> AgregarProveedor([FromBody] Proveedore proveedor)
        {
            // Verifica si los datos del proveedor son nulos
            if (proveedor == null)
            {
                return BadRequest("Proveedor data is null.");
            }

            // Verifica si todos los campos requeridos están presentes
            if (string.IsNullOrEmpty(proveedor.TcNombre) ||
                string.IsNullOrEmpty(proveedor.TcProvincia) ||
                string.IsNullOrEmpty(proveedor.TcDireccion) ||
                string.IsNullOrEmpty(proveedor.TcEmail) ||
                string.IsNullOrEmpty(proveedor.TcEstado))
            {
                return BadRequest("All fields are required.");
            }

            try
            {
                // Llama al método del contexto para agregar el proveedor
                await _context.AgregarProveedorAsync(proveedor);

                return Ok("Proveedor agregado exitosamente.");
            }
            catch (Exception ex)
            {
                // Loguea el error
                _logger.LogError(ex, "Error al agregar el proveedor.");

                // Devuelve una respuesta de error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /*Este procedimiento se paso para HomeController*/
        //public async Task<IActionResult> RegistroProveedor([FromBody] Proveedore registroProveedor)
        //{
        //    // Verifica si los datos del proveedor son nulos
        //    if (registroProveedor == null)
        //    {
        //        return BadRequest("Proveedor inválido.");
        //    }

        //    // Verifica si todos los campos requeridos están presentes
        //    if (string.IsNullOrEmpty(registroProveedor.TcNombre) ||
        //        string.IsNullOrEmpty(registroProveedor.TcProvincia) ||
        //        string.IsNullOrEmpty(registroProveedor.TcDireccion) ||
        //        string.IsNullOrEmpty(registroProveedor.TcEmail))
        //    {
        //        return BadRequest("Todos los campos son requeridos.");
        //    }

        //    try
        //    {
        //        // Llama al método del contexto para registrar el proveedor
        //        await _context.RegistroProveedorAsync(new Proveedore
        //        {
        //            TcNombre = registroProveedor.TcNombre,
        //            TcProvincia = registroProveedor.TcProvincia,
        //            TcDireccion = registroProveedor.TcDireccion,
        //            TcEmail = registroProveedor.TcEmail
        //            // TcEstado se establecerá en "inactivo" en el método RegistroProveedorAsync
        //        });

        //        return Ok("Proveedor agregado exitosamente.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Loguea el error
        //        _logger.LogError(ex, "Error al agregar el proveedor.");

        //        // Devuelve una respuesta de error
        //        return StatusCode(500, $"Error al agregar el proveedor: {ex.Message}");
        //    }
        //}
    }

    }









    