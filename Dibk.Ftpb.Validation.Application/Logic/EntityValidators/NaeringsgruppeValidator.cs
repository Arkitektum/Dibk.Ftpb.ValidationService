﻿using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class NaeringsgruppeValidator : KodelisteValidator
    {
        //public override string ruleXmlElement { get { return "naeringsgruppe"; } set { ruleXmlElement = value; } }

        //public NaeringsgruppeValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, ICodeListService codeListService)
        //    : base(formValidatorConfiguration, parentValidator, codeListService)
        //{
        //    _codeListService = codeListService;
        //}


        public NaeringsgruppeValidator(IList<EntityValidatorNode> entityValidationGroup, int nodeId, ICodeListService codeListService)
            : base(entityValidationGroup, nodeId, codeListService)
        {
            _codeListService = codeListService;
        }



    }
}
