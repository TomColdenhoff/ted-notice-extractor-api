using TedDocumentExtractorApi.Notices.Sections.ValueObjects;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class AppealsBodySection : Section
	{
		public AppealsBody AppealsBody { get; set; }
		
		public AppealsBodySection(string sectionName) : base("VI.4.1", sectionName, null)
		{
		}
	}
}