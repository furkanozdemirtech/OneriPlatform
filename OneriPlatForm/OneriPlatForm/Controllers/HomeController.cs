using OneriPlatform.BusinessLayer;
using OneriPlatform.BusinessLayer.Results;
using OneriPlatform.Entities;
using OneriPlatform.Entities.ValueObjects;
using System.Web.Mvc;

namespace OneriPlatForm.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private SuggestionUserManager SuggestionUser = new SuggestionUserManager();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResultcs<SuggestionUsers> res = SuggestionUser.LoginUser(model);
                if (res.Result == null)
                {
                    res.AddError(OneriPlatform.Entities.Messages.ErrorMessageCode.UserCouldNotFind, "Kullanıcı Bulunamadı");
                    return View(model);
                }
                Session["login"] = res.Result;
                RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}