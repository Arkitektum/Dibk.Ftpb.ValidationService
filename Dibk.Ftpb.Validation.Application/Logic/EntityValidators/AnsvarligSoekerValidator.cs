using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnsvarligSoekerValidator : AktoerValidator
    {
        public override string ruleXmlElement { get { return "ansvarligSoeker"; } set { ruleXmlElement = value; } }

        public AnsvarligSoekerValidator(FormValidatorConfiguration formValidatorConfiguration, IEnkelAdresseValidator enkelAdresseValidator,
                IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator kodelisteValidator, ICodeListService codeListService)
            : base(formValidatorConfiguration, enkelAdresseValidator, kontaktpersonValidator, kodelisteValidator, codeListService)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _kodelisteValidator = kodelisteValidator;

        }

    }
}