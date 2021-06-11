﻿using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltaksformaalValidator : KodelisteValidator
    {
        public override string ruleXmlElement { get { return "tiltaksformaal"; } set { ruleXmlElement = value; } }

        public TiltaksformaalValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum parentValidator, ICodeListService codeListService)
            : base(entityValidatorOrchestrator, parentValidator, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
