# NDocument

`NDocument` is a library that allows you to programatically create different kinds of documents.
The following formats are currently supported.

- Markdown
- HTML
- Excel (Tables only)

`NDocument` exposes a `DocumentBuilder` class that can be used to create a markdown or HTML document.
`NDocument` also exposes a `MarkdownDocumentBuilder` and an `HTMLDocumentBuilder`, this is done so that in the future, these can be extended with markdown or HTML specific elements. For now it's recommend to use the generic `DocumentBuilder` as it has the exact same functionality.

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


```C#

// Create a document using the generic document builder

var outputStream = new MemoryStream();

var documentBuilder = new DocumentBuilder(options)
    .WithHeader1(header1)
    .WithHeader2(header2)
    .WithHeader3(header3)
    .WithHeader4(header4)
    .WithParagraph(paragraph)
    .WithUnorderedList(unorderedList)
    .WithOrderedList(orderedList)
    .WithTable(productTableRows) // More on tables below
    .WriteToStreamAsync(outputStream, DocumentType.Markdown); // Or HTML

```

```C#

```C#

// Create a document using the markdown document builder

var outputStream = new MemoryStream();

var markdownDocumentBuilder = new MarkdownDocumentBuilder(options)
    .WithHeader1(header1)
    .WithHeader2(header2)
    .WithHeader3(header3)
    .WithHeader4(header4)
    .WithParagraph(paragraph)
    .WithUnorderedList(unorderedList)
    .WithOrderedList(orderedList)
    .WithTable(productTableRows) // More on tables below
    .WriteToStreamAsync(outputStream);

```

```C#

// Create a document using the html document builder

var outputStream = new MemoryStream();

var htmlDocumentBuilder = new HtmlDocumentBuilder(options)
    .WithHeader1(header1)
    .WithHeader2(header2)
    .WithHeader3(header3)
    .WithHeader4(header4)
    .WithParagraph(paragraph)
    .WithUnorderedList(unorderedList)
    .WithOrderedList(orderedList)
    .WithTable(productTableRows) // More on tables below
    .WriteToStreamAsync(outputStream);
```

## Tables

NDocument supports the creation of tables by creating a POCO (Plain Old C# Object). It will use the name of the property has the column name, and will order the table columns in the same order as the properties defined in the POCO.
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
    .WithTable(productTableRows)
    .WriteToStream(outputStream);

```

### 4. Or instead wrap it in the table class and write it to a stream directly

```C#
var outputStream = new MemoryStream();
var table = new Table<ProductsTableRow>(productTableRows);
table.WriteAsMarkdownToStreamAsync(outputStream); // Markdown
table.WriteAsHtmlToStreamAsync(outputStream); // HTML
table.WriteToExcel(excelDocumentOptions) // Excel
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
