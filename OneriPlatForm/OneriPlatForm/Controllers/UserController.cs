using OneriPlatform.Entities.ValueObjects;
using System.Web.Mvc;

namespace OneriPlatForm.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return RedirectToAction("/Home/Index");
        }
        public ActionResult Logout()
        {
            return View();
        }
        [Authorize]
        public ActionResult Register()
        {
            return View();
        }
    }
}