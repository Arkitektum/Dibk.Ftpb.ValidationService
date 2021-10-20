using System;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class SignaturValidationEntity
    {
        public DateTime? Signaturdato { get; set; }
        public string SignertAv { get; set; }
        public string SignertPaaVegneAv { get; set; }
    }
}
