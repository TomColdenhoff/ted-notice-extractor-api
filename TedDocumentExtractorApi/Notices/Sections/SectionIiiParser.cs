using System;
using System.Linq;
using System.Text.RegularExpressions;
using TedDocumentExtractorApi.LookUps;
using TedDocumentExtractorApi.Notices.Sections.SubSections;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class SectionIiiParser : SectionParser, ISectionParser
	{
		public SectionIiiParser(string noticeContent, TedLabelDictionary tedLabelDictionary, Language noticeLanguage) : base(noticeContent, tedLabelDictionary, noticeLanguage)
		{
		}

		public Section Parse()
		{
			var conditionsParticipSection = ParseConditionsParticip();
			var conditionsContractSection = ParseConditionsContract();
			
			var sectionIii = new Section("Section III", TedLabelDictionary.GetTranslationFor("info_legal", NoticeLanguage), new Section[]{conditionsParticipSection, conditionsContractSection});
			return sectionIii;
		}
		
		private ConditionsParticipSection ParseConditionsParticip()
		{
			var situationPersonalInclSection = ParseSituationPersonalIncl();
			var economicFinancialStandingSection = ParseEconomicFinancialStanding();
			var technicalCapacitySection = ParseTechnicalCapacity();
			var contractReservedSection = ParseContractReserved();
			
			var conditionsParticipSection = new ConditionsParticipSection(
				TedLabelDictionary.GetTranslationFor("conditions_particip", NoticeLanguage), new Section[]
				{
					situationPersonalInclSection,
					economicFinancialStandingSection,
					technicalCapacitySection,
					contractReservedSection
				});
			
			return conditionsParticipSection;
		}

		private SituationPersonalInclSection ParseSituationPersonalIncl()
		{
			var infoEvaluatingRequirementTranslation =
				TedLabelDictionary.GetTranslationFor("info_evaluating_requir", NoticeLanguage);
			
			var infoEvaluatingRequirementMatch = Regex.Match(NoticeContent,
				$@"(?<={infoEvaluatingRequirementTranslation}: )(.*?)\s?(?=III\.1)",
				RegexOptions.IgnoreCase);

			var requirementStrings = infoEvaluatingRequirementMatch.Groups[1].Value.Split("- ", StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim(' ', ',')).ToArray();
			var situationPersonalInclSection =
				new SituationPersonalInclSection(
					TedLabelDictionary.GetTranslationFor("situation_personal_incl", NoticeLanguage))
				{
					Requirements = requirementStrings
				};


			return situationPersonalInclSection;
		}

		private EconomicFinancialStandingSection ParseEconomicFinancialStanding()
		{
			var economicFinancialStandingTranslation =
				TedLabelDictionary.GetTranslationFor("economic_financial_standing", NoticeLanguage);
			var infoEvaluatingWethRequirTranslation =
				TedLabelDictionary.GetTranslationFor("info_evaluating_weth_requir", NoticeLanguage);
			var minStandardsRequiredTranslation =
				TedLabelDictionary.GetTranslationFor("min_standards_required", NoticeLanguage);
			
			//TODO criteria_selection_docs
			
			var infoEvaluatingWethRequirMatch = Regex.Match(NoticeContent,
				$@"(?<=III\.1\.2\) {economicFinancialStandingTranslation} {infoEvaluatingWethRequirTranslation}: )(.*?)\s?(?={minStandardsRequiredTranslation})",
				RegexOptions.IgnoreCase);
			var minStandardsRequiredMatch = Regex.Match(NoticeContent,
				$@"(?<={minStandardsRequiredTranslation}: )(.*?)\s?(?=III\.1\.3)",
				RegexOptions.IgnoreCase);
			
			var infoEvaluatingWethRequirStrings = infoEvaluatingWethRequirMatch.Groups[1].Value.Split(", -", StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim(' ', ',', '-')).ToArray();
			var minStandardsRequiredStrings = minStandardsRequiredMatch.Groups[1].Value.Split("-", StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim(' ', ',')).ToArray();

			var economicFinancialStandingSection =
				new EconomicFinancialStandingSection(economicFinancialStandingTranslation)
				{
					InfoEvaluatingWethRequir = infoEvaluatingWethRequirStrings,
					MinStandardsRequired = minStandardsRequiredStrings
				};
			
			return economicFinancialStandingSection;
		}
		
		private TechnicalCapacitySection ParseTechnicalCapacity()
		{
			var technicalCapacityTranslation =
				TedLabelDictionary.GetTranslationFor("technical_capacity", NoticeLanguage);
			var infoEvaluatingWethRequirTranslation =
				TedLabelDictionary.GetTranslationFor("info_evaluating_weth_requir", NoticeLanguage);
			var minStandardsRequiredTranslation =
				TedLabelDictionary.GetTranslationFor("min_standards_required", NoticeLanguage);
			
			//TODO criteria_selection_docs

			var sectionText = Regex.Match(NoticeContent,
				$@"(?<=III\.1\.3\) {technicalCapacityTranslation} )(.*?)\s?(?=III\.1\.5)", RegexOptions.IgnoreCase).Groups[1].Value;
			
			var infoEvaluatingWethRequirMatch = Regex.Match(sectionText,
				$@"(?<={infoEvaluatingWethRequirTranslation}: )(.*?)\s?(?={minStandardsRequiredTranslation})",
				RegexOptions.IgnoreCase);
			var minStandardsRequiredMatch = Regex.Match(sectionText,
				$@"(?<={minStandardsRequiredTranslation}: )(.*?)\s?(?=III\.1\.5)",
				RegexOptions.IgnoreCase);
			
			var infoEvaluatingWethRequirStrings = infoEvaluatingWethRequirMatch.Groups[1].Value.Split(", -", StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim(' ', ',', '-')).ToArray();
			var minStandardsRequiredStrings = minStandardsRequiredMatch.Groups[1].Value.Split("-", StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim(' ', ',')).ToArray();

			var economicFinancialStandingSection =
				new TechnicalCapacitySection(technicalCapacityTranslation)
				{
					InfoEvaluatingWethRequir = infoEvaluatingWethRequirStrings,
					MinStandardsRequired = minStandardsRequiredStrings
				};
			
			return economicFinancialStandingSection;
		}

		private ContractsReservedSection ParseContractReserved()
		{
			//TODO validate logic, find notice with a filled in III.1.5

			var contractsReservedTranslation =
				TedLabelDictionary.GetTranslationFor("contracts_reserved", NoticeLanguage);
			var restrictedShelteredWorkshopTranslation =
				TedLabelDictionary.GetTranslationFor("restricted_sheltered_workshop", NoticeLanguage);
			var restrictedShelteredProgramTranslation =
				TedLabelDictionary.GetTranslationFor("restricted_sheltered_program", NoticeLanguage);

			var restrictedShelteredWorkshopMatch =
				Regex.Match(NoticeContent, $"{restrictedShelteredWorkshopTranslation}",
				RegexOptions.IgnoreCase);
			var restrictedShelteredProgramMatch = Regex.Match(NoticeContent,
				$"{restrictedShelteredProgramTranslation}",
				RegexOptions.IgnoreCase);

			var contractReservedSection = new ContractsReservedSection(contractsReservedTranslation)
			{
				RestrictedShelteredWorkshop = restrictedShelteredWorkshopMatch.Value,
				RestrictedShelteredProgram = restrictedShelteredProgramMatch.Value
			};
			
			return contractReservedSection;
		}
		
		private ConditionsContractSection ParseConditionsContract()
		{
			var conditionsContractTranslation = TedLabelDictionary.GetTranslationFor("conditions_contract", NoticeLanguage);

			var particularProfessionInfoSection = ParseParticularProfsessionInfo();
			
			// TODO Find notice with III.2.* filled in
			
			return new ConditionsContractSection(conditionsContractTranslation, new Section[] {particularProfessionInfoSection});
		}

		private ParticularProfessionInfoSection ParseParticularProfsessionInfo()
		{
			var particularProfessionInfoTranslation = TedLabelDictionary.GetTranslationFor("particular_profession_info", NoticeLanguage);
			var particularProfessionReservedTranslation =
				TedLabelDictionary.GetTranslationFor("particular_profession_reserved", NoticeLanguage);
			var refLawLegProvTranslation = TedLabelDictionary.GetTranslationFor("ref_law_reg_prov", NoticeLanguage);

			// TODO Find a notice with this section filled in.
			
			return new ParticularProfessionInfoSection(particularProfessionInfoTranslation)
			{
				ParticularProfessionReserved = ""
			};
		}
	}
}