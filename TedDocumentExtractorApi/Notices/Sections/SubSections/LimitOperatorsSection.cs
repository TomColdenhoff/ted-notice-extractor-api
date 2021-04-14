using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class LimitOperatorsSection : Section
	{
		public int EnvisagedNumber { get; set; }
		
		public int EnvisagedMin { get; set; }
		
		public int MaxNumber { get; set; }
		
		public string CriteriaChoosingLimited { get; set; }
		
		public LimitOperatorsSection(string sectionName) : base("II.2.9", sectionName, null)
		{
		}
	}
}