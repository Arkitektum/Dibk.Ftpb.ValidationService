﻿using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class BygningstypeValidator : KodelisteValidator
    {
        public override string ruleXmlElement { get { return "bygningstype"; } set { ruleXmlElement = value; } }

        public BygningstypeValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, ICodeListService codeListService)
            : base(formValidatorConfiguration, parentValidator, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
