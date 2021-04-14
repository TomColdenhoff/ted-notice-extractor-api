using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class JointProcurement : Section
	{
		public string Value { get; set; }

		public JointProcurement(string sectionName) : base("I.2", sectionName, null)
		{
			
		}
	}
}