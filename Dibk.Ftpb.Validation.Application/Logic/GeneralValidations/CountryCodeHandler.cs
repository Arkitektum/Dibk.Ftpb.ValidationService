using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Logic.GeneralValidations
{
    public class CountryCodeHandler
    {
        // Generate List of valid country codes
        // The list is formated as:
        // - ISO 3166-1 Alpha 2 country codes,
        // - SSB-specific 3-digit country code,
        // - name of country in Norwegian Bokmål (NOBM), uppercase (for addressing purpose)  
        //
        // References: 3-digit SSB:            https://www.ssb.no/klass/klassifikasjoner/91
        //                 ISO 3166-1 Alpha 2:  https://www.ssb.no/klass/klassifikasjoner/100

        private static List<PostalCountryCode> _postList = new List<PostalCountryCode>()
        {
                new PostalCountryCode("AE", "426", "DE FORENTE ARABISKE EMIRATER"),
                new PostalCountryCode("AF", "404", "AFGHANISTAN"),
                new PostalCountryCode("AG", "603", "ANTIGUA OG BARBUDA"),
                new PostalCountryCode("AI", "660", "ANGUILLA"),
                new PostalCountryCode("AL", "111", "ALBANIA"),
                new PostalCountryCode("AM", "406", "ARMENIA"),
                new PostalCountryCode("AO", "204", "ANGOLA"),
                new PostalCountryCode("AQ", "", "ANTARCTICA"),
                new PostalCountryCode("AR", "705", "ARGENTINA"),
                new PostalCountryCode("AS", "802", "AMERIKANSK SAMOA"),
                new PostalCountryCode("AT", "153", "ØSTERRIKE"),
                new PostalCountryCode("AU", "805", "AUSTRALIA"),
                new PostalCountryCode("AW", "657", "ARUBA"),
                new PostalCountryCode("AX", "", "ÅLAND"),
                new PostalCountryCode("AZ", "407", "ASERBAJDSJAN"),
                new PostalCountryCode("BA", "155", "BOSNIA-HERCEGOVINA"),
                new PostalCountryCode("BB", "602", "BARBADOS"),
                new PostalCountryCode("BD", "410", "BANGLADESH"),
                new PostalCountryCode("BE", "112", "BELGIA"),
                new PostalCountryCode("BF", "393", "BURKINA FASO"),
                new PostalCountryCode("BG", "113", "BULGARIA"),
                new PostalCountryCode("BH", "409", "BAHRAIN"),
                new PostalCountryCode("BI", "216", "BURUNDI"),
                new PostalCountryCode("BJ", "229", "BENIN"),
                new PostalCountryCode("BL", "687", "SAINT-BARTHELEMY"),
                new PostalCountryCode("BM", "606", "BERMUDA"),
                new PostalCountryCode("BN", "416", "BRUNEI DARUSSALAM"),
                new PostalCountryCode("BO", "710", "BOLIVIA"),
                new PostalCountryCode("BQ", "659", "BONAIRE, SAINT EUSTATIUS OG SABA"),
                new PostalCountryCode("BR", "715", "BRASIL"),
                new PostalCountryCode("BS", "605", "BAHAMAS"),
                new PostalCountryCode("BT", "412", "BHUTAN"),
                new PostalCountryCode("BV", "", "BOUVET ISLAND"),
                new PostalCountryCode("BW", "205", "BOTSWANA"),
                new PostalCountryCode("BY", "120", "HVITERUSSLAND"),
                new PostalCountryCode("BZ", "604", "BELIZE"),
                new PostalCountryCode("CA", "612", "CANADA"),
                new PostalCountryCode("CC", "808", "KOKOSØYENE (KEELINGØYENE)"),
                new PostalCountryCode("CD", "278", "KONGO"),
                new PostalCountryCode("CF", "337", "SENTRALAFRIKANSKE REPUBLIKK"),
                new PostalCountryCode("CG", "279", "KONGO-BRAZZAVILLE"),
                new PostalCountryCode("CH", "141", "SVEITS"),
                new PostalCountryCode("CI", "239", "ELFENBENSKYSTEN"),
                new PostalCountryCode("CK", "809", "COOKØYENE"),
                new PostalCountryCode("CL", "725", "CHILE"),
                new PostalCountryCode("CM", "270", "KAMERUN"),
                new PostalCountryCode("CN", "484", "KINA"),
                new PostalCountryCode("CO", "730", "COLOMBIA"),
                new PostalCountryCode("CR", "616", "COSTA RICA"),
                new PostalCountryCode("CU", "620", "CUBA"),
                new PostalCountryCode("CV", "273", "KAPP VERDE"),
                new PostalCountryCode("CW", "661", "CURACAO"),
                new PostalCountryCode("CX", "807", "CHRISTMASØYA"),
                new PostalCountryCode("CY", "500", "KYPROS"),
                new PostalCountryCode("CZ", "158", "TSJEKKIA"),
                new PostalCountryCode("DE", "144", "TYSKLAND"),
                new PostalCountryCode("DJ", "250", "DJIBOUTI"),
                new PostalCountryCode("DK", "101", "DANMARK"),
                new PostalCountryCode("DM", "622", "DOMINICA"),
                new PostalCountryCode("DO", "624", "DEN DOMINIKANSKE REPUBLIKK"),
                new PostalCountryCode("DZ", "203", "ALGERIE"),
                new PostalCountryCode("EC", "735", "ECUADOR"),
                new PostalCountryCode("EE", "115", "ESTLAND"),
                new PostalCountryCode("EG", "249", "EGYPT"),
                new PostalCountryCode("EH", "304", "VEST-SAHARA"),
                new PostalCountryCode("ER", "241", "ERITREA"),
                new PostalCountryCode("ES", "137", "SPANIA"),
                new PostalCountryCode("ET", "246", "ETIOPIA"),
                new PostalCountryCode("FI", "103", "FINLAND"),
                new PostalCountryCode("FJ", "811", "FIJI"),
                new PostalCountryCode("FK", "740", "FALKLANDSØYENE (MALVINAS)"),
                new PostalCountryCode("FM", "826", "MIKRONESIAFØDERASJONEN"),
                new PostalCountryCode("FO", "104", "FÆRØYENE"),
                new PostalCountryCode("FR", "117", "FRANKRIKE"),
                new PostalCountryCode("GA", "254", "GABON"),
                new PostalCountryCode("GB", "139", "STORBRITANNIA"),
                new PostalCountryCode("GD", "629", "GRENADA"),
                new PostalCountryCode("GE", "430", "GEORGIA"),
                new PostalCountryCode("GF", "745", "FRANSK GUYANA"),
                new PostalCountryCode("GG", "162", "GUERNSEY"),
                new PostalCountryCode("GH", "260", "GHANA"),
                new PostalCountryCode("GI", "118", "GIBRALTAR"),
                new PostalCountryCode("GL", "102", "GRØNLAND"),
                new PostalCountryCode("GM", "256", "GAMBIA"),
                new PostalCountryCode("GN", "264", "GUINEA"),
                new PostalCountryCode("GP", "631", "GUADELOUPE"),
                new PostalCountryCode("GQ", "235", "EKVATORIAL-GUINEA"),
                new PostalCountryCode("GR", "119", "HELLAS"),
                new PostalCountryCode("GS", "", "SØR-GEORGIA/SØNDRE SANDWICHØYENE"),
                new PostalCountryCode("GT", "632", "GUATEMALA"),
                new PostalCountryCode("GU", "817", "GUAM"),
                new PostalCountryCode("GW", "266", "GUINEA-BISSAU"),
                new PostalCountryCode("GY", "720", "GUYANA"),
                new PostalCountryCode("HK", "436", "HONG KONG"),
                new PostalCountryCode("HM", "", "HEARD- OG MCDONALDØYENE"),
                new PostalCountryCode("HN", "644", "HONDURAS"),
                new PostalCountryCode("HR", "122", "KROATIA"),
                new PostalCountryCode("HT", "636", "HAITI"),
                new PostalCountryCode("HU", "152", "UNGARN"),
                new PostalCountryCode("ID", "448", "INDONESIA"),
                new PostalCountryCode("IE", "121", "IRLAND"),
                new PostalCountryCode("IL", "460", "ISRAEL"),
                new PostalCountryCode("IM", "164", "ISLE OF MAN"),
                new PostalCountryCode("IN", "444", "INDIA"),
                new PostalCountryCode("IO", "213", "DET BRITISKE TERR. I INDIAHAVET"),
                new PostalCountryCode("IQ", "452", "IRAK"),
                new PostalCountryCode("IR", "456", "IRAN"),
                new PostalCountryCode("IS", "105", "ISLAND"),
                new PostalCountryCode("IT", "123", "ITALIA"),
                new PostalCountryCode("JE", "163", "JERSEY"),
                new PostalCountryCode("JM", "648", "JAMAICA"),
                new PostalCountryCode("JO", "476", "JORDAN"),
                new PostalCountryCode("JP", "464", "JAPAN"),
                new PostalCountryCode("KE", "276", "KENYA"),
                new PostalCountryCode("KG", "502", "KIRGISISTAN"),
                new PostalCountryCode("KH", "478", "KAMBODSJA"),
                new PostalCountryCode("KI", "815", "KIRIBATI"),
                new PostalCountryCode("KM", "220", "KOMORENE"),
                new PostalCountryCode("KN", "677", "SAINT KITTS OG NEVIS"),
                new PostalCountryCode("KP", "488", "NORD-KOREA"),
                new PostalCountryCode("KR", "492", "SØR-KOREA"),
                new PostalCountryCode("KW", "496", "KUWAIT"),
                new PostalCountryCode("KY", "613", "CAYMANØYENE"),
                new PostalCountryCode("KZ", "480", "KASAKHSTAN"),
                new PostalCountryCode("LA", "504", "LAOS"),
                new PostalCountryCode("LB", "508", "LIBANON"),
                new PostalCountryCode("LC", "678", "SAINT LUCIA"),
                new PostalCountryCode("LI", "128", "LIECHTENSTEIN"),
                new PostalCountryCode("LK", "424", "SRI LANKA"),
                new PostalCountryCode("LR", "283", "LIBERIA"),
                new PostalCountryCode("LS", "281", "LESOTHO"),
                new PostalCountryCode("LT", "136", "LITAUEN"),
                new PostalCountryCode("LU", "129", "LUXEMBURG"),
                new PostalCountryCode("LV", "124", "LATVIA"),
                new PostalCountryCode("LY", "286", "LIBYA"),
                new PostalCountryCode("MA", "303", "MAROKKO"),
                new PostalCountryCode("MC", "130", "MONACO"),
                new PostalCountryCode("MD", "138", "MOLDOVA"),
                new PostalCountryCode("ME", "160", "MONTENEGRO"),
                new PostalCountryCode("MF", "686", "SAINT MARTIN, FR"),
                new PostalCountryCode("MG", "289", "MADAGASKAR"),
                new PostalCountryCode("MH", "835", "MARSHALLØYENE"),
                new PostalCountryCode("MK", "156", "MAKEDONIA"),
                new PostalCountryCode("ML", "299", "MALI"),
                new PostalCountryCode("MM", "420", "MYANMAR/BURMA"),
                new PostalCountryCode("MN", "516", "MONGOLIA"),
                new PostalCountryCode("MO", "510", "MACAO"),
                new PostalCountryCode("MP", "840", "NORDRE MARIANENE"),
                new PostalCountryCode("MQ", "650", "MARTINIQUE"),
                new PostalCountryCode("MR", "306", "MAURITANIA"),
                new PostalCountryCode("MS", "654", "MONTSERRAT"),
                new PostalCountryCode("MT", "126", "MALTA"),
                new PostalCountryCode("MU", "307", "MAURITIUS"),
                new PostalCountryCode("MV", "513", "MALDIVENE"),
                new PostalCountryCode("MW", "296", "MALAWI"),
                new PostalCountryCode("MX", "652", "MEXICO"),
                new PostalCountryCode("MY", "512", "MALAYSIA"),
                new PostalCountryCode("MZ", "319", "MOSAMBIK"),
                new PostalCountryCode("NA", "308", "NAMIBIA"),
                new PostalCountryCode("NC", "833", "NY CALEDONIA"),
                new PostalCountryCode("NE", "309", "NIGER"),
                new PostalCountryCode("NF", "822", "NORFOLKØYA"),
                new PostalCountryCode("NG", "313", "NIGERIA"),
                new PostalCountryCode("NI", "664", "NICARAGUA"),
                new PostalCountryCode("NL", "127", "NEDERLAND"),
                new PostalCountryCode("NO", "0", "NORGE"),
                new PostalCountryCode("NP", "528", "NEPAL"),
                new PostalCountryCode("NR", "818", "NAURU"),
                new PostalCountryCode("NU", "821", "NIUE"),
                new PostalCountryCode("NZ", "820", "NEW ZEALAND"),
                new PostalCountryCode("OM", "520", "OMAN"),
                new PostalCountryCode("PA", "668", "PANAMA"),
                new PostalCountryCode("PE", "760", "PERU"),
                new PostalCountryCode("PF", "814", "FRANSK POLYNESIA"),
                new PostalCountryCode("PG", "827", "PAPUA NY-GUINEA"),
                new PostalCountryCode("PH", "428", "FILIPPINENE"),
                new PostalCountryCode("PK", "534", "PAKISTAN"),
                new PostalCountryCode("PL", "131", "POLEN"),
                new PostalCountryCode("PM", "676", "SAINT PIERRE OG MIQUELON"),
                new PostalCountryCode("PN", "828", "PITCAIRN"),
                new PostalCountryCode("PR", "685", "PUERTO RICO"),
                new PostalCountryCode("PS", "524", "PALESTINA"),
                new PostalCountryCode("PT", "132", "PORTUGAL"),
                new PostalCountryCode("PW", "839", "PALAU"),
                new PostalCountryCode("PY", "755", "PARAGUAY"),
                new PostalCountryCode("QA", "540", "QATAR"),
                new PostalCountryCode("RE", "323", "REUNION"),
                new PostalCountryCode("RO", "133", "ROMANIA"),
                new PostalCountryCode("RS", "159", "SERBIA"),
                new PostalCountryCode("RU", "140", "RUSSLAND"),
                new PostalCountryCode("RW", "329", "RWANDA"),
                new PostalCountryCode("SA", "544", "SAUDI-ARABIA"),
                new PostalCountryCode("SB", "806", "SALOMONØYENE"),
                new PostalCountryCode("SC", "338", "SEYCHELLENE"),
                new PostalCountryCode("SD", "356", "SUDAN"),
                new PostalCountryCode("SE", "106", "SVERIGE"),
                new PostalCountryCode("SG", "548", "SINGAPORE"),
                new PostalCountryCode("SH", "209", "SANKT HELENA"),
                new PostalCountryCode("SI", "146", "SLOVENIA"),
                new PostalCountryCode("SJ", "", "SVALBARD OG JAN MAYEN"),
                new PostalCountryCode("SK", "157", "SLOVAKIA"),
                new PostalCountryCode("SL", "339", "SIERRA LEONE"),
                new PostalCountryCode("SM", "134", "SAN MARINO"),
                new PostalCountryCode("SN", "336", "SENEGAL"),
                new PostalCountryCode("SO", "346", "SOMALIA"),
                new PostalCountryCode("SR", "765", "SURINAM"),
                new PostalCountryCode("SS", "355", "SØR-SUDAN"),
                new PostalCountryCode("ST", "333", "SAO TOME OG PRINCIPE"),
                new PostalCountryCode("SV", "672", "EL SALVADOR"),
                new PostalCountryCode("SX", "658", "SINT MARTEEN (NEDERLANDSK DEL)"),
                new PostalCountryCode("SY", "564", "SYRIA"),
                new PostalCountryCode("SZ", "357", "SWAZILAND"),
                new PostalCountryCode("TC", "681", "TURKS OG CAICOSØYENE"),
                new PostalCountryCode("TD", "373", "TSJAD"),
                new PostalCountryCode("TF", "", "FRANSKE SØRLIGE TERRITORIER"),
                new PostalCountryCode("TG", "376", "TOGO"),
                new PostalCountryCode("TH", "568", "THAILAND"),
                new PostalCountryCode("TJ", "550", "TADSJIKISTAN"),
                new PostalCountryCode("TK", "829", "TOKELAU"),
                new PostalCountryCode("TL", "537", "ØST-TIMOR"),
                new PostalCountryCode("TM", "552", "TURKMENISTAN"),
                new PostalCountryCode("TN", "379", "TUNISIA"),
                new PostalCountryCode("TO", "813", "TONGA"),
                new PostalCountryCode("TR", "143", "TYRKIA"),
                new PostalCountryCode("TT", "680", "TRINIDAD OG TOBAGO"),
                new PostalCountryCode("TV", "816", "TUVALU"),
                new PostalCountryCode("TW", "432", "TAIWAN"),
                new PostalCountryCode("TZ", "369", "TANZANIA"),
                new PostalCountryCode("UA", "148", "UKRAINA"),
                new PostalCountryCode("UG", "386", "UGANDA"),
                new PostalCountryCode("UM", "819", "USA MINDRE UTENFORLIGGENDE ØYER"),
                new PostalCountryCode("US", "684", "USA"),
                new PostalCountryCode("UY", "770", "URUGUAY"),
                new PostalCountryCode("UZ", "554", "USBEKISTAN"),
                new PostalCountryCode("VA", "154", "VATIKANSTATEN"),
                new PostalCountryCode("VC", "679", "SAINT VINCENT OG GRENADINE"),
                new PostalCountryCode("VE", "775", "VENEZUELA"),
                new PostalCountryCode("VG", "608", "JOMFRUØYENE, BRITISK"),
                new PostalCountryCode("VI", "601", "JOMFRUØYENE, US"),
                new PostalCountryCode("VN", "575", "VIETNAM"),
                new PostalCountryCode("VU", "812", "VANUATU"),
                new PostalCountryCode("WF", "832", "WALLIS OG FUTUNA"),
                new PostalCountryCode("WS", "830", "SAMOA"),
                new PostalCountryCode("YE", "578", "JEMEN"),
                new PostalCountryCode("YT", "322", "MAYOTTE"),
                new PostalCountryCode("ZA", "359", "SØR-AFRIKA"),
                new PostalCountryCode("ZM", "389", "ZAMBIA"),
                new PostalCountryCode("ZW", "326", "ZIMBABWE"),
                new PostalCountryCode("XK", "161", "KOSOVO"),
                new PostalCountryCode("", "980", ""),
                new PostalCountryCode("XC", "", "CEUTA OG MELILLA"),
                new PostalCountryCode("XB", "", "KANARIØYENE"),
                new PostalCountryCode("", "990", "")

        };

        public static List<PostalCountryCode> getListOfCountryCodes()
        {
            return _postList;
        }

        // This function returns true/false thus indicating whether or not the input string, interpreted as a country code,
        // is in the internal list of country codes
        // The input string can be either ISO 3166-1 Alpha 2 or SSB 3-digits format
        // In case of exception, boolean false is returned
        public static bool VerifyCountryCode(string countryCode)
        {
            if (!string.IsNullOrEmpty(countryCode))
            {
                //TODO log land kode feil
                try
                {
                    return _postList.Exists(cc => cc.IsoCode == countryCode || cc.SsbCode == countryCode);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        // This function returns true/false thus indicating whether or not the input string, interpreted as a country code,
        // is representing Norway. Empty country code also indicates Norway.
        // The input string can be either ISO 3166-1 Alpha 2 or SSB 3-digits format
        public static bool IsCountryNorway(string countryCode)
        {
            bool isNorway = false;
            countryCode = countryCode?.ToUpper();
            if (countryCode == "0" || countryCode == "NO" || string.IsNullOrEmpty(countryCode))
            {
                isNorway = true;
            }

            return isNorway;
        }

        // This function returns the country name associated with the input string, interpreted as a country code, if it is in the 
        //  internal list of country codes. The country name is in Norwegian spelling (bokmål, NOBM) and uppercase.
        //  The input string can be either ISO 3166-1 Alpha 2 or SSB 3-digits format.
        //  If no single reply is found or another exception is raised, empty string is returned
        public static string ReturnCountryName(string countryCode)
        {
            string name = String.Empty;

            try
            {
                if (_postList.Exists(cc => cc.IsoCode == countryCode))
                {
                    name = _postList.Single(a => a.IsoCode.Equals(countryCode)).Country;
                }
                else if (_postList.Exists(cc => cc.SsbCode == countryCode))
                {
                    name = _postList.Single(a => a.SsbCode.Equals(countryCode)).Country;
                }
                else
                {
                    name = String.Empty;
                }
            }

            catch (Exception excep)
            {
                name = String.Empty;
            }

            return name;
        }

        // This function returns the ISO 3166-1 Alpha 2 country code associated with the input string, interpreted as a SSB 3-digit country code,
        //  if it is in the internal list of country codes. 
        //  The input string can be either ISO 3166-1 Alpha 2 or SSB 3-digits format
        //  If no single reply is found or another exception is raised, empty string is returned
        public static string ReturnCountryIsoCode(string countryCode)
        {
            string isoCode = String.Empty;

            try
            {
                if (_postList.Exists(cc => cc.SsbCode == countryCode))
                {
                    isoCode = _postList.Single(a => a.SsbCode.Equals(countryCode)).IsoCode;
                }
                else if (_postList.Exists(cc => cc.IsoCode == countryCode))
                {
                    isoCode = countryCode; //_postList.Single(a => a.IsoCode.Equals(countryCode)).IsoCode;
                }
                else
                {
                    isoCode = String.Empty;
                }
            }
            catch (Exception excep)
            {
                isoCode = String.Empty;
            }

            return isoCode;
        }

        //  This function will return an adjusted postal code if the input string is all numeric.
        //  An adjusted postal code consists of ISO 3166-1 Alpha 2 country code. hyphen prefixing
        //  the input postal code.
        //  If the input postal code is not all-numeric it is returned unchanged
        //  If an exception is thrown, a empty string will be returned 
        public static string ReturnAdjustedPostalcode(string postalCode, string isoCode)
        {
            string returnPostalcode = String.Empty;
            int i;

            try
            {
                if (int.TryParse(postalCode, out i) && !string.IsNullOrEmpty(isoCode))
                {
                    if (isoCode.Equals("NO", StringComparison.InvariantCultureIgnoreCase))
                    {
                        returnPostalcode = postalCode;
                    }
                    else
                    {
                        returnPostalcode = isoCode + "-" + postalCode;
                    }
                }
                else
                {
                    returnPostalcode = postalCode;
                }
            }
            catch (Exception excep)
            {
                returnPostalcode = String.Empty;
            }

            return returnPostalcode;
        }
    }


    // Public class to hold a row of information of the relation between:
    //   - ISO 3166-1 Alpha 2 country codes,
    //   - SSB-specific 3-digit country code,
    //   - name of country in Norwegian Bokmål (NOBM), uppercase (for addressing purpose)  
    public class PostalCountryCode
    {
        public string IsoCode { get; set; }
        public string SsbCode { get; set; }
        public string Country { get; set; }

        public PostalCountryCode()
        {

        }

        public PostalCountryCode(string isoCode, string ssbCode, string country)
        {
            IsoCode = isoCode;
            SsbCode = ssbCode;
            Country = country;
        }
    }
}
