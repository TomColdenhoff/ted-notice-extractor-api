using System.Collections.Generic;
using TedDocumentExtractorApi.Notices.Sections.ValueObjects;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class PlacePerformanceSection : Section
	{
		public NutsCode[] NutsCodes { get; set; }
		
		public string MainSitePlaceWorksDelivery { get; set; }
		
		public PlacePerformanceSection(string sectionName) : base("II.2.3", sectionName, null)
		{
		}
	}
}