using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class EconomicFinancialStandingSection : Section
	{
		public string[] InfoEvaluatingWethRequir { get; set; }
		
		public string[] MinStandardsRequired { get; set; }
		
		public EconomicFinancialStandingSection(string sectionName) : base("III.1.2", sectionName, null)
		{
		}
	}
}