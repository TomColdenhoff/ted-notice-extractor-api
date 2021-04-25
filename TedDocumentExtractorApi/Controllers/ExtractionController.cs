using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TedDocumentExtractorApi.Notices;
using TedDocumentExtractorApi.Util;

namespace TedDocumentExtractorApi.Controllers
{
	[Route("api/[controller]")]
	public class ExtractionController : ControllerBase
	{
		private readonly NoticeParserFactory _noticeParserFactory;

		public ExtractionController(NoticeParserFactory noticeParserFactory)
		{
			_noticeParserFactory = noticeParserFactory;
		}
		
		
		[HttpPost]
		[Consumes("multipart/form-data", "application/pdf")]
		[Route("file")]
		public IActionResult ExtractFile([FromForm] IFormFile file)
		{
			if (file != null && file.ContentType != "application/pdf")
			{
				return new UnsupportedMediaTypeResult();
			}
			
			var hasAcceptHeader = HttpContext.Request.Headers.TryGetValue("Accept", out var value);
			if (!hasAcceptHeader)
			{
				return BadRequest("Request is missing 'Accept' header");
			}
			
			var content = PdfUtil.ExtractStringFromPdf(file.OpenReadStream());
			
			return ParseAndCreateActionResult(value, content);
		}
		
		[HttpPost]
		[Consumes("text/plain")]
		[Route("text")]
		public async Task<IActionResult> ExtractText()
		{
			using var reader = new StreamReader(Request.Body);
			var noticeString = await reader.ReadToEndAsync(); // Read the content body of the text/plain request
			
			if (string.IsNullOrWhiteSpace(noticeString))
			{
				return BadRequest();
			}

			var hasAcceptHeader = HttpContext.Request.Headers.TryGetValue("Accept", out var value);
			if (!hasAcceptHeader)
			{
				return BadRequest("Request is missing 'Accept' header");
			}

			return ParseAndCreateActionResult(value, noticeString);
		}

		private IActionResult ParseAndCreateActionResult(StringValues value, string content)
		{
			switch (value)
			{
				case "text/plain":
					return Ok(content);
				case "application/json":
					Response.Headers["Content-Type"] = "application/vnd.tomcoldenhoff+json";

					var parser = _noticeParserFactory.GetNoticeParser(content);

					return Ok(parser.ParseNotice());
				default:
					return BadRequest(
						"Server isn't able to fulfill negotiation about client 'Accept' header. Can fulfill: text/plain, application/json");
			}
		}
	}
}