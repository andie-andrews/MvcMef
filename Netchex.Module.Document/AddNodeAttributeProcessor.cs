namespace Netchex.Module.Document
{
    using System.ComponentModel.Composition;
    using System.Linq;

    using HtmlAgilityPack;

    using Netchex.Dependencies;
    using Netchex.Dto;

    /// <summary>
    /// The add node attribute document processor used to add an attribute to nodes within a <see cref="HtmlDocument"/> based on a provided <see cref="Data.Models.DocumentNodeRule"/>.
    /// The class is derived from the <see cref="DocumentNodeProcessor"/> and implements the <see cref="IDocumentProcessor"/> interface.
    /// </summary>
    [Export(typeof(IDocumentProcessor))]
    public class AddNodeAttributeProcessor : DocumentNodeProcessor, IDocumentProcessor
    {
        /// <summary>
        /// Gets the document node action type that is supported by the <see cref="AddNodeAttributeProcessor"/>.
        /// </summary>
        public override ActionTypes ActionType
        {
            get
            {
                return ActionTypes.AddNodeAttribute;
            }
        }

        /// <summary>
        /// The process used by the <see cref="AddNodeAttributeProcessor"/> to process the specified <see cref="Data.Models.DocumentNodeRule"/> for the provided <see cref="HtmlDocument"/>.
        /// </summary>
        /// <param name="document">
        /// A <see cref="HtmlDocument"/> to be processed.
        /// </param>
        /// <param name="rule">
        /// The <see cref="Data.Models.DocumentNodeRule"/> used to apply the action for the <see cref="ActionType"/>.
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
                    if (node.Attributes.Any(attrib => attrib.Name == rule.NodeAttributeName))
                    {
                        node.SetAttributeValue(rule.NodeAttributeName, rule.NodeAttributeValue);
                    }
                    else
                    {
                        node.Attributes.Add(rule.NodeAttributeName, rule.NodeAttributeValue);
                    }
                }
            }

            return document;
        }
    }
}
