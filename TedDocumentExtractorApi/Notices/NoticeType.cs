using System.ComponentModel;

namespace TedDocumentExtractorApi.Notices
{
	public enum NoticeType
	{
		Unknown,
		
		[Description("notice_pin")]
		NoticePin,
		
		[Description("notice_contract")]
		NoticeContract,
		
		[Description("notice_contract_award")]
		NoticeContractAward
	}
}