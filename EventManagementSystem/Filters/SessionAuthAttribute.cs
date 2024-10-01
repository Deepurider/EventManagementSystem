using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SessionAuthAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var username = context.HttpContext.Session.GetString("username");
        if (string.IsNullOrEmpty(username))
        {
            context.Result = new RedirectToActionResult("Login", "User", null);
        }
    }
}
