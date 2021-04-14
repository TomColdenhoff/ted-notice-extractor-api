using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class RecurrenceInfoSection : Section
	{
		public bool RecurrentProcurement { get; set; }
		
		public string FurtherNoticeTiming { get; set; }
		
		public RecurrenceInfoSection(string sectionName) : base("VI.1", sectionName, null)
		{
		}
	}
}