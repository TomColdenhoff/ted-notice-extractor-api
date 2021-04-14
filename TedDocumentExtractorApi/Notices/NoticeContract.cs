using System.Collections.Generic;
using TedDocumentExtractorApi.Notices.Sections;

namespace TedDocumentExtractorApi.Notices
{
	public class NoticeContract : Notice
	{
		
		public NoticeContract(IEnumerable<Section> sections)
		{
			NoticeType = NoticeType.NoticeContract;
			FormType = "F02";
			Sections = sections;
		}
	}
}