using System;
using System.ComponentModel;
using System.Reflection;
using TedDocumentExtractorApi.LookUps;

namespace TedDocumentExtractorApi.Converters
{
	public class LanguageStringToEnumConverter
	{
		public static Language GetEnumValueFromDescription(string description)
		{
			var fis = (MemberInfo[])typeof(Language).GetFields();

			foreach (var fi in fis)
			{
				var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
					return (Language)Enum.Parse(typeof(Language), fi.Name);
			}

			return Language.Unknown;
		}
	}
}