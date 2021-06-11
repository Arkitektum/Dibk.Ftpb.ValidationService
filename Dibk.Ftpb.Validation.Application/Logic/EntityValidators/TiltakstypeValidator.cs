using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltakstypeValidator : KodelisteValidator
    {
        public override string ruleXmlElement { get { return "type"; } set { ruleXmlElement = value; } }

        public TiltakstypeValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum parentValidator, ICodeListService codeListService)
            : base(entityValidatorOrchestrator, parentValidator, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
