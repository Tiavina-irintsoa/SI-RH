using Microsoft.AspNetCore.Mvc;

namespace RH.Controllers{
    public class TestController : Controller{
    public IActionResult Liste()
    {
        HttpContext.Session.SetString( "test" , "bjr" );
        return Redirect( "test" );
    }
    
    public IActionResult test()
    {
        return Redirect( "test" );
    }
}
}