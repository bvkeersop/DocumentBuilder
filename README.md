# DocumentBuilder

![Workflows: dotnet](https://github.com/bvkeersop/DocumentBuilder/actions/workflows/pipeline.yml/badge.svg)
![GitHub](https://img.shields.io/github/license/bvkeersop/DocumentBuilder)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bvkeersop_DocumentBuilder&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=bvkeersop_DocumentBuilder)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=bvkeersop_DocumentBuilder&metric=coverage)](https://sonarcloud.io/summary/new_code?id=bvkeersop_DocumentBuilder)

`DocumentBuilder` is a library that uses the `Builder` pattern to enable you to declaratively create different kinds of documents easily.
It is **not** a full-fledged solution for creating complex documents. `DocumentBuilder` focuses on ease of use.

The following formats are currently supported:

- Markdown
- HTML
- PDF
- Excel

`DocumentBuilder` API exposes a stream and a filepath based API.

# Table of Contents

- [DocumentBuilder](#documentbuilder)
- [Table of Contents](#table-of-contents)
  - [Creating a document](#creating-a-document)
    - [Generic](#generic)
    - [Markdown](#markdown)
    - [HTML](#html)
      - [Styles](#styles)
    - [PDF](#pdf)
    - [Excel](#excel)
  - [Tables](#tables)
    - [Creating a Table](#creating-a-table)
      - [1. Define a POCO](#1-define-a-poco)
      - [2. Put your POCOs in an IEnumerable](#2-put-your-pocos-in-an-ienumerable)
      - [3. Use it inside a document builder to generate a table](#3-use-it-inside-a-document-builder-to-generate-a-table)
  - [Options](#options)
  - [DocumentOptions](#documentoptions)
  - [Markdown](#markdown-1)
      - [MarkdownTableOptions](#markdowntableoptions)
  - [HTML](#html-1)
      - [HtmlDocumentOptions](#htmldocumentoptions)
    - [Attributes](#attributes)
  - [Credits](#credits)
  - [Future work](#future-work)

## Creating a document

Below are examples of how you can use the document builders provided by `DocumentBuilder` to create markdown, HTML and Excel documents. You can write your document to a stream by providing an output stream, or to a file by providing a file path.

```C#

// Given the following data:
var header1 = "Header1";
var header2 = "Header2";
var header3 = "Header3";
var header4 = "Header4";
var paragraph = "An interesting paragraph";
var imageName = "imageName";
var imagePath = "./image";
var imageCaption = "This is an image";
var blockquote = "blockquote";
var lanuage = "C#";
var codeblock = "codeblock";
var raw = "raw";

var orderedList = new List<string>
{
    "an",
    "ordered",
    "list"
};

var unorderedList = new List<string>
{
    "an",
    "unordered",
    "list"
};

```

### Generic

The generic document builder allows you to create generic documents that can easily be written to a stream as either Markdown or HTML.
The generic document builder is marked as deprecated/obsolete. It's recommended to use the specific builders instead. 

```C#

// Create a document using the generic document builder
var outputStream = new MemoryStream();

var documentBuilder = new DocumentBuilder(options)
    .AddHeader1(header1)
    .AddHeader2(header2)
    .AddHeader3(header3)
    .AddHeader4(header4)
    .AddParagraph(paragraph)
    .AddImage(imageName, imagePath, imageCaption)
    .AddUnorderedList(unorderedList)
    .AddOrderedList(orderedList)
    .AddTable(productTableRows); // More on tables below
    
    
await documentBuilder.BuildAsync(outputStream, DocumentType.Markdown); // Or HTML (DocumentType.HTML)
```

### Markdown

The `MarkdownDocumentBuilder` allows you to create Markdown documents.

```C#

// Create a document using the markdown document builder
var outputStream = new MemoryStream();

var markdownDocumentBuilder = new MarkdownDocumentBuilder(options)
    .AddHeader1(header1)
    .AddHeader2(header2)
    .AddHeader3(header3)
    .AddHeader4(header4)
    .AddParagraph(paragraph)
    .AddImage(imageName, imagePath, imageCaption)
    .AddUnorderedList(unorderedList)
    .AddOrderedList(orderedList)
    .AddTable(productTableRows) // More on tables below
    .AddHorizontalRule()
    .AddBlockquote(blockquote)
    .AddFencedCodeblock(codeblock, language)
    .AddRaw(raw);
    
await markdownDocumentBuilder.BuildAsync(outputStream); // Or file path

```

### HTML

The `HTMLDocumentBuilder` allows you to create Markdown documents.

```C#

// Create a document using the html document builder
var outputStream = new MemoryStream();

var htmlDocumentBuilder = new HtmlDocumentBuilder(options)
    .AddHeader1(header1)
    .AddHeader2(header2)
    .AddHeader3(header3)
    .AddHeader4(header4)
    .AddImage(imageName, imagePath, imageCaption)
    .AddParagraph(paragraph)
    .AddUnorderedList(unorderedList)
    .AddOrderedList(orderedList)
    .AddTable(productTableRows) // More on tables below
    .AddRaw(raw);

await htmlDocumentBuilder.BuildAsync(outputStream); // Or file path
```

#### Styles

It's also possible to add styles to your HTML document. This can be done by adding attributes and referencing a stylesheet, or by placing the styles inline.

##### Stylesheet

```C#
var htmlDocumentBuilder = new HtmlDocumentBuilder()
    .AddHeader1("A Large header")
        .WithClass("large")
        .WithId("header-large-1")
        .WithAttribute("my-custom-attribute", "a specific value")
        .AddStylesheetByRef("mystyle.css");

htmlDocumentBuilder.BuildAsync(htmlFileStream);
```

which will render to:

```HTML
<!DOCTYPE html>
<html>
<head>
  <link rel="stylesheet" type="text/css" href="mystyle.css" />
</head>
  <body>
    <h1 class="large" id="header-large-1" my-custom-attribute="a specific value">A Large header</h1>
  </body>
</html>
```

##### Inline styles



### PDF

There's no specific builder for PDF documents, however the `DocumentBuilder.Pdf` NuGet package contains an extension method for the `HTMLDocumentBuilder` named `BuildAsPdfAsync`. This extension method converts your HTMLDocument to a PDF document.
Since this extension method relies on [TheArtOfDev.HtmlRenderer.PdfSharp](https://github.com/ArthurHub/HTML-Renderer) for the conversion, the `BSD-3-Clause` implicitly applies to `DocumentBuilder.Pdf`.

```C#

// Create a document using the html document builder
var styleSheet = "h1 {\r\n  color: blue;\r\n}\r\np {\r\n  color: red;\r\n}";
var outputStream = new MemoryStream();

var htmlDocumentBuilder = new HtmlDocumentBuilder(options)
    .AddHeader1(header1)
    .AddHeader2(header2)
    .AddHeader3(header3)
    .AddHeader4(header4)
    .AddImage(imageName, imagePath, imageCaption)
    .AddParagraph(paragraph)
    .AddUnorderedList(unorderedList)
    .AddOrderedList(orderedList)
    .AddTable(productTableRows) // More on tables below
    .AddRaw(raw);

var pageSize = PdfSharp.PageSize.A4,
var margin = 20
await htmlDocumentBuilder.BuildAsPdfAsync(outputStream, styleSheet, pageSize, margin); // Or file path
```

### Excel

The Excel document builder allows you to create Excel documents. Since the structure of Excel documents is not comparable to Markdown or HTML, it's not supported by the `GenericDocumentBuilder`.

```C#

// Create a document using the excel document builder
var outputStream = new MemoryStream();

var excelDocumentBuilder = new ExcelDocumentBuilder(options)
    .AddWorksheet("my-worksheet-name")
    .AddTable(productTableRows); // More on tables below

await excelDocumentBuilder.BuildAsync(outputStream);  // Or file path
```

## Tables

`DocumentBuilder` supports the creation of tables by creating a POCO (Plain Old C# Object). It will use the name of the property as the column name and will order the columns as defined on the POCO. There are [options](#options) available to configure this.

### Creating a Table

#### 1. Define a POCO

```C#

public class ProductTableRow
{
    public string Id { get; set; }
    public string Amount { get; set; }
    public string Price { get; set; }
    public string Description { get; set; }
}

```

#### 2. Put your POCOs in an IEnumerable

```C#

var productTableRows = new List<ProductTableRow>
{
    productTableRowOne,
    productTableRowTwo,
    productTableRowThree,
};

```

#### 3. Use it inside a document builder to generate a table

```C#

var outputStream = new MemoryStream();

var markdownDocumentBuilder = new MarkdownDocumentBuilder(options)
    .AddTable(productTableRows)
    .BuildAsync(outputStream);

```

> NOTE: In case of using an object, the values written to the table cell will be the object's `ToString()` method.

If the provided table row is an empty enumerable, it will be skipped.

## Options

## DocumentOptions

All document types have the following options:

| Option                    | Type                    | Description                                                  | DefaultValue |
| ------------------------- | ----------------------- |------------------------------------------------------------- | ------------ |
| LineEnding                | LineEnding              | What line ending to use                                      | Environment  |
| BehaviorOnEmptyEnumerable | EmptyEnumerableBehavior | What behavior to display when a provided enumerable is empty | SkipRender   |

> **LineEndings**: *Environment, Windows, Linux*
> **EmptyEnumerableBehavior**: *SkipRender, Render, ThrowException*

## Markdown

#### MarkdownTableOptions

| Option                  | Type           | Description                                  | DefaultValue |
| ----------------------- | ---------------|--------------------------------------------- | ------------ |
| Formatting              | Formatting     | How to align the table                       | AlignColumns |
| BoldColumnNames         | bool           | Wheter to have the column names in bold text | false        |
| DefaultAlignment        | Alignment      | What default (github) alignment to use       | None         |

> **Formatting**: *AlignColumns, None*
> **Alignment**: *None, Left, Right, Center*

## HTML

#### HtmlDocumentOptions

| Option                  | Type            | Description                     | DefaultValue |
| ----------------------- | ----------------|-------------------------------- | ------------ |
| LineEnding              | LineEnding      | What line ending to use         | Environment  |
| IndentationType         | IndentationType | What type of indentation to use | Spaces       |
| IndentationSize         | int             | The size of one indentation     | 2            |

> **IndentationTypes**: *Spaces, Tabs*

### Attributes

You can annotate your POCO properties with the `Column` attribute.

``` C#

public class ProductTableRow
{
    [Column(name: "ProductId", order: 1)] // Overwrite the column name
    public string Id { get; set; }

    [Column(nameof(Description))] // Not specifying an order will default the value to int.Max
    public string Description { get; set; }

    public string Price { get; set; }

    [Column(alignment: Alignment.Center)] // Applies github style markdown to align the column
    public string Amount { get; set; }
}

```

You can annotate your POCO properties with the `IgnoreColumn` attribute. This will ignore the column when converting to a document.

``` C#

public class ProductTableRow
{
    [Column(name: "ProductId", order: 1)] // Overwrite the column name
    public string Id { get; set; }

    [Column(nameof(Description))] // Not specifying an order will default the value to int.Max
    public string Description { get; set; }

    [IgnoreColumn]
    public string Price { get; set; }

    [Column(alignment: Alignment.Center)] // Applies github style markdown to align the column
    public string Amount { get; set; }
}

```

## Credits

`DocumentBuilder` is made possible by the following projects:

- [ClosedXML](https://github.com/ClosedXML/ClosedXML)
  - Used for creating Excel documents.
- [TheArtOfDev.HtmlRenderer.PdfSharp](https://github.com/ArthurHub/HTML-Renderer)
  - Used for converting HTML documents to PDF documents.
- [FluentAssertions](https://fluentassertions.com/)
  - Used for testing.
- [NSubstitute](https://nsubstitute.github.io/)
   - Used for testing.

## Future work

If there are any features that you would like to see implemented, please create an issue with the `enhancement` label on the [Github Issues](https://github.com/bvkeersop/DocumentBuilder/issues) page. Note that I am working on this project in my free time, and might not have time to implement your request (or simply decline it since I don't see the added value for the project).

Currently, I'm still looking to implement the following (no deadline set):

- Word support
- More insertables for Excel
- Better styling options for Excel

## Issues

I've learn things about programming and the structure of the project along the way, and would've done things differently with the insights I have now. This is however a big refactor, so I don't know if I'll get around to doing that.
Some improvements would be:

- Getting rid of the `GenericDocumentBuilder`.
  - It serves no use as it's limited by generic concepts that do not cross multiple builders. The flexibility is not needed.
- Splitting different document types in different assemblies.
  - When the `GenericDocumentBuilder` is gone, generic base classing can be abandoned. This means the NuGet packages could be made more light weight.
- Indenting and creating newlines should not be the responsiblity of the element, but rather the document/writer.
  - This is now arranged in the element classes, but it doesn't belong there.
- Building should lead to a `Document` representation, which in turn can be Saved.

// TODO:
(inline) Styles are not yet fully supported