using TedDocumentExtractorApi.LookUps;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public abstract class SectionParser
	{
		protected readonly string NoticeContent;
		protected readonly TedLabelDictionary TedLabelDictionary;
		protected readonly Language NoticeLanguage;
		
		public SectionParser(string noticeContent, TedLabelDictionary tedLabelDictionary, Language noticeLanguage)
		{
			NoticeContent = noticeContent;
			TedLabelDictionary = tedLabelDictionary;
			NoticeLanguage = noticeLanguage;
		}
	}
}