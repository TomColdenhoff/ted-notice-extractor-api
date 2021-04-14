using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class FrameworkContractDpsInfoSection : Section
	{
		public string NoticeInvolvesFramework { get; set; }
		
		public string FrameworkSingle { get; set; }
		
		public string FrameworkSeveral { get; set; }
		
		public string[] FrameworkParticipEnvis { get; set; }
		
		public string NoticeInvolvesDps { get; set; }
		
		public string DpsPurchasers { get; set; }
		
		public string FrameworkJustFour { get; set; }
		
		public FrameworkContractDpsInfoSection(string sectionName) : base("IV.1.3", sectionName, null)
		{
		}
	}
}