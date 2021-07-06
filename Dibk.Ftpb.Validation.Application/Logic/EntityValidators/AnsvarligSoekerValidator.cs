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
        //        IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService)
        //    : base(formValidatorConfiguration, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, codeListService)
        //{
        //    _codeListService = codeListService;
        //    _enkelAdresseValidator = enkelAdresseValidator;
        //    _kontaktpersonValidator = kontaktpersonValidator;
        //    _partstypeValidator = partstypeValidator;

        //}

        public AnsvarligSoekerValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid, IEnkelAdresseValidator enkelAdresseValidator,
                IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService)
            : base(entityValidatorTree, nodeid, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, codeListService)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _partstypeValidator = partstypeValidator;

        }

    }
}