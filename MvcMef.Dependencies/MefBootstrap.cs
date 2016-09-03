using System.ComponentModel.Composition;

namespace MvcMef.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    public static class MefBootstrap
    {
        public static CompositionContainer Container { get; private set; }

        public static bool IsIntialized { get; private set; }
        public static void Intialize()
        {
            
            try
            {
                if (!IsIntialized)
                {
                    
                List<string> files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "MvcMef*.dll", SearchOption.AllDirectories)
              .Where(o => !o.Replace(AppDomain.CurrentDomain.BaseDirectory, "").Contains(@"obj\")).ToList();
                var catalogAggregator = new AggregateCatalog();

                foreach (var file in files)
                {
                    catalogAggregator.Catalogs.Add(new AssemblyCatalog(Assembly.LoadFrom(file)));
                }

                Container = new CompositionContainer(catalogAggregator);

                Container.ComposeParts(Container);
                IsIntialized = true;

                }
            }
            catch (Exception)
            {
                IsIntialized = false;
                throw;
            }
          


        }
    }
}