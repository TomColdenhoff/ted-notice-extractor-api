using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class DescriptionProcurementSection : Section
	{
		public string DescriptionProcurement { get; set; }
		
		public DescriptionProcurementSection(string sectionName) : base("II.2.4", sectionName, null)
		{
		}
	}
}