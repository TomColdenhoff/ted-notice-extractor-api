using System.Collections.Generic;
using TedDocumentExtractorApi.Notices.Sections;

namespace TedDocumentExtractorApi.Notices
{
	public abstract class Notice
	{
		public NoticeType NoticeType { get; set; }
		public string FormType { get; set; }

		public IEnumerable<Section> Sections { get; set; }
	}
}