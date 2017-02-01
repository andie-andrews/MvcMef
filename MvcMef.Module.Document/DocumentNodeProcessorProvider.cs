namespace MvcMef.Module.Document
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using MvcMef.Dto;

    /// <summary>
    /// The document node processor provider provides the instance needed to process a document node rule.
    /// </summary>
    [Export(typeof(DocumentNodeProcessorProvider))]
    public class DocumentNodeProcessorProvider
    {
        /// <summary>
        /// Gets or sets the available processors.
        /// </summary>
        [ImportMany(typeof(IDocumentProcessor))]
        public IEnumerable<IDocumentProcessor> AvailableProcessors { get; private set; }

        /// <summary>
        /// Provides a processor designated to process a document node action.
        /// </summary>
        /// <param name="actionType">
        /// The document node action type.
        /// </param>
        /// <returns>
        /// A <see cref="IDocumentProcessor"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// If there are not any processors then a processor can be provided. Only one processer can be specified for each <see cref="DocumentNodeActionType"/>
        /// </exception>
        public IDocumentProcessor ProvideProcessor(ActionTypes actionType)
        {
            if (this.AvailableProcessors == null || !this.AvailableProcessors.Any())
            {
                return null;
            }

           return this.AvailableProcessors.FirstOrDefault(processor => processor.CanProcessDocument(actionType));
        }

    }
}
