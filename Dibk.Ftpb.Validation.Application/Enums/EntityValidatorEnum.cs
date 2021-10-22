﻿namespace Dibk.Ftpb.Validation.Application.Enums
{
    public enum EntityValidatorEnum
    {
        //TODO https://stackoverflow.com/a/60454909

        [EntityValidatorEnumerationAttribute(XmlNode = "anleggstype", ValidatorId = "1")]
        AnleggstypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "ansvarligSoeker", ValidatorId = "2")]
        AnsvarligSoekerValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "arbeidsplasser", ValidatorId = "3")]
        ArbeidsplasserValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "beskrivelseAvTiltak", ValidatorId = "4")]
        BeskrivelseAvTiltakValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "bygningstype", ValidatorId = "5")]
        BygningstypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "dispensasjon", ValidatorId = "6")]
        DispensasjonValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "eiendomByggested/eiendom{0}", ValidatorId = "7")]
        EiendomByggestedValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "adresse", ValidatorId = "8")]
        EiendomsAdresseValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "adresse", ValidatorId = "9")]
        EnkelAdresseValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "adresse", ValidatorId = "10")]
        EnkelAdresseValidatorV2,

        [EntityValidatorEnumerationAttribute(XmlNode = "fakturamottaker", ValidatorId = "11")]
        FakturamottakerValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "bruk", ValidatorId = "12")]
        FormaaltypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "kontaktperson", ValidatorId = "13")]
        KontaktpersonValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "eiendomsidentifikasjon", ValidatorId = "14")]
        MatrikkelValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "naeringsgruppe", ValidatorId = "15")]
        NaeringsgruppeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "partstype", ValidatorId = "16")]
        PartstypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "krav{0}", ValidatorId = "17")]
        SjekklistekravValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "sjekklistepunkt", ValidatorId = "18")]
        SjekklistepunktValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "tiltaksformaal{0}", ValidatorId = "19")]
        TiltaksformaalValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "tiltakshaver", ValidatorId = "20")]
        TiltakshaverValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "type{0}", ValidatorId = "21")]
        TiltakstypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "arbeidsplasser", ValidatorId = "22")]
        ArbeidsplasserValidatorV2,

        [EntityValidatorEnumerationAttribute(XmlNode = "metadata", ValidatorId = "23")]
        MetadataValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "arbeidstilsynetsSaksnummer", ValidatorId = "24")]
        ArbeidstilsynetsSaksnummerValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "kommunensSaksnummer", ValidatorId = "25")]
        KommunensSaksnummerValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "betaling", ValidatorId = "26")]
        BetalingValidator,

        //FTB
        [EntityValidatorEnumerationAttribute(XmlNode = "ansvarsrett", ValidatorId = "27")]
        AnsvarsrettValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "foretak", ValidatorId = "28")]
        ForetakValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "ansvarsomraader/ansvarsomraade{0}", ValidatorId = "29")]
        AnsvarsomraadeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "funksjon", ValidatorId = "30")]
        FunksjonValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "tiltaksklasse", ValidatorId = "31")]
        TiltaksklasseValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "kontaktperson", ValidatorId = "32")]
        KontaktpersonValidatorV2,

        [EntityValidatorEnumerationAttribute(XmlNode = "eiendomsidentifikasjon", ValidatorId = "33")]
        EiendomsidentifikasjonValidatorV2,

        [EntityValidatorEnumerationAttribute(XmlNode = "adresse", ValidatorId = "34")]
        EiendomsAdresseValidatorV2,

        [EntityValidatorEnumerationAttribute(XmlNode = "ansvarligSoeker", ValidatorId = "35")]
        AnsvarligSoekerValidatorV2,
    }
}
