using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.IO;

namespace BookstoreApp.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var message = $"[{System.DateTime.Now}] Exception: {exception.Message}\nStackTrace: {exception.StackTrace}\n";
            File.AppendAllText("error.log", message);

            context.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
                {
                    Model = new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier }
                }
            };
            context.ExceptionHandled = true;
        }
    }
}
