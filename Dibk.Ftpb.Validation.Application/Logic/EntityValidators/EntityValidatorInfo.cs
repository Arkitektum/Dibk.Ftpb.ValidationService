using Dibk.Ftpb.Validation.Application.Enums;
using System;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EntityValidatorInfo
    {
        private EntityValidatorEnum _entityValidator;
        private EntityValidatorEnum? _parentValidator;
        private EntityValidatorEnum? _grandParentValidator;
        private string _parentValidatorXPathElement;
        public EntityValidatorEnum EntityValidator { get => _entityValidator; set => _entityValidator = value; }
        public EntityValidatorEnum? ParentValidator { get => _parentValidator; set => _parentValidator = value; }
        public EntityValidatorEnum? GrandparentValidator { get => _grandParentValidator; set => _grandParentValidator = value; }
        public string ParentValidatorXPathElement { get => _parentValidatorXPathElement; set => _parentValidatorXPathElement = value; }

        public EntityValidatorInfo(EntityValidatorEnum entityValidator)
            : this(entityValidator, null) { }

        public EntityValidatorInfo(EntityValidatorEnum entityValidator, EntityValidatorEnum? parentValidator)
            : this(entityValidator, parentValidator, null) { }

        public EntityValidatorInfo(EntityValidatorEnum entityValidator, EntityValidatorEnum? parentValidator, EntityValidatorEnum? grandparentValidator)
        {
            _entityValidator = entityValidator;
            _parentValidator = parentValidator;
            _grandParentValidator = grandparentValidator;

            if (parentValidator == null)
            {
                _parentValidatorXPathElement = "";
            }
            else
            {
                string grandpar = "";
                if (grandparentValidator != null)
                {
                    grandpar = $"{GetXPathElement(grandparentValidator)}/";
                }
                
                var parent = GetXPathElement(parentValidator);
                _parentValidatorXPathElement = $"{grandpar}{parent}";
            }
        }

        private string GetXPathElement(EntityValidatorEnum? validator)
        {
            switch (validator)
            {
                case EntityValidatorEnum.EiendomByggestedValidator:
                    return "eiendomByggested{0}";
                case EntityValidatorEnum.TiltakshaverValidator:
                    return "tiltakshaver";
                case EntityValidatorEnum.FakturamottakerValidator:
                    return "fakturamottaker";
                case EntityValidatorEnum.AnsvarligSoekerValidator:
                    return "ansvarligSoeker";
                case EntityValidatorEnum.DispensasjonValidator:
                    return "dispensasjon";
                case EntityValidatorEnum.SjekklistekravValidator:
                    return "krav{0}";
                //case EntityValidatorEnum.SjekklistepunktValidator:
                //    return "sjekklistepunkt";
                case EntityValidatorEnum.BeskrivelseAvTiltakValidator:
                    return "beskrivelseAvTiltak";
                case EntityValidatorEnum.FormaaltypeValidator:
                    return "bruk";
                //case EntityValidatorEnum.AnleggstypeValidator:
                //    return "anleggstype";
                //case EntityValidatorEnum.NaeringsgruppeValidator:
                //    return "naeringsgruppe";
                //case EntityValidatorEnum.BygningstypeValidator:
                //    return "bygningstype";
                //case EntityValidatorEnum.TiltaksformaalValidator:
                //    return "tiltaksformaal{0}";
                //case EntityValidatorEnum.TiltakstypeValidator:
                //    return "type{0}";
            }
            throw new ArgumentOutOfRangeException("Missing valid validator configuration.");
        }
    }
}
