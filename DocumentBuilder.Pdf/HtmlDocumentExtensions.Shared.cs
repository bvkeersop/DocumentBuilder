using DocumentBuilder.Helpers;
using DocumentBuilder.Html.Model;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace DocumentBuilder.Pdf
{
    // Copyright(c) 2009, José Manuel Menéndez Poo
    // Copyright(c) 2013, Arthur Teplitzki
    // All rights reserved.

    // Redistribution and use in source and binary forms, with or without modification,
    // are permitted provided that the following conditions are met:

    //  Redistributions of source code must retain the above copyright notice, this
    //  list of conditions and the following disclaimer.

    //  Redistributions in binary form must reproduce the above copyright notice, this
    //  list of conditions and the following disclaimer in the documentation and/or
    //  other materials provided with the distribution.

    //  Neither the name of the menendezpoo.com, ArthurHub nor the names of its
    //  contributors may be used to endorse or promote products derived from
    //  this software without specific prior written permission.

    // THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
    // ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
    // WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    // DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
    // ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
    // (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
    //    LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
    // ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
    // (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
    // SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

    /// <summary>
    /// Extension methods for the <see cref="IHtmlDocumentBuilder"/> to convert HTML documents to PDF.
    /// These extension methods rely on <see cref="TheArtOfDev.HtmlRenderer.PdfSharp"/>, which uses the BSD-3-Clause license.
    /// see https://github.com/ArthurHub/HTML-Renderer for more information.
    /// </summary>
    public static partial class HtmlDocumentBuilderExtensions
    {
        private static CssData? GetCssDataFromStream(Stream? styleSheetStream)
        {
            if (styleSheetStream == null)
            {
                return null;
            }

            var styleSheet = StreamHelper.GetStreamContents(styleSheetStream);
            return PdfGenerator.ParseStyleSheet(styleSheet);
        }

        private static async Task<string> GetHtml(HtmlDocument htmlDocument)
        {
            var htmlDocumentStream = new MemoryStream();
            await htmlDocument.SaveAsync(htmlDocumentStream);
            return StreamHelper.GetStreamContents(htmlDocumentStream);
        }
    }
}