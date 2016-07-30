using System;

namespace Netchex.Module.Document
{
    using System.ComponentModel.Composition;

    using HtmlAgilityPack;

    using Netchex.Dependencies;
    using Netchex.Dto;

    [Export(typeof(IDocumentProcessor))]
    class ReplaceDocumentNodeProcessor : DocumentNodeProcessor, IDocumentProcessor
    {
        public override ActionTypes ActionType
        {
            get
            {
                return ActionTypes.ReplaceNode;
            }
        }

        protected override HtmlDocument Process(HtmlDocument document, IDocumentNodeRule rule)
        {
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(rule.SelectNodeXPath);
            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    HtmlNode newNode = HtmlNode.CreateNode(rule.CreateNodeHtml);
                    newNode.InnerHtml= node.InnerHtml;
                    node.ParentNode.ReplaceChild(newNode, node);
                }
            }

            return document;

        }
    }
}
