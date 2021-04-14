using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class QuantityScopeContract : Section
	{
		public QuantityScopeContract(string sectionName, IEnumerable<Section> sections) : base("II.1", sectionName, sections)
		{
		}
	}
}