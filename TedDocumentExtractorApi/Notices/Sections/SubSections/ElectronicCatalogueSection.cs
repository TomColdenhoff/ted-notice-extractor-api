using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class ElectronicCatalogueSection : Section
	{
		public string ElectronicCatalogueRequired { get; set; }
		
		public ElectronicCatalogueSection(string sectionName) : base("II.2.12", sectionName, null)
		{
		}
	}
}