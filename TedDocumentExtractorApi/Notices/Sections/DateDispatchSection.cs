using System;
using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class DateDispatchSection : Section
	{
		public DateTime DateDispatch { get; set; }
		
		public DateDispatchSection(string sectionName) : base("VI.5", sectionName, null)
		{
		}
	}
}