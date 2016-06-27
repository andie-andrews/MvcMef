using System.ComponentModel.Composition;
using System.Web.Mvc;
using Netchex.Dependencies;
using Netchex.Web;
using Netchex.Models;
namespace Netchex.Controllers
{
    [ControllerExportAttribute(typeof(HomeController))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class HomeController : Controller
    {

        private ViewModelProvider viewModelProvider;

        [ImportingConstructor]
        public HomeController(ViewModelProvider viewModelProvider)
        {
            this.viewModelProvider = viewModelProvider;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Netchex Test.";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}