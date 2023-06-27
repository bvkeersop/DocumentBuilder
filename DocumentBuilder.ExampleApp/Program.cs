using DocumentBuilder.DocumentBuilders;
using DocumentBuilder.ExampleApp;
using DocumentBuilder.Excel.Options;
using DocumentBuilder.Markdown.Options;
using DocumentBuilder.Core.Enumerations;

Console.WriteLine("DocumentBuilder - Example Program");
Console.WriteLine("Running this program will build some example documents, displaying how this library can be used");

var markdownDocumentFilePath = ".\\rick-astley-never-gonna-give-you-up.md";
Console.WriteLine($"Creating an example markdown document at {markdownDocumentFilePath}");
await CreateExampleMarkdownDocument(markdownDocumentFilePath);

var excelDocumentFilePath = ".\\my-favorite-songs.xlsx";
Console.WriteLine($"Creating an example excel document at {excelDocumentFilePath}");
CreateExampleExcelDocument(excelDocumentFilePath);


async static Task CreateExampleMarkdownDocument(string filePath)
{
    // (Optional) Create a MarkdownDocumentOptions instance for configuring specifics
    var options = new MarkdownDocumentOptionsBuilder()
        .WithIndentationProvider(IndentationType.Spaces, 2)
        .WithNewLineProvider(LineEndings.Environment)
        .WithMarkdownTableOptions(new MarkdownTableOptions())
        .Build();

    // Example of how you can use a POCO to create tables
    var songDetails = new SongDetails
    {
        Artist = "Rick Astley",
        Title = "Never gonna give you up",
        Album = "Whenever You Need Somebody",
        Released = new DateOnly(1987, 7, 27),
    };

    // Create the markdown document
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



//var styleSheet = "h1 {\r\n  color: blue;\r\n}\r\np {\r\n  color: red;\r\n}";
//using var cssStream = new MemoryStream();
//var writer = new StreamWriter(cssStream);
//writer.Write(styleSheet);
//writer.Flush();
//cssStream.Seek(0, SeekOrigin.Begin);

//using var pdfFileStream = File.Create("./test.pdf");
//using var htmlFileStream = File.Create("./test.html");
//var htmlDocumentBuilder = new HtmlDocumentBuilder()
//    .AddHeader1("A Large header").WithClass("large").WithId("header-large-1").WithAttribute("my-custom-attribute", "a specific value");
//    .AddParagraph("Should be red");

//await htmlDocumentBuilder.BuildAsync(htmlFileStream);
//await htmlDocumentBuilder.BuildAsPdfAsync(pdfFileStream, cssStream);

//using var htmlFileStream = File.Create("./test1.html");

//var htmlDocumentBuilder = new HtmlDocumentBuilder()
//    .AddHeader1("A Large header")
//        .WithClass("large")
//        .WithId("header-large-1")
//        .WithAttribute("my-custom-attribute", "a specific value")
//        .AddStylesheetByRef("mystyle.css");

//htmlDocumentBuilder.BuildAsync(htmlFileStream);