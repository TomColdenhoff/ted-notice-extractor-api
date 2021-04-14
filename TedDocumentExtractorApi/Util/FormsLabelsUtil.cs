using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using TedDocumentExtractorApi.Converters;
using TedDocumentExtractorApi.LookUps;

namespace TedDocumentExtractorApi.Util
{
	public class FormsLabelsUtil
	{
		private static readonly int LabelColumn = 0, BeginTranslatedLabelColumn = 3, EndTranslatedLabelColumn = 27;
		private static readonly string[] LanguageLookUp = new string[EndTranslatedLabelColumn + 1];
		
		public static Dictionary<string, Dictionary<Language, string>> ParseLabelMappingSheet(string filepath)
		{
			
			var translations = new Dictionary<string, Dictionary<Language, string>>();

			using var stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
			using var reader = ExcelReaderFactory.CreateReader(stream);
			reader.Read();
			for (var column = BeginTranslatedLabelColumn; column <= EndTranslatedLabelColumn; column++)
			{
				LanguageLookUp[column] = reader.GetString(column);
			}
					
			do
			{
				while (reader.Read())
				{
					translations.Add(reader.GetString(LabelColumn), ParseRow(reader));
				}
			} while (reader.NextResult());

			return translations;
		}

		private static Dictionary<Language, string> ParseRow(IExcelDataReader row)
		{
			var translatedLabels = new Dictionary<Language, string>();
			for (var column = BeginTranslatedLabelColumn; column <= EndTranslatedLabelColumn; column++)
			{

				var language = LanguageStringToEnumConverter.GetEnumValueFromDescription(LanguageLookUp[column]);
				if (language == Language.Unknown)
				{
					continue;
				}
				
				translatedLabels.Add(language, row.GetString(column));
			}

			return translatedLabels;
		}
	}
}