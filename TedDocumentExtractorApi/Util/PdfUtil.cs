using System;
using System.IO;
using System.Linq;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Geometry;

namespace TedDocumentExtractorApi.Util
{
	public class PdfUtil
	{
		public static string ExtractStringFromPdf(Stream stream)
		{
			var stringBuilder = new StringBuilder();
			
			using var document = PdfDocument.Open(stream);
			foreach (var page in document.GetPages())
			{
				var areaWithoutBorders = new PdfRectangle(0, 50, page.Width, page.Height - 50);
				var words = page.GetWords().Where(w => areaWithoutBorders.Contains(w.BoundingBox)).ToList();
				var pageText = string.Join(" ", words);
				stringBuilder.Append(pageText);
			}

			return stringBuilder.ToString();
		}
	}
}