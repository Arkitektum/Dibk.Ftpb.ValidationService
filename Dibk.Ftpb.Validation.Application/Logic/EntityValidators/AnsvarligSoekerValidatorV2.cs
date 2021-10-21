using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnsvarligSoekerValidatorV2 : AktoerValidatorV2
    {
        public AnsvarligSoekerValidatorV2(IList<EntityValidatorNode> entityValidatorTree, IEnkelAdresseValidator enkelAdresseValidator,
                IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService, string[] partstypes = null)
            : base(entityValidatorTree, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, codeListService, partstypes)
        {
        }
    }
}