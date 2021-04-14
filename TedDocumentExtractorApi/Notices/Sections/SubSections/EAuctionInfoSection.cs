using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class EAuctionInfoSection : Section
	{
		public string EAuctionWillUsed { get; set; }
		
		public string EAuctionInfoAddit { get; set; }
		
		public EAuctionInfoSection(string sectionName) : base("IV.1.6", sectionName, null)
		{
		}
	}
}