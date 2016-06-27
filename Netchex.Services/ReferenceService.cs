using System;
using Netchex.Dependencies;
using System.ComponentModel.Composition;
using Netchex.Dependencies.Models;
using System.Collections.Generic;
using System.Linq;

namespace Netchex.Services
{
    [Export(typeof(IReferenceService))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class ReferenceService : IReferenceService
    {
        private readonly IRepositoryWorker repositoryWorker;

        [ImportingConstructor]
        public ReferenceService(IRepositoryWorker repositoryWorker)
        {
            this.repositoryWorker = repositoryWorker;
        }

        public List<ILookupReference> GetFrequencyReferenceList()
        {
            return this.repositoryWorker.Repository<Data.Frequency, Dto.LookupReference>().Query().Get().ToList<ILookupReference>();

        }
    }
}
