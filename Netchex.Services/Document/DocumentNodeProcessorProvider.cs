using System;
using System.Collections.Generic;
using System.Linq;

namespace GeauxBiz.Common.Utilities.Html
{
    using System.ComponentModel.Composition;

    using TreeOfMana.Data.Models;
    using TreeOfMana.Dto;
    using TreeOfMana.Services.Document;

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
        /// <param name="documentNodeActionType">
        /// The document node action type.
        /// </param>
        /// <returns>
        /// A <see cref="IDocumentProcessor"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// If there are not any processors then a processor can be provided. Only one processer can be specified for each <see cref="DocumentNodeActionType"/>
        /// </exception>
        public IDocumentProcessor ProvideProcessor(DocumentNodeActionTypes documentNodeActionType)
        {
            if (this.AvailableProcessors == null || !this.AvailableProcessors.Any())
            {
                throw new Exception("No processors available");
            }

           return this.AvailableProcessors.First(processor => processor.CanProcessDocument(documentNodeActionType));
        }

    }
}
