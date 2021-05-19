using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList
{
    public interface ICodeListService
    {
        Task<Dictionary<string, CodelistFormat>> GetCodeList(ArbeidstilsynetCodeListNames cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet);
        Task<Dictionary<string, CodelistFormat>> GetCodeList(FtbCodeListNames cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet);
        bool IsCodelistValid(ArbeidstilsynetCodeListNames codeListName, string codeValue);
        bool IsCodelistValid(FtbCodeListNames codeListName, string codeValue);
    }
}
