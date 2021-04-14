using System;
using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections.SubSections
{
	public class OpeningConditionsSection : Section
	{
		public DateTime DateTime { get; set; }
		
		public string OpeningPlace { get; set; }
		
		public string OpeningAdditInfo { get; set; }
		
		public OpeningConditionsSection(string sectionName) : base("IV.2.7", sectionName, null)
		{
		}
	}
}