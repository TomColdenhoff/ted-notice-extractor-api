using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class TypeContract : Section
	{
		public string Value { get; set; }
		
		public TypeContract(string sectionName) : base("II.1.3", sectionName, null)
		{
		}
	}
}