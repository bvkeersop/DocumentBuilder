# NDocument

`NDocument` is a library that allows you to programatically create different kinds of documents.
The following formats are currently supported.

- Markdown
- HTML

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

// Create a markdown document

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
    .WriteToOutputStream(outputStream);

```

```C#

// Or an html document

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
    .WriteToOutputStream(outputStream);
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
    .WriteToOutputStream(outputStream);

```

### 4. Or instead wrap it in the table class and write it to a stream directly

```C#
var outputStream = new MemoryStream();
var table = new Table<ProductsTableRow>(productTableRows);
table.WriteAsMarkdownToStreamAsync(outputStream); // Markdown
table.WriteAsHtmlToStreamAsync(outputStream); // HTML
```

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
| TableAlignment          | TableAlignment | How to align the table                       | AlignColumns |
| BoldColumnNames         | bool           | Wheter to have the column names in bold text | false        |

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

    [Column(nameof(Price), 3)]
    public string Price { get; set; }

    [Column(nameof(Amount), 2)]
    public string Amount { get; set; }
}

```
