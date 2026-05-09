using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace BookstoreApp.Filters
{
    public class AuthPageFilter : IPageFilter
    {
        private readonly string[] _roles;

        public AuthPageFilter(params string[] roles)
        {
            _roles = roles;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
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
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }
    }
}
