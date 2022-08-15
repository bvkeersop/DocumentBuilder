# Table of Contents

- [Table of Contents](#table-of-contents)
- [DocumentBuilder](#documentbuilder)
  - [Creating a document](#creating-a-document)
    - [Markdown](#markdown)
    - [HTML](#html)
    - [Excel](#excel)
  - [Tables](#tables)
    - [1. Define a POCO](#1-define-a-poco)
    - [2. Put your POCOs in an IEnumerable](#2-put-your-pocos-in-an-ienumerable)
    - [3. Use it inside a document builder to generate a table](#3-use-it-inside-a-document-builder-to-generate-a-table)
  - [Options](#options)
  - [Generic](#generic)
  - [Markdown](#markdown-1)
      - [MarkdownTableOptions](#markdowntableoptions)
  - [HTML](#html-1)
      - [HtmlDocumentOptions](#htmldocumentoptions)
    - [Attributes](#attributes)
  - [Credits](#credits)
  - [Future work](#future-work)

# DocumentBuilder

`DocumentBuilder` is a library that uses the `Builder` pattern to enable you to declaratively create different kinds of documents is an easy way.
It is **not** a full fledged solution for creating complex documents. `DocumentBuilder` focuses on ease of use.

The following formats are currently supported:

- Markdown
- HTML
- Excel

`DocumentBuilder` exposes a `GenericDocumentBuilder` class that can be used to create a markdown or HTML document, it does not support Excel since it's document structure is too different. `DocumentBuilder` also exposes a `MarkdownDocumentBuilder` and an `HTMLDocumentBuilder`, this is done so that in the future, these can be extended with markdown or HTML specific elements. For now it's recommend to use the generic `GenericDocumentBuilder` as it has the exact same functionality.

## Creating a document

Create documents as follows:

```C#

// Given the following data:

var header1 = "Header1";
var header2 = "Header2";
var header3 = "Header3";
var header4 = "Header4";
var paragraph = "An interesting paragraph";

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

### Generic Documents

The generic document builder allows you to create generic documents that can easily be written to a markdown or HTML format.

```C#

// Create a document using the generic document builder

var outputStream = new MemoryStream();

var documentBuilder = new DocumentBuilder(options)
    .AddHeader1(header1)
    .AddHeader2(header2)
    .AddHeader3(header3)
    .AddHeader4(header4)
    .AddParagraph(paragraph)
    .AddUnorderedList(unorderedList)
    .AddOrderedList(orderedList)
    .AddTable(productTableRows) // More on tables below
    .WriteToStreamAsync(outputStream, DocumentType.Markdown); // Or HTML (DocumentType.HTML)

```

### Markdown

```C#

The markdown document builder allows you to create markdown documents, it is not yet different from the `GenericDocumentBuilder`, but might include markdown specific functionality in the future.

```C#

// Create a document using the markdown document builder

var outputStream = new MemoryStream();

var markdownDocumentBuilder = new MarkdownDocumentBuilder(options)
    .AddHeader1(header1)
    .AddHeader2(header2)
    .AddHeader3(header3)
    .AddHeader4(header4)
    .AddParagraph(paragraph)
    .AddUnorderedList(unorderedList)
    .AddOrderedList(orderedList)
    .AddTable(productTableRows) // More on tables below
    .WriteToStreamAsync(outputStream);

```

### HTML

The HTML document builder allows you to create HTML documents, it is not yet different from the `GenericDocumentBuilder`, but might include HTML specific functionality in the future.

```C#

// Create a document using the html document builder

var outputStream = new MemoryStream();

var htmlDocumentBuilder = new HtmlDocumentBuilder(options)
    .AddHeader1(header1)
    .AddHeader2(header2)
    .AddHeader3(header3)
    .AddHeader4(header4)
    .AddParagraph(paragraph)
    .AddUnorderedList(unorderedList)
    .AddOrderedList(orderedList)
    .AddTable(productTableRows) // More on tables below
    .WriteToStreamAsync(outputStream);
```

### Excel

The Excel document builder allows you to create excel documents. Since the structure of excel document is not comparable to markdown or HTML, it's not supported by the `GenericDocumentBuilder`.

```C#

// Create a document using the excel document builder

var outputStream = new MemoryStream();

var excelDocumentBuilder = new ExcelDocumentBuilder(options)
    .AddWorksheet("my-worksheet-name")
    .AddTable(productTableRows) // More on tables below
    .WriteToStreamAsync(outputStream);
```

## Tables

`DocumentBuilder` supports the creation of tables by creating a POCO (Plain Old C# Object). It will use the name of the property has the column name, and will order the table columns in the same order as the properties defined in the POCO.
There are [options](#options) available to configure this.

### 1. Define a POCO

```C#

public class ProductTableRow
{
    public string Id { get; set; }
    public string Amount { get; set; }
    public string Price { get; set; }
    public string Description { get; set; }
}

```

### 2. Put your POCOs in an IEnumerable

```C#

var productTableRows = new List<ProductTableRow>
{
    productTableRowOne,
    productTableRowTwo,
    productTableRowThree,
};

```

### 3. Use it inside a document builder to generate a table

```C#

var outputStream = new MemoryStream();

var markdownDocumentBuilder = new MarkdownDocumentBuilder(options)
    .AddTable(productTableRows)
    .WriteToStream(outputStream);

```

> NOTE: The values written to the table cell will be the object's `ToString()` method

## Options

## Generic

All document types have the following options:

| Option                  | Type           | Description                                  | DefaultValue |
| ----------------------- | ---------------|--------------------------------------------- | ------------ |
| LineEnding              | LineEnding     | What line ending to use                      | Environment  |

> **LineEndings**: *Environment, Windows, Linux*

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

## Credits

`DocumentBuilder` is made possible by the following projects:

- [ClosedXML](https://github.com/ClosedXML/ClosedXML)
- [FluentAssertions](https://fluentassertions.com/)
- [NSubstitute](https://nsubstitute.github.io/)

## Future work

If there's any features that you would like to see implemented, please create a issue with the `enhancement` label at the [Github Issues](https://github.com/bvkeersop/DocumentBuilder/issues) page. Note that I am working on this project in my free time, and might not have time to implement your request (or simply decline it since I don't see the added value for the project).

Currently I'm still looking to implement the following (no deadline set):

- Releasing a MVP on NuGet
- Image support for Markdown and HTML
- Raw insertions for Markdown and HTML
- Word support
- More insertables for Excel
- Better styling options for Excel