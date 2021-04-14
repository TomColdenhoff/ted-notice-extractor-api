using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class SituationPersonalInclSection : Section
	{
		
		public string[] Requirements { get; set; }
		
		public SituationPersonalInclSection(string sectionName) : base("III.1.1", sectionName, null)
		{
		}
	}
}