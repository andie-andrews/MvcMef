using System.ComponentModel.Composition;
using System.Web.Mvc;
using MvcMef.Dependencies;
using MvcMef.Web;
using MvcMef.Models;
namespace MvcMef.Controllers
{
    [ControllerExportAttribute(typeof(HomeController))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class HomeController : Controller, IMefController
    {

        private ViewModelProvider viewModelProvider;

        [ImportingConstructor]
        public HomeController(ViewModelProvider viewModelProvider)
        {
            this.viewModelProvider = viewModelProvider;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "MvcMef Test.";

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