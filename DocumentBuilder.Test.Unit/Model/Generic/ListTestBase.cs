﻿using DocumentBuilder.Factories;
using DocumentBuilder.Model.Generic;
using DocumentBuilder.Options;
using DocumentBuilder.Test.Unit.TestHelpers;
using DocumentBuilder.Utilities;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Model.Generic
{
    public class ListTestBase : TestBase
    {
        protected IEnumerable<ProductTableRowWithoutAttributes> _exampleProducts;

        [TestInitialize]
        public void TestBaseInitialize()
        {
            _exampleProducts = ExampleProductsGenerator.CreateTableRowsWithoutAttributes();
        }

        protected async Task AssertToHtmlReturnsCorrectHtmlList(ListBase<ProductTableRowWithoutAttributes> list, string htmlListIndicator, HtmlDocumentOptions options, int indentationLevel)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize, indentationLevel);
            var blub = indentationProvider.GetIndentation(0);

            // Act
            var htmlOrderedList = await list.ToHtmlAsync(options, indentationLevel);

            // Assert
            var expectedHtmlOrderedList = CreateExpectedHtmlList(htmlListIndicator, newLineProvider, indentationProvider);
            htmlOrderedList.Should().Be(expectedHtmlOrderedList);
        }

        protected async Task AssertToMarkdownReturnsCorrectMarkdownlList(ListBase<ProductTableRowWithoutAttributes> list, string markdownListIndicator, MarkdownDocumentOptions options)
        {
            // Arrange
            var newLineProvider = NewLineProviderFactory.Create(options.LineEndings);

            // Act
            var markdownOrderedList = await list.ToMarkdownAsync(options);

            // Assert
            var expectedMarkdownOrderedList = CreateExpectedMarkdownOrderedList(markdownListIndicator, newLineProvider);
            markdownOrderedList.Should().Be(expectedMarkdownOrderedList);
        }

        protected string CreateExpectedMarkdownOrderedList(string markdownListIndicator, INewLineProvider newLineProvider)
        {
            var i = markdownListIndicator;
            var string1 = _exampleProducts.ElementAt(0).ToString();
            var string2 = _exampleProducts.ElementAt(1).ToString();
            var string3 = _exampleProducts.ElementAt(2).ToString();

            return
                $"{i} {string1}" + newLineProvider.GetNewLine() +
                $"{i} {string2}" + newLineProvider.GetNewLine() +
                $"{i} {string3}" + newLineProvider.GetNewLine();
        }

        protected string CreateExpectedHtmlList(string htmlListIndicator, INewLineProvider newLineProvider, IIndentationProvider indentationProvider)
        {
            var li = htmlListIndicator;
            var string1 = _exampleProducts.ElementAt(0).ToString();
            var string2 = _exampleProducts.ElementAt(1).ToString();
            var string3 = _exampleProducts.ElementAt(2).ToString();

            return
                GetIndentation(indentationProvider, 0) +
                $"<{li}>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                  $"<li>{string1}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                  $"<li>{string2}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 1) +
                  $"<li>{string3}</li>" + GetNewLineAndIndentation(newLineProvider, indentationProvider, 0) +
                $"</{li}>" + GetNewLine(newLineProvider);
        }
    }
}
