using System.Web.Mvc;
using System.ComponentModel.Composition;
using MvcMef.Web;
using MvcMef.Dependencies;
using MvcMef.Models;
using MvcMef.Models.Employee;
using System.Collections.Generic;
using System.Linq;

namespace MvcMef.Controllers
{

    [ControllerExport(typeof(EmployeeController))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    [Authorize]
    public class EmployeeController : Controller, IMefController
    {
      
        private ViewModelProvider viewModelProvider;
      
        [ImportingConstructor]
        public EmployeeController(ViewModelProvider viewModelProvider)
        {
            this.viewModelProvider = viewModelProvider;
           
        }
        public ActionResult Index()
        {
            var vm = this.viewModelProvider.ProvideViewModel(typeof(EmployeesViewModel)) as EmployeesViewModel;

            return View(vm);
        }
        public ActionResult Calculate(int? id)
        {
            var vm = this.viewModelProvider.ProvideViewModel(typeof(EmployeeViewModel)) as EmployeeViewModel;
            vm.IntializeEmployee(id);
            return View(vm);
        }
        public ActionResult Edit(int? id)
        {
            var vm = this.viewModelProvider.ProvideViewModel(typeof(EmployeeViewModel)) as EmployeeViewModel;
            vm.IntializeEmployee(id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection values)
        {

            string json = ToJson(values, new[] { "Employee.", "__RequestVerificationToken" });
            
            var vm = this.viewModelProvider.ProvideViewModel(typeof(EmployeeViewModel)) as EmployeeViewModel;
            vm.Update(json);
            return View(vm);
        }

        public static string ToJson(System.Web.Mvc.FormCollection collection, string[] prefixIgnore)
        {
            var list = new Dictionary<string, string>();
            foreach (string key in collection.Keys)
            {
                string name = key;
                prefixIgnore.ToList().ForEach(ignore => name = name.Replace(ignore, string.Empty));
                if (!string.IsNullOrWhiteSpace(name))
                {
                    string value = collection[key];
                    value = value.Replace("true,false", "true");
                    list.Add(name, value);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
    }
}