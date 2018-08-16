using Junior.DataAccessLayer.Context;
using System.Web.Mvc;

namespace Junior.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}