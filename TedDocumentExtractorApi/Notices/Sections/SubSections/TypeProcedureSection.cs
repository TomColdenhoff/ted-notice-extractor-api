using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class TypeProcedureSection : Section
	{
		public string TypeProcedure { get; set; }

		public TypeProcedureSection(string sectionName) : base("IV.1", sectionName, null)
		{
		}
	}
}