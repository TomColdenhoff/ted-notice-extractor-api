using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class CpvCodes : Section
	{
		public Cpv[] CpvMain { get; set; }
		
		public Cpv[] CpvAdditional { get; set; }
		
		public CpvCodes(string sectionNumber, string sectionName) : base(sectionNumber, sectionName, null)
		{
		}
	}
}