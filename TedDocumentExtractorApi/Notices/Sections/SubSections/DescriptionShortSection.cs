using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class DescriptionShortSection : Section
	{
		
		public string Value { get; set; }
		
		public DescriptionShortSection(string sectionName) : base("II.1.4", sectionName, null)
		{
			
		}
	}
}