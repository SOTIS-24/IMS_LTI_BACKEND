using Microsoft.AspNetCore.Mvc;

namespace backend.IServices
{
    public interface ILtiService
    {
        public bool IsCanvasRequestValid(IDictionary<string, string> ltiParams);
        public string GetRedirectionUrl(IDictionary<string, string> ltiParams);
    }
}
