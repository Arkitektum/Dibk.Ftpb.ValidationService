using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnleggstypeValidator : KodelisteValidator
    {
        public override string ruleXmlElement { get { return "anleggstype"; } set { ruleXmlElement = value; } }

        //public AnleggstypeValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, ICodeListService codeListService)
        //    : base(formValidatorConfiguration, parentValidator, codeListService)
        //{
        //    _codeListService = codeListService;
        //}
        public AnleggstypeValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, EntityValidatorEnum grandParentValidator, ICodeListService codeListService)
            : base(formValidatorConfiguration, parentValidator, grandParentValidator, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
