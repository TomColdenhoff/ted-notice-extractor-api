using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class MainActivity : Section
	{
		public string Value { get; set; }
		
		public MainActivity(string sectionName) : base("I.5", sectionName, null)
		{
		}
	}
}