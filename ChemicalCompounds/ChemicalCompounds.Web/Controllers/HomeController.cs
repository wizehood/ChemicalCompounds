using ChemicalCompounds.DataAccessLayer.Context;
using Serilog;
using System.Web.Mvc;

namespace ChemicalCompounds.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Log.Information("GET Home/Index triggered");

            return View();
        }
    }
}