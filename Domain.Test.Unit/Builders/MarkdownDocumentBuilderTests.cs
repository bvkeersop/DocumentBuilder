﻿using FluentAssertions;
using NDocument.Domain.Builders;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Factories;
using NDocument.Domain.Options;
using NDocument.Domain.Test.Unit.TestHelpers;

namespace NDocument.Domain.Test.Unit.Builders
{
    [TestClass]
    public class MarkdownDocumentBuilderTests : BuilderTestBase
    {
        [TestMethod]
        public async Task Build_CreatesMarkdownDocument()
        {
            // Arrange
            var options = new MarkdownDocumentOptions()
            {
                LineEndings = LineEndings.Environment,
                MarkdownTableOptions = new MarkdownTableOptions
                {
                    BoldColumnNames = false,
                    Formatting = Formatting.AlignColumns
                }
            };

            var outputStream = new MemoryStream();

            var markdownDocumentBuilder = new MarkdownDocumentBuilder(options)
                .WithHeader1(_header1)
                .WithHeader2(_header2)
                .WithHeader3(_header3)
                .WithHeader4(_header4)
                .WithParagraph(_paragraph)
                .WithUnorderedList(_unorderedList)
                .WithOrderedList(_orderedList)
                .WithTable(_productTableRowsWithoutHeaders);

            // Act
            await markdownDocumentBuilder.WriteToStreamAsync(outputStream);

            // Assert
            var expectedMarkdownDocument = GetExpectedMarkdownDocument(options);
            var markdownDocument = StreamHelper.GetStreamContents(outputStream);
            markdownDocument.Should().Be(expectedMarkdownDocument);
        }
    }
}
