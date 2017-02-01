namespace MvcMef.Module.Document
{
    using HtmlAgilityPack;

    using MvcMef.Dependencies;
    using MvcMef.Dto;

    /// <summary>
    /// The DocumentProcessor interface.
    /// </summary>
    public interface IDocumentProcessor
    {
        /// <summary>
        /// Method used to indicate whether the processor can process the specified <see cref="ActionTypes"/>
        /// </summary>
        /// <param name="actionType">
        /// The document node action type.
        /// </param>
        /// <returns>
        /// A <see cref="bool"/> value determining whether the processor is applicable or not.
        /// </returns>
        bool CanProcessDocument(ActionTypes actionType);

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
        /// </returns>
        /// <exception cref="Exception">
        /// If the <see cref="DocumentNodeActionType"/> of the rule parameter is not the supported action type an exception will be thrown.
        /// </exception>
        HtmlDocument ProcessHtmlDocument(HtmlDocument document, IDocumentNodeRule rule);
    }
}
