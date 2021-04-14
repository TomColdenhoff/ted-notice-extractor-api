using TedDocumentExtractorApi.LookUps;
using TedDocumentExtractorApi.Notices.Sections;

namespace TedDocumentExtractorApi.Notices
{
	public class NoticeContractParser : NoticeParser
	{
		public NoticeContractParser(string noticeContent, Language noticeLanguage, TedLabelDictionary tedLabelDictionary) : base(noticeContent, noticeLanguage, tedLabelDictionary)
		{
		}

		public override Notice ParseNotice()
		{
			return ParseContractNotice();
		}
		
		private NoticeContract ParseContractNotice()
		{
			var sectionI = SectionParserFactory.GetSectionParser(NoticeContent, TedLabelDictionary, NoticeLanguage, NoticeSection.SectionI).Parse();
			var sectionIi = SectionParserFactory.GetSectionParser(NoticeContent, TedLabelDictionary, NoticeLanguage, NoticeSection.SectionIi).Parse();
			var sectionIii = SectionParserFactory.GetSectionParser(NoticeContent, TedLabelDictionary, NoticeLanguage, NoticeSection.SectionIii).Parse();
			var sectionIv = SectionParserFactory.GetSectionParser(NoticeContent, TedLabelDictionary, NoticeLanguage, NoticeSection.SectionIv).Parse();
			var sectionVi = SectionParserFactory.GetSectionParser(NoticeContent, TedLabelDictionary, NoticeLanguage, NoticeSection.SectionIv).Parse();
			
			return new NoticeContract(new []{sectionI, sectionIi, sectionIii, sectionIv, sectionVi});
		}
	}
}