using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class AtilChecklistApiHttpClient : ChecklistApiHttpClient
    {
        public AtilChecklistApiHttpClient(HttpClient httpClient, IOptions<AtilChecklistSettings> options) : base(httpClient, options)
        {
        }
    }
}
