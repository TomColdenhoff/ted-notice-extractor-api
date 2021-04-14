using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class LanguagesAllowedSection : Section
	{
		public string[] LanguagesAllowed { get; set; }
		
		public LanguagesAllowedSection(string sectionName) : base("IV.2.4", sectionName, null)
		{
		}
	}
}