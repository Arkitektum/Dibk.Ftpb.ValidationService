﻿using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnleggstypeValidator : KodelisteValidator
    {
        public AnleggstypeValidator(IList<EntityValidatorNode> entityValidatorTree, ICodeListService codeListService)
            : base(entityValidatorTree,  FtbKodeListeEnum.Anleggstype, RegistryType.Byggesoknad, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
