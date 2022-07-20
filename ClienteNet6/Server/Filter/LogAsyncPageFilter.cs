using ClienteNet6.Server.Context.Model;
using ClienteNet6.Server.Controllers;
using ClienteNet6.Server.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ClienteNet6.Server.Filter
{
    /// <summary>
    /// Log filter
    /// </summary>
    /// <remarks>
    ///     <para>Add log only in GET action results</para>
    /// </remarks>
    internal class LogAsyncActionFilter : IAsyncActionFilter
    {
        private readonly ILogService _logService;

        public LogAsyncActionFilter(ILogService logService)
        {
            _logService = logService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            await OnActionExecutingAsync(context);

            await next();

            await OnActionExecutedAsync(context);
        }

        private async Task OnActionExecutingAsync(ActionExecutingContext context)
        {
            // Don't log login or users
            if (context.Controller.GetType().Equals(typeof(LoginController)) ||
                context.Controller.GetType().Equals(typeof(UsuarioController)))
            {
                return;
            }

            // Log only users auth
            if (!context.HttpContext.User.Claims.Any())
            {
                return;
            }

            try
            {
                var logBd = new Log
                {
                    PageUrl = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "",
                    User =
                        context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email)) is null ?
                        "" :
                        context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email)).Value
                };

                await _logService.SendLog(logBd);
            }
            catch { }

            await Task.CompletedTask;
        }

        private async Task OnActionExecutedAsync(ActionExecutingContext context)
        {
            await Task.CompletedTask;
        }
    }
}
