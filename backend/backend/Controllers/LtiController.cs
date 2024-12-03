using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using LtiAdvantage.Lti;
using backend.IServices;

namespace backend.Controllers
{
    [Route("")]
    [ApiController]
    public class LtiController : ControllerBase
    {
        //dodato radi preuzimanja consumer keya
        
        private readonly ILtiService _service;
        public LtiController(ILtiService service)
        {
            _service = service;
        }

    
        [HttpPost("lti_launch")]
        public IActionResult Launch([FromForm] IDictionary<string, string> ltiParams)
        {
            // validacija zadatog consumer keya
            if (!_service.IsCanvasRequestValid(ltiParams))
            {
                return Unauthorized("Invalid LTI parameters");
            }

            return Redirect(_service.GetRedirectionUrl(ltiParams));
        }
        

        private string GenerateAuthorizationCode()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] buffer = new byte[16]; 
                rng.GetBytes(buffer);
                return Convert.ToBase64String(buffer); 
            }
        }

       

       
       
    }
}

