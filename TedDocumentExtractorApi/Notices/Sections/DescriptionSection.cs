using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class DescriptionSection : Section
	{
		public DescriptionSection(string sectionName, IEnumerable<Section> sections) : base("II.2", sectionName, sections)
		{
		}
	}
}