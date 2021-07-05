using System;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common
{
    public class EntityValidatorInfo
    {
        private string _rulePath;
        private EntityValidatorEnum _entityValidator;
        private EntityValidatorEnum? _parentValidator;
        private EntityValidatorEnum? _grandParentValidator;


        private string _xPathBetweenRootAndEndElement;
        public EntityValidatorEnum EntityValidator { get => _entityValidator; set => _entityValidator = value; }
        public EntityValidatorEnum? ParentValidator { get => _parentValidator; set => _parentValidator = value; }
        public EntityValidatorEnum? GrandparentValidator { get => _grandParentValidator; set => _grandParentValidator = value; }
        public string XPathBetweenRootAndEndElement { get => _xPathBetweenRootAndEndElement; set => _xPathBetweenRootAndEndElement = value; }
        public string RulePath { get => _rulePath; set => _rulePath = value; }

        public EntityValidatorInfo(EntityValidatorEnum entityValidator)
            : this(entityValidator, null) { }

        public EntityValidatorInfo(EntityValidatorEnum entityValidator, EntityValidatorEnum? parentValidator)
            : this(entityValidator, parentValidator, null) { }

        public EntityValidatorInfo(EntityValidatorEnum entityValidator, EntityValidatorEnum? parentValidator, EntityValidatorEnum? grandparentValidator)
        {
            _entityValidator = entityValidator;
            _parentValidator = parentValidator;
            _grandParentValidator = grandparentValidator;
            _rulePath = $"{((int)entityValidator)}";


            if (parentValidator == null)
            {
                _xPathBetweenRootAndEndElement = "";
                _rulePath = $"{((int)entityValidator)}";
            }
            else
            {
                string grandpar = "";
                string grandparInt = null;
                if (grandparentValidator != null)
                {
                    grandpar = $"{GetXPathElement(grandparentValidator)}/";
                    grandparInt = $"{((int)grandparentValidator)}";
                    //_rulePath = $"{((int)grandparentValidator)}";
                }

                var parent = GetXPathElement(parentValidator);


                _rulePath = $"{((int)parentValidator)}.{_rulePath}";
                _xPathBetweenRootAndEndElement = $"{grandpar}{parent}";
                _rulePath = $"{_rulePath}.{((int)parentValidator)}.{((int)entityValidator)}";
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
