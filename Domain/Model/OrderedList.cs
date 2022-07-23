﻿using NDocument.Domain.Constants;
using NDocument.Domain.Options;

namespace NDocument.Domain.Model
{
    public class OrderedList<T> : ListBase<T>
    {
        public OrderedList(IEnumerable<T> value) : base(value)
        {
        }

        public override ValueTask<string> ToMarkdownAsync(MarkdownDocumentOptions options)
        {
            return CreateMarkdownList(MarkdownIndicators.OrderedListItem, options);
        }

        public override ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel)
        {
            return CreateHtmlListAsync(HtmlIndicators.OrderedList, options, indentationLevel);
        }
    }
}
