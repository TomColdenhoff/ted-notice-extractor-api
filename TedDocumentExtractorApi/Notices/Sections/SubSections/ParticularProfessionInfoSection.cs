using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class ParticularProfessionInfoSection : Section
	{
		public string ParticularProfessionReserved { get; set; }
		
		public string RefLawRegProv { get; set; }
		
		public ParticularProfessionInfoSection(string sectionName) : base("III.2.1", sectionName, null)
		{
		}
	}
}