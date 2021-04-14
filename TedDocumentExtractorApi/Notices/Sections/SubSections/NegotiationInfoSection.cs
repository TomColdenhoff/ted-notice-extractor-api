using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class NegotiationInfoSection : Section
	{
		public string NegotiationWithout { get; set; }
		
		public NegotiationInfoSection(string sectionName) : base("IV.1.5", sectionName, null)
		{
		}
	}
}