using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace BookstoreApp.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public AuthFilter(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRole = context.HttpContext.Session.GetString("UserRole");
            
            if (string.IsNullOrEmpty(userRole))
            {
                context.Result = new RedirectToPageResult("/Account/Login");
                return;
            }

            if (_roles.Length > 0 && !System.Array.Exists(_roles, r => r == userRole))
            {
                context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
