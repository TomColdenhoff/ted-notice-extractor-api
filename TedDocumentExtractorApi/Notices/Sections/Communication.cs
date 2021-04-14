using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class Communication : Section
	{
		public string AddressObtainDocsUrl { get; set; }
		
		public string AddressFurtherInfo { get; set; }
		
		public NameAddressContact AddressFurtherInfoNameAddressContact { get; set; }
		
		public string AddressSendTendersUrl { get; set; }
		
		public bool AddressToAbove { get; set; }
		
		public NameAddressContact AddressFollowing { get; set; }

		public Communication(string sectionName) : base("I.3", sectionName, null)
		{
		}
	}
}