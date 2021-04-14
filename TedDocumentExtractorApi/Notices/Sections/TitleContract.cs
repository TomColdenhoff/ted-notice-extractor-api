namespace TedDocumentExtractorApi.Notices.Sections
{
	public class TitleContract : Section
	{
		public string Title { get; set; }
		
		public string FileRef { get; set; }
		
		public TitleContract(string sectionNumber, string sectionName) : base(sectionNumber, sectionName, null)
		{
		}
	}
}