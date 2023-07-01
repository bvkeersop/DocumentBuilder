using DocumentBuilder.DocumentBuilders;
using DocumentBuilder.ExampleApp;
using DocumentBuilder.Excel.Options;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Core.Enumerations;
using DocumentBuilder.Html.Options;
using DocumentBuilder.Html;
using DocumentBuilder.Pdf;

Console.WriteLine("DocumentBuilder - Example Program");
Console.WriteLine("Running this program will build some example documents, displaying how this library can be used");

var up = "..\\..\\..";

var markdownDocumentFilePath = $"\\Resources\\rick-astley-never-gonna-give-you-up.md";
var fullMarkdownDocumentFilePath = $"{up}{markdownDocumentFilePath}";
Console.WriteLine($"Creating an example markdown document at {markdownDocumentFilePath}");
await CreateExampleMarkdownDocument(fullMarkdownDocumentFilePath);

var htmlDocumentFilePath = "\\Resources\\rick-astley-never-gonna-give-you-up.html";
var fullHtmlDocumentFilePath = $"{up}{htmlDocumentFilePath}";
Console.WriteLine($"Creating an example html document at {htmlDocumentFilePath}");
await CreateExampleHtmlDocument(fullHtmlDocumentFilePath);

var pdfDocumentFilePath = "\\Resources\\rick-astley-never-gonna-give-you-up.pdf";
var fullPdfDocumentFilePath = $"{up}{pdfDocumentFilePath}";
Console.WriteLine($"Creating an example pdf document (using html) at {pdfDocumentFilePath}");
await CreateExampleHtmlDocument(fullPdfDocumentFilePath, saveAsPdf: true);

var excelDocumentFilePath = "\\Resources\\my-favorite-songs.xlsx";
var fullExcelDocumentFilePath = $"{up}{excelDocumentFilePath}";
Console.WriteLine($"Creating an example excel document at {excelDocumentFilePath}");
CreateExampleExcelDocument(fullExcelDocumentFilePath);

Console.WriteLine($"Example documents have been created at their stated path, press any key to continue...");
Console.ReadKey();

async static Task CreateExampleMarkdownDocument(string filePath)
{
    // (Optional) Create a MarkdownDocumentOptions instance for configuring specifics
    var options = new MarkdownDocumentOptionsBuilder()
        .WithIndentationProvider(IndentationType.Spaces, 2)
        .WithNewLineProvider(LineEndings.Environment)
        .WithMarkdownTableOptions(new MarkdownTableOptions())
        .Build();

    // Or just use the defaults
    options = new MarkdownDocumentOptions();

    // Example of how you can use a POCO to create tables
    var songDetails = new SongDetails
    {
        Artist = "Rick Astley",
        Title = "Never gonna give you up",
        Album = "Whenever You Need Somebody",
        Released = new DateOnly(1987, 7, 27),
    };

    // Create the markdown document, options are optional
    var markdownDocument = new MarkdownDocumentBuilder(options)
        .AddHeader1("Rick Astley - Never gonna give you up")
        .AddTable(songDetails)
        .AddImage("rick-astley", ".\\Resources\\rick-astley.jpg", caption: "Rick Astley performing his hit song")
        .AddHorizontalRule()
        .AddHeader2("Verse 1")
        .AddParagraph("We're no strangers to love. You know the rules and so do I (do I). A full commitment's what I'm thinking of. You wouldn't get this from any other guy")
        .AddParagraph("I just wanna tell you how I'm feeling. Gotta make you understand")
        .AddHeader2("Chorus")
        .AddParagraph("Never gonna give you up. Never gonna let you down. Never gonna run around and desert you. Never gonna make you cry. Never gonna say goodbye. Never gonna tell a lie and hurt you")
        .Build();

    // Save the markdown document (either by stream, or by providing a filepath directly)
    using var fileStream = File.Create(filePath);
    await markdownDocument.SaveAsync(fileStream);
}


static void CreateExampleExcelDocument(string filePath)
{
    // (Optional) Create a MarkdownDocumentOptions instance for configuring specifics
    var options = new ExcelDocumentOptions();

    // Example of how you can use a POCO to create tables
    var songDetails = new List<SongDetails>
    {
        new SongDetails
        {
            Artist = "Rick Astley",
            Title = "Never gonna give you up",
            Album = "Whenever You Need Somebody",
            Released = new DateOnly(1987, 7, 27),
        },
        new SongDetails
        {
            Artist = "Eduard Khil",
            Title =  "I am Glad, 'cause I'm Finally Returning Back Home",
            Album = "Single",
            Released = new DateOnly(1976, 1, 1)
        }
    };

    // Create the excel document
    var excelDocument = new ExcelDocumentBuilder()
        .AddWorksheet("My favorite songs")
        .AddTable(songDetails)
        .Build();

    // Save the excel document (either by stream, or by providing a filepath directly)
    excelDocument.Save(filePath);
}

async static Task CreateExampleHtmlDocument(string filePath, bool saveAsPdf = false)
{
    // (Optional) Create a MarkdownDocumentOptions instance for configuring specifics
    var options = new HtmlDocumentOptionsBuilder()
        .WithIndentationProvider(IndentationType.Tabs, 1)
        .WithNewLineProvider(LineEndings.Linux)
        .Build();

    // Or just use the defaults
    options = new HtmlDocumentOptions();

    // Example of how you can use a POCO to create tables
    var songDetails = new SongDetails
    {
        Artist = "Rick Astley",
        Title = "Never gonna give you up",
        Album = "Whenever You Need Somebody",
        Released = new DateOnly(1987, 7, 27),
    };

    // Create the markdown document, options are optional
    var htmlDocument = new HtmlDocumentBuilder(options)
        .AddDivStart().WithId("content")
            .AddHeader1("Rick Astley - Never gonna give you up").WithClass("title").WithClass("large")
            .AddTable(songDetails)
            .AddFigure("rick-astley", ".\\Resources\\rick-astley.jpg", caption: "Rick Astley performing his hit song")
            .AddDivStart().WithClass("verse")
                .AddHeader2("Verse 1")
                .AddParagraph("We're no strangers to love. You know the rules and so do I (do I). A full commitment's what I'm thinking of. You wouldn't get this from any other guy")
                .AddParagraph("I just wanna tell you how I'm feeling. Gotta make you understand")
            .AddDivEnd()
            .AddDivStart().WithClass("chorus")
                .AddHeader2("Chorus")
                .AddParagraph("Never gonna give you up. Never gonna let you down. Never gonna run around and desert you. Never gonna make you cry. Never gonna say goodbye. Never gonna tell a lie and hurt you")
            .AddDivEnd()
        .AddDivEnd()
        .Build();

    // Save the html document (either by stream, or by providing a filepath directly)
    if (!saveAsPdf)
    {
        using var fileStream = File.Create(filePath);
        await htmlDocument.SaveAsync(fileStream);
    }
    // Or save the html document as a pdf instead
    else
    {
        using var fileStream = File.Create(filePath);
        await htmlDocument.SaveAsPdfAsync(fileStream, string.Empty);
    }
}