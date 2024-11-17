using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using LtiAdvantage.Lti;

namespace backend.Controllers
{
    [Route("api/lti")]
    [ApiController]
    public class LtiController : ControllerBase
    {
        [HttpGet("jwk")]
        public IActionResult GetJwk()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                var publicKey = rsa.ExportParameters(false);
                //gener. jwk
                var jwk = new
                {
                    keys = new[]
                    {
                        new
                        {
                            kty = "RSA",  // tip kljuca
                            use = "sig",  // za potpisivanje
                            kid = "my-key-id",  // id kljuca
                            alg = "RS256",  // alg
                            n = Convert.ToBase64String(publicKey.Modulus!),  
                            e = Convert.ToBase64String(publicKey.Exponent!),  
                        }
                    }
                };

                return Ok(jwk);
            }
        }


        [HttpGet("auth")]
        public IActionResult Auth([FromQuery] string client_id, [FromQuery] string redirect_uri)//, [FromQuery] string state)
        {
            // provjera da li je zahtjev validan... dodati i druge provjere
            if (string.IsNullOrEmpty(client_id) || string.IsNullOrEmpty(redirect_uri))
            {
                return BadRequest("Invalid request parameters");
            }

            // generisem OAuth2 authorization code ili id token
            var authorizationCode = GenerateAuthorizationCode();

            // posalji korisnika na redirect_uri sa authorization code

            //var redirectUrl = $"{redirect_uri}?code={authorizationCode}&state={state}";
            var decodedRedirectUri = Uri.UnescapeDataString(redirect_uri);
            var redirectUrl = $"{redirect_uri}";

            return Redirect(decodedRedirectUri);
        }


        [HttpPost("launch")]
        public IActionResult Launch([FromForm] IDictionary<string, string> ltiParams)
        {
            // validacija lti parametara
            /*if (!ltiParams.ContainsKey("id_token") || !ltiParams.ContainsKey("lti_message_type"))
            {
                return BadRequest("Invalid LTI parameters");
            }

            // obrada lti parmaetara
            var idToken = ltiParams["id_token"];

            // verif. id_tokena
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(idToken) as JwtSecurityToken;

            // izvlacim podatke iz tokena, kao što su korisnički podaci, kurs, itd.
            var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var courseId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "context_id")?.Value;

            // npr vratim odgovor canvasu
            var response = new { message = "LTI Launch successful", userId, courseId };

            return Ok(response);*/

            return Redirect("https://192.168.140.1:3000");
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
