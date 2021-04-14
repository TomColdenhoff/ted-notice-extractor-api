using System.Text.RegularExpressions;
using TedDocumentExtractorApi.LookUps;

namespace TedDocumentExtractorApi.Notices.Sections
{
	public class SectionIParser : SectionParser, ISectionParser
	{

		public SectionIParser(string noticeContent, TedLabelDictionary tedLabelDictionary, Language noticeLanguage) : base(noticeContent, tedLabelDictionary, noticeLanguage)
		{
		}
		
		public Section Parse()
		{
			var nameAddressSegment = Regex.Match(NoticeContent,
				@$"(?<={TedLabelDictionary.GetTranslationFor("name_address_contact", NoticeLanguage)} )(.*?)\s(?=I\.2\))", RegexOptions.IgnoreCase).Groups[1].Value;
			var nameAddressContact = ParseNameAddressContact(nameAddressSegment);
			var nameAddressContactSection = new NameAddressContactSection(TedLabelDictionary.GetTranslationFor("name_address_contact", NoticeLanguage))
			{
				NameAddressContact = nameAddressContact
			};
			var jointProcurementSection = ParseJointProcurementSectiom();
			var communicationSection = ParseCommunicationSection();
			var caTypeSection = ParseCaType();
			var mainActivity = ParseMainActivity();
			
			var section = new Section("Section I", TedLabelDictionary.GetTranslationFor("ca", NoticeLanguage), new Section[] {nameAddressContactSection, jointProcurementSection, communicationSection, caTypeSection, mainActivity});
			return section;
		}
		
		private NameAddressContact ParseNameAddressContact(string segment)
		{
			//TODO Support multiple organizations (joint contract)
			var nameOfficialTranslation = TedLabelDictionary.GetTranslationFor("name_official", NoticeLanguage);
			var nationalIdTranslation = TedLabelDictionary.GetTranslationFor("national_id", NoticeLanguage);
			var addressPostalTranslation = TedLabelDictionary.GetTranslationFor("address_postal", NoticeLanguage);
			var addressTownTranslation = TedLabelDictionary.GetTranslationFor("address_town", NoticeLanguage);
			var nutscodeTranslation = TedLabelDictionary.GetTranslationFor("nutscode", NoticeLanguage);
			var addressPostcodeTranslation = TedLabelDictionary.GetTranslationFor("address_postcode", NoticeLanguage);
			var addressCountryTranslation = TedLabelDictionary.GetTranslationFor("address_country", NoticeLanguage);
			var contactpointTranslation = TedLabelDictionary.GetTranslationFor("contactpoint", NoticeLanguage);
			var addressPhoneTranslation = TedLabelDictionary.GetTranslationFor("address_phone", NoticeLanguage);
			var addressEmailTranslation = TedLabelDictionary.GetTranslationFor("address_email", NoticeLanguage);
			var addressFaxTranslation = TedLabelDictionary.GetTranslationFor("address_fax", NoticeLanguage);
			var urlGeneralTranslation = TedLabelDictionary.GetTranslationFor("url_general", NoticeLanguage);
			var urlBuyerprofileFaxTranslation = TedLabelDictionary.GetTranslationFor("url_buyerprofile", NoticeLanguage);
			
			var nameOfficial = Regex.Match(segment, $@"(?<={nameOfficialTranslation}: )(.*?)\s(?={nationalIdTranslation})").Groups[1].Value;
			var nationalId = Regex.Match(segment, @$"(?<={nationalIdTranslation}: )(.*?)\s(?={addressPostalTranslation})").Groups[1].Value;
			var addressPostal = Regex.Match(segment, @$"(?<={addressPostalTranslation}: )(.*?)\s(?={addressTownTranslation})").Groups[1].Value;
			var addressTown = Regex.Match(segment, @$"(?<={addressTownTranslation}: )(.*?)\s(?={nutscodeTranslation})").Groups[1].Value;
			var nutscode = Regex.Match(segment, @$"(?<={nutscodeTranslation}: )(.*?)\s(?={addressPostcodeTranslation})").Groups[1].Value;
			var postcode = Regex.Match(segment, @$"(?<={addressPostcodeTranslation}: )(.*?)\s(?={addressCountryTranslation})").Groups[1].Value;
			var addressCountry = Regex.Match(segment, @$"(?<={addressCountryTranslation}:)(.*?)\s(?={contactpointTranslation})").Groups[1].Value;
			var contactpoint = Regex.Match(segment, @$"(?<={contactpointTranslation}: )(.*?)\s(?={addressPhoneTranslation})").Groups[1].Value;
			var addressPhone = Regex.Match(segment, @$"(?<={addressPhoneTranslation}: )(.*?)\s(?={addressEmailTranslation})").Groups[1].Value;
			var addressEmail = Regex.Match(segment, @$"(?<={addressEmailTranslation}: )(.*?)\s(?={addressFaxTranslation})").Groups[1].Value;
			var addressFax = Regex.Match(segment, @$"(?<={addressFaxTranslation}: )(.*?)\s(?=)").Groups[1].Value;
			var urlGeneral = Regex.Match(segment, @$"(?<={urlGeneralTranslation}: )(.*?)\s(?={urlBuyerprofileFaxTranslation})").Groups[1].Value;
			var urlBuyerProfile = Regex.Match(segment, @$"(?<={urlBuyerprofileFaxTranslation}: )(.*?)\s(?=I\.2)").Groups[1].Value;

			var section = new NameAddressContact()
			{
				NameOfficial = nameOfficial,
				NationalId = nationalId,
				AddressPostal = addressPostal,
				AddressTown = addressTown,
				Nutscode = nutscode,
				AddressPostcode = postcode,
				AddressCountry = addressCountry,
				Contactpoint = contactpoint,
				AddressPhone = addressPhone,
				AddressEmail = addressEmail,
				AddressFax = addressFax,
				Internets = new Internets()
				{
					UrlGeneral = urlGeneral,
					UrlBuyerprofile = urlBuyerProfile
				}
			};
			
			return section;
		}

