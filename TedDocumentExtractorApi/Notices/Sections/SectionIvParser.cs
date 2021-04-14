using System;
using System.Text.RegularExpressions;
using TedDocumentExtractorApi.LookUps;
using TedDocumentExtractorApi.Notices.Sections.SubSections;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class SectionIvParser : SectionParser, ISectionParser
	{
		public SectionIvParser(string noticeContent, TedLabelDictionary tedLabelDictionary, Language noticeLanguage) : base(noticeContent, tedLabelDictionary, noticeLanguage)
		{
		}

		public Section Parse()
		{
			var descriptionSection = ParseSectionIvDescriptionSection();
			var infoAdminSection = ParseSectionIvInfoAdminSection();
			var sectionIv = new Section("Section IV",
				TedLabelDictionary.GetTranslationFor("procedure", NoticeLanguage), new Section[] {descriptionSection, infoAdminSection});
			return sectionIv;
		}
		
		private Section ParseSectionIvDescriptionSection()
		{
			var typeProcedureSection = ParseTypeProcedure();
			var frameworkContractDpsInfoSection = ParseFrameworkContractDpsInfo();
			var reductionDuringDialogueSection = ParseReductionDuringDialogue();
			var negotiationInfoSection = ParseNegotiationInfo();
			var eAuctionInfoSection = ParseEAuctionInfo();
			var gpaInfoSection = ParseGpaInfo();
			
			return new Section("IV.1", TedLabelDictionary.GetTranslationFor("description", NoticeLanguage),
				new Section[] {typeProcedureSection, frameworkContractDpsInfoSection, reductionDuringDialogueSection, negotiationInfoSection, eAuctionInfoSection, gpaInfoSection});

		}

		private TypeProcedureSection ParseTypeProcedure()
		{
			var typeProcedureTranslation = TedLabelDictionary.GetTranslationFor("type_procedure", NoticeLanguage);

			var typeProcedureMatch = Regex.Match(NoticeContent,
				$@"(?<={typeProcedureTranslation} )(.*?)\s?(?=IV)", RegexOptions.IgnoreCase);

			return new TypeProcedureSection(typeProcedureTranslation)
			{
				TypeProcedure = typeProcedureMatch.Groups[1].Value
			};
		}

		private FrameworkContractDpsInfoSection ParseFrameworkContractDpsInfo()
		{
			var frameworkContractDpsInfoTranslation =
				TedLabelDictionary.GetTranslationFor("framework_contract_dps_info", NoticeLanguage);

			var noticeInvolvesFrameworkTranslation =
				TedLabelDictionary.GetTranslationFor("notice_involves_framework", NoticeLanguage);
			var frameworkSingleTranslation = TedLabelDictionary.GetTranslationFor("framework_single", NoticeLanguage);
			var frameworkSeveralTranslation =
				TedLabelDictionary.GetTranslationFor("framework_several", NoticeLanguage);
			var frameworkParticipEnvisTranslation =
				TedLabelDictionary.GetTranslationFor("framework_particip_envis", NoticeLanguage); // TODO
			var noticeInvolvesDpsTranslation = TedLabelDictionary.GetTranslationFor("notice_involves_dps", NoticeLanguage);
			var dpsPurchasersTranslation = TedLabelDictionary.GetTranslationFor("dps_purchasers", NoticeLanguage);
			var frameworkJustFourTranslation = TedLabelDictionary.GetTranslationFor("framework_just_four", NoticeLanguage);

			var noticeInvolvesFrameworkMatch = Regex.Match(NoticeContent, noticeInvolvesFrameworkTranslation,
				RegexOptions.IgnoreCase);
			var frameworkSingleMatch = Regex.Match(NoticeContent, frameworkSingleTranslation,
				RegexOptions.IgnoreCase);
			var frameworkSeveralMatch = Regex.Match(NoticeContent, frameworkSeveralTranslation,
				RegexOptions.IgnoreCase);
			var noticeInvolvesDpsMatch = Regex.Match(NoticeContent, noticeInvolvesDpsTranslation,
				RegexOptions.IgnoreCase);
			var dpsPurchasersMatch = Regex.Match(NoticeContent, dpsPurchasersTranslation,
				RegexOptions.IgnoreCase);

			// TODO Find notice for frameworkParticipEnvisTranslation, frameworkJustFourTranslation
			
			return new FrameworkContractDpsInfoSection(frameworkContractDpsInfoTranslation)
			{
				NoticeInvolvesFramework = noticeInvolvesFrameworkMatch.Value,
				FrameworkSingle = frameworkSingleMatch.Value,
				FrameworkSeveral = frameworkSeveralMatch.Value,
				NoticeInvolvesDps = noticeInvolvesDpsMatch.Value,
				DpsPurchasers = dpsPurchasersMatch.Value
			};
		}

		private ReductionDuringDialogueSection ParseReductionDuringDialogue()
		{
			var reductionDuringDialogueTranslation =
				TedLabelDictionary.GetTranslationFor("reduction_during_dialogue", NoticeLanguage);
			var reductionRecourseTranslation =
				TedLabelDictionary.GetTranslationFor("reduction_recourse", NoticeLanguage);

			var reductionRecourseMatch =
				Regex.Match(NoticeContent, $"{reductionRecourseTranslation}", RegexOptions.IgnoreCase);

			return new ReductionDuringDialogueSection(reductionDuringDialogueTranslation)
			{
				ReductionRecourse = reductionRecourseMatch.Value
			};
		}

		private NegotiationInfoSection ParseNegotiationInfo()
		{
			var negotiationInfoTranslation = TedLabelDictionary.GetTranslationFor("negotiation_info", NoticeLanguage);
			var negotiationWithoutTranslation =
				TedLabelDictionary.GetTranslationFor("negotiation_without", NoticeLanguage);
			
			// TODO match negotiation_without.
			
			return new NegotiationInfoSection(negotiationInfoTranslation);
		}

		private EAuctionInfoSection ParseEAuctionInfo()
		{
			var eAuctionInfoTranslation = TedLabelDictionary.GetTranslationFor("eauction_info", NoticeLanguage);
			var eAuctionWillUsedTranslation = TedLabelDictionary.GetTranslationFor("eauction_will_used", NoticeLanguage);
			var eAuctionInfoAddit = TedLabelDictionary.GetTranslationFor("eauction_info_addit", NoticeLanguage);

			// TODO match eauction_will_used, eauction_info_addit
			
			return new EAuctionInfoSection(eAuctionInfoTranslation);
		}

		private GpaInfoSection ParseGpaInfo()
		{
			var gpaInfoTranslation = TedLabelDictionary.GetTranslationFor("gpa_info", NoticeLanguage);
			var gpaCoveredTranslation = TedLabelDictionary.GetTranslationFor("gpa_covered", NoticeLanguage);
			var yesTranslation = TedLabelDictionary.GetTranslationFor("_yes", NoticeLanguage);
 
			var gpaCoveredMatch = Regex.Match(NoticeContent, $@"(?<={gpaCoveredTranslation}: )(.*?)\s?(?=IV)",
				RegexOptions.IgnoreCase);
			
			return new GpaInfoSection(gpaInfoTranslation)
			{
				GpaCovered = gpaCoveredMatch.Groups[1].Value == yesTranslation
			};
		}
		
		private Section ParseSectionIvInfoAdminSection()
		{
			var pubPreviousSection = ParsePubPrevious();
			var limitReceiptTendersReqParSection = ParseLimitReceiptTendersReqPart();
			var dateDispatchTenderParticipate = ParseDateDispatchTenderParticipate();
			var languagesAllowedSection = ParseLanguagesAllowed();
			var minTimeMaintainSection = ParseMinTimeMaintain();
			var openingConditionSection = ParseOpeningCondition();
			return new Section("IV.2", TedLabelDictionary.GetTranslationFor("info_admin", NoticeLanguage),
				new Section[] {pubPreviousSection, limitReceiptTendersReqParSection, dateDispatchTenderParticipate, languagesAllowedSection, minTimeMaintainSection, openingConditionSection});
		}

		private PubPreviousSection ParsePubPrevious()
		{
			// TODO match number_o, H_one_following, notice_pin, notice_buyer_profile
			
			return new PubPreviousSection(TedLabelDictionary.GetTranslationFor("pub_previous", NoticeLanguage));
		}

		private LimitReceiptTendersReqPartSection ParseLimitReceiptTendersReqPart()
		{
			var limitReceiptTendersReqPartTranslation =
				TedLabelDictionary.GetTranslationFor("limit_receipt_tenders_req_part", NoticeLanguage);
			var dateTranslation = TedLabelDictionary.GetTranslationFor("date", NoticeLanguage);
			var timeTranslation = TedLabelDictionary.GetTranslationFor("time", NoticeLanguage);

			var limitReceiptTendersReqPartMatch = Regex.Match(NoticeContent,
				$@"(?<={limitReceiptTendersReqPartTranslation})(.*?)\s?(?=IV\.2)",
				RegexOptions.IgnoreCase);
			var dateMatch = Regex.Match(limitReceiptTendersReqPartMatch.Value, $@"(?<={dateTranslation}: )(.*?)\s?(?={timeTranslation})",
				RegexOptions.IgnoreCase);
			var timeMatch = Regex.Match(limitReceiptTendersReqPartMatch.Value,
				$@"(?<={timeTranslation}: )(.*)",
				RegexOptions.IgnoreCase);

			var dateSplits = dateMatch.Groups[1].Value.Split("/");
			var timeSplits = timeMatch.Groups[1].Value.Split(":");
			
			return new LimitReceiptTendersReqPartSection(limitReceiptTendersReqPartTranslation)
			{
				DateTime = new DateTime(int.Parse(dateSplits[2]), int.Parse(dateSplits[1]), int.Parse(dateSplits[0]), int.Parse(timeSplits[0]), int.Parse(timeSplits[1]), 0)
			};
		}

		private DateDispatchTenderParticipateSection ParseDateDispatchTenderParticipate()
		{
			var dateDispatchTenderParticipateTranslation =
				TedLabelDictionary.GetTranslationFor("date_dispatch_tender_participate", NoticeLanguage);

			// TODO match date
			
			return new DateDispatchTenderParticipateSection(dateDispatchTenderParticipateTranslation);
		}

		private LanguagesAllowedSection ParseLanguagesAllowed()
		{
			var languagesAllowedTranslation =
				TedLabelDictionary.GetTranslationFor("languages_allowed", NoticeLanguage);
			
			var languagesAllowedMatch = Regex.Match(NoticeContent, $@"(?<={languagesAllowedTranslation}: )(.*?)\s?(?=IV\.2)",
				RegexOptions.IgnoreCase);

			var languageSplits = languagesAllowedMatch.Groups[1].Value.Split("•",
				StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
			
			//TODO Validate with multiple languages.

			return new LanguagesAllowedSection(languagesAllowedTranslation)
			{
				LanguagesAllowed = languageSplits
			};
		}

		private MinTimeMaintainSection ParseMinTimeMaintain()
		{
			var minTimeMaintainTranslation =
				TedLabelDictionary.GetTranslationFor("min_time_maintain", NoticeLanguage);
			var dateTenderValidTranslation =
				TedLabelDictionary.GetTranslationFor("date_tender_valid", NoticeLanguage);
			var durationMonthsTranslation = TedLabelDictionary.GetTranslationFor("duration_months", NoticeLanguage);
			var fromStatedDateTranslation = TedLabelDictionary.GetTranslationFor("from_stated_date", NoticeLanguage);

			var durationMonthsMatch = Regex.Match(NoticeContent, $@"(?<={minTimeMaintainTranslation} {durationMonthsTranslation}: )(.*?)\s?(?=\({fromStatedDateTranslation}\))", RegexOptions.IgnoreCase);

			var tryParseResult = int.TryParse(durationMonthsMatch.Groups[1].Value, out var months);

			//TODO Match date_tender_valid.
			
			return new MinTimeMaintainSection(minTimeMaintainTranslation)
			{
				DurationMonths = months,
				FromStatedDate = fromStatedDateTranslation
			};
		}

		private OpeningConditionsSection ParseOpeningCondition()
		{
			var openingConditionsTranslation =
				TedLabelDictionary.GetTranslationFor("opening_conditions", NoticeLanguage);
			var dateTranslation = TedLabelDictionary.GetTranslationFor("date", NoticeLanguage);
			var timeTranslation = TedLabelDictionary.GetTranslationFor("time", NoticeLanguage);
			var openingPlaceTranslation = TedLabelDictionary.GetTranslationFor("opening_place", NoticeLanguage);
			var openingAdditInfoTranslation =
				TedLabelDictionary.GetTranslationFor("opening_addit_info", NoticeLanguage);
			var sectionViTranslation = TedLabelDictionary.GetTranslationFor("section_6", NoticeLanguage);

			var openingConditionsMatch = Regex.Match(NoticeContent,
				$@"(?<={openingConditionsTranslation})(.*?)\s?(?={sectionViTranslation})", RegexOptions.IgnoreCase);
			var dateMatch = Regex.Match(NoticeContent,
				$@"(?<={openingConditionsTranslation} {dateTranslation}: )(.*?)\s?(?={timeTranslation})", RegexOptions.IgnoreCase);
			var timeMatch = Regex.Match(openingConditionsMatch.Value,
				$@"(?<={timeTranslation}: )(.*?)\s?(?={openingPlaceTranslation})", RegexOptions.IgnoreCase);
			var openingPlaceMatch = Regex.Match(openingConditionsMatch.Value,
				$@"(?<={openingPlaceTranslation}: )(.*?)\s?(?={openingAdditInfoTranslation})", RegexOptions.IgnoreCase);
			var openingAdditInfoMatch = Regex.Match(NoticeContent,
				$@"(?<={openingAdditInfoTranslation}: )(.*?)\s?(?={sectionViTranslation})", RegexOptions.IgnoreCase);
			
			//TODO validate opening_place and opening_addit_info
			
			var dateSplits = dateMatch.Groups[1].Value.Split("/");
			var timeSplits = timeMatch.Groups[1].Value.Split(":");

			return new OpeningConditionsSection(openingConditionsTranslation)
			{
				DateTime = string.IsNullOrWhiteSpace(dateMatch.Groups[1].Value)
					? DateTime.MinValue
					: new DateTime(int.Parse(dateSplits[2]), int.Parse(dateSplits[1]), int.Parse(dateSplits[0]),
						int.Parse(timeSplits[0]), int.Parse(timeSplits[1]), 0),
				OpeningPlace = openingPlaceMatch.Groups[1].Value,
				OpeningAdditInfo = openingAdditInfoMatch.Groups[1].Value
			};
		}
	}
}