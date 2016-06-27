using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netchex.Models
{
    using System.ComponentModel.Composition;

    [Export(typeof(ViewModelProvider))]
    public class ViewModelProvider
    {
        [ImportMany(typeof(IViewModel))]
        protected List<IViewModel> ViewModels { get; set; }

        public ViewModelProvider()
        {

        }
        public IViewModel ProvideViewModel(Type viewModelType)
        {
            return ViewModels.FirstOrDefault(x => x.GetType() == viewModelType);
        }
    }
}