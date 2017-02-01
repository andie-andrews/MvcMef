namespace MvcMef.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using MvcMef.Dependencies;
    using Module.Document;

    [Export(typeof(IDocumentService))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public class DocumentService : IDocumentService
    {
        private readonly IRepositoryWorker repositoryWorker;

        [ImportingConstructor]
        public DocumentService(IDocumentParser documentParser, IRepositoryWorker repositoryWorker)
        {
            this.DocumentParser = documentParser;
            this.repositoryWorker = repositoryWorker;
        }

        protected DocumentParser DocumentParser { get; private set; }

        public string CleanDocument(string dataToParse)
        {
            this.DocumentParser.LoadDocument(dataToParse);
            IList<IDocumentNodeRule> rules = this.repositoryWorker.Repository<MvcMef.Data.Rule, Dto.DocumentNodeRule>().Query().Include(x => x.ActionType).Get()
               .ToList<IDocumentNodeRule>();

            return this.DocumentParser.CleanDocument(rules);
        }
    }
}
