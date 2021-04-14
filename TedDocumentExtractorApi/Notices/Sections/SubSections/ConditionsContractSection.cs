using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class ConditionsContractSection : Section	
	{
		public ConditionsContractSection(string sectionName, IEnumerable<Section> sections) : base("III.2", sectionName, sections)
		{
		}
	}
}