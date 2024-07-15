using LubrilogixTest.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace WebApp_OpenIDConnect_DotNet.Controllers
{

    [Authorize]
    public class LubrilogixController : Controller
    {

        private readonly ILogger<LubrilogixController> _logger;
        private readonly LubrilogixDbContext _context;

        public LubrilogixController(ILogger<LubrilogixController> logger, LubrilogixDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        protected async Task<List<string>> GetGroupClaimsAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var groupClaims = await ClaimsHelper.GetUserClaimsAsync(HttpContext);
                return groupClaims;
            }

            return new List<string>(); // Return an empty list or handle accordingly if user is not authenticated
        }

    


        public IActionResult Inicio()
        {
            return View();
        }

        public async Task<IActionResult> Inventario()
        {
            ViewBag.GroupClaims = await GetGroupClaimsAsync();
            return View();
        }

        public async Task<IActionResult> Proveedores()
        {
            ViewBag.GroupClaims = await GetGroupClaimsAsync();
            return View();
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
            return View();
        }






    }
}
