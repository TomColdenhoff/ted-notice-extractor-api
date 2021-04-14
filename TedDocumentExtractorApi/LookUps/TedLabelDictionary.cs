using System;
using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using TedDocumentExtractorApi.Converters;
using TedDocumentExtractorApi.Util;

namespace TedDocumentExtractorApi.LookUps
{
	public class TedLabelDictionary
	{
		private readonly Dictionary<string, Dictionary<Language, string>> translations;
		
		public TedLabelDictionary()
		{
			translations = FormsLabelsUtil.ParseLabelMappingSheet("Forms_Labels_R209.xlsx");
		}

		public string GetTranslationFor(string label, Language language)
		{
			return translations[label][language];
		}

	}
}