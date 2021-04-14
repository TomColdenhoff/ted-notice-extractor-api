using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class CaType : Section
	{
		public string Value { get; set; }

		public CaType(string sectionName) : base("I.4", sectionName, null)
		{
		}
	}
}