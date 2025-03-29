using Microsoft.AspNetCore.Http;
using System;

namespace PeachPieWebApp.Server
{
    public class PhpService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhpService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Greet(string name)
        {

            var context = _httpContextAccessor.HttpContext;
            var phpContext = context?.Features.Get<IPhpContextFeature>()?.Context;

            if (phpContext == null)
            {
                throw new InvalidOperationException("PHP context not available");
            }


            var result = phpContext.Call("greet", name);

            return result;
        }

        //    public int Calculate(string operation, int x, int y) =>
        //        (int)_httpContextAccessor.Call($"Calculator::{operation}", x, y);
        
    }
}
