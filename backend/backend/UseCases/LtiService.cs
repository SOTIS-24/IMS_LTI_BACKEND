using backend.IServices;
using backend.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.UseCases
{
    public class LtiService : ILtiService
    {
        private readonly IConfiguration _configuration;

        public LtiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool IsCanvasRequestValid(IDictionary<string, string> ltiParams)
        {
            return ltiParams.ContainsKey("oauth_consumer_key") && _configuration["LTI:ConsumerKey"] == ltiParams["oauth_consumer_key"];
        }

        public string GetRedirectionUrl(IDictionary<string, string> ltiParams)
        {
            string username = ltiParams["custom_canvas_user_login_id"]; //vjer nece uvijek biti email, ali sam pri dodavanju korisnika stavila da je id email
            string userId = ltiParams["custom_canvas_user_id"];
            string courseId = ltiParams["custom_canvas_course_id"]; //da li ove id-jeve zadati u bazi, zakucati
            string courseName = ltiParams["context_title"];
            Role role = GetRole(ltiParams["roles"]);
            
            //za sad studenta vodim na prikaz testova samo, a prof na test form
            if(role.Equals(Role.Learner))
            {
                return "https://192.168.140.1:3000";
            }
            else
            {
                return "https://192.168.140.1:3000/test-form";
            }
            
        }
        
        private Role GetRole(string roleParam)
        {
            if(roleParam.Contains("Instructor")) return Role.Instructor;
            else return Role.Learner;
        }
    }
}
