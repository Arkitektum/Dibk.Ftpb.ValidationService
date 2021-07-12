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
        Task<Dictionary<string, CodelistFormat>> GetCodeList(object cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet);
        bool IsCodelistValid(object codeListName, string codeValue);
        bool IsCodelistLabelValid(object codeListName, string codeValue, string codeName, RegistryType registryType = RegistryType.Byggesoknad);


        //Task<Dictionary<string, CodelistFormat>> GetCodeList(ArbeidstilsynetCodeListNames cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet);
        //Task<Dictionary<string, CodelistFormat>> GetCodeList(FtbKodeListeEnum cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet);
        //bool IsCodelistValid(ArbeidstilsynetCodeListNames codeListName, string codeValue);
        //bool IsCodelistValid(FtbKodeListeEnum codeListName, string codeValue);
        //bool IsCodelistLabelValid(FtbKodeListeEnum codeListName, string codeValue, string codeName, RegistryType registryType = RegistryType.Byggesoknad);
    }

    public class CodelistFtp : ICodeListService
    {
        public Task<Dictionary<string, CodelistFormat>> GetCodeList(object cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet)
        {
            throw new NotImplementedException();
        }

        public bool IsCodelistValid(object codeListName, string codeValue)
        {
            throw new NotImplementedException();
        }

        public bool IsCodelistLabelValid(object codeListName, string codeValue, string codeName,
            RegistryType registryType = RegistryType.Byggesoknad)
        {
            throw new NotImplementedException();
        }
    }
}
