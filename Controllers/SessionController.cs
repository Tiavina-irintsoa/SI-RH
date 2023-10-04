using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class SessionController : Controller
{
    protected bool CookieIdAdminExists()
    {
        return HttpContext.Request.Cookies.ContainsKey("idadmin");
    }
}