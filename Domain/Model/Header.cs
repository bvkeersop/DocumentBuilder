﻿using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public abstract class Header : GenericElement
    {
        public string Value { get; }

        protected Header(string value)
        {
            Value = value;
        }

        protected ValueTask<string> CreateMarkdownHeader(string headerIndicator, MarkdownDocumentOptions options)
        {
            var markdown = $"{headerIndicator} {Value}";
            return ConvertToMarkdown(markdown, options);
        }

        protected ValueTask<string> CreateHtmlHeader(string headerIndicator, HtmlDocumentOptions options)
        {
            var html = $"<{headerIndicator}>{Value}</{headerIndicator}>";
            return ConvertToHtml(html, options);
        }
    }
}
