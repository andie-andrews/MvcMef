using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netchex.Dependencies;
using Netchex.Dependencies.Models;
using System.ComponentModel.Composition;

namespace Netchex.Services
{
    [Export(typeof(IEmployeeService))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class EmployeeService : IEmployeeService
    {
        [Import]
        public ExportFactory<IRepositoryWorker> RepositoryWorkerFactory { get; set; }
        protected IRepositoryWorker repositoryWorker
        {
            get { return RepositoryWorkerFactory.CreateExport().Value; }
        }
        public IEmployee GetItem(int? id)
        {
            IEmployee employee = null;
            if (id != null)
                employee = this.repositoryWorker.Repository<Data.Employee, Dto.Employee>().FindById(id);

            return employee ?? new Dto.Employee();
        }
        public List<IEmployee> GetItems()
        {
            return this.repositoryWorker.Repository<Data.Employee, Dto.Employee>().Query().Include(x => x.Frequency).Get().ToList<IEmployee>();
        }

        public IEmployee Update(string employee)
        {
            Dto.Employee data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dto.Employee>(employee);
            if (data.Id == 0)
            {
                Data.Employee entity = this.repositoryWorker.Repository<Data.Employee, Dto.Employee>().Insert(data);
                data.Id = entity.Id;
            }
            else
            {
                this.repositoryWorker.Repository<Data.Employee, Dto.Employee>().Update(data);

            }
            return data;
        }

    }
}
