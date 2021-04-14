using System;
using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class MinTimeMaintainSection : Section
	{
		public DateTime DateTenderValid { get; set; }
		
		public int DurationMonths { get; set; }
		
		public string FromStatedDate { get; set; }
		
		public MinTimeMaintainSection(string sectionName) : base("IV.2.6", sectionName, null)
		{
		}
	}
}