		private JointProcurement ParseJointProcurementSectiom()
		{
			var jointProcurementTranslation =
				TedLabelDictionary.GetTranslationFor("joint_procurement", NoticeLanguage);
			var jointProcurementMatch = Regex.Match(NoticeContent, @$"(?<=I\.2\) {jointProcurementTranslation} )(.*?)\s(?=I\.3)", RegexOptions.IgnoreCase);
			var procurement = new JointProcurement(jointProcurementTranslation);
			procurement.Value = jointProcurementMatch.Groups[1].Value;
			
			return procurement;
		}

		private Communication ParseCommunicationSection()
		{
			var communicationTranslation =
			TedLabelDictionary.GetTranslationFor("info_tendering", NoticeLanguage);
			var addressObtainsDocsTranslation = TedLabelDictionary.GetTranslationFor("address_obtain_docs", NoticeLanguage);
			var addressFurtherInfoTranslation =
				TedLabelDictionary.GetTranslationFor("address_furtherinfo", NoticeLanguage);
			var addressSendTendersRequest =
				TedLabelDictionary.GetTranslationFor("address_send_tenders_request", NoticeLanguage);
			var addressAnotherTranslation = TedLabelDictionary.GetTranslationFor("address_another", NoticeLanguage);
			var addressSendTendersTranslation =
				TedLabelDictionary.GetTranslationFor("address_send_tenders", NoticeLanguage);
			var addressToAboveTranslation = TedLabelDictionary.GetTranslationFor("address_to_above", NoticeLanguage);
			var addressFollowingTranslation =
				TedLabelDictionary.GetTranslationFor("address_following", NoticeLanguage);

			var addressObtainAddress =
				Regex.Match(NoticeContent, @$"(?<=I\.3\) {communicationTranslation} {addressObtainsDocsTranslation}:)(.*?)\s(?={addressFurtherInfoTranslation})", RegexOptions.IgnoreCase);
			
			var communicationSection = new Communication(communicationTranslation);
			communicationSection.AddressObtainDocsUrl = addressObtainAddress.Groups[1].Value.Replace(" ", "");
			
			var betweenAddressFurtherAndAddressSendTendersRequest = Regex.Match(NoticeContent,
				@$"(?<={addressFurtherInfoTranslation} )(.*?)\s(?={addressSendTendersRequest})",
				RegexOptions.IgnoreCase).Groups[1].Value; //Extract the text between the address_further_info header and the address_send_tenders header.

			if (Regex.Match(betweenAddressFurtherAndAddressSendTendersRequest, $"{addressAnotherTranslation}",
				RegexOptions.IgnoreCase).Success) // If there is a new address etc.
			{
				var nameAddressContact = ParseNameAddressContact(betweenAddressFurtherAndAddressSendTendersRequest);
				communicationSection.AddressFurtherInfoNameAddressContact = nameAddressContact;
			}
			else
			{
				communicationSection.AddressFurtherInfo = betweenAddressFurtherAndAddressSendTendersRequest;
			}

			var betweenAddressSendTenderRequestAndCaType = Regex.Match(NoticeContent,
				@$"(?<={addressSendTendersRequest} )(.*?)\s?(?=I\.4)",
				RegexOptions.IgnoreCase).Groups[1].Value; //Extract the text between the address_send_tenders header and the I.4 section.

			var addressSendTendersUrl = Regex.Match(betweenAddressSendTenderRequestAndCaType,
				@$"(?<={addressSendTendersTranslation}:)(.*?)\s(?=({addressToAboveTranslation}|{addressFollowingTranslation}))",
				RegexOptions.IgnoreCase).Groups[1].Value.Replace(" ", "");
			communicationSection.AddressSendTendersUrl = addressSendTendersUrl;

			if (Regex.Match(betweenAddressSendTenderRequestAndCaType, $"{addressToAboveTranslation}",
				RegexOptions.IgnoreCase).Success)
			{
				communicationSection.AddressToAbove = true;
			}
			else //TODO Validate if address following parsing works
			{
				communicationSection.AddressFollowing =
					ParseNameAddressContact(betweenAddressSendTenderRequestAndCaType);
			}

			return communicationSection;
		}

		private CaType ParseCaType()
		{
			var caTypeTranslation = TedLabelDictionary.GetTranslationFor("ca_type", NoticeLanguage);

			var caTypeValue = Regex.Match(NoticeContent, @$"(?<={caTypeTranslation} )(.*?)\s?(?=I\.5)", RegexOptions.IgnoreCase).Groups[1].Value;

			var caType = new CaType(caTypeTranslation)
			{
				Value = caTypeValue
			};

			return caType;
		}

		private MainActivity ParseMainActivity()
		{
			var mainactivityTranslation = TedLabelDictionary.GetTranslationFor("mainactivity", NoticeLanguage);
			var sectionIiTranslation = TedLabelDictionary.GetTranslationFor("section_2", NoticeLanguage);
			
			var mainActivityValue = Regex.Match(NoticeContent, @$"(?<={mainactivityTranslation} . )(.*?)\s?(?={sectionIiTranslation})", RegexOptions.IgnoreCase).Groups[1].Value;

			var mainActivity = new MainActivity(mainactivityTranslation)
			{
				Value = mainActivityValue
			};

			return mainActivity;
		}

	}
}