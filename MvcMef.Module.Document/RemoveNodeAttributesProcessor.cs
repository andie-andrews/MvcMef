namespace Netchex.Module.Document
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using HtmlAgilityPack;

    using Netchex.Dependencies;
    using Netchex.Dto;

    /// <summary>
    /// The remove node attributes processor used to remove attributes from nodes within a <see cref="HtmlDocument"/> based on a provided <see cref="Data.Models.DocumentNodeRule"/>.
    /// The class is derived from the <see cref="DocumentNodeProcessor"/> and implements the <see cref="IDocumentProcessor"/> interface.
    /// </summary>
    [Export(typeof(IDocumentProcessor))]
    public class RemoveNodeAttributesProcessor : DocumentNodeProcessor, IDocumentProcessor
    {
        /// <summary>
        /// Gets the document node action type that is supported by the <see cref="RemoveNodeAttributesProcessor"/>.
        /// </summary>
        public override ActionTypes ActionType
        {
            get
            {
                return ActionTypes.RemoveNodeAttribute;
            }
        }

        /// <summary>
        /// The process used by the <see cref="RemoveNodeAttributesProcessor"/> to process the specified <see cref="Data.Models.DocumentNodeRule"/> for the provided <see cref="HtmlDocument"/>.
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
                    if (string.IsNullOrWhiteSpace(rule.NodeAttributeName))
                    {
                        if (string.IsNullOrWhiteSpace(rule.NodeAttributeExclusions))
                        {
                            node.Attributes.RemoveAll();
                        }
                        else
                        {
                            HtmlAttribute exclusionAttribute = node.Attributes.FirstOrDefault(attrib => attrib.Name == rule.NodeAttributeExclusions);

                            node.Attributes.RemoveAll();

                            if (exclusionAttribute != null)
                            {
                                node.Attributes.Add(exclusionAttribute);
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(rule.NodeAttributeExclusions))
                        {
                            node.Attributes[rule.NodeAttributeName].Remove();
                        }
                        else
                        {
                            throw new Exception("Cannot remove a specified attribute and have an exlcusion attribute");
                        }
                    }
                }
            }

            return document;
        }
    }
}
