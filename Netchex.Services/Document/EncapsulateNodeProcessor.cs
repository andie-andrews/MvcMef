namespace TreeOfMana.Services.Document
{
    using System.ComponentModel.Composition;

    using HtmlAgilityPack;

    using TreeOfMana.Dependencies;
    using TreeOfMana.Dto;

    /// <summary>
    /// The encapsulate node processor used to encapsulate a node within a <see cref="HtmlDocument"/> based on a provided <see cref="DocumentNodeRule"/>.
    /// The class is derived from the <see cref="DocumentNodeProcessor"/> and implements the <see cref="IDocumentProcessor"/> interface.
    /// </summary>
    [Export(typeof(IDocumentProcessor))]
    public class EncapsulateNodeProcessor : DocumentNodeProcessor, IDocumentProcessor
    {
        /// <summary>
        /// Gets the document node action type that is supported by the <see cref="EncapsulateNodeProcessor"/>.
        /// </summary>
        public override DocumentNodeActionTypes DocumentNodeActionType
        {
            get
            {
                return DocumentNodeActionTypes.EncapsulateNode;
            }
        }

        /// <summary>
        /// The process used by the <see cref="EncapsulateNodeProcessor"/> to process the specified <see cref="DocumentNodeRule"/> for the provided <see cref="HtmlDocument"/>.
        /// </summary>
        /// <param name="document">
        /// A <see cref="HtmlDocument"/> to be processed.
        /// </param>
        /// <param name="rule">
        /// The <see cref="DocumentNodeRule"/> used to apply the action for the <see cref="DocumentNodeActionType"/>.
        /// </param>
        /// <returns>
        /// A processed <see cref="HtmlDocument"/>.
        /// </returns>
        protected override HtmlDocument Process(HtmlDocument document, IDocumentNodeRule rule)
        {
            HtmlNodeCollection result = document.DocumentNode.SelectNodes(rule.SelectNodeXPath);
            if (result != null)
            {
                foreach (HtmlNode node in result)
                {
                    HtmlNode underlineNode = HtmlNode.CreateNode(rule.CreateNodeHtml);
                    underlineNode.AppendChild(node.CloneNode(false));
                    node.ParentNode.ReplaceChild(underlineNode, node);
                }
            }

            return document;
        }
    }
}
