using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnsvarligSoekerValidator : AktoerValidator
    {
        //public override string ruleXmlElement { get { return "ansvarligSoeker"; } set { ruleXmlElement = value; } }

        //public AnsvarligSoekerValidator(FormValidatorConfiguration formValidatorConfiguration, IEnkelAdresseValidator enkelAdresseValidator,
        //        IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator kodelisteValidator, ICodeListService codeListService)
        //    : base(formValidatorConfiguration, enkelAdresseValidator, kontaktpersonValidator, kodelisteValidator, codeListService)
        //{
        //    _codeListService = codeListService;
        //    _enkelAdresseValidator = enkelAdresseValidator;
        //    _kontaktpersonValidator = kontaktpersonValidator;
        //    _kodelisteValidator = kodelisteValidator;

        //}

        public AnsvarligSoekerValidator(IList<EntityValidatorNode> entityValidationGroup, int nodeid, IEnkelAdresseValidator enkelAdresseValidator,
                IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator kodelisteValidator, ICodeListService codeListService)
            : base(entityValidationGroup, nodeid, enkelAdresseValidator, kontaktpersonValidator, kodelisteValidator, codeListService)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _kodelisteValidator = kodelisteValidator;

        }

    }
}