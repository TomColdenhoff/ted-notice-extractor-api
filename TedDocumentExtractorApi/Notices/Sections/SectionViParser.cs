using System;
using System.Globalization;
using System.Text.RegularExpressions;
using TedDocumentExtractorApi.LookUps;
using TedDocumentExtractorApi.Notices.Sections.SubSections;
using TedDocumentExtractorApi.Notices.Sections.ValueObjects;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class SectionViParser : SectionParser, ISectionParser
	{
		public SectionViParser(string noticeContent, TedLabelDictionary tedLabelDictionary, Language noticeLanguage) : base(noticeContent, tedLabelDictionary, noticeLanguage)
		{
		}

		public Section Parse()
		{
			var recurrenceInfoSection = ParseRecurrenceInfoSection();
			var infoEWorkflowSection = ParseInfoEWorkflowSection();
			var infoAdditionalSection = ParseInfoAdditionalSection();
			var appealsProcedureSection = ParseAppealsProcedureSection();
			var dateDispatchSection = ParseDateDispatchSection();
			var sectionVi = new Section("Section VI",
				TedLabelDictionary.GetTranslationFor("info_complement", NoticeLanguage), new Section[] {recurrenceInfoSection, infoEWorkflowSection, infoAdditionalSection, appealsProcedureSection, dateDispatchSection});

			return sectionVi;
		}
		
		private RecurrenceInfoSection ParseRecurrenceInfoSection()
		{
			var recurrenceInfoTranslation = TedLabelDictionary.GetTranslationFor("recurrence_info", NoticeLanguage);
			var recurrentProcurementTranslation =
				TedLabelDictionary.GetTranslationFor("recurrent_procurement", NoticeLanguage);
			var furtherNoticesTimingTranslation =
				TedLabelDictionary.GetTranslationFor("further_notices_timing", NoticeLanguage);
			var yesTranslation = TedLabelDictionary.GetTranslationFor("_yes", NoticeLanguage);
			
			var recurrentProcurementMatch = Regex.Match(NoticeContent,
				$@"(?<={recurrentProcurementTranslation}: )(.*?)\s?(?={furtherNoticesTimingTranslation}|VI\.)", RegexOptions.IgnoreCase);
			
			//TODO match further_notices_timing

			return new RecurrenceInfoSection(recurrenceInfoTranslation)
			{
				RecurrentProcurement = recurrentProcurementMatch.Groups[1].Value == yesTranslation
			};
		}

		private InfoEWorkflowSection ParseInfoEWorkflowSection()
		{
			var infoEWorkflowTranslation = TedLabelDictionary.GetTranslationFor("info_eworkflow", NoticeLanguage);
			var eOrderingUsedTranslation = TedLabelDictionary.GetTranslationFor("eordering_used", NoticeLanguage);
			var eInvoicingUsedTranslation = TedLabelDictionary.GetTranslationFor("einvoicing_used", NoticeLanguage);
			var ePaymentUsedTranslation = TedLabelDictionary.GetTranslationFor("epayment_used", NoticeLanguage);

			var eOrderingUsedMatch = Regex.Match(NoticeContent, eOrderingUsedTranslation, RegexOptions.IgnoreCase);
			var eInvoicingUsedMatch = Regex.Match(NoticeContent, eInvoicingUsedTranslation, RegexOptions.IgnoreCase);
			var ePaymentUsedMatch = Regex.Match(NoticeContent, ePaymentUsedTranslation, RegexOptions.IgnoreCase);

			// TODO Validate eordering_used, einvoicing_used, epayment_used
			
			return new InfoEWorkflowSection(infoEWorkflowTranslation)
			{
				EOrderingUsed = eOrderingUsedMatch.Value,
				EInvoicingUsed = eInvoicingUsedMatch.Value,
				EPaymentUsed = ePaymentUsedMatch.Value
			};
		}

		private InfoAdditionalSection ParseInfoAdditionalSection()
		{
			var infoAdditionalTranslation = TedLabelDictionary.GetTranslationFor("info_additional", NoticeLanguage);

			var infoAdditionalMatch =
				Regex.Match(NoticeContent, $@"(?<={infoAdditionalTranslation} )(.*?)\s?(?=VI\.)", RegexOptions.IgnoreCase);

			//TODO validate info_additional
			
			return new InfoAdditionalSection("VI.3", infoAdditionalTranslation)
			{
				InfoAdditional = infoAdditionalMatch.Value
			};
		}

		private Section ParseAppealsProcedureSection()
		{
			var appealsBodySection = ParseAppealsBody();
			var mediationBodySection = ParseMediationBody();
			var appealsLodgingSection = ParseAppealsLodging();
			var appealsInfoSection = ParseAppealsInfo();
			return new Section("VI.4", TedLabelDictionary.GetTranslationFor("appeals_procedure", NoticeLanguage),
				new Section[] {appealsBodySection, mediationBodySection, appealsLodgingSection, appealsInfoSection});
		}

		private AppealsBodySection ParseAppealsBody()
		{
			var appealsBodyTranslation = TedLabelDictionary.GetTranslationFor("appeals_body", NoticeLanguage);
			
			var appealsBodyMatch =
				Regex.Match(NoticeContent, $@"(?<={appealsBodyTranslation} )(.*?)\s?(?=VI\.)", RegexOptions.IgnoreCase);

			var appealsBody = ParseAppealsBody(appealsBodyMatch.Groups[1].Value);

			return new AppealsBodySection(appealsBodyTranslation)
			{
				AppealsBody = appealsBody
			};
		}

		private MediationBodySection ParseMediationBody()
		{
			var mediationBodyTranslation = TedLabelDictionary.GetTranslationFor("mediation_body", NoticeLanguage);
			
			var mediationBodyMatch =
				Regex.Match(NoticeContent, $@"(?<={mediationBodyTranslation} )(.*?)\s?(?=VI\.)", RegexOptions.IgnoreCase);

			var mediationBody = ParseAppealsBody(mediationBodyMatch.Groups[1].Value);
			
			//TODO validate mediation_body

			return new MediationBodySection(mediationBodyTranslation)
			{
				MediationBody = mediationBody
			};
		}

		private AppealsBody ParseAppealsBody(string segment)
		{
			var nameOfficialTranslation = TedLabelDictionary.GetTranslationFor("name_official", NoticeLanguage);
			var addressPostalTranslation = TedLabelDictionary.GetTranslationFor("address_postal", NoticeLanguage);
			var addressTownTranslation = TedLabelDictionary.GetTranslationFor("address_town", NoticeLanguage);
			var addressPostcodeTranslation = TedLabelDictionary.GetTranslationFor("address_postcode", NoticeLanguage);
			var addressCountryTranslation = TedLabelDictionary.GetTranslationFor("address_country", NoticeLanguage);
			var addressEmailTranslation = TedLabelDictionary.GetTranslationFor("address_email", NoticeLanguage);
			var addressPhoneTranslation = TedLabelDictionary.GetTranslationFor("address_phone", NoticeLanguage);
			var internetTranslation = TedLabelDictionary.GetTranslationFor("internet", NoticeLanguage);
			var addressFaxTranslation = TedLabelDictionary.GetTranslationFor("address_fax", NoticeLanguage);
			
			var nameOfficial = Regex.Match(segment, $@"(?<={nameOfficialTranslation}: )(.*?)\s(?={addressPostalTranslation})").Groups[1].Value;
			var addressPostal = Regex.Match(segment, @$"(?<={addressPostalTranslation}: )(.*?)\s(?={addressTownTranslation})").Groups[1].Value;
			var addressTown = Regex.Match(segment, @$"(?<={addressTownTranslation}: )(.*?)\s(?={addressPostcodeTranslation})").Groups[1].Value;
			var postcode = Regex.Match(segment, @$"(?<={addressPostcodeTranslation}: )(.*?)\s(?={addressCountryTranslation})").Groups[1].Value;
			var addressCountry = Regex.Match(segment, @$"(?<={addressCountryTranslation}:)(.*?)\s(?={addressEmailTranslation})").Groups[1].Value;
			var addressEmail = Regex.Match(segment, @$"(?<={addressEmailTranslation}: )(.*?)\s(?={addressPhoneTranslation})").Groups[1].Value;
			var addressPhone = Regex.Match(segment, @$"(?<={addressPhoneTranslation}: )(.*?)\s(?={internetTranslation})").Groups[1].Value;
			var internet = Regex.Match(segment, $@"(?<={internetTranslation}: )(.*?)\s(?={addressFaxTranslation})")
				.Groups[1].Value.Replace(" ", "");
			var addressFax = Regex.Match(segment, @$"(?<={addressFaxTranslation}: )(.*?)\s(?=)").Groups[1].Value;

			var appealsBody = new AppealsBody()
			{
				NameOfficial = nameOfficial,
				AddressPostal = addressPostal,
				AddressTown = addressTown,
				AddressPostcode = postcode,
				AddressCountry = addressCountry,
				AddressEmail = addressEmail,
				AddressPhone = addressPhone,
				Internet = internet,
				AddressFax = addressFax
			};

			return appealsBody;
		}

		private AppealsLodgingSection ParseAppealsLodging()
		{
			var appealsLodgingTranslation = TedLabelDictionary.GetTranslationFor("appeals_lodging", NoticeLanguage);
			var appealsDeadlineTranslation = TedLabelDictionary.GetTranslationFor("appeals_deadline", NoticeLanguage);
			
			var appealsDeadlineMatch = Regex.Match(NoticeContent, $@"(?<={appealsDeadlineTranslation}: )(.*?)\s?(?=VI\.)", RegexOptions.IgnoreCase);

			// TODO validate appeals_deadline
			
			return new AppealsLodgingSection(appealsLodgingTranslation)
			{
				AppealsDeadline = appealsDeadlineMatch.Groups[1].Value
			};
		}

		private AppealsInfoSection ParseAppealsInfo()
		{
			var appealsInfoTranslation = TedLabelDictionary.GetTranslationFor("appeals_info", NoticeLanguage);
			
			var appealsInfoMatch =
				Regex.Match(NoticeContent, $@"(?<={appealsInfoTranslation} )(.*?)\s?(?=VI\.)", RegexOptions.IgnoreCase);

			var appealsInfo = ParseAppealsBody(appealsInfoMatch.Groups[1].Value);
			
			//TODO validate appeals_info

			return new AppealsInfoSection(appealsInfoTranslation)
			{
				AppealsInfo = appealsInfo
			};
		}

		private DateDispatchSection ParseDateDispatchSection()
		{
			var dateDispatchTranslation = TedLabelDictionary.GetTranslationFor("date_dispatch", NoticeLanguage);

			var dateDispatchMatch = Regex.Match(NoticeContent, $"(?<={dateDispatchTranslation} )(.*)",
				RegexOptions.IgnoreCase);
			// 
			return new DateDispatchSection(dateDispatchTranslation)
			{
				DateDispatch = DateTime.ParseExact(dateDispatchMatch.Groups[1].Value, "dd/MM/yyyy", CultureInfo.CurrentCulture)
			};
		}
	}
}