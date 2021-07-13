using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList
{
    public interface ICodeListService
    {
        Task<Dictionary<string, CodelistFormat>> GetCodeList(object cocelistName, RegistryType registryType);
        bool? IsCodelistValid(object codeListName, string codeValue, RegistryType registryType);
        bool? IsCodelistLabelValid(object codeListName, string codeValue, string codeName, RegistryType registryType);
    }
}
