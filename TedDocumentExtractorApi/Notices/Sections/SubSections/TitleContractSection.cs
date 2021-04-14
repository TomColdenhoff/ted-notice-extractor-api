using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class TitleContractSection : Section
	{
		public string TitleContract { get; set; }
		
		public string LotNumber { get; set; }
		
		public TitleContractSection(string sectionName) : base("II.2.1", sectionName, null)
		{
		}
	}
}