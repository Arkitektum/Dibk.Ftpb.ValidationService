﻿using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    /// <summary>
    /// Må huskes på:
    /// - Ved opprettelse av ny validering for et skjema eller valideringsentitet; husk å initialiser reglen i metode InitializeValidationRules()
    ///   
    /// </summary>
    //TODO: automatisk sjekk på at ingen regler blir validert uten at de først er initialisert.
    public class EntityValidatorOrchestrator
    {
        public string ValidatorFormName { get; set; }
        public string ValidatorFormXPath { get; set; }
        public List<EntityValidatorInfo> Validators = new List<EntityValidatorInfo>();
    }
}