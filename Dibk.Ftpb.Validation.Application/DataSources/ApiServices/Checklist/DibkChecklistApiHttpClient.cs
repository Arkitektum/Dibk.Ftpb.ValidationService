using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class DibkChecklistApiHttpClient : ChecklistApiHttpClient
    {
        public DibkChecklistApiHttpClient(HttpClient httpClient, IOptions<AtilChecklistSettings> options) : base(httpClient, options)
        {

        }
    }
}
