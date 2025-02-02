using Microsoft.AspNetCore.Mvc;

namespace RealState.Presentation.Controllers
{
    [Route("erroes/{code}")]
    public class ErrorsController : Controller
    {
        public IActionResult Error(int code)
        {
            if (code == 401)
                return RedirectToAction("Login", "Account");
            else if (code == 403)
                return RedirectToAction("AccessDenied", "Account");
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Unauthorization()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
