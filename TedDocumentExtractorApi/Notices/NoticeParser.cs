using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using NTextCat;
using TedDocumentExtractorApi.LookUps;
using TedDocumentExtractorApi.Notices.Sections;
using TedDocumentExtractorApi.Notices.Sections.SubSections;
using TedDocumentExtractorApi.Notices.Sections.ValueObjects;
using Language = TedDocumentExtractorApi.LookUps.Language;

namespace TedDocumentExtractorApi.Notices
{
	public abstract class NoticeParser
	{
		protected readonly string NoticeContent;
		protected readonly Language NoticeLanguage;
		protected readonly TedLabelDictionary TedLabelDictionary;

		public NoticeParser(string noticeContent, Language noticeLanguage, TedLabelDictionary tedLabelDictionary)
		{
			NoticeContent = noticeContent;
			NoticeLanguage = noticeLanguage;
			TedLabelDictionary = tedLabelDictionary;
		}
		
		public abstract Notice ParseNotice();
		




		

		

		

		

		
		
		

		
	}
}