using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnleggstypeValidator : KodelisteValidator
    {
        public override string ruleXmlElement { get { return "anleggstype"; } set { ruleXmlElement = value; } }

        public AnleggstypeValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum parentValidator, ICodeListService codeListService)
            : base(entityValidatorOrchestrator, parentValidator, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
