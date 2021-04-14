using System;
using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class DateDispatchTenderParticipateSection : Section
	{
		public DateTime Date { get; set; }
		
		public DateDispatchTenderParticipateSection(string sectionName) : base("IV.2.3", sectionName, null)
		{
		}
	}
}