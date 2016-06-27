namespace TreeOfMana.Services.Document
{
    using System.ComponentModel.Composition;
    using System.Linq;

    using HtmlAgilityPack;

    using TreeOfMana.Dependencies;
    using TreeOfMana.Dto;

    using DocumentNodeRule = TreeOfMana.Data.Models.DocumentNodeRule;

    /// <summary>
    /// The remove node processor used to remove nodes within a <see cref="HtmlDocument"/> based on a provided <see cref="TreeOfMana.Data.Models.DocumentNodeRule"/>.
    /// The class is derived from the <see cref="DocumentNodeProcessor"/> and implements the <see cref="IDocumentProcessor"/> interface.
    /// </summary>
    [Export(typeof(IDocumentProcessor))]
    public class RemoveNodeProcessor : DocumentNodeProcessor, IDocumentProcessor
    {
        /// <summary>
        /// Gets the document node action type that is supported by the <see cref="RemoveNodeProcessor"/>.
        /// </summary>
        public override DocumentNodeActionTypes DocumentNodeActionType
        {
            get
            {
                return DocumentNodeActionTypes.RemoveNode;
            }
        }

        /// <summary>
        /// The process used by the <see cref="RemoveNodeProcessor"/> to process the specified <see cref="TreeOfMana.Data.Models.DocumentNodeRule"/> for the provided <see cref="HtmlDocument"/>.
        /// </summary>
        /// <param name="document">
        /// A <see cref="HtmlDocument"/> to be processed.
        /// </param>
        /// <param name="rule">
        /// The <see cref="TreeOfMana.Data.Models.DocumentNodeRule"/> used to apply the action for the <see cref="DocumentNodeActionType"/>.
        /// </param>
        /// <returns>
        /// A processed <see cref="HtmlDocument"/>.
        /// </returns>
        protected override HtmlDocument Process(HtmlDocument document, IDocumentNodeRule rule)
        {
            return this.RemoveNodes(document, rule.SelectNodeXPath);
        }

        /// <summary>
        /// This method removes all elements from the document specified by the xpath
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        /// <param name="xpath">
        /// The xpath.
        /// </param>
        /// <returns>
        /// The <see cref="HtmlDocument"/> with the specified nodes removed.
        /// </returns>
        private HtmlDocument RemoveNodes(HtmlDocument document, string xpath)
        {
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);

            if (nodes != null)
            {
                for (int index = 0; index < nodes.Count(); index++)
                {
                    HtmlNode node = nodes[index];
                    node.Remove();
                }
            }

            return document;
        }
    }
}
