﻿using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface ISjekklistekravValidator
    {
        string ruleXmlElement { get; }
        ValidationResult ValidationResult { get; }
        ValidationResult Validate(IEnumerable<SjekklistekravValidationEntity> sjekklistekrav);
    }
}