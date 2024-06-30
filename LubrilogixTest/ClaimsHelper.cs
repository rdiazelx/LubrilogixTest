using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;


public static class ClaimsHelper
{
    public static async Task<List<string>> GetUserClaimsAsync(HttpContext httpContext)
    {
        var groupClaims = new List<string>();

        if (httpContext.User.Identity.IsAuthenticated)
        {
            var idToken = await httpContext.GetTokenAsync("id_token");

            if (!string.IsNullOrEmpty(idToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(idToken);

                groupClaims = jwtToken.Claims
                    .Where(c => c.Type == "groups")
                    .Select(c => c.Value)
                    .ToList();
            }
        }

        return groupClaims;
    }
}
