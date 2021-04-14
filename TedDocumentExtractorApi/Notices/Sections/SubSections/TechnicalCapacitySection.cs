using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class TechnicalCapacitySection : Section
	{
		public string[] InfoEvaluatingWethRequir { get; set; }
		
		public string[] MinStandardsRequired { get; set; }
		
		public TechnicalCapacitySection(string sectionName) : base("III.1.3", sectionName, null)
		{
		}
	}
}