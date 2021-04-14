using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class InfoAdditionalSection : Section
	{
		
		public string InfoAdditional { get; set; }
		
		public InfoAdditionalSection(string sectionNumber, string sectionName) : base(sectionNumber, sectionName, null)
		{
		}
	}
}