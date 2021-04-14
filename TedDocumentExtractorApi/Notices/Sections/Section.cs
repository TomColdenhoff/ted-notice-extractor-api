using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class Section
	{
		public string SectionNumber { get; set; }

		public string SectionName { get; set; }

		// Dangerous design decision. System.Text.Json doesn't support serializing derived types unless it's an object. 
		// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism
		public IEnumerable<object> Sections { get; private set; }

		public Section(string sectionNumber, string sectionName, IEnumerable<Section> sections)
		{
			SectionNumber = sectionNumber;
			SectionName = sectionName;
			Sections = sections;
		}

	}
}