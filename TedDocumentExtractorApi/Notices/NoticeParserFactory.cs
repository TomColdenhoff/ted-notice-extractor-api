using System;
using System.Linq;
using System.Text.RegularExpressions;
using NTextCat;
using TedDocumentExtractorApi.LookUps;

namespace TedDocumentExtractorApi.Notices
{
	public class NoticeParserFactory
	{
		private readonly TedLabelDictionary _tedLabelDictionary;
		private readonly RankedLanguageIdentifier _rankedLanguageIdentifier;
		
		public NoticeParserFactory()
		{
			_tedLabelDictionary = new TedLabelDictionary();
			
			var factory = new RankedLanguageIdentifierFactory();
			_rankedLanguageIdentifier = factory.Load("Core14.profile.xml");
		}
		
		/// <summary>
		/// Returns a notice parser bases on the type of notice that needs to be parsed.
		/// </summary>
		/// <param name="noticeContent"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public NoticeParser GetNoticeParser(string noticeContent)
		{
			var language = DetectLanguage(noticeContent);
			
			switch (ParseNoticeType(noticeContent, language))
			{
				case NoticeType.NoticeContract:
					return new NoticeContractParser(noticeContent, language, _tedLabelDictionary);
				case NoticeType.Unknown:
					throw new NotImplementedException();
					break;
				case NoticeType.NoticePin:
					throw new NotImplementedException();
					break;
				case NoticeType.NoticeContractAward:
					throw new NotImplementedException();
					break;
				default:
					return null;
			}
		}
		
		private Language DetectLanguage(string noticeContent)
		{
			// can be an absolute or relative path. Beware of 260 chars limitation of the path length in Windows. Linux allows 4096 chars.
			var languages = _rankedLanguageIdentifier.Identify(noticeContent.Substring(0, 500));
			var iso = languages.FirstOrDefault()?.Item1.Iso639_2T;

			if (iso == null)
			{
				return Language.Unknown;
			}
			
			var success = Enum.TryParse(typeof(Language), iso, true, out var language);
			
			return success ? (Language)language : Language.Unknown;
		}

		private NoticeType ParseNoticeType(string noticeContent, Language noticeLanguage)
		{
			var translatedValue = _tedLabelDictionary.GetTranslationFor("notice_contract", noticeLanguage);
			
			if (Regex.Match(noticeContent, translatedValue, RegexOptions.IgnoreCase).Success)
			{
				return NoticeType.NoticeContract;
			}
			
			throw new NotImplementedException();
		}
	}
}