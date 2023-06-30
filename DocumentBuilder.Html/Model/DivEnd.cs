﻿using DocumentBuilder.Constants;
using DocumentBuilder.Html.Extensions;
using DocumentBuilder.Html.Options;

namespace DocumentBuilder.Html.Model;

internal class DivEnd : IHtmlElement
{
    public Attributes Attributes { get; } = new Attributes();

    public ValueTask<string> ToHtmlAsync(HtmlDocumentOptions options, int indentationLevel = 0) => new(Indicators.Div.ToHtmlEndTag());
}