
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltakshaverValidator : AktoerValidator
    {
        //public override string ruleXmlElement { get { return "tiltakshaver"; } set { ruleXmlElement = value; } }

        //public TiltakshaverValidator(FormValidatorConfiguration formValidatorConfiguration, IEnkelAdresseValidator enkelAdresseValidator,
        //        IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService)
        //    : base(formValidatorConfiguration, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, codeListService)
        //{
        //    _codeListService = codeListService;
        //    _enkelAdresseValidator = enkelAdresseValidator;
        //    _kontaktpersonValidator = kontaktpersonValidator;
        //    _partstypeValidator = partstypeValidator;

        //}
        public TiltakshaverValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, IEnkelAdresseValidator enkelAdresseValidator, IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService)
            : base(entityValidatorTree, nodeId, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, codeListService)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _partstypeValidator = partstypeValidator;
        }

    }
}
