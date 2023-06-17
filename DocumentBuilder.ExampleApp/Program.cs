using DocumentBuilder.DocumentBuilders;
using DocumentBuilder.Pdf;

var styleSheet = "h1 {\r\n  color: blue;\r\n}\r\np {\r\n  color: red;\r\n}";
using var cssStream = new MemoryStream();
var writer = new StreamWriter(cssStream);
writer.Write(styleSheet);
writer.Flush();
cssStream.Seek(0, SeekOrigin.Begin);

using var pdfFileStream = File.Create("./test.pdf");
using var htmlFileStream = File.Create("./test.html");
var htmlDocumentBuilder = new HtmlDocumentBuilder()
    .AddHeader1("Should be blue")
    .AddParagraph("Should be red");

await htmlDocumentBuilder.BuildAsync(htmlFileStream);
await htmlDocumentBuilder.BuildAsPdfAsync(pdfFileStream, cssStream);