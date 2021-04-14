using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class OptionsInfoSection : Section
	{
		public bool Options { get; set; }
		
		public string OptionsDescription { get; set; }
		
		public OptionsInfoSection(string sectionName) : base("II.2.11", sectionName, null)
		{
		}
	}
}