using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class NameAddressContactSection : Section
	{

		public NameAddressContact NameAddressContact { get; set; }
		
		public NameAddressContactSection(string translatedSectionName) : base("I.1", translatedSectionName, null)
		{
			
		}
	}
}