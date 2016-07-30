namespace Netchex.Module.Document
{
    using System;

    using HtmlAgilityPack;

    using Netchex.Dependencies;
    using Netchex.Dto;

    /// <summary>
    /// This is a base abstract class used to create document node processor.
    /// </summary>
    public abstract class DocumentNodeProcessor
    {
        /// <summary>
        /// Gets the document node action type that the processor supports. This cannot conflict with any other processor.
        /// </summary>
        public abstract ActionTypes ActionType { get; }

        /// <summary>
        /// Method used to indicate whether the processor can process the specified <see cref="ActionType"/>
        /// </summary>
        /// <param name="actionType">
        /// The document node action type.
        /// </param>
        /// <returns>
        /// A <see cref="bool"/> value determining whether the processor is applicable or not.
        /// </returns>
        public bool CanProcessDocument(ActionTypes actionType)
        {
            return this.ActionType == actionType;
        }

        /// <summary>
        /// The public method that provides access to process the actions needed for the instance of the processor.
        /// </summary>
        /// <param name="document">
        /// The document to be processed.
        /// </param>
        /// <param name="rule">
        /// The rule needed by the processer to perform the specified action.
        /// </param>
        /// <returns>
        /// The <see cref="Response{T}"/> of type <see cref="HtmlDocument"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// If the <see cref="ActionType"/> of the rule parameter is not the supported action type an exception will be thrown.
        /// </exception>
        public HtmlDocument ProcessHtmlDocument(HtmlDocument document, IDocumentNodeRule rule)
        {
            if (!this.CanProcessDocument((ActionTypes)rule.DocumentNodeActionTypeId))
            {
                throw new Exception("Document node processor does not support document node action provided");
            }

            HtmlDocument processedDocument = this.Process(document, rule);
            return processedDocument;
        }

        /// <summary>
        /// The process method that is implemented in derived classes to process the document processing action.
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        /// <param name="rule">
        /// The <see cref="DocumentNodeRule"/>.
        /// </param>
        /// <returns>
        /// The <see cref="HtmlDocument"/>.
        /// </returns>
        protected abstract HtmlDocument Process(HtmlDocument document, IDocumentNodeRule rule);
    }
}
