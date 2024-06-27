using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication2.Filter;

public class AuthFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!string.IsNullOrEmpty(context.HttpContext.Session.GetString("Username")) ||
            HasAllowAnonymous(context) == true) return;
        context.Result = new RedirectToActionResult("ShowLogin", "Home", null);
    }

    private bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        var endpoint = context.HttpContext.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
        {
            return true;
        }

        return false;
    }
}