using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class ContractsReservedSection : Section
	{
		public string RestrictedShelteredWorkshop { get; set; }
		
		public string RestrictedShelteredProgram { get; set; }
		
		public ContractsReservedSection(string sectionName) : base("III.1.5", sectionName, null)
		{
		}
	}
}