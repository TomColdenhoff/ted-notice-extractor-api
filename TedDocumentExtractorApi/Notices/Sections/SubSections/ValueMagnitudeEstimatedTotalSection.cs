using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class ValueMagnitudeEstimatedTotalSection : Section
	{
		
		public string ValueExclusiveVat { get; set; }
		
		public string Currency { get; set; }
		
		public ValueMagnitudeEstimatedTotalSection(string sectionNumber, string sectionName) : base(sectionNumber, sectionName, null)
		{
		}
	}
}