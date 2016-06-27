namespace TreeOfMana.Services.Document
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using GeauxBiz.Common.Utilities.Html;

    using HtmlAgilityPack;

    using TreeOfMana.Dependencies;
    using TreeOfMana.Dto;

    /// <summary>
    /// Object representation of an html string providing various utilities
    /// </summary>
    [Export(typeof(IDocumentParser))]
    public class DocumentParser : IDocumentParser
    {
        #region Private Members

        /// <summary>
        /// Private HtmlDocument class instance that will be performing various operations against the html string
        /// </summary>
        private HtmlDocument document;

        #endregion

        #region ctor
       
        public DocumentParser()
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the document node processor provider. 
        /// </summary>
        [Import(typeof(DocumentNodeProcessorProvider))]
        protected DocumentNodeProcessorProvider DocumentNodeProcessorProvider { get; set; }

        #region Public Methods

        public void LoadDocument(string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                throw new ArgumentNullException("html");
            }

            this.document = new HtmlDocument();
            this.document.LoadHtml(html);
        }

        /// <summary>
        /// Sets an attribute's value for given node collection
        /// </summary>
        /// <param name="nodes">Collection of HtmlNodeCollection instances for which the attributes will be set</param>
        /// <param name="attribute">Name of attribute</param>
        /// <param name="value">Value of attribute</param>
        public void SetAttribute(HtmlNodeCollection nodes, string attribute, string value)
        {
            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    node.SetAttributeValue(attribute, value);
                }
            }
        }

        /// <summary>
        /// Sets the autocomplete attribute where applicable
        /// </summary>
        /// <param name="value">Value of autocomplete attribute to set</param>
        /// <remarks>
        /// Value of autocomplete attribute may vary between browsers, i.e. Chrome; therefore,
        /// it is up to the caller to decide.
        /// </remarks>
        public void SetAutoCompleteAttribute(string value)
        {
            HtmlNodeCollection inputNodes = this.document.DocumentNode.SelectNodes("//input");
            this.SetAttribute(inputNodes, "autocomplete", value);
        }

        /// <summary>
        /// Method used to determine if the document is clean HTML without the script and style elements along with all elements containing no attributes.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCleanDocument()
        {
            HtmlNodeCollection invalidElements = this.document.DocumentNode.SelectNodes(@"//script|//style");
            IEnumerable<HtmlNode> nodesWithAttributes = this.document.DocumentNode.DescendantsAndSelf().Where(node => node.HasAttributes).ToList();

            return (invalidElements == null || invalidElements.ToList().Any()) && !nodesWithAttributes.Any();
        }

     
        /// <summary>
        /// This method cleans the HTML document by removing script and style elements along with all attributes within elements.
        /// </summary>
        /// <param name="documentNodeRules">
        /// A list of document node rules.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CleanDocument(IList<IDocumentNodeRule> documentNodeRules)
        {
            try
            {
                if (documentNodeRules != null && documentNodeRules.Any())
                {
                    foreach (IDocumentNodeRule documentNodeRule in documentNodeRules.OrderBy(x => x.Order))
                    {
                        var processor = this.DocumentNodeProcessorProvider.ProvideProcessor((DocumentNodeActionTypes)documentNodeRule.DocumentNodeActionTypeId);
                        HtmlDocument htmlDocument = processor.ProcessHtmlDocument(this.document, documentNodeRule);
                        if (htmlDocument != null)
                        {
                            this.document = htmlDocument;
                        }
                    }
                }

                return this.document.DocumentNode.WriteTo();
            }
            catch (Exception ex)
            {
                // Log exceptions
                return null;
            }
        }

        /// <summary>
        /// Returns a string representation of a <see cref="DocumentParser"  /> class instance
        /// </summary>
        /// <returns>
        /// String value representing this instance
        /// </returns>
        public override string ToString()
        {
            return this.document.DocumentNode.WriteTo();
        }
        #endregion
    }
}
