using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltakshaverEnkelAdresseValidator : EnkelAdresseValidator
    {
        public TiltakshaverEnkelAdresseValidator(IList<EntityValidatorNode> entityValidatorTree, IPostalCodeService postalCodeService) : base(entityValidatorTree, postalCodeService)
        {
        }
    }
}
