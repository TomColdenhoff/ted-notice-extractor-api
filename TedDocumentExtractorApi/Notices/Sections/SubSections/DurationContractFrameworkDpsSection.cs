using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class DurationContractFrameworkDpsSection : Section
	{
		public int DurationMonths { get; set; }
		
		public int InDays { get; set; }
		
		public string Starting { get; set; }
		
		public string End { get; set; }
		
		public bool RenewalsSubject { get; set; }
		
		public string RenewalsDescription { get; set; }
		
		public DurationContractFrameworkDpsSection(string sectionName) : base("II.2.7", sectionName, null)
		{
			
		}
	}
}