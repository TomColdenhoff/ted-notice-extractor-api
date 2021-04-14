using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		public IActionResult Extract([FromForm] IFormFile file)
		{
			if (file.ContentType != "application/pdf")
			{
				return new UnsupportedMediaTypeResult();
			}

			var content = PdfUtil.ExtractStringFromPdf(file.OpenReadStream());

			var hasAcceptHeader = HttpContext.Request.Headers.TryGetValue("Accept", out var value);
			if (!hasAcceptHeader)
			{
				return BadRequest("Request is missing 'Accept' header");
			}
			
			switch (value)
			{
				case "text/plain":
					return Ok(content);
				case "application/json":
					Response.Headers["Content-Type"] = "application/vnd.tomcoldenhoff+json";

					var parser = _noticeParserFactory.GetNoticeParser(content);
					
					return Ok(parser.ParseNotice());
				default:
					return BadRequest("Server isn't able to fulfill negotiation about client 'Accept' header. Can fulfill: text/plain, application/json");
			}
		}
	}
}