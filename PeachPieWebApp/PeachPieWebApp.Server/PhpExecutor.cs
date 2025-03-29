// Services/PhpFunctionInvoker.cs
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pchp.Core;
using Pchp.Core.Utilities;
using Peachpie.AspNetCore.Web;
using System;
using System.Threading;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;


namespace PeachPieWebApp.Server
{

    public class PhpExecutor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly Peachpie.AspNetCore.Web.IPhpScriptProvider _scriptProvider;
        static AsyncLocal<Context> s_ctx = new AsyncLocal<Context>();
        public PhpExecutor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public object CallPhpFunction(string functionName, params object[] args)
        {
            var httpContext = _httpContextAccessor.HttpContext
                ?? throw new InvalidOperationException("HTTP context not available");

            ContextExtensions.CurrentContextProvider = () => s_ctx.Value ??= Context.CreateEmpty();
            var ctx = ContextExtensions.CurrentContext;
            //Context.AddScriptReference(typeof(CustomFunc).Assembly); // or Assembly.Load("phplib")

            //ctx.Include("", "program.php");
            ctx.Include("", "index.php", once: true, throwOnError: true);

            try
            {
                return ctx.Call(functionName, args);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed to execute PHP function '{functionName}'", ex);
            }
        }


    }
}
