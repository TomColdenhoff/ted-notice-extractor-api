using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class ConditionsParticipSection : Section
	{
		public ConditionsParticipSection(string sectionName, IEnumerable<Section> sections) : base("III.1", sectionName, sections)
		{
		}
	}
}