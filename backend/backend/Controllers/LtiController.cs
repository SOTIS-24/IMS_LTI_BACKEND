using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using LtiAdvantage.Lti;

namespace backend.Controllers
{
    [Route("")]
    [ApiController]
    public class LtiController : ControllerBase
    {
        //dodato radi preuzimanja consumer keya
        private readonly IConfiguration _configuration;
        public LtiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    
        [HttpPost("lti_launch")]
        public IActionResult Launch([FromForm] IDictionary<string, string> ltiParams)
        {
            // validacija zadatog consumer keya
            if (!IsCanvasRequestValid(ltiParams))
            {
                return Unauthorized("Invalid LTI parameters");
            }

            return RedirectToPage(ltiParams);
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

        private bool IsCanvasRequestValid(IDictionary<string, string> ltiParams)
        {
            return ltiParams.ContainsKey("oauth_consumer_key") && _configuration["LTI:ConsumerKey"] == ltiParams["oauth_consumer_key"];
        }

        private IActionResult RedirectToPage(IDictionary<string, string> ltiParams)
        {
            string username = ltiParams["custom_canvas_user_login_id"]; //vjer nece uvijek biti email, ali sam pri dodavanju korisnika stavila da je id email
            string userId = ltiParams["custom_canvas_user_id"];
            string courseId = ltiParams["custom_canvas_course_id"]; //da li ove id-jeve zadati u bazi, zakucati
            string courseName = ltiParams["context_title"]; 
            return Redirect("https://192.168.140.1:3000");
        }
    }
}

