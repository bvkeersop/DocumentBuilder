﻿using DocumentBuilder.Constants;
using DocumentBuilder.Extensions;
using DocumentBuilder.Factories;
using DocumentBuilder.Options;
using System.Text;

namespace DocumentBuilder.Model.Generic
{
    public abstract class ListBase<TValue> : GenericElement
    {
        protected IEnumerable<TValue> Value { get; }

        protected ListBase(IEnumerable<TValue> value)
        {
            Value = value;
        }

        protected ValueTask<string> CreateMarkdownList(string prepend, MarkdownDocumentOptions options)
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in Value)
            {
                var value = $"{prepend} {item}";
                var listItem = AddNewLine(value, options);
                stringBuilder.Append(listItem);
            }

            var list = stringBuilder.ToString();
            return new ValueTask<string>(list); // Already has a new line
        }

        protected async ValueTask<string> CreateHtmlListAsync(string listIndicator, HtmlDocumentOptions options, int indentationLevel = 0)
        {
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, indentationLevel);
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(indentationProvider.GetIndentation(0));
            stringBuilder.Append(listIndicator.ToHtmlStartTag());
            stringBuilder.Append(newLineProvider.GetNewLine());

            foreach (var item in Value)
            {
                var listItem = await CreateHtmlListItem(item, options, indentationLevel + 1);
                stringBuilder.Append(listItem);
            }

            stringBuilder.Append(indentationProvider.GetIndentation(0));
            stringBuilder.Append(listIndicator.ToHtmlEndTag());
            stringBuilder.Append(newLineProvider.GetNewLine());
            return stringBuilder.ToString();
        }

        private static ValueTask<string> CreateHtmlListItem(TValue? item, HtmlDocumentOptions options, int indentationLevel)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(HtmlIndicators.ListItem.ToHtmlStartTag());
            stringBuilder.Append(item);
            stringBuilder.Append(HtmlIndicators.ListItem.ToHtmlEndTag());
            var listItem = stringBuilder.ToString();
            return WrapWithIndentationAndNewLine(listItem, options, indentationLevel);
        }
    }
}
