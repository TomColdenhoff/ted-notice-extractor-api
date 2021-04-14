using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TedDocumentExtractorApi.LookUps;
using TedDocumentExtractorApi.Notices.Sections.SubSections;
using TedDocumentExtractorApi.Notices.Sections.ValueObjects;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class SectionIiParser : SectionParser, ISectionParser
	{
		public SectionIiParser(string noticeContent, TedLabelDictionary tedLabelDictionary, Language noticeLanguage) : base(noticeContent, tedLabelDictionary, noticeLanguage)
		{
		}
		
		public Section Parse()
		{
			var sectionIiSections = new List<Section> {ParseQuantityScopeContract()};
			sectionIiSections.AddRange(ParseDescriptionSections());

			var sectionIi = new Section("Section II", TedLabelDictionary.GetTranslationFor("object", NoticeLanguage), sectionIiSections);
			return sectionIi;
		}
		
		private QuantityScopeContract ParseQuantityScopeContract()
		{
			var quantityScopeContractTranslation =
				TedLabelDictionary.GetTranslationFor("quantity_scope_contract", NoticeLanguage);

			var quantityScopeContractSegment = Regex.Match(NoticeContent,
				@$"(?<={quantityScopeContractTranslation} )(.*?)\s?(?=II\.2)", RegexOptions.IgnoreCase).Groups[1].Value; // We want to extract all the text from section II.1 before parsing. We do this to prevent false matches with II.2
			
			var titleContract = ParseTitleContract(quantityScopeContractSegment, "II.1.1");
			var cpvCodes = ParseCpvCodes(quantityScopeContractSegment, "II.1.2");
			var typeContract = ParseTypeContract(quantityScopeContractSegment);
			var descriptionShort = ParseDescriptionShort(quantityScopeContractSegment);
			var valueMagnitudeEstimatedTotal = ParseValueMagnitudeEstimatedTotal(quantityScopeContractSegment, "II.1.5");
			var lotsInfo = ParseLotsInfo(quantityScopeContractSegment);

			var quantityScopeContract = new QuantityScopeContract(quantityScopeContractTranslation, new Section[]
			{
				titleContract, 
				cpvCodes,
				typeContract,
				descriptionShort,
				valueMagnitudeEstimatedTotal,
				lotsInfo
			});

			return quantityScopeContract;
		}
		
		private TitleContract ParseTitleContract(string content, string section)
		{
			var titleContractTranslation = TedLabelDictionary.GetTranslationFor("title_contract", NoticeLanguage);
			var filerefTranslation = TedLabelDictionary.GetTranslationFor("fileref", NoticeLanguage);

			var title = Regex.Match(content, @$"(?<={titleContractTranslation} )(.*?)\s?(?={filerefTranslation})",
				RegexOptions.IgnoreCase).Groups[1].Value;
			var fileref = Regex.Match(content, @$"(?<={filerefTranslation}: )(.*?)\s?(?=II\.)",
				RegexOptions.IgnoreCase).Groups[1].Value;

			var titleContract = new TitleContract(section, titleContractTranslation)
			{
				Title = title,
				FileRef = fileref
			};

			return titleContract;
		}
		
		private CpvCodes ParseCpvCodes(string content, string section)
		{
			var cpvMainTranslation = TedLabelDictionary.GetTranslationFor("cpv_main", NoticeLanguage);
			var cpvAdditionalTranslation = TedLabelDictionary.GetTranslationFor("cpv_supplem", NoticeLanguage);

			var cpvMainMatches = Regex.Matches(content,
				$@"(?<={cpvMainTranslation}: )(.*?) - (.*?)\s?(?={cpvAdditionalTranslation})", RegexOptions.IgnoreCase);
			var cpvAdditionalMatches = Regex.Matches(content,
				$@"(?<={cpvAdditionalTranslation}: )(-|(.*?) - (.*?))\s?(?=Hoofdcategorie|II\.)", RegexOptions.IgnoreCase);

			var cpvCodes = new CpvCodes(section, TedLabelDictionary.GetTranslationFor("cpv", NoticeLanguage));
			cpvCodes.CpvMain = new Cpv[cpvMainMatches.Count];
			for (var i = 0; i < cpvMainMatches.Count; i++)
			{
				var match = cpvMainMatches[i];
				cpvCodes.CpvMain[i] = new Cpv()
				{
					Code = match.Groups[1].Value,
					Description = match.Groups[2].Value
				};
			}

			if (cpvAdditionalMatches.Count > 0 && !cpvAdditionalMatches[0].Groups[1].Value.Equals("-"))
			{
				cpvCodes.CpvAdditional = new Cpv[cpvAdditionalMatches.Count];
				for (var i = 0; i < cpvAdditionalMatches.Count; i++)
				{
					var match = cpvAdditionalMatches[i];
					cpvCodes.CpvAdditional[i] = new Cpv()
					{
						Code = match.Groups[1].Value,
						Description = match.Groups[2].Value
					};
				}
			}
			else
			{
				cpvCodes.CpvAdditional = Array.Empty<Cpv>();
			}
			
			return cpvCodes;
		}

		private TypeContract ParseTypeContract(string content)
		{
			var typeContractTranslation = TedLabelDictionary.GetTranslationFor("type_contract", NoticeLanguage);

			var contractTypeMatch = Regex.Match(content, $@"(?<=II\.1\.3\) {typeContractTranslation} )(.*?)\s?(?=II)", RegexOptions.IgnoreCase);
			
			return new TypeContract(typeContractTranslation)
			{
				Value = contractTypeMatch.Groups[1].Value
			};
		}

		private DescriptionShortSection ParseDescriptionShort(string content)
		{
			var descriptionShortTranslation = TedLabelDictionary.GetTranslationFor("descr_short", NoticeLanguage);

			var shortDescriptionShort =
				Regex.Match(content, $@"(?<=II\.1\.4\) {descriptionShortTranslation} )(.*?)\s?(?=II)", RegexOptions.IgnoreCase);

			return new DescriptionShortSection(descriptionShortTranslation)
			{
				Value = shortDescriptionShort.Groups[1].Value
			};
		}

		private ValueMagnitudeEstimatedTotalSection ParseValueMagnitudeEstimatedTotal(string content, string sectionNumber)
		{
			var valueMagnitudeEstimatedTotalTranslation = TedLabelDictionary.GetTranslationFor("value_magnitude_estimated_total", NoticeLanguage);
			var valueExlVatTranslation = TedLabelDictionary.GetTranslationFor("value_excl_vat", NoticeLanguage);
			var currencyTranslation = TedLabelDictionary.GetTranslationFor("currency", NoticeLanguage);
			
			var valueExlVatMatch = Regex.Match(content, $@"(?<={valueExlVatTranslation}: )(.*?)\s?(?={currencyTranslation})", RegexOptions.IgnoreCase);
			var currencyMatch = Regex.Match(content, $@"(?<={currencyTranslation}: )(.*?)\s?(?=II)", RegexOptions.IgnoreCase);

			return new ValueMagnitudeEstimatedTotalSection(sectionNumber, valueMagnitudeEstimatedTotalTranslation)
			{
				ValueExclusiveVat = valueExlVatMatch.Groups[1].Value,
				Currency = currencyMatch.Groups[1].Value
			};
		}

		private LotsInfoSection ParseLotsInfo(string content)
		{
			var lotsInfoTranslation = TedLabelDictionary.GetTranslationFor("lots_info", NoticeLanguage);
			var divisionLotsTranslation = TedLabelDictionary.GetTranslationFor("division_lots", NoticeLanguage);
			var yesTranslation = TedLabelDictionary.GetTranslationFor("_yes", NoticeLanguage);
			var lotsSubmittedForTranslation =
				TedLabelDictionary.GetTranslationFor("lots_submitted_for", NoticeLanguage);
			
			// TODO lots_submitted_for, lots_max_awarded, lots_combination_possible
			
			var divisionLotsMatch = Regex.Match(content,
				$@"(?<=II\.1\.6\) {lotsInfoTranslation} {divisionLotsTranslation}: )(.*?)\s?(?={lotsSubmittedForTranslation}|II)",
				RegexOptions.IgnoreCase);
			var divisionLotsValue = divisionLotsMatch.Groups[1].Value;

			var divisionLots = divisionLotsValue == yesTranslation;

			var lotsSubmittedForMatch = Regex.Match(content, $@"(?<={lotsSubmittedForTranslation}: )(.*)", RegexOptions.IgnoreCase);

			return new LotsInfoSection(lotsInfoTranslation)
			{
				DivisionLots = divisionLots,
				LotsSubmittedFor = lotsSubmittedForMatch.Groups[1].Value
			};
		}
		
		private IEnumerable<DescriptionSection> ParseDescriptionSections()
		{
			var descriptionTranslation = TedLabelDictionary.GetTranslationFor("description", NoticeLanguage);
			var section3Translation = TedLabelDictionary.GetTranslationFor("section_3", NoticeLanguage);
			
			var descriptionSegments = Regex.Matches(NoticeContent,
				$@"(?<=II\.2\) {descriptionTranslation} )(.*?)\s?(?={descriptionTranslation} II\.2\.1|{section3Translation})", RegexOptions.IgnoreCase);

			var descriptionSections = new List<DescriptionSection>();

			foreach (var match in descriptionSegments.ToList())
			{
				var segment = match.Groups[1].Value;

				var titleContractSection = ParseTitleContract(segment);
				var cpvCodesSection = ParseCpvCodes(segment, TedLabelDictionary.GetTranslationFor("cpv_additional", NoticeLanguage));
				var placePerformanceSection = ParsePlacePerformance(segment);
				var descriptionProcurementSection = ParseDescriptionProcurement(segment);
				var awardCriteriaSection = ParseAwardCriteriaSection(segment);
				var valueMagnitudeEstimatedSection = ParseValueMagnitudeEstimatedTotal(segment, "II.2.6");
				var durationContractFrameworkDpsSection = ParseDurationContractFrameworkDps(segment);
				var limitOperatorsSection = ParseLimitOperators(segment);
				var variantsInfoSection = ParseVariantsInfo(segment);
				var optionsInfoSection = ParseOptionsInfo(segment);
				var electronicCatalogueSection = ParseElectronicCatalogue(segment);
				var euProgrInfoSection = ParseEuProgrInfo(segment);
				var parseInfoAdditionalSection = ParseInfoAdditional(segment);
				
				var descriptionSection = new DescriptionSection(descriptionTranslation, 
					new Section[]
					{
						titleContractSection,
						cpvCodesSection,
						placePerformanceSection,
						descriptionProcurementSection,
						awardCriteriaSection,
						valueMagnitudeEstimatedSection,
						durationContractFrameworkDpsSection,
						limitOperatorsSection,
						variantsInfoSection,
						optionsInfoSection,
						electronicCatalogueSection,
						euProgrInfoSection,
						parseInfoAdditionalSection
					});
				descriptionSections.Add(descriptionSection);
			}
			
			return descriptionSections;
		}
		
		private TitleContractSection ParseTitleContract(string content)
		{
			var titleContractTranslation = TedLabelDictionary.GetTranslationFor("title_contract", NoticeLanguage);
			var lotNumberTranslation = TedLabelDictionary.GetTranslationFor("lot_number", NoticeLanguage);

			var titleContractMatch = Regex.Match(content,
				$@"(?<=II\.2\.1\) {titleContractTranslation} )(.*?)\s?(?={lotNumberTranslation})", RegexOptions.IgnoreCase);
			var lotNumberMatch = Regex.Match(content, $@"(?<={lotNumberTranslation}: )(.*?)\s?(?=II\.2\.2)", RegexOptions.IgnoreCase);

			return new TitleContractSection(titleContractTranslation)
			{
				TitleContract = titleContractMatch.Groups[1].Value,
				LotNumber = lotNumberMatch.Groups[1].Value
			};
		}

		private PlacePerformanceSection ParsePlacePerformance(string content)
		{
			var placePerformanceTranslation = TedLabelDictionary.GetTranslationFor("place_performance", NoticeLanguage);
			var nutsCodeTranslation = TedLabelDictionary.GetTranslationFor("nutscode", NoticeLanguage);
			var mainSitePlaceTranslation =
				TedLabelDictionary.GetTranslationFor("mainsiteplace_works_delivery", NoticeLanguage);

			var nutsCodeMatches = Regex.Matches(content,
				$@"(?<={nutsCodeTranslation}: )((.*?) (.*?))\s?(?={nutsCodeTranslation}|II\.2|{mainSitePlaceTranslation})",
				RegexOptions.IgnoreCase);

			var nutsCodes = nutsCodeMatches.ToList().Select(nutsCodeMatch => new NutsCode()
				{Code = nutsCodeMatch.Groups[2].Value, DisplayName = nutsCodeMatch.Groups[3].Value}).ToList();

			var mainSiteMatch = Regex.Match(content, $@"(?<={mainSitePlaceTranslation}: )(.*?)\s?(?=II\.2)",
				RegexOptions.IgnoreCase);

			return new PlacePerformanceSection(placePerformanceTranslation)
			{
				NutsCodes = nutsCodes.ToArray(),
				MainSitePlaceWorksDelivery = mainSiteMatch.Groups[1].Value
			};
		}

		private DescriptionProcurementSection ParseDescriptionProcurement(string content)
		{
			var descriptionProcurementTranslation = TedLabelDictionary.GetTranslationFor("descr_procurement", NoticeLanguage);

			var descriptionMatch = Regex.Match(content, $@"(?<={descriptionProcurementTranslation}: )(.*?)\s?(?=II\.2)", RegexOptions.IgnoreCase);

			return new DescriptionProcurementSection(descriptionProcurementTranslation)
			{
				DescriptionProcurement = descriptionMatch.Groups[1].Value
			};
		}

		private AwardCriteriaSection ParseAwardCriteriaSection(string content)
		{
			var awardCriteriaTranslation = TedLabelDictionary.GetTranslationFor("award_criteria", NoticeLanguage);
			var awardCriterionQualityTranslation =
				TedLabelDictionary.GetTranslationFor("award_criterion_quality", NoticeLanguage);
			var awardCriterionNameTranslation =
				TedLabelDictionary.GetTranslationFor("award_criterion_name", NoticeLanguage);
			var weightingTranslation = TedLabelDictionary.GetTranslationFor("weighting", NoticeLanguage);
			var priceTranslation = TedLabelDictionary.GetTranslationFor("price", NoticeLanguage);

			var awardCriterionMatches = Regex.Matches(content,
				$@"(?<={awardCriterionQualityTranslation} )({awardCriterionNameTranslation}: (.*?)\s?)({weightingTranslation}: (.*?))\s?(?={awardCriterionQualityTranslation}|{priceTranslation}|II\.2)",
				RegexOptions.IgnoreCase);

			var awardCriteria = new List<AwardCriteria>();
			
			foreach (var match in awardCriterionMatches.ToList())
			{
				awardCriteria.Add(new AwardCriteria()
				{
					AwardCriterionQuality = awardCriterionQualityTranslation,
					AwardCriterionName = match.Groups[2].Value,
					Weighting = int.Parse(match.Groups[4].Value)
				});
			}

			var priceMatch = Regex.Match(content, $@"(?<={priceTranslation} {weightingTranslation}: )(.*?)\s?(?=II\.2)", RegexOptions.IgnoreCase);

			var price = new Price()
			{
				Weight = int.Parse(priceMatch.Groups[1].Value)
			};

			return new AwardCriteriaSection(awardCriteriaTranslation)
			{
				AwardCriteria = awardCriteria.ToArray(),
				Price = price
			};
		}

		private DurationContractFrameworkDpsSection ParseDurationContractFrameworkDps(string content)
		{
			var durationMonthsTranslation = TedLabelDictionary.GetTranslationFor("duration_months", NoticeLanguage);
			var inDaysTranslation = TedLabelDictionary.GetTranslationFor("indays", NoticeLanguage);
			var startingTranslation = TedLabelDictionary.GetTranslationFor("starting", NoticeLanguage);
			var endTranslation = TedLabelDictionary.GetTranslationFor("end", NoticeLanguage);
			var renewalsSubjectTranslation = TedLabelDictionary.GetTranslationFor("renewals_subject", NoticeLanguage);
			var yesTranslation = TedLabelDictionary.GetTranslationFor("_yes", NoticeLanguage);
			var renewalsDescriptionTranslation =
				TedLabelDictionary.GetTranslationFor("renewals_descr", NoticeLanguage);

			var durationMonthsMatch = Regex.Match(content,
				$@"(?<={durationMonthsTranslation}: )(.*?)\s?(?={renewalsSubjectTranslation})", RegexOptions.IgnoreCase);
			var durationDaysMatch = Regex.Match(content,
				$@"(?<={inDaysTranslation}: )(.*?)\s?(?={renewalsSubjectTranslation})", RegexOptions.IgnoreCase);
			var startingMatch = Regex.Match(content,
				$@"(?<={startingTranslation}: )(.*?)\s?(?={endTranslation})", RegexOptions.IgnoreCase);
			var endMatch = Regex.Match(content,
				$@"(?<={endTranslation}: )(.*?)\s?(?={renewalsSubjectTranslation})", RegexOptions.IgnoreCase);
			var renewalsSubjectMatch = Regex.Match(content,
				$@"(?<={renewalsSubjectTranslation}: )(.*?)\s?(?=II\.2|{renewalsDescriptionTranslation})", RegexOptions.IgnoreCase);
			var renewalsDescriptionMatch = Regex.Match(content,
				$@"(?<={renewalsDescriptionTranslation}: )(.*?)\s?(?=II\.2)", RegexOptions.IgnoreCase);

			int.TryParse(durationMonthsMatch.Groups[1].Value, out var durationMonths);
			int.TryParse(durationDaysMatch.Groups[1].Value, out var durationDays);
			var durationContractFrameworkDpsSection =
				new DurationContractFrameworkDpsSection(
					TedLabelDictionary.GetTranslationFor("duration_contract_framework_dps", NoticeLanguage))
				{
					DurationMonths = durationMonths,
					InDays = durationDays,
					Starting = startingMatch.Groups[1].Value,
					End = endMatch.Groups[1].Value,
					RenewalsSubject = renewalsSubjectMatch.Groups[1].Value == yesTranslation,
					RenewalsDescription = renewalsDescriptionMatch.Groups[1].Value
				};

			return durationContractFrameworkDpsSection;
		}

		private LimitOperatorsSection ParseLimitOperators(string content)
		{
			var envisagedNumberTranslation = TedLabelDictionary.GetTranslationFor("envisaged_number", NoticeLanguage);
			var envisagedMinTranslation = TedLabelDictionary.GetTranslationFor("envisaged_min", NoticeLanguage);
			var maxNumberTranslation = TedLabelDictionary.GetTranslationFor("max_number", NoticeLanguage);
			var criteriaChoosingLimitedTranslation =
				TedLabelDictionary.GetTranslationFor("criteria_choosing_limited", NoticeLanguage);

			var envisagedNumberMatch = Regex.Match(content,
				$@"(?<={envisagedNumberTranslation}: )(.*?)\s?(?={criteriaChoosingLimitedTranslation})");
			var envisagedMinMatch = Regex.Match(content,
				$@"(?<={envisagedMinTranslation}: )(.*?)\s?(?={maxNumberTranslation})");
			var maxNumberMatch = Regex.Match(content,
				$@"(?<={maxNumberTranslation}: )(.*?)\s?(?={criteriaChoosingLimitedTranslation})");
			var criteriaChoosingLimitedMatch = Regex.Match(content,
				$@"(?<={criteriaChoosingLimitedTranslation}: )(.*?)\s?(?=II\.2)");

			int.TryParse(envisagedNumberMatch.Groups[1].Value, out var envisagedNumber);
			int.TryParse(envisagedMinMatch.Groups[1].Value, out var envisagedMin);
			int.TryParse(maxNumberMatch.Groups[1].Value, out var maxNumber);

			var limitOperators =
				new LimitOperatorsSection(TedLabelDictionary.GetTranslationFor("limit_operators", NoticeLanguage))
				{
					EnvisagedNumber = envisagedNumber,
					EnvisagedMin = envisagedMin,
					MaxNumber = maxNumber,
					CriteriaChoosingLimited = criteriaChoosingLimitedMatch.Groups[1].Value
				};
			
			return limitOperators;
		}

		private VariantsInfoSection ParseVariantsInfo(string content)
		{
			var variantsAcceptedTranslation =
				TedLabelDictionary.GetTranslationFor("variants_accepted", NoticeLanguage);
			var yesTranslation = TedLabelDictionary.GetTranslationFor("_yes", NoticeLanguage);

			var variantsAcceptedMatch = Regex.Match(content, $@"(?<={variantsAcceptedTranslation}: )(.*?)\s?(?=II\.2)");

			var variantsInfoSection =
				new VariantsInfoSection(TedLabelDictionary.GetTranslationFor("variants_info", NoticeLanguage))
				{
					VariantsAccepted = variantsAcceptedMatch.Groups[1].Value == yesTranslation
				};

			return variantsInfoSection;
		}

		private OptionsInfoSection ParseOptionsInfo(string content)
		{
			var optionsTranslation = TedLabelDictionary.GetTranslationFor("options", NoticeLanguage);
			var optionsDescriptionTranslation = TedLabelDictionary.GetTranslationFor("options_descr", NoticeLanguage);
			var yesTranslation = TedLabelDictionary.GetTranslationFor("_yes", NoticeLanguage);
			
			var optionsMatch = Regex.Match(content, $@"(?<={optionsTranslation}: )(.*?)\s?(?=II\.2|{optionsDescriptionTranslation})");
			var optionsDescriptionMatch = Regex.Match(content, $@"(?<={optionsDescriptionTranslation}: )(.*?)\s?(?=II\.2)");

			var optionsInfoSection =
				new OptionsInfoSection(TedLabelDictionary.GetTranslationFor("options_info", NoticeLanguage))
				{
					Options = optionsMatch.Groups[1].Value == yesTranslation,
					OptionsDescription = optionsDescriptionMatch.Groups[1].Value
				};

			return optionsInfoSection;
		}

		private ElectronicCatalogueSection ParseElectronicCatalogue(string content)
		{
			var electronicCatalogueRequiredTranslation =
				TedLabelDictionary.GetTranslationFor("electronic_catalogue_required", NoticeLanguage);
			
			var electronicCatalogueRequiredMatch = Regex.Match(content, $@"(?<={electronicCatalogueRequiredTranslation}: )(.*?)\s?(?=II\.2)", RegexOptions.IgnoreCase);

			var electronicCatalogueSection =
				new ElectronicCatalogueSection(
					TedLabelDictionary.GetTranslationFor("electronic_catalogue", NoticeLanguage))
				{
					ElectronicCatalogueRequired = electronicCatalogueRequiredMatch.Groups[1].Value
				};

			return electronicCatalogueSection;
		}
		
		private EuProgrInfoSection ParseEuProgrInfo(string content)
		{
			var euProgrRelatedTranslation = TedLabelDictionary.GetTranslationFor("eu_progr_related", NoticeLanguage);
			var euProgrRefTranslation = TedLabelDictionary.GetTranslationFor("eu_progr_ref", NoticeLanguage);
			var yesTranslation = TedLabelDictionary.GetTranslationFor("_yes", NoticeLanguage);
			
			var euProgrRelatedMatch = Regex.Match(content, $@"(?<={euProgrRelatedTranslation}: )(.*?)\s?(?=II\.2|{euProgrRefTranslation})");
			var euProgrRefMatch = Regex.Match(content, $@"(?<={euProgrRefTranslation}: )(.*?)\s?(?=II\.2)");

			var euProgrInfoSection =
				new EuProgrInfoSection(TedLabelDictionary.GetTranslationFor("eu_progr_info", NoticeLanguage))
				{
					EuProgrRelated = euProgrRelatedMatch.Groups[1].Value == yesTranslation,
					EuProgrRef = euProgrRefMatch.Groups[1].Value
				};

			return euProgrInfoSection;
		}

		private InfoAdditionalSection ParseInfoAdditional(string content)
		{
			content = content.Replace("II.2", "");
			var infoAdditionalTranslation = TedLabelDictionary.GetTranslationFor("info_additional", NoticeLanguage);
			
			var infoAdditionalMatch = Regex.Match(content, $@"(?<={infoAdditionalTranslation}: )(.*)"); // We can match everything after the look behind because it's the end of the given content.

			var infoAdditionalSection = new InfoAdditionalSection("II.2.14", infoAdditionalTranslation)
			{
				InfoAdditional = infoAdditionalMatch.Groups[1].Value 
			};

			return infoAdditionalSection;
		}
	}
}