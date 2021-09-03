using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnsvarligSoekerEnkelAdresseValidator : EnkelAdresseValidator
    {
        public AnsvarligSoekerEnkelAdresseValidator(IList<EntityValidatorNode> entityValidatorTree, IPostalCodeService postalCodeService) : base(entityValidatorTree, postalCodeService)
        {
        }
    }
}
