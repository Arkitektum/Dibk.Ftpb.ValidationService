using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EntityValidatorInfo
    {
        private string _entityValidator;
        private EntityValidatorEnum? _parentValidator;
        private string _xPathAfterParent;

        public EntityValidatorInfo(string entityValidator)
            : this(entityValidator, null) { }

        public EntityValidatorInfo(string entityValidator, EntityValidatorEnum? parentValidator)
        {
            _entityValidator = entityValidator;
            _parentValidator = parentValidator;

            if (parentValidator == null)
            {
                _xPathAfterParent = "";
            }
            else
            {
                switch (parentValidator)
                {
                    case EntityValidatorEnum.EiendomByggestedValidator:
                        _xPathAfterParent = "eiendomByggested{0}";
                        break;
                    case EntityValidatorEnum.TiltakshaverValidator:
                        _xPathAfterParent = "tiltakshaver";
                        break;
                    case EntityValidatorEnum.FakturamottakerValidator:
                        _xPathAfterParent = "fakturamottaker";
                        break;




                }
            }
        }


        public string EntityValidator { get => _entityValidator; set => _entityValidator = value;  }
        public EntityValidatorEnum? ParentValidator { get => _parentValidator; set => _parentValidator = value; }
        public string XPathAfterParent { get => _xPathAfterParent; set => _xPathAfterParent = value; }
    }
}
