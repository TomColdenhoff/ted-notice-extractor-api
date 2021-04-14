using System.Collections.Generic;
using TedDocumentExtractorApi.Notices.Sections.ValueObjects;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class AppealsInfoSection : Section
	{
		public AppealsBody AppealsInfo { get; set; }
		
		public AppealsInfoSection(string sectionName) : base("VI.4.4", sectionName, null)
		{ }
	}
}