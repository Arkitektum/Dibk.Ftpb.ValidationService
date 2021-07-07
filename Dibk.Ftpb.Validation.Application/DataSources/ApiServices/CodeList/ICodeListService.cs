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
        Task<Dictionary<string, CodelistFormat>> GetCodeList(ArbeidstilsynetCodeListNames cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet);
        Task<Dictionary<string, CodelistFormat>> GetCodeList(FtbKodeListeEnums cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet);
        bool IsCodelistValid(ArbeidstilsynetCodeListNames codeListName, string codeValue);
        bool IsCodelistValid(FtbKodeListeEnums codeListName, string codeValue);
    }
}
