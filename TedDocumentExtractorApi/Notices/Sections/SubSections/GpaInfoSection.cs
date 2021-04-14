using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class GpaInfoSection : Section
	{
		public bool GpaCovered { get; set; }
		
		public GpaInfoSection(string sectionName) : base("IV.1.8", sectionName, null)
		{
		}
	}
}