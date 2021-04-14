using System;
using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class LimitReceiptTendersReqPartSection : Section
	{
		public DateTime DateTime { get; set; }
		
		public LimitReceiptTendersReqPartSection(string sectionName) : base("IV.2.2", sectionName, null)
		{
		}
	}
}