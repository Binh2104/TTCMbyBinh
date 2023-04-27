using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace QBTourDuLich.Models.Authenciation
{
    public class Authenciation_Admin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Access" },
                        {"Action", "Login" }
                    });
            }
            else if (context.HttpContext.Session.GetString("Loai") != "1")
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Home" },
                        {"Action", "noPremission" }
                    });
            }
        }
    }
}
