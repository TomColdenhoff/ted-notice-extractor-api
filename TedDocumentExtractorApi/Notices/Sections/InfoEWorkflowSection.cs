using System.Collections.Generic;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class InfoEWorkflowSection : Section
	{
		public string EOrderingUsed { get; set; }
		
		public string EInvoicingUsed { get; set; }
		
		public string EPaymentUsed { get; set; }
		
		public InfoEWorkflowSection(string sectionName) : base("VI.2", sectionName, null)
		{
		}
	}
}