using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class VariantsInfoSection : Section
	{
		public bool VariantsAccepted { get; set; }
		
		public VariantsInfoSection(string sectionName) : base("II.2.10", sectionName, null)
		{
		}
	}
}