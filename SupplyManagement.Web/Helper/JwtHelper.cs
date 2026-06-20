using System.IdentityModel.Tokens.Jwt;

namespace SupplyManagement.Web.Helper
{
    public static class JwtHelper
    {
        public static string GetRole(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    return "";

                token = token.Replace("Bearer ", "").Trim();

                var handler = new JwtSecurityTokenHandler();

                if (!handler.CanReadToken(token))
                    return "";

                var jwt = handler.ReadJwtToken(token);

                return jwt.Claims.FirstOrDefault(x =>
                    x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                )?.Value ?? "";
            }
            catch
            {
                return "";
            }
        }
    }
}
