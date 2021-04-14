using System.Collections.Generic;
using TedDocumentExtractorApi.Notices.Sections.ValueObjects;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class MediationBodySection : Section
	{
		public AppealsBody MediationBody { get; set; }
		
		public MediationBodySection(string sectionName) : base("VI.4.2", sectionName, null)
		{
		}
	}
}