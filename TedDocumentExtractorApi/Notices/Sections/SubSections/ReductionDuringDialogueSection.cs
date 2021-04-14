using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class ReductionDuringDialogueSection : Section
	{
		
		public string ReductionRecourse { get; set; }
		
		public ReductionDuringDialogueSection(string sectionName) : base("IV.1.4", sectionName, null)
		{
		}
	}
}