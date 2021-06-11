using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class NaeringsgruppeValidator : KodelisteValidator
    {
        public override string ruleXmlElement { get { return "naeringsgruppe"; } set { ruleXmlElement = value; } }

        public NaeringsgruppeValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum parentValidator, ICodeListService codeListService)
            : base(entityValidatorOrchestrator, parentValidator, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
