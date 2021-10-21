using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class FormPropertyService
    {
        private readonly IConfiguration _configuration;
        private List<FormProperties> _forms;

        public FormPropertyService(IConfiguration configuration)
        {
            _configuration = configuration;

            var formPropertiesFromConfig = _configuration.GetSection("FormProperties").GetChildren().ToList()
                .Select(x =>
                                 (
                                      x.GetValue<string>("DataFormatVersion"),
                                      x.GetValue<string>("ServiceAuthority"),
                                      x.GetValue<string>("ProcessCategory")
                                  )
                            ).ToList<(string DataFormatVersion, string ServiceAuthority, string ProcessCategory)>();

            _forms = formPropertiesFromConfig.Select(x => new FormProperties()
            {
                DataFormatVersion = x.DataFormatVersion,
                ProcessCategory = x.ProcessCategory,
                ServiceAuthority = x.ServiceAuthority
            }).ToList();

        }

        public FormProperties GetFormProperties(string dataFormatId, string dataFormatVersion)
        {
            try
            {
                foreach (var form in _forms)
                {
                    if (form.DataFormatId == dataFormatId && form.DataFormatVersion == dataFormatVersion)
                    {
                        return form;
                    }
                }

                throw new NullReferenceException($"Illegal dataFormatVersion '{dataFormatId}' : '{dataFormatVersion}'");
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException($"Illegal dataFormatVersion '{dataFormatId}':'{dataFormatVersion}'");
            }
        }







    }
}
