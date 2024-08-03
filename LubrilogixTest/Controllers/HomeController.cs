using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using LubrilogixTest.Models;
using Microsoft.EntityFrameworkCore;




namespace Lubrilogix.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LubrilogixDbContext _context;


        public HomeController(ILogger<HomeController> logger, LubrilogixDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        //public async Task<IActionResult> Index()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var accessToken = await HttpContext.GetTokenAsync("access_token");
        //        var idToken = await HttpContext.GetTokenAsync("id_token");
        //        var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

        //        // Logging tokens for debugging
        //        _logger.LogInformation($"Access Token: {accessToken}");
        //        _logger.LogInformation($"ID Token: {idToken}");
        //        _logger.LogInformation($"Refresh Token: {refreshToken}");

        //        if (!string.IsNullOrEmpty(idToken))
        //        {
        //            var handler = new JwtSecurityTokenHandler();
        //            var jwtToken = handler.ReadJwtToken(idToken);

        //            // Extract claims from the ID token
        //            var claims = jwtToken.Claims;

        //            // Example: Get a specific claim
        //            var nameClaim = claims.FirstOrDefault(c => c.Type == "name")?.Value;

        //            ViewBag.Name = nameClaim;
        //            ViewBag.Claims = claims;
        //        }
        //        else
        //        {
        //            ViewBag.Message = "ID token is null";
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "User is not authenticated";
        //    }

        //    return View();
        //}

        private async Task SetUserClaimsInViewBag()
        {
            if (User.Identity.IsAuthenticated)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var idToken = await HttpContext.GetTokenAsync("id_token");
                var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

                // Logging tokens for debugging
                _logger.LogInformation($"Access Token: {accessToken}");
                _logger.LogInformation($"ID Token: {idToken}");
                _logger.LogInformation($"Refresh Token: {refreshToken}");

                if (!string.IsNullOrEmpty(idToken))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(idToken);

                    // Extract claims from the ID token
                    var claims = jwtToken.Claims;

                    // Example: Get a specific claim
                    var nameClaim = claims.FirstOrDefault(c => c.Type == "name")?.Value;

                    // Extract group claims
                    var groupClaims = claims.Where(c => c.Type == "groups").Select(c => c.Value).ToList();

                    ViewBag.GroupClaims = groupClaims;
                    ViewBag.Name = nameClaim;
                    ViewBag.Claims = claims;
                }
                else
                {
                    ViewBag.Message = "ID token is null";
                }
            }
            else
            {
                ViewBag.Message = "User is not authenticated";
            }
        }

        public async Task<IActionResult> Index()
        {

            await SetUserClaimsInViewBag();

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Home", new { returnUrl = "/Lubrilogix/Inicio" });
            }


            return View();
        }



        public IActionResult SignIn(string returnUrl = "/")
        {
            // If the user is authenticated, redirect to the specified returnUrl
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }

            // Otherwise, redirect to the login page
            return Challenge(); // This will initiate the authentication process
        }

        public async Task<IActionResult> userClaims()
        {
            var groupClaims = await ClaimsHelper.GetUserClaimsAsync(HttpContext);

            ViewBag.GroupClaims = groupClaims;


            await SetUserClaimsInViewBag();
            return View();
        }


        public IActionResult Soporte()
        {
            return View();
        }

        public IActionResult RegistroProveedores()
        {
            return View();
        }




        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #region Registro de proveedor
        [HttpPost]
        public async Task<IActionResult> RegistroProveedor([FromBody] Proveedore registroProveedor)
        {
            // Verifica si los datos del proveedor son nulos
            if (registroProveedor == null)
            {
                return BadRequest("Proveedor inválido.");
            }

            // Verifica si todos los campos requeridos están presentes
            if (string.IsNullOrEmpty(registroProveedor.TcNombre) ||
                string.IsNullOrEmpty(registroProveedor.TcProvincia) ||
                string.IsNullOrEmpty(registroProveedor.TcDireccion) ||
                string.IsNullOrEmpty(registroProveedor.TcEmail))
            {
                return BadRequest("Todos los campos son requeridos.");
            }

            try
            {
                // Llama al método del contexto para registrar el proveedor
                await _context.RegistroProveedorAsync(new Proveedore
                {
                    TcNombre = registroProveedor.TcNombre,
                    TcProvincia = registroProveedor.TcProvincia,
                    TcDireccion = registroProveedor.TcDireccion,
                    TcEmail = registroProveedor.TcEmail
                    // TcEstado se establecerá en "inactivo" en el método RegistroProveedorAsync
                });

                return Ok("Proveedor agregado exitosamente.");
            }
            catch (Exception ex)
            {
                // Loguea el error
                _logger.LogError(ex, "Error al agregar el proveedor.");

                // Devuelve una respuesta de error
                return StatusCode(500, $"Error al agregar el proveedor: {ex.Message}");
            }
        }
        #endregion
    }
}