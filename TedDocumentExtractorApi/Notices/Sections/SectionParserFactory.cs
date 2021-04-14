using System;
using TedDocumentExtractorApi.LookUps;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public static class SectionParserFactory
	{
		public static ISectionParser GetSectionParser(string noticeContent, TedLabelDictionary tedLabelDictionary,
			Language noticeLanguage, NoticeSection noticeSection)
		{
			switch (noticeSection)
			{
				case NoticeSection.SectionI: 
					return new SectionIParser(noticeContent, tedLabelDictionary, noticeLanguage);
				case NoticeSection.SectionIi:
					return new SectionIiParser(noticeContent, tedLabelDictionary, noticeLanguage);
				case NoticeSection.SectionIii:
					return new SectionIiiParser(noticeContent, tedLabelDictionary, noticeLanguage);
				case NoticeSection.SectionIv:
					return new SectionIvParser(noticeContent, tedLabelDictionary, noticeLanguage);
				case NoticeSection.SectionV:
					break;
				case NoticeSection.SectionVi:
					return new SectionViParser(noticeContent, tedLabelDictionary, noticeLanguage);
				default:
					throw new ArgumentOutOfRangeException(nameof(noticeSection), noticeSection, null);
			}

			throw new ArgumentException("Couldn't create a parser for the given section");
		}
	}
}