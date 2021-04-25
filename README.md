# ted-notice-extractor-api

## 🤷🏽 What does the notice extractor do?
The 'TedDocumentExtractorApo' provides the possibility to convert a TED notice (directive 2014/24/EU) to a response in a json format or plain text.

## 💻 Example
Below you can see a part of a response the API will send to you if you ask it to extract a TED notice for you. 

Note: The extracted notice was in Dutch and this JSON response is a part of a whole and valid response.

```json
{
    "noticeType": "NoticeContract",
    "formType": "F02",
    "sections": [
        {
            "sectionNumber": "Section I",
            "sectionName": "Aanbestedende dienst",
            "sections": [
                {
                    "nameAddressContact": {
                        "nameOfficial": "Stichting Orion",
                        "nationalId": "441960466",
                        "addressPostal": "Bijlmerdreef 1289 -2",
                        "addressTown": "AMSTERDAM",
                        "nutscode": "NL",
                        "addressPostcode": "1103 TV",
                        "addressCountry": "NL",
                        "contactpoint": REDACTED BECAUSE OF PRIVACY,
                        "addressPhone": REDACTED BECAUSE OF PRIVACY,
                        "addressEmail": REDACTED BECAUSE OF PRIVACY,
                        "addressFax": "-",
                        "internets": {
                            "urlGeneral": "http://www.orion.nl",
                            "urlBuyerprofile": ""
                        }
                    },
                    "sectionNumber": "I.1",
                    "sectionName": "Naam en adressen",
                    "sections": null
                },
                {
                    "value": "-",
                    "sectionNumber": "I.2",
                    "sectionName": "gezamenlijke aanbesteding",
                    "sections": null
                },
                {
                    "addressObtainDocsUrl": "https://www.tenderned.nl/tenderned-web/aankondiging/detail/publicatie/akid/c1d7be6c08cbafe8245101857e37cd86",
                    "addressFurtherInfo": null,
                    "addressFurtherInfoNameAddressContact": {
                        "nameOfficial": "Yellow Way Consultancy B.V.",
                        "nationalId": "-",
                        "addressPostal": "Gervenstraat 4",
                        "addressTown": "‘s-Hertogenbosch",
                        "nutscode": "NL",
                        "addressPostcode": "5211 PD",
                        "addressCountry": "NL",
                        "contactpoint": REDACTED BECAUSE OF PRIVACY,
                        "addressPhone": REDACTED BECAUSE OF PRIVACY,
                        "addressEmail": REDACTED BECAUSE OF PRIVACY,
                        "addressFax": "-",
                        "internets": {
                            "urlGeneral": "http://www.yellowway.nl",
                            "urlBuyerprofile": ""
                        }
                    },
                    "addressSendTendersUrl": "https://www.tenderned.nl/tenderned-web/aankondiging/detail/publicatie/akid/c1d7be6c08cbafe8245101857e37cd86",
                    "addressToAbove": true,
                    "addressFollowing": null,
                    "sectionNumber": "I.3",
                    "sectionName": "Communicatie",
                    "sections": null
                },
                {
                    "value": "Regionale of plaatselijke instantie",
                    "sectionNumber": "I.4",
                    "sectionName": "Soort aanbestedende dienst",
                    "sections": null
                }
```

## 📲 How to use the API
To start extracting you'll need to send a HTTP request to the API.

API endpoint is: `https://<host_here>/api/extraction`

You'll have to make a `POST` request with a file in the body as form-date. You can also use the Accept header to tell the API what kind of response you want.

| Accept header possibilities | result |
| ------------- |-------------|
| application/json | The API will return a JSON format of the notice |
| text/plain | The API wil return the notice in plain text |

## 🏗 Current support
The API currently supports the following notice types:

| Code | Name | Note |
| ------------- |-------------|-------------|
| F02 | ContractNotice | Not fully supported yet, see the 'How to contribute' section for all the info about the missing support.|

The API currently supports the following languages:
| ISO | Name |
| ------------- |-------------|
| NLD | Dutch |

Adding new languages to the API won't be too much of a struggle as long as it's spoken in the European Union 🇪🇺.

## 🧑🏽‍🚀 How to contribute
If you would like to contribute you could validate or/and write code for the items you find further in this chapter.

If you are going to work on one of these items it's recommended to use a notice which has the data you need (ex. A joint contract notice to validate I.1 supporting a joint contract).


### Possible contributions
Note: names in `code blocks` are declared in the notice templates which can be found here: TODO
It can be that code has already been written, in that case it still needs to be validated with a notice containing that data. If the extractor fails in the validation proces the code should be improved and fixed.
- I

   I.1 needs to support a joint contract instead of one
   I.3 needs to support `address_following` 

- II

   II.1.6 needs to support `lots_submitted_for`, `lots_max_awarded`, `lots_combination_possible`

- III
  
   III.1.2 needs to support `criteria_selection_docs`
   III.1.5 needs to be validated, find notice with a filled in III.1.5
   
  
TODO FINISH
