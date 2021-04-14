using System.Collections.Generic;
using TedDocumentExtractorApi.Notices.Sections.ValueObjects;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class AwardCriteriaSection : Section
	{
		public AwardCriteria[] AwardCriteria { get; set; }
		
		public Price Price { get; set; }

		public AwardCriteriaSection(string sectionName) : base("II.2.5", sectionName, null)
		{
		}
	}
}