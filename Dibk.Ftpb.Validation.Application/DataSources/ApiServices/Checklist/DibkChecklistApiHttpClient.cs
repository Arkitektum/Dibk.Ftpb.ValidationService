using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class DibkChecklistApiHttpClient : ChecklistApiHttpClient
    {
        public DibkChecklistApiHttpClient(HttpClient httpClient, IOptions<AtilChecklistSettings> options) : base(httpClient, options)
        {

        }
    }
}
