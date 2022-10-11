using ContactDiary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Text;

namespace ContactDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly AgendaTelefonicaContext _conexion;

        public AutenticationController(IConfiguration config, AgendaTelefonicaContext context)
        {
            _secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
            _conexion = context;
        }

        [HttpPost]
        public IActionResult Validate(Usuario user)
        {
            var userLogged = _conexion.Usuarios.Where(
                obj => obj.Usuario1 == user.Usuario1 && obj.Clave == user.Clave).FirstOrDefault();

            bool userFinded = (userLogged != null ? true : false);

            if (!userFinded)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = ""});
            }
            else
            {
                var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim( new Claim(ClaimTypes.NameIdentifier, user.Usuario1) );

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), 
                    SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken( tokenDescriptor );

                string tokenCreated = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreated });
            }
        }
    }
}
