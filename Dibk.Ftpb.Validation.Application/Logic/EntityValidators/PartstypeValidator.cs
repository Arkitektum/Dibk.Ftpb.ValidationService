using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class PartstypeValidator : KodelisteValidator
    {
        public override string ruleXmlElement { get { return "partstype"; } set { ruleXmlElement = value; } }

        public PartstypeValidator(IList<EntityValidatorNode> entityValidationGroup, int nodeId, ICodeListService codeListService)
            : base(entityValidationGroup, nodeId, codeListService)
        {
            _codeListService = codeListService;
        }
        public PartstypeValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, ICodeListService codeListService)
            : base(formValidatorConfiguration, parentValidator, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
