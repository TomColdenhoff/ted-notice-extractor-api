using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class LotsInfoSection : Section
	{
		public bool DivisionLots { get; set; }
		
		public string LotsSubmittedFor { get; set; }
		
		public LotsInfoSection(string sectionName) : base("II.1.6", sectionName, null)
		{
		}
	}
}