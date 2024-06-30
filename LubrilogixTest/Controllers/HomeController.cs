using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using LubrilogixTest.Models;

namespace Lubrilogix.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            return View();
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
    }
}