using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class EuProgrInfoSection : Section
	{
		
		public bool EuProgrRelated { get; set; }
		
		public string EuProgrRef { get; set; }
		
		public EuProgrInfoSection(string sectionName) : base("II.2.13", sectionName, null)
		{
		}
	}
}