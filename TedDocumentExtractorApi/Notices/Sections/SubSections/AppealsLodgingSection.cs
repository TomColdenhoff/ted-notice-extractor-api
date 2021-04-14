using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class AppealsLodgingSection : Section
	{
		public string AppealsDeadline { get; set; }
		
		public AppealsLodgingSection(string sectionName) : base("VI.4.3", sectionName, null)
		{
		}
	}
}