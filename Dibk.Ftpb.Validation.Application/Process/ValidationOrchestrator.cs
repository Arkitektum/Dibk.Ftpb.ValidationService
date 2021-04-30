﻿using Dibk.Ftpb.Validation.Application.Logic;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Process
{
    /// <summary>
    ///…InstantiateValidationMessageContainer(……)
    ///…VerifyInputData(….)
    ///….ValidateFormEntity(….)
    ///…CompileValidationReport(….)    /// </summary>
    public class ValidationOrchestrator
    {
        private readonly IServiceProvider _services;
        public List<ValidationMessage> Messages { get; set; }

        public ValidationOrchestrator(IServiceProvider services)
        {
            _services = services;
        }

        public async Task ExecuteAsync(string xmlData, string dataFormatVersion)
        {
            IFormValidator formValidator = GetValidator(dataFormatVersion); //45957
            formValidator.Execute(xmlData);

            //return Task.CompletedTask;
        }


        public IFormValidator GetValidator(string dataFormatVersion)
        {
            //Retrieves classes implementing IForm, having FormDataFormatAttribute and filtering by its DataFormatId
            var type = typeof(IFormValidator);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                //.Where(p => type.IsAssignableFrom(p))
                .Where(t => t.IsDefined(typeof(FormDataAttribute), true))
                .Where(t => t.GetCustomAttribute<FormDataAttribute>().DataFormatVersion == dataFormatVersion);

            object formLogicInstance = null;
            if (types.Count() > 0)
            {
                //Resolves an instance of the class
                var formType = types.FirstOrDefault();
                formLogicInstance = _services.GetService(formType);
            }

            return formLogicInstance as IFormValidator;
        }
    }
}
